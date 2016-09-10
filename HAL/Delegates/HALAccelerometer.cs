using System.Runtime.InteropServices;
using HAL.NativeLoader;

// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALAccelerometer
    {
        static HALAccelerometer()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALAccelerometer>(LibraryLoaderHolder.NativeLoader);
        }

        public delegate void HAL_SetAccelerometerActiveDelegate([MarshalAs(UnmanagedType.I4)]bool active);
        [NativeDelegate]
        public static HAL_SetAccelerometerActiveDelegate HAL_SetAccelerometerActive;

        public delegate void HAL_SetAccelerometerRangeDelegate(HALAccelerometerRange range);
        [NativeDelegate]
        public static HAL_SetAccelerometerRangeDelegate HAL_SetAccelerometerRange;

        public delegate double HAL_GetAccelerometerXDelegate();
        [NativeDelegate]
        public static HAL_GetAccelerometerXDelegate HAL_GetAccelerometerX;

        public delegate double HAL_GetAccelerometerYDelegate();
        [NativeDelegate]
        public static HAL_GetAccelerometerYDelegate HAL_GetAccelerometerY;

        public delegate double HAL_GetAccelerometerZDelegate();
        [NativeDelegate]
        public static HAL_GetAccelerometerZDelegate HAL_GetAccelerometerZ;
    }
}

