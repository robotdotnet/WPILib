//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL_Base;

namespace HAL_RoboRIO
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALPower
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            HAL_Base.HALPower.GetVinVoltage = (HAL_Base.HALPower.GetVinVoltageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getVinVoltage"), typeof(HAL_Base.HALPower.GetVinVoltageDelegate));

            HAL_Base.HALPower.GetVinCurrent = (HAL_Base.HALPower.GetVinCurrentDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getVinCurrent"), typeof(HAL_Base.HALPower.GetVinCurrentDelegate));

            HAL_Base.HALPower.GetUserVoltage6V = (HAL_Base.HALPower.GetUserVoltage6VDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getUserVoltage6V"), typeof(HAL_Base.HALPower.GetUserVoltage6VDelegate));

            HAL_Base.HALPower.GetUserCurrent6V = (HAL_Base.HALPower.GetUserCurrent6VDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getUserCurrent6V"), typeof(HAL_Base.HALPower.GetUserCurrent6VDelegate));

            HAL_Base.HALPower.GetUserActive6V = (HAL_Base.HALPower.GetUserActive6VDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getUserActive6V"), typeof(HAL_Base.HALPower.GetUserActive6VDelegate));

            HAL_Base.HALPower.GetUserCurrentFaults6V = (HAL_Base.HALPower.GetUserCurrentFaults6VDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getUserCurrentFaults6V"), typeof(HAL_Base.HALPower.GetUserCurrentFaults6VDelegate));

            HAL_Base.HALPower.GetUserVoltage5V = (HAL_Base.HALPower.GetUserVoltage5VDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getUserVoltage5V"), typeof(HAL_Base.HALPower.GetUserVoltage5VDelegate));

            HAL_Base.HALPower.GetUserCurrent5V = (HAL_Base.HALPower.GetUserCurrent5VDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getUserCurrent5V"), typeof(HAL_Base.HALPower.GetUserCurrent5VDelegate));

            HAL_Base.HALPower.GetUserActive5V = (HAL_Base.HALPower.GetUserActive5VDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getUserActive5V"), typeof(HAL_Base.HALPower.GetUserActive5VDelegate));

            HAL_Base.HALPower.GetUserCurrentFaults5V = (HAL_Base.HALPower.GetUserCurrentFaults5VDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getUserCurrentFaults5V"), typeof(HAL_Base.HALPower.GetUserCurrentFaults5VDelegate));

            HAL_Base.HALPower.GetUserVoltage3V3 = (HAL_Base.HALPower.GetUserVoltage3V3Delegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getUserVoltage3V3"), typeof(HAL_Base.HALPower.GetUserVoltage3V3Delegate));

            HAL_Base.HALPower.GetUserCurrent3V3 = (HAL_Base.HALPower.GetUserCurrent3V3Delegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getUserCurrent3V3"), typeof(HAL_Base.HALPower.GetUserCurrent3V3Delegate));

            HAL_Base.HALPower.GetUserActive3V3 = (HAL_Base.HALPower.GetUserActive3V3Delegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getUserActive3V3"), typeof(HAL_Base.HALPower.GetUserActive3V3Delegate));

            HAL_Base.HALPower.GetUserCurrentFaults3V3 = (HAL_Base.HALPower.GetUserCurrentFaults3V3Delegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getUserCurrentFaults3V3"), typeof(HAL_Base.HALPower.GetUserCurrentFaults3V3Delegate));

        }
    }
}
