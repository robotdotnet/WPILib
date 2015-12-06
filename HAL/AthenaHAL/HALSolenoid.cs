//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;

namespace HAL.AthenaHAL
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALSolenoid
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALSolenoid.InitializeSolenoidPort = (Base.HALSolenoid.InitializeSolenoidPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeSolenoidPort"), typeof(Base.HALSolenoid.InitializeSolenoidPortDelegate));

            Base.HALSolenoid.FreeSolenoidPort = (Base.HALSolenoid.FreeSolenoidPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "freeSolenoidPort"), typeof(Base.HALSolenoid.FreeSolenoidPortDelegate));


            Base.HALSolenoid.CheckSolenoidModule = (Base.HALSolenoid.CheckSolenoidModuleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "checkSolenoidModule"), typeof(Base.HALSolenoid.CheckSolenoidModuleDelegate));

            Base.HALSolenoid.GetSolenoid = (Base.HALSolenoid.GetSolenoidDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getSolenoid"), typeof(Base.HALSolenoid.GetSolenoidDelegate));

            Base.HALSolenoid.GetAllSolenoids = (Base.HALSolenoid.GetAllSolenoidsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAllSolenoids"), typeof(Base.HALSolenoid.GetAllSolenoidsDelegate));

            Base.HALSolenoid.SetSolenoid = (Base.HALSolenoid.SetSolenoidDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setSolenoid"), typeof(Base.HALSolenoid.SetSolenoidDelegate));

            Base.HALSolenoid.GetPCMSolenoidBlackList = (Base.HALSolenoid.GetPCMSolenoidBlackListDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPCMSolenoidBlackList"), typeof(Base.HALSolenoid.GetPCMSolenoidBlackListDelegate));

            Base.HALSolenoid.GetPCMSolenoidVoltageStickyFault = (Base.HALSolenoid.GetPCMSolenoidVoltageStickyFaultDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPCMSolenoidVoltageStickyFault"), typeof(Base.HALSolenoid.GetPCMSolenoidVoltageStickyFaultDelegate));

            Base.HALSolenoid.GetPCMSolenoidVoltageFault = (Base.HALSolenoid.GetPCMSolenoidVoltageFaultDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPCMSolenoidVoltageFault"), typeof(Base.HALSolenoid.GetPCMSolenoidVoltageFaultDelegate));

            Base.HALSolenoid.ClearAllPCMStickyFaults_sol = (Base.HALSolenoid.ClearAllPCMStickyFaults_solDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "clearAllPCMStickyFaults_sol"), typeof(Base.HALSolenoid.ClearAllPCMStickyFaults_solDelegate));

        }
    }
}
