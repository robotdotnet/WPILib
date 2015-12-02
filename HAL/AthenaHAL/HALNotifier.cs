//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace HAL.Athena
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALNotifier
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            global::HAL.HALNotifier.InitializeNotifier = (global::HAL.HALNotifier.InitializeNotifierDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeNotifier"), typeof(global::HAL.HALNotifier.InitializeNotifierDelegate));

            global::HAL.HALNotifier.CleanNotifier = (global::HAL.HALNotifier.CleanNotifierDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "cleanNotifier"), typeof(global::HAL.HALNotifier.CleanNotifierDelegate));

            global::HAL.HALNotifier.UpdateNotifierAlarm = (global::HAL.HALNotifier.UpdateNotifierAlarmDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "updateNotifierAlarm"), typeof(global::HAL.HALNotifier.UpdateNotifierAlarmDelegate));

        }
    }
}
