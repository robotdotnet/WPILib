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

        public delegate int HAL_InitializeEncoderDelegate(int digitalSourceHandleA, HALAnalogTriggerType analogTriggerTypeA, int digitalSourceHandleB, HALAnalogTriggerType analogTriggerTypeB, [MarshalAs(UnmanagedType.I4)]bool reverseDirection, HALEncoderEncodingType encodingType, ref int status);
        [NativeDelegate]
        public static HAL_InitializeEncoderDelegate HAL_InitializeEncoder;

        public delegate void HAL_FreeEncoderDelegate(int encoder_handle, ref int status);
        [NativeDelegate]
        public static HAL_FreeEncoderDelegate HAL_FreeEncoder;

        public delegate int HAL_GetEncoderDelegate(int encoder_handle, ref int status);
        [NativeDelegate]
        public static HAL_GetEncoderDelegate HAL_GetEncoder;

        public delegate int HAL_GetEncoderRawDelegate(int encoder_handle, ref int status);
        [NativeDelegate]
        public static HAL_GetEncoderRawDelegate HAL_GetEncoderRaw;

        public delegate int HAL_GetEncoderEncodingScaleDelegate(int encoder_handle, ref int status);
        [NativeDelegate]
        public static HAL_GetEncoderEncodingScaleDelegate HAL_GetEncoderEncodingScale;

        public delegate void HAL_ResetEncoderDelegate(int encoder_handle, ref int status);
        [NativeDelegate]
        public static HAL_ResetEncoderDelegate HAL_ResetEncoder;

        public delegate double HAL_GetEncoderPeriodDelegate(int encoder_handle, ref int status);
        [NativeDelegate]
        public static HAL_GetEncoderPeriodDelegate HAL_GetEncoderPeriod;

        public delegate void HAL_SetEncoderMaxPeriodDelegate(int encoder_handle, double maxPeriod, ref int status);
        [NativeDelegate]
        public static HAL_SetEncoderMaxPeriodDelegate HAL_SetEncoderMaxPeriod;

        [return: MarshalAs(UnmanagedType.I4)]
        public delegate bool HAL_GetEncoderStoppedDelegate(int encoder_handle, ref int status);
        [NativeDelegate]
        public static HAL_GetEncoderStoppedDelegate HAL_GetEncoderStopped;

        [return: MarshalAs(UnmanagedType.I4)]
        public delegate bool HAL_GetEncoderDirectionDelegate(int encoder_handle, ref int status);
        [NativeDelegate]
        public static HAL_GetEncoderDirectionDelegate HAL_GetEncoderDirection;

        public delegate double HAL_GetEncoderDistanceDelegate(int encoder_handle, ref int status);
        [NativeDelegate]
        public static HAL_GetEncoderDistanceDelegate HAL_GetEncoderDistance;

        public delegate double HAL_GetEncoderRateDelegate(int encoder_handle, ref int status);
        [NativeDelegate]
        public static HAL_GetEncoderRateDelegate HAL_GetEncoderRate;

        public delegate void HAL_SetEncoderMinRateDelegate(int encoder_handle, double minRate, ref int status);
        [NativeDelegate]
        public static HAL_SetEncoderMinRateDelegate HAL_SetEncoderMinRate;

        public delegate void HAL_SetEncoderDistancePerPulseDelegate(int encoder_handle, double distancePerPulse, ref int status);
        [NativeDelegate]
        public static HAL_SetEncoderDistancePerPulseDelegate HAL_SetEncoderDistancePerPulse;

        public delegate void HAL_SetEncoderReverseDirectionDelegate(int encoder_handle, [MarshalAs(UnmanagedType.I4)]bool reverseDirection, ref int status);
        [NativeDelegate]
        public static HAL_SetEncoderReverseDirectionDelegate HAL_SetEncoderReverseDirection;

        public delegate void HAL_SetEncoderSamplesToAverageDelegate(int encoder_handle, int samplesToAverage, ref int status);
        [NativeDelegate]
        public static HAL_SetEncoderSamplesToAverageDelegate HAL_SetEncoderSamplesToAverage;

        public delegate int HAL_GetEncoderSamplesToAverageDelegate(int encoder_handle, ref int status);
        [NativeDelegate]
        public static HAL_GetEncoderSamplesToAverageDelegate HAL_GetEncoderSamplesToAverage;

        public delegate void HAL_SetEncoderIndexSourceDelegate(int encoder_handle, int digitalSourceHandle, HALAnalogTriggerType analogTriggerType, HALEncoderIndexingType type, ref int status);
        [NativeDelegate]
        public static HAL_SetEncoderIndexSourceDelegate HAL_SetEncoderIndexSource;

        public delegate int HAL_GetEncoderFPGAIndexDelegate(int encoder_handle, ref int status);
        [NativeDelegate]
        public static HAL_GetEncoderFPGAIndexDelegate HAL_GetEncoderFPGAIndex;

        public delegate double HAL_GetEncoderDecodingScaleFactorDelegate(int encoder_handle, ref int status);
        [NativeDelegate]
        public static HAL_GetEncoderDecodingScaleFactorDelegate HAL_GetEncoderDecodingScaleFactor;

        public delegate double HAL_GetEncoderDistancePerPulseDelegate(int encoder_handle, ref int status);
        [NativeDelegate]
        public static HAL_GetEncoderDistancePerPulseDelegate HAL_GetEncoderDistancePerPulse;

        public delegate HALEncoderEncodingType HAL_GetEncoderEncodingTypeDelegate(int encoder_handle, ref int status);
        [NativeDelegate]
        public static HAL_GetEncoderEncodingTypeDelegate HAL_GetEncoderEncodingType;
    }
}

