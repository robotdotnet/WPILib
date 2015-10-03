//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL_Base;

namespace HAL_RoboRIO
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALCompressor
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            HAL_Base.HALCompressor.InitializeCompressor = (HAL_Base.HALCompressor.InitializeCompressorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeCompressor"), typeof(HAL_Base.HALCompressor.InitializeCompressorDelegate));

            HAL_Base.HALCompressor.CheckCompressorModule = (HAL_Base.HALCompressor.CheckCompressorModuleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "checkCompressorModule"), typeof(HAL_Base.HALCompressor.CheckCompressorModuleDelegate));

            HAL_Base.HALCompressor.GetCompressor = (HAL_Base.HALCompressor.GetCompressorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCompressor"), typeof(HAL_Base.HALCompressor.GetCompressorDelegate));

            HAL_Base.HALCompressor.SetClosedLoopControl = (HAL_Base.HALCompressor.SetClosedLoopControlDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setClosedLoopControl"), typeof(HAL_Base.HALCompressor.SetClosedLoopControlDelegate));

            HAL_Base.HALCompressor.GetClosedLoopControl = (HAL_Base.HALCompressor.GetClosedLoopControlDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getClosedLoopControl"), typeof(HAL_Base.HALCompressor.GetClosedLoopControlDelegate));

            HAL_Base.HALCompressor.GetPressureSwitch = (HAL_Base.HALCompressor.GetPressureSwitchDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPressureSwitch"), typeof(HAL_Base.HALCompressor.GetPressureSwitchDelegate));

            HAL_Base.HALCompressor.GetCompressorCurrent = (HAL_Base.HALCompressor.GetCompressorCurrentDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCompressorCurrent"), typeof(HAL_Base.HALCompressor.GetCompressorCurrentDelegate));

            HAL_Base.HALCompressor.GetCompressorCurrentTooHighFault = (HAL_Base.HALCompressor.GetCompressorCurrentTooHighFaultDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCompressorCurrentTooHighFault"), typeof(HAL_Base.HALCompressor.GetCompressorCurrentTooHighFaultDelegate));

            HAL_Base.HALCompressor.GetCompressorCurrentTooHighStickyFault = (HAL_Base.HALCompressor.GetCompressorCurrentTooHighStickyFaultDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCompressorCurrentTooHighStickyFault"), typeof(HAL_Base.HALCompressor.GetCompressorCurrentTooHighStickyFaultDelegate));

            HAL_Base.HALCompressor.GetCompressorShortedStickyFault = (HAL_Base.HALCompressor.GetCompressorShortedStickyFaultDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCompressorShortedStickyFault"), typeof(HAL_Base.HALCompressor.GetCompressorShortedStickyFaultDelegate));

            HAL_Base.HALCompressor.GetCompressorShortedFault = (HAL_Base.HALCompressor.GetCompressorShortedFaultDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCompressorShortedFault"), typeof(HAL_Base.HALCompressor.GetCompressorShortedFaultDelegate));

            HAL_Base.HALCompressor.GetCompressorNotConnectedStickyFault = (HAL_Base.HALCompressor.GetCompressorNotConnectedStickyFaultDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCompressorNotConnectedStickyFault"), typeof(HAL_Base.HALCompressor.GetCompressorNotConnectedStickyFaultDelegate));

            HAL_Base.HALCompressor.GetCompressorNotConnectedFault = (HAL_Base.HALCompressor.GetCompressorNotConnectedFaultDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCompressorNotConnectedFault"), typeof(HAL_Base.HALCompressor.GetCompressorNotConnectedFaultDelegate));

            HAL_Base.HALCompressor.ClearAllPCMStickyFaults = (HAL_Base.HALCompressor.ClearAllPCMStickyFaultsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "clearAllPCMStickyFaults"), typeof(HAL_Base.HALCompressor.ClearAllPCMStickyFaultsDelegate));

        }
    }
}
