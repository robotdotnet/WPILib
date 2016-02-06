//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;

// ReSharper disable CheckNamespace

namespace HAL.Base
{
    public partial class HALSolenoid
    {
        static HALSolenoid()
        {
            HAL.Initialize();
        }

        public delegate SolenoidPortSafeHandle InitializeSolenoidPortDelegate(HALPortSafeHandle port_pointer, ref int status);
        public static InitializeSolenoidPortDelegate InitializeSolenoidPort;

        public delegate void FreeSolenoidPortDelegate(SolenoidPortSafeHandle solenoid_port_pointer);
        public static FreeSolenoidPortDelegate FreeSolenoidPort;

        public delegate bool CheckSolenoidModuleDelegate(byte module);
        public static CheckSolenoidModuleDelegate CheckSolenoidModule;

        public delegate bool GetSolenoidDelegate(SolenoidPortSafeHandle solenoid_port_pointer, ref int status);
        public static GetSolenoidDelegate GetSolenoid;

        public delegate byte GetAllSolenoidsDelegate(SolenoidPortSafeHandle solenoid_port_pointer, ref int status);
        public static GetAllSolenoidsDelegate GetAllSolenoids;

        public delegate void SetSolenoidDelegate(SolenoidPortSafeHandle solenoid_port_pointer, [MarshalAs(UnmanagedType.I1)]bool value, ref int status);
        public static SetSolenoidDelegate SetSolenoid;

        public delegate int GetPCMSolenoidBlackListDelegate(SolenoidPortSafeHandle solenoid_port_pointer, ref int status);
        public static GetPCMSolenoidBlackListDelegate GetPCMSolenoidBlackList;

        public delegate bool GetPCMSolenoidVoltageStickyFaultDelegate(SolenoidPortSafeHandle solenoid_port_pointer, ref int status);
        public static GetPCMSolenoidVoltageStickyFaultDelegate GetPCMSolenoidVoltageStickyFault;

        public delegate bool GetPCMSolenoidVoltageFaultDelegate(SolenoidPortSafeHandle solenoid_port_pointer, ref int status);
        public static GetPCMSolenoidVoltageFaultDelegate GetPCMSolenoidVoltageFault;

        public delegate void ClearAllPCMStickyFaults_solDelegate(SolenoidPortSafeHandle solenoid_port_pointer, ref int status);
        public static ClearAllPCMStickyFaults_solDelegate ClearAllPCMStickyFaults_sol;
    }
}
