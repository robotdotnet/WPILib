//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace HAL.Athena
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALPower
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            global::HAL.HALPower.GetVinVoltage = (global::HAL.HALPower.GetVinVoltageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getVinVoltage"), typeof(global::HAL.HALPower.GetVinVoltageDelegate));

            global::HAL.HALPower.GetVinCurrent = (global::HAL.HALPower.GetVinCurrentDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getVinCurrent"), typeof(global::HAL.HALPower.GetVinCurrentDelegate));

            global::HAL.HALPower.GetUserVoltage6V = (global::HAL.HALPower.GetUserVoltage6VDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getUserVoltage6V"), typeof(global::HAL.HALPower.GetUserVoltage6VDelegate));

            global::HAL.HALPower.GetUserCurrent6V = (global::HAL.HALPower.GetUserCurrent6VDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getUserCurrent6V"), typeof(global::HAL.HALPower.GetUserCurrent6VDelegate));

            global::HAL.HALPower.GetUserActive6V = (global::HAL.HALPower.GetUserActive6VDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getUserActive6V"), typeof(global::HAL.HALPower.GetUserActive6VDelegate));

            global::HAL.HALPower.GetUserCurrentFaults6V = (global::HAL.HALPower.GetUserCurrentFaults6VDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getUserCurrentFaults6V"), typeof(global::HAL.HALPower.GetUserCurrentFaults6VDelegate));

            global::HAL.HALPower.GetUserVoltage5V = (global::HAL.HALPower.GetUserVoltage5VDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getUserVoltage5V"), typeof(global::HAL.HALPower.GetUserVoltage5VDelegate));

            global::HAL.HALPower.GetUserCurrent5V = (global::HAL.HALPower.GetUserCurrent5VDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getUserCurrent5V"), typeof(global::HAL.HALPower.GetUserCurrent5VDelegate));

            global::HAL.HALPower.GetUserActive5V = (global::HAL.HALPower.GetUserActive5VDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getUserActive5V"), typeof(global::HAL.HALPower.GetUserActive5VDelegate));

            global::HAL.HALPower.GetUserCurrentFaults5V = (global::HAL.HALPower.GetUserCurrentFaults5VDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getUserCurrentFaults5V"), typeof(global::HAL.HALPower.GetUserCurrentFaults5VDelegate));

            global::HAL.HALPower.GetUserVoltage3V3 = (global::HAL.HALPower.GetUserVoltage3V3Delegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getUserVoltage3V3"), typeof(global::HAL.HALPower.GetUserVoltage3V3Delegate));

            global::HAL.HALPower.GetUserCurrent3V3 = (global::HAL.HALPower.GetUserCurrent3V3Delegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getUserCurrent3V3"), typeof(global::HAL.HALPower.GetUserCurrent3V3Delegate));

            global::HAL.HALPower.GetUserActive3V3 = (global::HAL.HALPower.GetUserActive3V3Delegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getUserActive3V3"), typeof(global::HAL.HALPower.GetUserActive3V3Delegate));

            global::HAL.HALPower.GetUserCurrentFaults3V3 = (global::HAL.HALPower.GetUserCurrentFaults3V3Delegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getUserCurrentFaults3V3"), typeof(global::HAL.HALPower.GetUserCurrentFaults3V3Delegate));

        }
    }
}
