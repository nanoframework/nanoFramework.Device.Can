//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

namespace nanoFramework.Device.Can
{
    /// <summary>
    /// Helper for common CAN timings for the STM32
    /// </summary>
    public static class CanTimingSTM32
    {
        /// <summary>
        /// 25 KBaud.
        /// </summary>
        public static CanSettings Timing500KBaud () {
            return new CanSettings(6, 8, 1, 0);
        }

    }

    /// <summary>
    /// Helper for common CAN timings for the ESP32. Values from https://github.com/espressif/esp-idf/blob/master/components/hal/include/hal/twai_types.h#L54
    /// </summary>
    public static class CanTimingESP32
    {
        /// <summary>
        /// 25 KBaud.
        /// </summary>
        public static CanSettings Timing25KBaud () {
            return new CanSettings(128, 16, 8, 3);
        }

        /// <summary>
        /// 50 KBaud.
        /// </summary>
        public static CanSettings Timing50KBaud () {
            return new CanSettings(80, 15, 4, 3);
        }

        /// <summary>
        /// 100 KBaud.
        /// </summary>
        public static CanSettings Timing100KBaud () {
            return new CanSettings(40, 15, 4, 3);
        }

        /// <summary>
        /// 125 KBaud.
        /// </summary>
        public static CanSettings Timing125KBaud () {
            return new CanSettings(32, 15, 4, 3);
        }

        /// <summary>
        /// 250 KBaud.
        /// </summary>
        public static CanSettings Timing250KBaud () {
            return new CanSettings(16, 15, 4, 3);
        }

        /// <summary>
        /// 500 KBaud.
        /// </summary>
        public static CanSettings Timing500KBaud () {
            return new CanSettings(8, 15, 4, 3);
        }

        /// <summary>
        /// 800 KBaud.
        /// </summary>
        public static CanSettings Timing800KBaud() {
            return new CanSettings(4, 16, 8, 3);
        }

        /// <summary>
        /// 1000 KBaud.
        /// </summary>
        public static CanSettings Timing1000KBaud () {
            return new CanSettings(4, 15, 4, 3);
        }
    }
}
