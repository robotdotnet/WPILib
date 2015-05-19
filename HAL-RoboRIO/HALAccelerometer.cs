//File automatically generated using robotdotnet-tools. Please do not modify.
using HAL_Base;
namespace HAL_RoboRIO
{
    public class HALAccelerometer
    {

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "setAccelerometerActive")]
        public static extern void setAccelerometerActive([System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool param0);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "setAccelerometerRange")]
        public static extern void setAccelerometerRange(AccelerometerRange param0);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getAccelerometerX")]
        public static extern double getAccelerometerX();

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getAccelerometerY")]
        public static extern double getAccelerometerY();

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getAccelerometerZ")]
        public static extern double getAccelerometerZ();
    }
}
