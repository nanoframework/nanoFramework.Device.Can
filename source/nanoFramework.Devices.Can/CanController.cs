//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using System;
using System.Runtime.CompilerServices;

namespace nanoFramework.Devices.Can
{
    // This should be a TypedEventHandler "EventHandler<CanMessageReceivedEventHandler>"
#pragma warning disable 1591
    public delegate void CanMessageReceivedEventHandler(
            Object sender,
            CanMessageReceivedEventArgs e);

#pragma warning restore 1591

    /// <summary>
    /// Represents a CAN controller on the system.
    /// </summary>
    public sealed class CanController : IDisposable
    {
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private static CanControllerEventListener s_eventListener = new CanControllerEventListener();

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private bool _disposed;

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private CanMessageReceivedEventHandler _callbacks = null;

        // this is used as the lock object 
        // a lock is required because multiple threads can access the CanController
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private readonly object _syncLock = new object();

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        internal readonly int _controllerId;

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private readonly CanSettings _settings;

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private readonly CanMessage _message;

        /// <summary>
        /// Controller ID for this <see cref="CanController"/>.
        /// </summary>
        public readonly string ControllerId;

        internal CanController(string controller, Can​Settings settings)
        {
            // the CAN id is an ASCII string with the format 'CANn'
            // need to grab 'n' from the string and convert that to the integer value from the ASCII code (do this by subtracting 48 from the char value)
            _controllerId = controller[3] - '0';

            ControllerId = controller;

            // check if this controller is already opened
            var myController = FindController(_controllerId);

            if (myController == null)
            {
                _settings = new CanSettings(settings);

                // call native init to allow HAL/PAL inits related with Can hardware
                NativeInit();

                // add controller to collection, with the ID as key 
                // ** just the index number ***
                CanControllerManager.ControllersCollection.Add(this);

                // add the controller to the event listener in order to receive the callbacks from the native interrupts
                s_eventListener.AddCanController(this);
            }
            else
            {
                // this controller already exists: throw an exception
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// Opens a CAN bus with the settings provided.
        /// </summary>
        /// <param name="controllerId">The id of the bus.</param>
        /// <param name="settings">The bus settings.</param>
        /// <returns>The CAN controller requested.</returns>
        public static CanController FromId(string controllerId, Can​Settings settings)
        {
            //TODO: some sanity checks on controllerId
            return new CanController(controllerId, settings);
        }

        /// <summary>
        /// Write message to CAN Bus.
        /// </summary>
        /// <param name="message">CAN mesage to write in CAN Bus.</param>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void WriteMessage(CanMessage message);

        internal static CanController FindController(int index)
        {
            for (int i = 0; i < CanControllerManager.ControllersCollection.Count; i++)
            {
                if (((CanController)CanControllerManager.ControllersCollection[i])._controllerId == index)
                {
                    return (CanController)CanControllerManager.ControllersCollection[i];
                }
            }

            return null;
        }

        #region IDisposable Support

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // remove controller from controller collection
                    CanControllerManager.ControllersCollection.Remove(this);

                    // remove the controller from the event listener
                    s_eventListener.RemoveCanController(_controllerId);
                }

                DisposeNative();

                _disposed = true;
            }
        }

#pragma warning disable 1591
        ~CanController()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            lock (_syncLock)
            {
                if (!_disposed)
                {
                    Dispose(true);

                    GC.SuppressFinalize(this);
                }
            }
        }

#pragma warning restore 1591

        #endregion

        #region Events

        /// <summary>
        /// Indicates that a message has been received through a <see cref="CanController"/> object.
        /// </summary>
        public event CanMessageReceivedEventHandler MessageReceived
        {
            add
            {
                lock (_syncLock)
                {
                    if (_disposed)
                    {
                        throw new ObjectDisposedException();
                    }

                    CanMessageReceivedEventHandler callbacksOld = _callbacks;
                    CanMessageReceivedEventHandler callbacksNew = (CanMessageReceivedEventHandler)Delegate.Combine(callbacksOld, value);

                    try
                    {
                        _callbacks = callbacksNew;
                    }
                    catch
                    {
                        _callbacks = callbacksOld;

                        throw;
                    }

                    NativeUpdateCallbacks();
                }
            }

            remove
            {
                lock (_syncLock)
                {
                    if (_disposed)
                    {
                        throw new ObjectDisposedException();
                    }

                    CanMessageReceivedEventHandler callbacksOld = _callbacks;
                    CanMessageReceivedEventHandler callbacksNew = (CanMessageReceivedEventHandler)Delegate.Remove(callbacksOld, value);

                    try
                    {
                        _callbacks = callbacksNew;
                    }
                    catch
                    {
                        _callbacks = callbacksOld;

                        throw;
                    }

                    NativeUpdateCallbacks();
                }
            }
        }


        /// <summary>
        /// Handles internal events and re-dispatches them to the publicly subscribed delegates.
        /// </summary>
        /// <param name="eventType">The <see cref="CanEvent"/>Event type.</param>

        internal void OnCanMessageReceivedInternal(CanEvent eventType)
        {
            CanMessageReceivedEventHandler callbacks = null;

            lock (_syncLock)
            {
                if (!_disposed)
                {
                    callbacks = _callbacks;
                }
            }

            callbacks?.Invoke(this, new CanMessageReceivedEventArgs(eventType));
        }

        #endregion

        #region Native Calls

        /// <summary>
        /// Get next <see cref="CanMessage"/> available in the _<see cref="CanController"/> internal buffer.
        /// If there are no more messages available null will be returned.
        /// </summary>
        /// <returns>
        /// A <see cref="CanMessage"/> or null if there are no more messages available.
        /// </returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern CanMessage GetMessage();

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void DisposeNative();

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern string GetDeviceSelector();

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void NativeInit();

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void NativeUpdateCallbacks();

        #endregion
    }
}
