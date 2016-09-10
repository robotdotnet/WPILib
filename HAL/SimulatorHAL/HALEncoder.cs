using System;
using System.Runtime.InteropServices;
using HAL.Base;
using HAL.NativeLoader;

// ReSharper disable CheckNamespace
namespace HAL.SimulatorHAL
{
    internal class HALEncoder
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALEncoder.HAL_InitializeEncoder = HAL_InitializeEncoder;
            Base.HALEncoder.HAL_FreeEncoder = HAL_FreeEncoder;
            Base.HALEncoder.HAL_GetEncoder = HAL_GetEncoder;
            Base.HALEncoder.HAL_GetEncoderRaw = HAL_GetEncoderRaw;
            Base.HALEncoder.HAL_GetEncoderEncodingScale = HAL_GetEncoderEncodingScale;
            Base.HALEncoder.HAL_ResetEncoder = HAL_ResetEncoder;
            Base.HALEncoder.HAL_GetEncoderPeriod = HAL_GetEncoderPeriod;
            Base.HALEncoder.HAL_SetEncoderMaxPeriod = HAL_SetEncoderMaxPeriod;
            Base.HALEncoder.HAL_GetEncoderStopped = HAL_GetEncoderStopped;
            Base.HALEncoder.HAL_GetEncoderDirection = HAL_GetEncoderDirection;
            Base.HALEncoder.HAL_GetEncoderDistance = HAL_GetEncoderDistance;
            Base.HALEncoder.HAL_GetEncoderRate = HAL_GetEncoderRate;
            Base.HALEncoder.HAL_SetEncoderMinRate = HAL_SetEncoderMinRate;
            Base.HALEncoder.HAL_SetEncoderDistancePerPulse = HAL_SetEncoderDistancePerPulse;
            Base.HALEncoder.HAL_SetEncoderReverseDirection = HAL_SetEncoderReverseDirection;
            Base.HALEncoder.HAL_SetEncoderSamplesToAverage = HAL_SetEncoderSamplesToAverage;
            Base.HALEncoder.HAL_GetEncoderSamplesToAverage = HAL_GetEncoderSamplesToAverage;
            Base.HALEncoder.HAL_SetEncoderIndexSource = HAL_SetEncoderIndexSource;
            Base.HALEncoder.HAL_GetEncoderFPGAIndex = HAL_GetEncoderFPGAIndex;
            Base.HALEncoder.HAL_GetEncoderDecodingScaleFactor = HAL_GetEncoderDecodingScaleFactor;
            Base.HALEncoder.HAL_GetEncoderDistancePerPulse = HAL_GetEncoderDistancePerPulse;
            Base.HALEncoder.HAL_GetEncoderEncodingType = HAL_GetEncoderEncodingType;
        }

        public static int HAL_InitializeEncoder(int digitalSourceHandleA, HALAnalogTriggerType analogTriggerTypeA, int digitalSourceHandleB, HALAnalogTriggerType analogTriggerTypeB, bool reverseDirection, HALEncoderEncodingType encodingType, ref int status)
        {
            throw new NotImplementedException();
        }

        public static void HAL_FreeEncoder(int encoder_handle, ref int status)
        {
            throw new NotImplementedException();
        }

        public static int HAL_GetEncoder(int encoder_handle, ref int status)
        {
            throw new NotImplementedException();
        }

        public static int HAL_GetEncoderRaw(int encoder_handle, ref int status)
        {
            throw new NotImplementedException();
        }

        public static int HAL_GetEncoderEncodingScale(int encoder_handle, ref int status)
        {
            throw new NotImplementedException();
        }

        public static void HAL_ResetEncoder(int encoder_handle, ref int status)
        {
        }

        public static double HAL_GetEncoderPeriod(int encoder_handle, ref int status)
        {
            throw new NotImplementedException();
        }

        public static void HAL_SetEncoderMaxPeriod(int encoder_handle, double maxPeriod, ref int status)
        {
            throw new NotImplementedException();
        }

        public static bool HAL_GetEncoderStopped(int encoder_handle, ref int status)
        {
            throw new NotImplementedException();
        }

        public static bool HAL_GetEncoderDirection(int encoder_handle, ref int status)
        {
            throw new NotImplementedException();
        }

        public static double HAL_GetEncoderDistance(int encoder_handle, ref int status)
        {
            throw new NotImplementedException();
        }

        public static double HAL_GetEncoderRate(int encoder_handle, ref int status)
        {
            throw new NotImplementedException();
        }

        public static void HAL_SetEncoderMinRate(int encoder_handle, double minRate, ref int status)
        {
        }

        public static void HAL_SetEncoderDistancePerPulse(int encoder_handle, double distancePerPulse, ref int status)
        {
        }

        public static void HAL_SetEncoderReverseDirection(int encoder_handle, bool reverseDirection, ref int status)
        {
        }

        public static void HAL_SetEncoderSamplesToAverage(int encoder_handle, int samplesToAverage, ref int status)
        {
        }

        public static int HAL_GetEncoderSamplesToAverage(int encoder_handle, ref int status)
        {
            throw new NotImplementedException();
        }

        public static void HAL_SetEncoderIndexSource(int encoder_handle, int digitalSourceHandle, HALAnalogTriggerType analogTriggerType, HALEncoderIndexingType type, ref int status)
        {
            throw new NotImplementedException();
        }

        public static int HAL_GetEncoderFPGAIndex(int encoder_handle, ref int status)
        {
            throw new NotImplementedException();
        }

        public static double HAL_GetEncoderDecodingScaleFactor(int encoder_handle, ref int status)
        {
            throw new NotImplementedException();
        }

        public static double HAL_GetEncoderDistancePerPulse(int encoder_handle, ref int status)
        {
            throw new NotImplementedException();
        }

        public static HALEncoderEncodingType HAL_GetEncoderEncodingType(int encoder_handle, ref int status)
        {
            throw new NotImplementedException();
        }
    }
}

