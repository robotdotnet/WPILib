//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;

namespace HAL_RoboRIO
{
    internal class HALNotifier
    {

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "initializeNotifier")]
        public static extern IntPtr initializeNotifier(Action<uint, IntPtr> ProcessQueue, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "cleanNotifier")]
        public static extern void cleanNotifier(IntPtr notifier_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "updateNotifierAlarm")]
        public static extern void updateNotifierAlarm(IntPtr notifier_pointer, uint triggerTime, ref int status);
    }
}
