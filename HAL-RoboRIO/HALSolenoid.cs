//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL_Base;

namespace HAL_RoboRIO
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALSolenoid
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            HAL_Base.HALSolenoid.InitializeSolenoidPort = (HAL_Base.HALSolenoid.InitializeSolenoidPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeSolenoidPort"), typeof(HAL_Base.HALSolenoid.InitializeSolenoidPortDelegate));

            HAL_Base.HALSolenoid.FreeSolenoidPort = (HAL_Base.HALSolenoid.FreeSolenoidPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "freeSolenoidPort"), typeof(HAL_Base.HALSolenoid.FreeSolenoidPortDelegate));


            HAL_Base.HALSolenoid.CheckSolenoidModule = (HAL_Base.HALSolenoid.CheckSolenoidModuleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "checkSolenoidModule"), typeof(HAL_Base.HALSolenoid.CheckSolenoidModuleDelegate));

            HAL_Base.HALSolenoid.GetSolenoid = (HAL_Base.HALSolenoid.GetSolenoidDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getSolenoid"), typeof(HAL_Base.HALSolenoid.GetSolenoidDelegate));

            HAL_Base.HALSolenoid.SetSolenoid = (HAL_Base.HALSolenoid.SetSolenoidDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setSolenoid"), typeof(HAL_Base.HALSolenoid.SetSolenoidDelegate));

            HAL_Base.HALSolenoid.GetPCMSolenoidBlackList = (HAL_Base.HALSolenoid.GetPCMSolenoidBlackListDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPCMSolenoidBlackList"), typeof(HAL_Base.HALSolenoid.GetPCMSolenoidBlackListDelegate));

            HAL_Base.HALSolenoid.GetPCMSolenoidVoltageStickyFault = (HAL_Base.HALSolenoid.GetPCMSolenoidVoltageStickyFaultDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPCMSolenoidVoltageStickyFault"), typeof(HAL_Base.HALSolenoid.GetPCMSolenoidVoltageStickyFaultDelegate));

            HAL_Base.HALSolenoid.GetPCMSolenoidVoltageFault = (HAL_Base.HALSolenoid.GetPCMSolenoidVoltageFaultDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPCMSolenoidVoltageFault"), typeof(HAL_Base.HALSolenoid.GetPCMSolenoidVoltageFaultDelegate));

            HAL_Base.HALSolenoid.ClearAllPCMStickyFaults_sol = (HAL_Base.HALSolenoid.ClearAllPCMStickyFaults_solDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "clearAllPCMStickyFaults_sol"), typeof(HAL_Base.HALSolenoid.ClearAllPCMStickyFaults_solDelegate));

        }
    }
}
