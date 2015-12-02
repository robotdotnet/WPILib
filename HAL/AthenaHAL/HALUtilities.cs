//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;

namespace HAL.Athena
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALUtilities
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALUtilities.DelayTicks = (Base.HALUtilities.DelayTicksDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "delayTicks"), typeof(Base.HALUtilities.DelayTicksDelegate));

            Base.HALUtilities.DelayMillis = (Base.HALUtilities.DelayMillisDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "delayMillis"), typeof(Base.HALUtilities.DelayMillisDelegate));

            Base.HALUtilities.DelaySeconds = (Base.HALUtilities.DelaySecondsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "delaySeconds"), typeof(Base.HALUtilities.DelaySecondsDelegate));

        }
    }
}
