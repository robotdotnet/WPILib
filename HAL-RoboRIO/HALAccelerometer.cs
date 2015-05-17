using HAL_Base;

namespace HAL_RoboRIO
{
    public class HALAccelerometer
    {
        /// Return Type: void
        ///param0: boolean
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "setAccelerometerActive")]
        public static extern void setAccelerometerActive([System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)] bool param0);


        /// Return Type: void
        ///param0: AccelerometerRange
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "setAccelerometerRange")]
        public static extern void setAccelerometerRange(AccelerometerRange param0);


        /// Return Type: double
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "getAccelerometerX")]
        public static extern double getAccelerometerX();


        /// Return Type: double
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "getAccelerometerY")]
        public static extern double getAccelerometerY();


        /// Return Type: double
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "getAccelerometerZ")]
        public static extern double getAccelerometerZ();
    }
}
