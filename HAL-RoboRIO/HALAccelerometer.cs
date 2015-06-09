//File automatically generated using robotdotnet-tools. Please do not modify.

using System.Runtime.InteropServices;
using HAL_Base;

namespace HAL_RoboRIO
{
    public class HALAccelerometer
    {

        [DllImport("libHALAthena_shared.so", EntryPoint = "setAccelerometerActive")]
        public static extern void setAccelerometerActive([MarshalAs(UnmanagedType.I1)] bool param0);

        [DllImport("libHALAthena_shared.so", EntryPoint = "setAccelerometerRange")]
        public static extern void setAccelerometerRange(AccelerometerRange param0);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getAccelerometerX")]
        public static extern double getAccelerometerX();

        [DllImport("libHALAthena_shared.so", EntryPoint = "getAccelerometerY")]
        public static extern double getAccelerometerY();

        [DllImport("libHALAthena_shared.so", EntryPoint = "getAccelerometerZ")]
        public static extern double getAccelerometerZ();
    }
}
