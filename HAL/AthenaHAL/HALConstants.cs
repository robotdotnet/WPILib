using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;
namespace HAL.AthenaHAL
{
internal class HALConstants
{
internal static void Initialize(IntPtr library, ILibraryLoader loader)
{

Base.HALConstants.HAL_GetSystemClockTicksPerMicrosecond = (Base.HALConstants.HAL_GetSystemClockTicksPerMicrosecondDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetSystemClockTicksPerMicrosecond"), typeof(Base.HALConstants.HAL_GetSystemClockTicksPerMicrosecondDelegate));
}
}
}

