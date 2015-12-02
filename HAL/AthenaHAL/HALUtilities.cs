//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace HAL.Athena
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALUtilities
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            global::HAL.HALUtilities.DelayTicks = (global::HAL.HALUtilities.DelayTicksDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "delayTicks"), typeof(global::HAL.HALUtilities.DelayTicksDelegate));

            global::HAL.HALUtilities.DelayMillis = (global::HAL.HALUtilities.DelayMillisDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "delayMillis"), typeof(global::HAL.HALUtilities.DelayMillisDelegate));

            global::HAL.HALUtilities.DelaySeconds = (global::HAL.HALUtilities.DelaySecondsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "delaySeconds"), typeof(global::HAL.HALUtilities.DelaySecondsDelegate));

        }
    }
}
