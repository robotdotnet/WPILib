//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;

namespace HAL.Athena
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALPower
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALPower.GetVinVoltage = (Base.HALPower.GetVinVoltageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getVinVoltage"), typeof(Base.HALPower.GetVinVoltageDelegate));

            Base.HALPower.GetVinCurrent = (Base.HALPower.GetVinCurrentDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getVinCurrent"), typeof(Base.HALPower.GetVinCurrentDelegate));

            Base.HALPower.GetUserVoltage6V = (Base.HALPower.GetUserVoltage6VDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getUserVoltage6V"), typeof(Base.HALPower.GetUserVoltage6VDelegate));

            Base.HALPower.GetUserCurrent6V = (Base.HALPower.GetUserCurrent6VDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getUserCurrent6V"), typeof(Base.HALPower.GetUserCurrent6VDelegate));

            Base.HALPower.GetUserActive6V = (Base.HALPower.GetUserActive6VDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getUserActive6V"), typeof(Base.HALPower.GetUserActive6VDelegate));

            Base.HALPower.GetUserCurrentFaults6V = (Base.HALPower.GetUserCurrentFaults6VDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getUserCurrentFaults6V"), typeof(Base.HALPower.GetUserCurrentFaults6VDelegate));

            Base.HALPower.GetUserVoltage5V = (Base.HALPower.GetUserVoltage5VDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getUserVoltage5V"), typeof(Base.HALPower.GetUserVoltage5VDelegate));

            Base.HALPower.GetUserCurrent5V = (Base.HALPower.GetUserCurrent5VDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getUserCurrent5V"), typeof(Base.HALPower.GetUserCurrent5VDelegate));

            Base.HALPower.GetUserActive5V = (Base.HALPower.GetUserActive5VDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getUserActive5V"), typeof(Base.HALPower.GetUserActive5VDelegate));

            Base.HALPower.GetUserCurrentFaults5V = (Base.HALPower.GetUserCurrentFaults5VDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getUserCurrentFaults5V"), typeof(Base.HALPower.GetUserCurrentFaults5VDelegate));

            Base.HALPower.GetUserVoltage3V3 = (Base.HALPower.GetUserVoltage3V3Delegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getUserVoltage3V3"), typeof(Base.HALPower.GetUserVoltage3V3Delegate));

            Base.HALPower.GetUserCurrent3V3 = (Base.HALPower.GetUserCurrent3V3Delegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getUserCurrent3V3"), typeof(Base.HALPower.GetUserCurrent3V3Delegate));

            Base.HALPower.GetUserActive3V3 = (Base.HALPower.GetUserActive3V3Delegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getUserActive3V3"), typeof(Base.HALPower.GetUserActive3V3Delegate));

            Base.HALPower.GetUserCurrentFaults3V3 = (Base.HALPower.GetUserCurrentFaults3V3Delegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getUserCurrentFaults3V3"), typeof(Base.HALPower.GetUserCurrentFaults3V3Delegate));

        }
    }
}
