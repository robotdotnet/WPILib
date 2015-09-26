//File automatically generated using robotdotnet-tools. Please do not modify.

using System.Runtime.InteropServices;
using System.Security;

namespace HAL_RoboRIO
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALPower
    {
        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getVinVoltage")]
        public static extern float getVinVoltage(ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getVinCurrent")]
        public static extern float getVinCurrent(ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getUserVoltage6V")]
        public static extern float getUserVoltage6V(ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getUserCurrent6V")]
        public static extern float getUserCurrent6V(ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getUserActive6V")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getUserActive6V(ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getUserCurrentFaults6V")]
        public static extern int getUserCurrentFaults6V(ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getUserVoltage5V")]
        public static extern float getUserVoltage5V(ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getUserCurrent5V")]
        public static extern float getUserCurrent5V(ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getUserActive5V")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getUserActive5V(ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getUserCurrentFaults5V")]
        public static extern int getUserCurrentFaults5V(ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getUserVoltage3V3")]
        public static extern float getUserVoltage3V3(ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getUserCurrent3V3")]
        public static extern float getUserCurrent3V3(ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getUserActive3V3")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getUserActive3V3(ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getUserCurrentFaults3V3")]
        public static extern int getUserCurrentFaults3V3(ref int status);
    }
}
