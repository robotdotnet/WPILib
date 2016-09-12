using System.Runtime.InteropServices;
using HAL.NativeLoader;

// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALEncoder
    {
        static HALEncoder()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALEncoder>(LibraryLoaderHolder.NativeLoader);
        }

        /// <summary>
        /// Use this to force load the definitions in the file
        /// </summary>
        public static void Ping()
        {
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_InitializeEncoderDelegate(int digitalSourceHandleA, HALAnalogTriggerType analogTriggerTypeA, int digitalSourceHandleB, HALAnalogTriggerType analogTriggerTypeB, [MarshalAs(UnmanagedType.Bool)]bool reverseDirection, HALEncoderEncodingType encodingType, ref int status);
        [NativeDelegate]
        public static HAL_InitializeEncoderDelegate HAL_InitializeEncoder;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_FreeEncoderDelegate(int encoder_handle, ref int status);
        [NativeDelegate]
        public static HAL_FreeEncoderDelegate HAL_FreeEncoder;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetEncoderDelegate(int encoder_handle, ref int status);
        [NativeDelegate]
        public static HAL_GetEncoderDelegate HAL_GetEncoder;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetEncoderRawDelegate(int encoder_handle, ref int status);
        [NativeDelegate]
        public static HAL_GetEncoderRawDelegate HAL_GetEncoderRaw;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetEncoderEncodingScaleDelegate(int encoder_handle, ref int status);
        [NativeDelegate]
        public static HAL_GetEncoderEncodingScaleDelegate HAL_GetEncoderEncodingScale;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_ResetEncoderDelegate(int encoder_handle, ref int status);
        [NativeDelegate]
        public static HAL_ResetEncoderDelegate HAL_ResetEncoder;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate double HAL_GetEncoderPeriodDelegate(int encoder_handle, ref int status);
        [NativeDelegate]
        public static HAL_GetEncoderPeriodDelegate HAL_GetEncoderPeriod;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetEncoderMaxPeriodDelegate(int encoder_handle, double maxPeriod, ref int status);
        [NativeDelegate]
        public static HAL_SetEncoderMaxPeriodDelegate HAL_SetEncoderMaxPeriod;

        [return: MarshalAs(UnmanagedType.I4)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_GetEncoderStoppedDelegate(int encoder_handle, ref int status);
        [NativeDelegate]
        public static HAL_GetEncoderStoppedDelegate HAL_GetEncoderStopped;

        [return: MarshalAs(UnmanagedType.I4)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_GetEncoderDirectionDelegate(int encoder_handle, ref int status);
        [NativeDelegate]
        public static HAL_GetEncoderDirectionDelegate HAL_GetEncoderDirection;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate double HAL_GetEncoderDistanceDelegate(int encoder_handle, ref int status);
        [NativeDelegate]
        public static HAL_GetEncoderDistanceDelegate HAL_GetEncoderDistance;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate double HAL_GetEncoderRateDelegate(int encoder_handle, ref int status);
        [NativeDelegate]
        public static HAL_GetEncoderRateDelegate HAL_GetEncoderRate;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetEncoderMinRateDelegate(int encoder_handle, double minRate, ref int status);
        [NativeDelegate]
        public static HAL_SetEncoderMinRateDelegate HAL_SetEncoderMinRate;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetEncoderDistancePerPulseDelegate(int encoder_handle, double distancePerPulse, ref int status);
        [NativeDelegate]
        public static HAL_SetEncoderDistancePerPulseDelegate HAL_SetEncoderDistancePerPulse;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetEncoderReverseDirectionDelegate(int encoder_handle, [MarshalAs(UnmanagedType.Bool)]bool reverseDirection, ref int status);
        [NativeDelegate]
        public static HAL_SetEncoderReverseDirectionDelegate HAL_SetEncoderReverseDirection;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetEncoderSamplesToAverageDelegate(int encoder_handle, int samplesToAverage, ref int status);
        [NativeDelegate]
        public static HAL_SetEncoderSamplesToAverageDelegate HAL_SetEncoderSamplesToAverage;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetEncoderSamplesToAverageDelegate(int encoder_handle, ref int status);
        [NativeDelegate]
        public static HAL_GetEncoderSamplesToAverageDelegate HAL_GetEncoderSamplesToAverage;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetEncoderIndexSourceDelegate(int encoder_handle, int digitalSourceHandle, HALAnalogTriggerType analogTriggerType, HALEncoderIndexingType type, ref int status);
        [NativeDelegate]
        public static HAL_SetEncoderIndexSourceDelegate HAL_SetEncoderIndexSource;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetEncoderFPGAIndexDelegate(int encoder_handle, ref int status);
        [NativeDelegate]
        public static HAL_GetEncoderFPGAIndexDelegate HAL_GetEncoderFPGAIndex;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate double HAL_GetEncoderDecodingScaleFactorDelegate(int encoder_handle, ref int status);
        [NativeDelegate]
        public static HAL_GetEncoderDecodingScaleFactorDelegate HAL_GetEncoderDecodingScaleFactor;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate double HAL_GetEncoderDistancePerPulseDelegate(int encoder_handle, ref int status);
        [NativeDelegate]
        public static HAL_GetEncoderDistancePerPulseDelegate HAL_GetEncoderDistancePerPulse;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate HALEncoderEncodingType HAL_GetEncoderEncodingTypeDelegate(int encoder_handle, ref int status);
        [NativeDelegate]
        public static HAL_GetEncoderEncodingTypeDelegate HAL_GetEncoderEncodingType;
    }
}

