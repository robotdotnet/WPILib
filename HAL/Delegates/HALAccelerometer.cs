using System;
using System.Runtime.InteropServices;
// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALAccelerometer
    {
        static HALAccelerometer()
        {
            HAL.Initialize();
        }

        public delegate void HAL_SetAccelerometerActiveDelegate([MarshalAs(UnmanagedType.I4)]bool active);
        public static HAL_SetAccelerometerActiveDelegate HAL_SetAccelerometerActive;

        public delegate void HAL_SetAccelerometerRangeDelegate(HALAccelerometerRange range);
        public static HAL_SetAccelerometerRangeDelegate HAL_SetAccelerometerRange;

        public delegate double HAL_GetAccelerometerXDelegate();
        public static HAL_GetAccelerometerXDelegate HAL_GetAccelerometerX;

        public delegate double HAL_GetAccelerometerYDelegate();
        public static HAL_GetAccelerometerYDelegate HAL_GetAccelerometerY;

        public delegate double HAL_GetAccelerometerZDelegate();
        public static HAL_GetAccelerometerZDelegate HAL_GetAccelerometerZ;
    }
}

