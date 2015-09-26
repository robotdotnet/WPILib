//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

// ReSharper disable CheckNamespace

namespace HAL_Base
{
    public partial class HALPower
    {
        static HALPower()
        {
            HAL.Initialize();
        }

        internal static void SetupDelegates()
        {
            string className = MethodBase.GetCurrentMethod().DeclaringType.Name;
            var types = HAL.HALAssembly.GetTypes();
            var q = from t in types where t.IsClass && t.Name == className select t;
            Type type = HAL.HALAssembly.GetType(q.ToList()[0].FullName);
            GetVinVoltage = (GetVinVoltageDelegate)Delegate.CreateDelegate(typeof(GetVinVoltageDelegate), type.GetMethod("getVinVoltage"));
            GetVinCurrent = (GetVinCurrentDelegate)Delegate.CreateDelegate(typeof(GetVinCurrentDelegate), type.GetMethod("getVinCurrent"));
            GetUserVoltage6V = (GetUserVoltage6VDelegate)Delegate.CreateDelegate(typeof(GetUserVoltage6VDelegate), type.GetMethod("getUserVoltage6V"));
            GetUserCurrent6V = (GetUserCurrent6VDelegate)Delegate.CreateDelegate(typeof(GetUserCurrent6VDelegate), type.GetMethod("getUserCurrent6V"));
            GetUserActive6V = (GetUserActive6VDelegate)Delegate.CreateDelegate(typeof(GetUserActive6VDelegate), type.GetMethod("getUserActive6V"));
            GetUserCurrentFaults6V = (GetUserCurrentFaults6VDelegate)Delegate.CreateDelegate(typeof(GetUserCurrentFaults6VDelegate), type.GetMethod("getUserCurrentFaults6V"));
            GetUserVoltage5V = (GetUserVoltage5VDelegate)Delegate.CreateDelegate(typeof(GetUserVoltage5VDelegate), type.GetMethod("getUserVoltage5V"));
            GetUserCurrent5V = (GetUserCurrent5VDelegate)Delegate.CreateDelegate(typeof(GetUserCurrent5VDelegate), type.GetMethod("getUserCurrent5V"));
            GetUserActive5V = (GetUserActive5VDelegate)Delegate.CreateDelegate(typeof(GetUserActive5VDelegate), type.GetMethod("getUserActive5V"));
            GetUserCurrentFaults5V = (GetUserCurrentFaults5VDelegate)Delegate.CreateDelegate(typeof(GetUserCurrentFaults5VDelegate), type.GetMethod("getUserCurrentFaults5V"));
            GetUserVoltage3V3 = (GetUserVoltage3V3Delegate)Delegate.CreateDelegate(typeof(GetUserVoltage3V3Delegate), type.GetMethod("getUserVoltage3V3"));
            GetUserCurrent3V3 = (GetUserCurrent3V3Delegate)Delegate.CreateDelegate(typeof(GetUserCurrent3V3Delegate), type.GetMethod("getUserCurrent3V3"));
            GetUserActive3V3 = (GetUserActive3V3Delegate)Delegate.CreateDelegate(typeof(GetUserActive3V3Delegate), type.GetMethod("getUserActive3V3"));
            GetUserCurrentFaults3V3 = (GetUserCurrentFaults3V3Delegate)Delegate.CreateDelegate(typeof(GetUserCurrentFaults3V3Delegate), type.GetMethod("getUserCurrentFaults3V3"));
        }

        public delegate float GetVinVoltageDelegate(ref int status);
        public static GetVinVoltageDelegate GetVinVoltage;

        public delegate float GetVinCurrentDelegate(ref int status);
        public static GetVinCurrentDelegate GetVinCurrent;

        public delegate float GetUserVoltage6VDelegate(ref int status);
        public static GetUserVoltage6VDelegate GetUserVoltage6V;

        public delegate float GetUserCurrent6VDelegate(ref int status);
        public static GetUserCurrent6VDelegate GetUserCurrent6V;

        [return: MarshalAs(UnmanagedType.I1)]public delegate bool GetUserActive6VDelegate(ref int status);
        public static GetUserActive6VDelegate GetUserActive6V;

        public delegate int GetUserCurrentFaults6VDelegate(ref int status);
        public static GetUserCurrentFaults6VDelegate GetUserCurrentFaults6V;

        public delegate float GetUserVoltage5VDelegate(ref int status);
        public static GetUserVoltage5VDelegate GetUserVoltage5V;

        public delegate float GetUserCurrent5VDelegate(ref int status);
        public static GetUserCurrent5VDelegate GetUserCurrent5V;

        [return: MarshalAs(UnmanagedType.I1)]public delegate bool GetUserActive5VDelegate(ref int status);
        public static GetUserActive5VDelegate GetUserActive5V;

        public delegate int GetUserCurrentFaults5VDelegate(ref int status);
        public static GetUserCurrentFaults5VDelegate GetUserCurrentFaults5V;

        public delegate float GetUserVoltage3V3Delegate(ref int status);
        public static GetUserVoltage3V3Delegate GetUserVoltage3V3;

        public delegate float GetUserCurrent3V3Delegate(ref int status);
        public static GetUserCurrent3V3Delegate GetUserCurrent3V3;

        [return: MarshalAs(UnmanagedType.I1)]public delegate bool GetUserActive3V3Delegate(ref int status);
        public static GetUserActive3V3Delegate GetUserActive3V3;

        public delegate int GetUserCurrentFaults3V3Delegate(ref int status);
        public static GetUserCurrentFaults3V3Delegate GetUserCurrentFaults3V3;
    }
}
