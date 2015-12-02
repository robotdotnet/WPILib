//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL;

namespace HAL_RoboRIO
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALSolenoid
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            global::HAL.HALSolenoid.InitializeSolenoidPort = (global::HAL.HALSolenoid.InitializeSolenoidPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeSolenoidPort"), typeof(global::HAL.HALSolenoid.InitializeSolenoidPortDelegate));

            global::HAL.HALSolenoid.FreeSolenoidPort = (global::HAL.HALSolenoid.FreeSolenoidPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "freeSolenoidPort"), typeof(global::HAL.HALSolenoid.FreeSolenoidPortDelegate));


            global::HAL.HALSolenoid.CheckSolenoidModule = (global::HAL.HALSolenoid.CheckSolenoidModuleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "checkSolenoidModule"), typeof(global::HAL.HALSolenoid.CheckSolenoidModuleDelegate));

            global::HAL.HALSolenoid.GetSolenoid = (global::HAL.HALSolenoid.GetSolenoidDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getSolenoid"), typeof(global::HAL.HALSolenoid.GetSolenoidDelegate));

            global::HAL.HALSolenoid.GetAllSolenoids = (global::HAL.HALSolenoid.GetAllSolenoidsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAllSolenoids"), typeof(global::HAL.HALSolenoid.GetAllSolenoidsDelegate));

            global::HAL.HALSolenoid.SetSolenoid = (global::HAL.HALSolenoid.SetSolenoidDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setSolenoid"), typeof(global::HAL.HALSolenoid.SetSolenoidDelegate));

            global::HAL.HALSolenoid.GetPCMSolenoidBlackList = (global::HAL.HALSolenoid.GetPCMSolenoidBlackListDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPCMSolenoidBlackList"), typeof(global::HAL.HALSolenoid.GetPCMSolenoidBlackListDelegate));

            global::HAL.HALSolenoid.GetPCMSolenoidVoltageStickyFault = (global::HAL.HALSolenoid.GetPCMSolenoidVoltageStickyFaultDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPCMSolenoidVoltageStickyFault"), typeof(global::HAL.HALSolenoid.GetPCMSolenoidVoltageStickyFaultDelegate));

            global::HAL.HALSolenoid.GetPCMSolenoidVoltageFault = (global::HAL.HALSolenoid.GetPCMSolenoidVoltageFaultDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPCMSolenoidVoltageFault"), typeof(global::HAL.HALSolenoid.GetPCMSolenoidVoltageFaultDelegate));

            global::HAL.HALSolenoid.ClearAllPCMStickyFaults_sol = (global::HAL.HALSolenoid.ClearAllPCMStickyFaults_solDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "clearAllPCMStickyFaults_sol"), typeof(global::HAL.HALSolenoid.ClearAllPCMStickyFaults_solDelegate));

        }
    }
}
