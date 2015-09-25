//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL_Base;

namespace HAL_RoboRIO
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALUtilities
    {
        internal static void Initialize(IntPtr library, IDllLoader loader)
        {
            HAL_Base.HALUtilities.DelayTicks = (HAL_Base.HALUtilities.DelayTicksDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "delayTicks"), typeof(HAL_Base.HALUtilities.DelayTicksDelegate));

            HAL_Base.HALUtilities.DelayMillis = (HAL_Base.HALUtilities.DelayMillisDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "delayMillis"), typeof(HAL_Base.HALUtilities.DelayMillisDelegate));

            HAL_Base.HALUtilities.DelaySeconds = (HAL_Base.HALUtilities.DelaySecondsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "delaySeconds"), typeof(HAL_Base.HALUtilities.DelaySecondsDelegate));

        }

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "delayTicks")]
        public static extern void delayTicks(int ticks);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "delayMillis")]
        public static extern void delayMillis(double ms);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "delaySeconds")]
        public static extern void delaySeconds(double s);
    }
}
