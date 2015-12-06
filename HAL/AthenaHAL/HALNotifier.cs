//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;

namespace HAL.AthenaHAL
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALNotifier
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALNotifier.InitializeNotifier = (Base.HALNotifier.InitializeNotifierDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeNotifier"), typeof(Base.HALNotifier.InitializeNotifierDelegate));

            Base.HALNotifier.CleanNotifier = (Base.HALNotifier.CleanNotifierDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "cleanNotifier"), typeof(Base.HALNotifier.CleanNotifierDelegate));

            Base.HALNotifier.UpdateNotifierAlarm = (Base.HALNotifier.UpdateNotifierAlarmDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "updateNotifierAlarm"), typeof(Base.HALNotifier.UpdateNotifierAlarmDelegate));

        }
    }
}
