//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

namespace nanoFramework.Devices.Can
{
    /// <summary>
    /// Represents the settings for CAN bus.
    /// </summary>
    public sealed class CanSettings
    {
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private byte _baudRatePrescaler;

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private byte _phaseSegment1;

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private byte _phaseSegment2;

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private byte _syncJumpWidth;

        /// <summary>
        /// Initializes a new instance of <see cref="CanSettings"/>.
        /// </summary>
        /// <param name="baudRatePrescaler">Bus baud rate prescaler.</param>
        /// <param name="phaseSegment1">Phase segment 1.</param>
        /// <param name="phaseSegment2">Phase segment 2.</param>
        /// <param name="syncJumpWidth">Synchronization jump width.</param>
        public Can​Settings(byte baudRatePrescaler, byte phaseSegment1, byte phaseSegment2, byte syncJumpWidth)
        {
            _baudRatePrescaler = baudRatePrescaler;
            _phaseSegment1 = phaseSegment1;
            _phaseSegment2 = phaseSegment2;
            _syncJumpWidth = syncJumpWidth;
        }

        /// <summary>
        /// Initializes a copy of a <see cref="CanSettings"/> object.
        /// </summary>
        /// <param name="value">Object to copy from.</param>
        internal CanSettings(Can​Settings value)
        {
            _baudRatePrescaler = value._baudRatePrescaler;
            _phaseSegment1 = value._phaseSegment1;
            _phaseSegment2 = value._phaseSegment2;
            _syncJumpWidth = value._syncJumpWidth;
        }

        /// <summary>
        /// Gets or sets the baud rate prescaler.
        /// </summary>
        public byte BaudRatePrescaler
        {
            get { return _baudRatePrescaler; }
            set { _baudRatePrescaler = value; }
        }

        /// <summary>
        /// Gets or sets the value for phase segment 1.
        /// </summary>
        public byte PhaseSegment1
        {
            get { return _phaseSegment1; }
            set { _phaseSegment1 = value; }
        }

        /// <summary>
        /// Gets or sets the value for phase segment 2.
        /// </summary>
        public byte PhaseSegment2
        {
            get { return _phaseSegment2; }
            set { _phaseSegment2 = value; }
        }

        /// <summary>
        /// Gets or sets the value for the synchronization jump width.
        /// </summary>
        public byte SyncJumpWidth
        {
            get { return _syncJumpWidth; }
            set { _syncJumpWidth = value; }
        }
    }
}
