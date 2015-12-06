//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;

namespace HAL.AthenaHAL
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALCompressor
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALCompressor.InitializeCompressor = (Base.HALCompressor.InitializeCompressorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeCompressor"), typeof(Base.HALCompressor.InitializeCompressorDelegate));

            Base.HALCompressor.CheckCompressorModule = (Base.HALCompressor.CheckCompressorModuleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "checkCompressorModule"), typeof(Base.HALCompressor.CheckCompressorModuleDelegate));

            Base.HALCompressor.GetCompressor = (Base.HALCompressor.GetCompressorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCompressor"), typeof(Base.HALCompressor.GetCompressorDelegate));

            Base.HALCompressor.SetClosedLoopControl = (Base.HALCompressor.SetClosedLoopControlDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setClosedLoopControl"), typeof(Base.HALCompressor.SetClosedLoopControlDelegate));

            Base.HALCompressor.GetClosedLoopControl = (Base.HALCompressor.GetClosedLoopControlDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getClosedLoopControl"), typeof(Base.HALCompressor.GetClosedLoopControlDelegate));

            Base.HALCompressor.GetPressureSwitch = (Base.HALCompressor.GetPressureSwitchDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPressureSwitch"), typeof(Base.HALCompressor.GetPressureSwitchDelegate));

            Base.HALCompressor.GetCompressorCurrent = (Base.HALCompressor.GetCompressorCurrentDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCompressorCurrent"), typeof(Base.HALCompressor.GetCompressorCurrentDelegate));

            Base.HALCompressor.GetCompressorCurrentTooHighFault = (Base.HALCompressor.GetCompressorCurrentTooHighFaultDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCompressorCurrentTooHighFault"), typeof(Base.HALCompressor.GetCompressorCurrentTooHighFaultDelegate));

            Base.HALCompressor.GetCompressorCurrentTooHighStickyFault = (Base.HALCompressor.GetCompressorCurrentTooHighStickyFaultDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCompressorCurrentTooHighStickyFault"), typeof(Base.HALCompressor.GetCompressorCurrentTooHighStickyFaultDelegate));

            Base.HALCompressor.GetCompressorShortedStickyFault = (Base.HALCompressor.GetCompressorShortedStickyFaultDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCompressorShortedStickyFault"), typeof(Base.HALCompressor.GetCompressorShortedStickyFaultDelegate));

            Base.HALCompressor.GetCompressorShortedFault = (Base.HALCompressor.GetCompressorShortedFaultDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCompressorShortedFault"), typeof(Base.HALCompressor.GetCompressorShortedFaultDelegate));

            Base.HALCompressor.GetCompressorNotConnectedStickyFault = (Base.HALCompressor.GetCompressorNotConnectedStickyFaultDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCompressorNotConnectedStickyFault"), typeof(Base.HALCompressor.GetCompressorNotConnectedStickyFaultDelegate));

            Base.HALCompressor.GetCompressorNotConnectedFault = (Base.HALCompressor.GetCompressorNotConnectedFaultDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCompressorNotConnectedFault"), typeof(Base.HALCompressor.GetCompressorNotConnectedFaultDelegate));

            Base.HALCompressor.ClearAllPCMStickyFaults = (Base.HALCompressor.ClearAllPCMStickyFaultsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "clearAllPCMStickyFaults"), typeof(Base.HALCompressor.ClearAllPCMStickyFaultsDelegate));

        }
    }
}
