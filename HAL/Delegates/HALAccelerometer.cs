using System.Runtime.InteropServices;
using NativeLibraryUtilities;

// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALAccelerometer
    {
        public static void Ping() { }

        static HALAccelerometer()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALAccelerometer>(LibraryLoaderHolder.NativeLoader);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetAccelerometerActiveDelegate([MarshalAs(UnmanagedType.Bool)]bool active);
        [NativeDelegate]
        public static HAL_SetAccelerometerActiveDelegate HAL_SetAccelerometerActive;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetAccelerometerRangeDelegate(HALAccelerometerRange range);
        [NativeDelegate]
        public static HAL_SetAccelerometerRangeDelegate HAL_SetAccelerometerRange;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate double HAL_GetAccelerometerXDelegate();
        [NativeDelegate]
        public static HAL_GetAccelerometerXDelegate HAL_GetAccelerometerX;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate double HAL_GetAccelerometerYDelegate();
        [NativeDelegate]
        public static HAL_GetAccelerometerYDelegate HAL_GetAccelerometerY;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate double HAL_GetAccelerometerZDelegate();
        [NativeDelegate]
        public static HAL_GetAccelerometerZDelegate HAL_GetAccelerometerZ;
    }
}

