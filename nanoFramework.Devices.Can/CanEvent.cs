//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

namespace nanoFramework.Devices.Can
{
    /// <summary>
    /// Describes the possible types of events for the CAN controller.
    /// </summary>
    public enum CanEvent
    {
        /// <summary>
        /// A CAN message was received.
        /// </summary>
        MessageReceived = 0,

        /// <summary>
        /// An error has occurred.
        /// </summary>
        ErrorOccurred
    }
}
