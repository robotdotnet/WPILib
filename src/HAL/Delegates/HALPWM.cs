using System.Runtime.InteropServices;
using FRC.NativeLibraryUtilities;

// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALPWM
    {
        static HALPWM()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALPWM>(LibraryLoaderHolder.NativeLoader);
        }

        /// <summary>
        /// Use this to force load the definitions in the file
        /// </summary>
        public static void Ping()
        {
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_InitializePWMPortDelegate(int port_handle, ref int status);
        [NativeDelegate] public static HAL_InitializePWMPortDelegate HAL_InitializePWMPort;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_FreePWMPortDelegate(int pwm_port_handle, ref int status);
        [NativeDelegate] public static HAL_FreePWMPortDelegate HAL_FreePWMPort;

        [return: MarshalAs(UnmanagedType.Bool)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_CheckPWMChannelDelegate(int channel);
        [NativeDelegate] public static HAL_CheckPWMChannelDelegate HAL_CheckPWMChannel;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetPWMConfigDelegate(int pwm_port_handle, double maxPwm, double deadbandMaxPwm, double centerPwm, double deadbandMinPwm, double minPwm, ref int status);
        [NativeDelegate] public static HAL_SetPWMConfigDelegate HAL_SetPWMConfig;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetPWMConfigRawDelegate(int pwm_port_handle, int maxPwm, int deadbandMaxPwm, int centerPwm, int deadbandMinPwm, int minPwm, ref int status);
        [NativeDelegate] public static HAL_SetPWMConfigRawDelegate HAL_SetPWMConfigRaw;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_GetPWMConfigRawDelegate(int pwm_port_handle, ref int maxPwm, ref int deadbandMaxPwm, ref int centerPwm, ref int deadbandMinPwm, ref int minPwm, ref int status);
        [NativeDelegate] public static HAL_GetPWMConfigRawDelegate HAL_GetPWMConfigRaw;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetPWMEliminateDeadbandDelegate(int pwm_port_handle, [MarshalAs(UnmanagedType.Bool)]bool eliminateDeadband, ref int status);
        [NativeDelegate] public static HAL_SetPWMEliminateDeadbandDelegate HAL_SetPWMEliminateDeadband;

        [return: MarshalAs(UnmanagedType.Bool)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_GetPWMEliminateDeadbandDelegate(int pwm_port_handle, ref int status);
        [NativeDelegate] public static HAL_GetPWMEliminateDeadbandDelegate HAL_GetPWMEliminateDeadband;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetPWMRawDelegate(int pwm_port_handle, int value, ref int status);
        [NativeDelegate] public static HAL_SetPWMRawDelegate HAL_SetPWMRaw;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetPWMSpeedDelegate(int pwm_port_handle, double speed, ref int status);
        [NativeDelegate] public static HAL_SetPWMSpeedDelegate HAL_SetPWMSpeed;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetPWMPositionDelegate(int pwm_port_handle, double position, ref int status);
        [NativeDelegate] public static HAL_SetPWMPositionDelegate HAL_SetPWMPosition;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetPWMDisabledDelegate(int pwm_port_handle, ref int status);
        [NativeDelegate] public static HAL_SetPWMDisabledDelegate HAL_SetPWMDisabled;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetPWMRawDelegate(int pwm_port_handle, ref int status);
        [NativeDelegate] public static HAL_GetPWMRawDelegate HAL_GetPWMRaw;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate double HAL_GetPWMSpeedDelegate(int pwm_port_handle, ref int status);
        [NativeDelegate] public static HAL_GetPWMSpeedDelegate HAL_GetPWMSpeed;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate double HAL_GetPWMPositionDelegate(int pwm_port_handle, ref int status);
        [NativeDelegate] public static HAL_GetPWMPositionDelegate HAL_GetPWMPosition;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_LatchPWMZeroDelegate(int pwm_port_handle, ref int status);
        [NativeDelegate] public static HAL_LatchPWMZeroDelegate HAL_LatchPWMZero;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetPWMPeriodScaleDelegate(int pwm_port_handle, int squelchMask, ref int status);
        [NativeDelegate] public static HAL_SetPWMPeriodScaleDelegate HAL_SetPWMPeriodScale;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetLoopTimingDelegate(ref int status);
        [NativeDelegate] public static HAL_GetLoopTimingDelegate HAL_GetLoopTiming;
    }
}

