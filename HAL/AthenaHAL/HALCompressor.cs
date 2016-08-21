using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;
namespace HAL.AthenaHAL
{
internal class HALCompressor
{
internal static void Initialize(IntPtr library, ILibraryLoader loader)
{

Base.HALCompressor.HAL_InitializeCompressor = (Base.HALCompressor.HAL_InitializeCompressorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_InitializeCompressor"), typeof(Base.HALCompressor.HAL_InitializeCompressorDelegate));

Base.HALCompressor.HAL_CheckCompressorModule = (Base.HALCompressor.HAL_CheckCompressorModuleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_CheckCompressorModule"), typeof(Base.HALCompressor.HAL_CheckCompressorModuleDelegate));

Base.HALCompressor.HAL_GetCompressor = (Base.HALCompressor.HAL_GetCompressorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetCompressor"), typeof(Base.HALCompressor.HAL_GetCompressorDelegate));

Base.HALCompressor.HAL_SetCompressorClosedLoopControl = (Base.HALCompressor.HAL_SetCompressorClosedLoopControlDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetCompressorClosedLoopControl"), typeof(Base.HALCompressor.HAL_SetCompressorClosedLoopControlDelegate));

Base.HALCompressor.HAL_GetCompressorClosedLoopControl = (Base.HALCompressor.HAL_GetCompressorClosedLoopControlDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetCompressorClosedLoopControl"), typeof(Base.HALCompressor.HAL_GetCompressorClosedLoopControlDelegate));

Base.HALCompressor.HAL_GetCompressorPressureSwitch = (Base.HALCompressor.HAL_GetCompressorPressureSwitchDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetCompressorPressureSwitch"), typeof(Base.HALCompressor.HAL_GetCompressorPressureSwitchDelegate));

Base.HALCompressor.HAL_GetCompressorCurrent = (Base.HALCompressor.HAL_GetCompressorCurrentDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetCompressorCurrent"), typeof(Base.HALCompressor.HAL_GetCompressorCurrentDelegate));

Base.HALCompressor.HAL_GetCompressorCurrentTooHighFault = (Base.HALCompressor.HAL_GetCompressorCurrentTooHighFaultDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetCompressorCurrentTooHighFault"), typeof(Base.HALCompressor.HAL_GetCompressorCurrentTooHighFaultDelegate));

Base.HALCompressor.HAL_GetCompressorCurrentTooHighStickyFault = (Base.HALCompressor.HAL_GetCompressorCurrentTooHighStickyFaultDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetCompressorCurrentTooHighStickyFault"), typeof(Base.HALCompressor.HAL_GetCompressorCurrentTooHighStickyFaultDelegate));

Base.HALCompressor.HAL_GetCompressorShortedStickyFault = (Base.HALCompressor.HAL_GetCompressorShortedStickyFaultDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetCompressorShortedStickyFault"), typeof(Base.HALCompressor.HAL_GetCompressorShortedStickyFaultDelegate));

Base.HALCompressor.HAL_GetCompressorShortedFault = (Base.HALCompressor.HAL_GetCompressorShortedFaultDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetCompressorShortedFault"), typeof(Base.HALCompressor.HAL_GetCompressorShortedFaultDelegate));

Base.HALCompressor.HAL_GetCompressorNotConnectedStickyFault = (Base.HALCompressor.HAL_GetCompressorNotConnectedStickyFaultDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetCompressorNotConnectedStickyFault"), typeof(Base.HALCompressor.HAL_GetCompressorNotConnectedStickyFaultDelegate));

Base.HALCompressor.HAL_GetCompressorNotConnectedFault = (Base.HALCompressor.HAL_GetCompressorNotConnectedFaultDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetCompressorNotConnectedFault"), typeof(Base.HALCompressor.HAL_GetCompressorNotConnectedFaultDelegate));
}
}
}

