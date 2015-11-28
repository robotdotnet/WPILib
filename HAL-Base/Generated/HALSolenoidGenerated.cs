//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;

// ReSharper disable CheckNamespace

namespace HAL_Base
{
    public partial class HALSolenoid
    {
        static HALSolenoid()
        {
            HAL.Initialize();
        }

        public delegate IntPtr InitializeSolenoidPortDelegate(IntPtr port_pointer, ref int status);
        public static InitializeSolenoidPortDelegate InitializeSolenoidPort;

        public delegate void FreeSolenoidPortDelegate(IntPtr solenoid_port_pointer);
        public static FreeSolenoidPortDelegate FreeSolenoidPort;

        public delegate bool CheckSolenoidModuleDelegate(byte module);
        public static CheckSolenoidModuleDelegate CheckSolenoidModule;

        public delegate bool GetSolenoidDelegate(IntPtr solenoid_port_pointer, ref int status);
        public static GetSolenoidDelegate GetSolenoid;

        public delegate byte GetAllSolenoidsDelegate(IntPtr solenoid_port_pointer, ref int status);
        public static GetAllSolenoidsDelegate GetAllSolenoids;

        public delegate void SetSolenoidDelegate(IntPtr solenoid_port_pointer, [MarshalAs(UnmanagedType.I1)]bool value, ref int status);
        public static SetSolenoidDelegate SetSolenoid;

        public delegate int GetPCMSolenoidBlackListDelegate(IntPtr solenoid_port_pointer, ref int status);
        public static GetPCMSolenoidBlackListDelegate GetPCMSolenoidBlackList;

        public delegate bool GetPCMSolenoidVoltageStickyFaultDelegate(IntPtr solenoid_port_pointer, ref int status);
        public static GetPCMSolenoidVoltageStickyFaultDelegate GetPCMSolenoidVoltageStickyFault;

        public delegate bool GetPCMSolenoidVoltageFaultDelegate(IntPtr solenoid_port_pointer, ref int status);
        public static GetPCMSolenoidVoltageFaultDelegate GetPCMSolenoidVoltageFault;

        public delegate void ClearAllPCMStickyFaults_solDelegate(IntPtr solenoid_port_pointer, ref int status);
        public static ClearAllPCMStickyFaults_solDelegate ClearAllPCMStickyFaults_sol;
    }
}
