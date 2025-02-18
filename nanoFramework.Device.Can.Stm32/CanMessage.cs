//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using System;
using System.Runtime.CompilerServices;

namespace nanoFramework.Device.Can
{
    /// <summary>
    /// CAN message.
    /// </summary>
    public class CanMessage
    {
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private uint _id;

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private CanMessageIdType _identifierType;

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private CanMessageFrameType _frameType;

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private byte[] _message;

        /// <summary>
        /// Message ID (SID or EID format, depending on <see cref="IdentifierType"/>).
        /// </summary>
        public uint Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Message identifier type.
        /// </summary>
        public CanMessageIdType IdentifierType
        {
            get { return _identifierType; }
            set { _identifierType = value; }
        }

        /// <summary>
        /// Message frame type.
        /// </summary>
        public CanMessageFrameType FrameType
        {
            get { return _frameType; }
            set { _frameType = value; }
        }

        /// <summary>
        /// Message data.
        /// </summary>
        /// <remarks>
        /// Maximum lenght of data buffer is 8.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">If the message buffer exceeds the maximum allowed lenght.</exception>
        public byte[] Message
        {
            get { return _message; }
            set { _message = value; }
        }

        /// <summary>
        /// Creates a CAN message.
        /// </summary>
        public CanMessage(uint id, CanMessageIdType identifierType, CanMessageFrameType frameType, byte[] message)
        {
            _id = id;
            _identifierType = identifierType;
            _frameType = frameType;
            _message = message;
        }
    }
}
