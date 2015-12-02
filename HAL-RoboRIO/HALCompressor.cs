//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL;

namespace HAL_RoboRIO
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALCompressor
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            global::HAL.HALCompressor.InitializeCompressor = (global::HAL.HALCompressor.InitializeCompressorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeCompressor"), typeof(global::HAL.HALCompressor.InitializeCompressorDelegate));

            global::HAL.HALCompressor.CheckCompressorModule = (global::HAL.HALCompressor.CheckCompressorModuleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "checkCompressorModule"), typeof(global::HAL.HALCompressor.CheckCompressorModuleDelegate));

            global::HAL.HALCompressor.GetCompressor = (global::HAL.HALCompressor.GetCompressorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCompressor"), typeof(global::HAL.HALCompressor.GetCompressorDelegate));

            global::HAL.HALCompressor.SetClosedLoopControl = (global::HAL.HALCompressor.SetClosedLoopControlDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setClosedLoopControl"), typeof(global::HAL.HALCompressor.SetClosedLoopControlDelegate));

            global::HAL.HALCompressor.GetClosedLoopControl = (global::HAL.HALCompressor.GetClosedLoopControlDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getClosedLoopControl"), typeof(global::HAL.HALCompressor.GetClosedLoopControlDelegate));

            global::HAL.HALCompressor.GetPressureSwitch = (global::HAL.HALCompressor.GetPressureSwitchDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPressureSwitch"), typeof(global::HAL.HALCompressor.GetPressureSwitchDelegate));

            global::HAL.HALCompressor.GetCompressorCurrent = (global::HAL.HALCompressor.GetCompressorCurrentDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCompressorCurrent"), typeof(global::HAL.HALCompressor.GetCompressorCurrentDelegate));

            global::HAL.HALCompressor.GetCompressorCurrentTooHighFault = (global::HAL.HALCompressor.GetCompressorCurrentTooHighFaultDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCompressorCurrentTooHighFault"), typeof(global::HAL.HALCompressor.GetCompressorCurrentTooHighFaultDelegate));

            global::HAL.HALCompressor.GetCompressorCurrentTooHighStickyFault = (global::HAL.HALCompressor.GetCompressorCurrentTooHighStickyFaultDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCompressorCurrentTooHighStickyFault"), typeof(global::HAL.HALCompressor.GetCompressorCurrentTooHighStickyFaultDelegate));

            global::HAL.HALCompressor.GetCompressorShortedStickyFault = (global::HAL.HALCompressor.GetCompressorShortedStickyFaultDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCompressorShortedStickyFault"), typeof(global::HAL.HALCompressor.GetCompressorShortedStickyFaultDelegate));

            global::HAL.HALCompressor.GetCompressorShortedFault = (global::HAL.HALCompressor.GetCompressorShortedFaultDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCompressorShortedFault"), typeof(global::HAL.HALCompressor.GetCompressorShortedFaultDelegate));

            global::HAL.HALCompressor.GetCompressorNotConnectedStickyFault = (global::HAL.HALCompressor.GetCompressorNotConnectedStickyFaultDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCompressorNotConnectedStickyFault"), typeof(global::HAL.HALCompressor.GetCompressorNotConnectedStickyFaultDelegate));

            global::HAL.HALCompressor.GetCompressorNotConnectedFault = (global::HAL.HALCompressor.GetCompressorNotConnectedFaultDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCompressorNotConnectedFault"), typeof(global::HAL.HALCompressor.GetCompressorNotConnectedFaultDelegate));

            global::HAL.HALCompressor.ClearAllPCMStickyFaults = (global::HAL.HALCompressor.ClearAllPCMStickyFaultsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "clearAllPCMStickyFaults"), typeof(global::HAL.HALCompressor.ClearAllPCMStickyFaultsDelegate));

        }
    }
}
