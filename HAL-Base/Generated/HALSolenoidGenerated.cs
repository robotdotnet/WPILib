//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Linq;
using System.Reflection;
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

        internal static void SetupDelegates()
        {
            string className = MethodBase.GetCurrentMethod().DeclaringType.Name;
            var types = HAL.HALAssembly.GetTypes();
            var q = from t in types where t.IsClass && t.Name == className select t;
            Type type = HAL.HALAssembly.GetType(q.ToList()[0].FullName);
            InitializeSolenoidPort = (InitializeSolenoidPortDelegate)Delegate.CreateDelegate(typeof(InitializeSolenoidPortDelegate), type.GetMethod("initializeSolenoidPort"));
            CheckSolenoidModule = (CheckSolenoidModuleDelegate)Delegate.CreateDelegate(typeof(CheckSolenoidModuleDelegate), type.GetMethod("checkSolenoidModule"));
            GetSolenoid = (GetSolenoidDelegate)Delegate.CreateDelegate(typeof(GetSolenoidDelegate), type.GetMethod("getSolenoid"));
            SetSolenoid = (SetSolenoidDelegate)Delegate.CreateDelegate(typeof(SetSolenoidDelegate), type.GetMethod("setSolenoid"));
            GetPCMSolenoidBlackList = (GetPCMSolenoidBlackListDelegate)Delegate.CreateDelegate(typeof(GetPCMSolenoidBlackListDelegate), type.GetMethod("getPCMSolenoidBlackList"));
            GetPCMSolenoidVoltageStickyFault = (GetPCMSolenoidVoltageStickyFaultDelegate)Delegate.CreateDelegate(typeof(GetPCMSolenoidVoltageStickyFaultDelegate), type.GetMethod("getPCMSolenoidVoltageStickyFault"));
            GetPCMSolenoidVoltageFault = (GetPCMSolenoidVoltageFaultDelegate)Delegate.CreateDelegate(typeof(GetPCMSolenoidVoltageFaultDelegate), type.GetMethod("getPCMSolenoidVoltageFault"));
            ClearAllPCMStickyFaults_sol = (ClearAllPCMStickyFaults_solDelegate)Delegate.CreateDelegate(typeof(ClearAllPCMStickyFaults_solDelegate), type.GetMethod("clearAllPCMStickyFaults_sol"));
        }

        public delegate IntPtr InitializeSolenoidPortDelegate(IntPtr port_pointer, ref int status);
        public static InitializeSolenoidPortDelegate InitializeSolenoidPort;

        public delegate bool CheckSolenoidModuleDelegate(byte module);
        public static CheckSolenoidModuleDelegate CheckSolenoidModule;

        public delegate bool GetSolenoidDelegate(IntPtr solenoid_port_pointer, ref int status);
        public static GetSolenoidDelegate GetSolenoid;

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
