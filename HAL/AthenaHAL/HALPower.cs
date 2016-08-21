using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;
namespace HAL.AthenaHAL
{
internal class HALPower
{
internal static void Initialize(IntPtr library, ILibraryLoader loader)
{

Base.HALPower.HAL_GetVinVoltage = (Base.HALPower.HAL_GetVinVoltageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetVinVoltage"), typeof(Base.HALPower.HAL_GetVinVoltageDelegate));

Base.HALPower.HAL_GetVinCurrent = (Base.HALPower.HAL_GetVinCurrentDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetVinCurrent"), typeof(Base.HALPower.HAL_GetVinCurrentDelegate));

Base.HALPower.HAL_GetUserVoltage6V = (Base.HALPower.HAL_GetUserVoltage6VDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetUserVoltage6V"), typeof(Base.HALPower.HAL_GetUserVoltage6VDelegate));

Base.HALPower.HAL_GetUserCurrent6V = (Base.HALPower.HAL_GetUserCurrent6VDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetUserCurrent6V"), typeof(Base.HALPower.HAL_GetUserCurrent6VDelegate));

Base.HALPower.HAL_GetUserActive6V = (Base.HALPower.HAL_GetUserActive6VDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetUserActive6V"), typeof(Base.HALPower.HAL_GetUserActive6VDelegate));

Base.HALPower.HAL_GetUserCurrentFaults6V = (Base.HALPower.HAL_GetUserCurrentFaults6VDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetUserCurrentFaults6V"), typeof(Base.HALPower.HAL_GetUserCurrentFaults6VDelegate));

Base.HALPower.HAL_GetUserVoltage5V = (Base.HALPower.HAL_GetUserVoltage5VDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetUserVoltage5V"), typeof(Base.HALPower.HAL_GetUserVoltage5VDelegate));

Base.HALPower.HAL_GetUserCurrent5V = (Base.HALPower.HAL_GetUserCurrent5VDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetUserCurrent5V"), typeof(Base.HALPower.HAL_GetUserCurrent5VDelegate));

Base.HALPower.HAL_GetUserActive5V = (Base.HALPower.HAL_GetUserActive5VDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetUserActive5V"), typeof(Base.HALPower.HAL_GetUserActive5VDelegate));

Base.HALPower.HAL_GetUserCurrentFaults5V = (Base.HALPower.HAL_GetUserCurrentFaults5VDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetUserCurrentFaults5V"), typeof(Base.HALPower.HAL_GetUserCurrentFaults5VDelegate));

Base.HALPower.HAL_GetUserVoltage3V3 = (Base.HALPower.HAL_GetUserVoltage3V3Delegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetUserVoltage3V3"), typeof(Base.HALPower.HAL_GetUserVoltage3V3Delegate));

Base.HALPower.HAL_GetUserCurrent3V3 = (Base.HALPower.HAL_GetUserCurrent3V3Delegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetUserCurrent3V3"), typeof(Base.HALPower.HAL_GetUserCurrent3V3Delegate));

Base.HALPower.HAL_GetUserActive3V3 = (Base.HALPower.HAL_GetUserActive3V3Delegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetUserActive3V3"), typeof(Base.HALPower.HAL_GetUserActive3V3Delegate));

Base.HALPower.HAL_GetUserCurrentFaults3V3 = (Base.HALPower.HAL_GetUserCurrentFaults3V3Delegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetUserCurrentFaults3V3"), typeof(Base.HALPower.HAL_GetUserCurrentFaults3V3Delegate));
}
}
}

