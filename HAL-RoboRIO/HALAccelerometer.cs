//File automatically generated using robotdotnet-tools. Please do not modify.

using System.Runtime.InteropServices;
using HAL_Base;

namespace HAL_RoboRIO
{
    public class HALAccelerometer
    {

        [DllImport(HAL_RoboRIO.HAL.LibhalathenaSharedSo, EntryPoint = "setAccelerometerActive")]
        public static extern void setAccelerometerActive([MarshalAs(UnmanagedType.I1)] bool param0);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setAccelerometerRange")]
        public static extern void setAccelerometerRange(AccelerometerRange param0);

        [DllImport(HAL_RoboRIO.HAL.LibhalathenaSharedSo, EntryPoint = "getAccelerometerX")]
        public static extern double getAccelerometerX();

        [DllImport(HAL_RoboRIO.HAL.LibhalathenaSharedSo, EntryPoint = "getAccelerometerY")]
        public static extern double getAccelerometerY();

        [DllImport(HAL_RoboRIO.HAL.LibhalathenaSharedSo, EntryPoint = "getAccelerometerZ")]
        public static extern double getAccelerometerZ();
    }
}
