//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Runtime.Events;

namespace nanoFramework.Devices.Can
{
    internal class CanMessageEvent : BaseEvent
    {
        public int ControllerIndex;
        public CanEvent Event;
    }
}
