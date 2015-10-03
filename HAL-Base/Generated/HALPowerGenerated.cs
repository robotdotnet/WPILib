//File automatically generated using robotdotnet-tools. Please do not modify.

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
