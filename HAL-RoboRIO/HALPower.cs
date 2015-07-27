//File automatically generated using robotdotnet-tools. Please do not modify.

using System.Runtime.InteropServices;

namespace HAL_RoboRIO
{
    internal class HALPower
    {

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getVinVoltage")]
        internal static extern float getVinVoltage(ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getVinCurrent")]
        internal static extern float getVinCurrent(ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getUserVoltage6V")]
        internal static extern float getUserVoltage6V(ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getUserCurrent6V")]
        internal static extern float getUserCurrent6V(ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getUserActive6V")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool getUserActive6V(ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getUserCurrentFaults6V")]
        internal static extern int getUserCurrentFaults6V(ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getUserVoltage5V")]
        internal static extern float getUserVoltage5V(ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getUserCurrent5V")]
        internal static extern float getUserCurrent5V(ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getUserActive5V")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool getUserActive5V(ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getUserCurrentFaults5V")]
        internal static extern int getUserCurrentFaults5V(ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getUserVoltage3V3")]
        internal static extern float getUserVoltage3V3(ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getUserCurrent3V3")]
        internal static extern float getUserCurrent3V3(ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getUserActive3V3")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool getUserActive3V3(ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getUserCurrentFaults3V3")]
        internal static extern int getUserCurrentFaults3V3(ref int status);
    }
}
