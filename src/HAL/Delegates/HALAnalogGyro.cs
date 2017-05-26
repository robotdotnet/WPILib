using System.Runtime.InteropServices;
using FRC.NativeLibraryUtilities;

// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALAnalogGyro
    {
        public static void Ping() { }

        static HALAnalogGyro()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALAnalogGyro>(LibraryLoaderHolder.NativeLoader);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_InitializeAnalogGyroDelegate(int handle, ref int status);
        [NativeDelegate] public static HAL_InitializeAnalogGyroDelegate HAL_InitializeAnalogGyro;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetupAnalogGyroDelegate(int handle, ref int status);
        [NativeDelegate] public static HAL_SetupAnalogGyroDelegate HAL_SetupAnalogGyro;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_FreeAnalogGyroDelegate(int handle);
        [NativeDelegate] public static HAL_FreeAnalogGyroDelegate HAL_FreeAnalogGyro;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetAnalogGyroParametersDelegate(int handle, double voltsPerDegreePerSecond, double offset, int center, ref int status);
        [NativeDelegate] public static HAL_SetAnalogGyroParametersDelegate HAL_SetAnalogGyroParameters;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetAnalogGyroVoltsPerDegreePerSecondDelegate(int handle, double voltsPerDegreePerSecond, ref int status);
        [NativeDelegate] public static HAL_SetAnalogGyroVoltsPerDegreePerSecondDelegate HAL_SetAnalogGyroVoltsPerDegreePerSecond;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_ResetAnalogGyroDelegate(int handle, ref int status);
        [NativeDelegate] public static HAL_ResetAnalogGyroDelegate HAL_ResetAnalogGyro;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_CalibrateAnalogGyroDelegate(int handle, ref int status);
        [NativeDelegate] public static HAL_CalibrateAnalogGyroDelegate HAL_CalibrateAnalogGyro;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetAnalogGyroDeadbandDelegate(int handle, double volts, ref int status);
        [NativeDelegate] public static HAL_SetAnalogGyroDeadbandDelegate HAL_SetAnalogGyroDeadband;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate double HAL_GetAnalogGyroAngleDelegate(int handle, ref int status);
        [NativeDelegate] public static HAL_GetAnalogGyroAngleDelegate HAL_GetAnalogGyroAngle;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate double HAL_GetAnalogGyroRateDelegate(int handle, ref int status);
        [NativeDelegate] public static HAL_GetAnalogGyroRateDelegate HAL_GetAnalogGyroRate;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate double HAL_GetAnalogGyroOffsetDelegate(int handle, ref int status);
        [NativeDelegate] public static HAL_GetAnalogGyroOffsetDelegate HAL_GetAnalogGyroOffset;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetAnalogGyroCenterDelegate(int handle, ref int status);
        [NativeDelegate] public static HAL_GetAnalogGyroCenterDelegate HAL_GetAnalogGyroCenter;
    }
}

