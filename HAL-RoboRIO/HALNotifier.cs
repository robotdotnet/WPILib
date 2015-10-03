//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL_Base;

namespace HAL_RoboRIO
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALNotifier
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            HAL_Base.HALNotifier.InitializeNotifier = (HAL_Base.HALNotifier.InitializeNotifierDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeNotifier"), typeof(HAL_Base.HALNotifier.InitializeNotifierDelegate));

            HAL_Base.HALNotifier.CleanNotifier = (HAL_Base.HALNotifier.CleanNotifierDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "cleanNotifier"), typeof(HAL_Base.HALNotifier.CleanNotifierDelegate));

            HAL_Base.HALNotifier.UpdateNotifierAlarm = (HAL_Base.HALNotifier.UpdateNotifierAlarmDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "updateNotifierAlarm"), typeof(HAL_Base.HALNotifier.UpdateNotifierAlarmDelegate));

        }
    }
}
