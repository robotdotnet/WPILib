//File automatically generated using robotdotnet-tools. Please do not modify.

using System.Runtime.InteropServices;
using HAL_Base;

namespace HAL_RoboRIO
{
    internal class HALAccelerometer
    {

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setAccelerometerActive")]
        internal static extern void setAccelerometerActive([MarshalAs(UnmanagedType.I1)] bool param0);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setAccelerometerRange")]
        internal static extern void setAccelerometerRange(AccelerometerRange param0);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAccelerometerX")]
        internal static extern double getAccelerometerX();

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAccelerometerY")]
        internal static extern double getAccelerometerY();

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAccelerometerZ")]
        internal static extern double getAccelerometerZ();
    }
}
