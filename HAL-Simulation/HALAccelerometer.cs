﻿using System.Runtime.InteropServices;

namespace HAL_FRC
{
    public enum AccelerometerRange
    {
        /// kRange_2G -> 0
        kRange_2G = 0,

        /// kRange_4G -> 1
        kRange_4G = 1,

        /// kRange_8G -> 2
        kRange_8G = 2,
    }

    public class HALAccelerometer
    {
        /// Return Type: void
        ///param0: boolean
        [DllImport("libHALAthena_shared.so", EntryPoint = "setAccelerometerActive")]
        public static extern void setAccelerometerActive([MarshalAs(UnmanagedType.I1)] bool param0);


        /// Return Type: void
        ///param0: AccelerometerRange
        [DllImport("libHALAthena_shared.so", EntryPoint = "setAccelerometerRange")]
        public static extern void setAccelerometerRange(AccelerometerRange param0);


        /// Return Type: double
        [DllImport("libHALAthena_shared.so", EntryPoint = "getAccelerometerX")]
        public static extern double getAccelerometerX();


        /// Return Type: double
        [DllImport("libHALAthena_shared.so", EntryPoint = "getAccelerometerY")]
        public static extern double getAccelerometerY();


        /// Return Type: double
        [DllImport("libHALAthena_shared.so", EntryPoint = "getAccelerometerZ")]
        public static extern double getAccelerometerZ();
    }
}
