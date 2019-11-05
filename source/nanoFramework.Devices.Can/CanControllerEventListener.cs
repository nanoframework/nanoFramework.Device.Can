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
        private ArrayList _canControllersMap = new ArrayList();

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
                device = FindCanController(canMessageEvent.ControllerIndex);

                if (device != null)
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
                _canControllersMap.Add(controller);
            }
        }

        public void RemoveCanController(int index)
        {
            lock (_canControllersMap)
            {
                var controller = FindCanController(index);

                if (controller != null)
                {
                    _canControllersMap.Remove(controller);
                }
            }
        }
        private CanController FindCanController(int controllerId)
        {
            for (int i = 0; i < _canControllersMap.Count; i++)
            {
                if (((CanController)_canControllersMap[i])._controllerId == controllerId)
                {
                    return (CanController)_canControllersMap[i];
                }
            }

            return null;
        }
    }
}
