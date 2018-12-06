//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Runtime.Events;
using System;
using System.Collections;

namespace nanoFramework.Devices.Can
{
    internal class CanControllerEventListener : IEventProcessor, IEventListener
    {
        // Map of serial device numbers to CanController objects.
        private Hashtable _canControllersMap = new Hashtable();

        public CanControllerEventListener()
        {
            EventSink.AddEventProcessor(EventCategory.Can, this);
            EventSink.AddEventListener(EventCategory.Can, this);
        }

        public BaseEvent ProcessEvent(uint data1, uint data2, DateTime time)
        {
            return new CanMessageEvent
            {
                // Data1 is packed by PostManagedEvent, so we need to unpack the high word.
                ControllerIndex = (int)(data1 >> 16),
                Event = (CanEvent)data2
            };
        }

        public void InitializeForEventSource()
        {
        }

        public bool OnEvent(BaseEvent ev)
        {
            var canMessageEvent = (CanMessageEvent)ev;
            CanController device = null;

            lock (_canControllersMap)
            {
                if (_canControllersMap.Contains(canMessageEvent.ControllerIndex))
                {
                    device = (CanController)_canControllersMap[canMessageEvent.ControllerIndex];
                }
            }

            // Avoid calling this under a lock to prevent a potential lock inversion.
            if (device != null)
            {
                device.OnCanMessageReceivedInternal(canMessageEvent.Event);
            }

            return true;
        }

        public void AddCanController(CanController controller)
        {
            lock (_canControllersMap)
            {
                _canControllersMap[controller._controllerId] = controller;
            }
        }

        public void RemoveCanController(int index)
        {
            lock (_canControllersMap)
            {
                if (_canControllersMap.Contains(index))
                {
                    _canControllersMap.Remove(index);
                }
            }
        }
    }
}
