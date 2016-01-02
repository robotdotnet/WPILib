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
            Base.HALNotifier.InitializeNotifier = (Base.HALNotifier.InitializeNotifierDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeNotifierShim"), typeof(Base.HALNotifier.InitializeNotifierDelegate));
            Base.HALNotifier.GetNotifierParam = (Base.HALNotifier.GetNotifierParamDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getNotifierParam"), typeof(Base.HALNotifier.GetNotifierParamDelegate));
            Base.HALNotifier.CleanNotifier = (Base.HALNotifier.CleanNotifierDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "cleanNotifierShim"), typeof(Base.HALNotifier.CleanNotifierDelegate));
            Base.HALNotifier.StopNotifierAlarm = (Base.HALNotifier.StopNotifierAlarmDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "stopNotifierAlarm"), typeof(Base.HALNotifier.StopNotifierAlarmDelegate));
            Base.HALNotifier.UpdateNotifierAlarm = (Base.HALNotifier.UpdateNotifierAlarmDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "updateNotifierAlarm"), typeof(Base.HALNotifier.UpdateNotifierAlarmDelegate));

        }
    }
}
