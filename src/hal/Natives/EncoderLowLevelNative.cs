﻿using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
using System;

namespace Hal.Natives
{
    public unsafe class EncoderLowLevelNative
    {
        public EncoderLowLevelNative(IFunctionPointerLoader loader)
        {
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader));
            }

            HAL_FreeEncoderFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, void >)loader.GetProcAddress("HAL_FreeEncoder");
            HAL_GetEncoderFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_GetEncoder");
            HAL_GetEncoderDecodingScaleFactorFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Double >)loader.GetProcAddress("HAL_GetEncoderDecodingScaleFactor");
            HAL_GetEncoderDirectionFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_GetEncoderDirection");
            HAL_GetEncoderDistanceFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Double >)loader.GetProcAddress("HAL_GetEncoderDistance");
            HAL_GetEncoderDistancePerPulseFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Double >)loader.GetProcAddress("HAL_GetEncoderDistancePerPulse");
            HAL_GetEncoderEncodingScaleFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_GetEncoderEncodingScale");
            HAL_GetEncoderEncodingTypeFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, Hal.EncoderEncodingType >)loader.GetProcAddress("HAL_GetEncoderEncodingType");
            HAL_GetEncoderFPGAIndexFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_GetEncoderFPGAIndex");
            HAL_GetEncoderPeriodFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Double >)loader.GetProcAddress("HAL_GetEncoderPeriod");
            HAL_GetEncoderRateFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Double >)loader.GetProcAddress("HAL_GetEncoderRate");
            HAL_GetEncoderRawFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_GetEncoderRaw");
            HAL_GetEncoderSamplesToAverageFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_GetEncoderSamplesToAverage");
            HAL_InitializeEncoderFunc = (delegate* unmanaged[Cdecl] < System.Int32, Hal.AnalogTriggerType, System.Int32, Hal.AnalogTriggerType, System.Int32, Hal.EncoderEncodingType, int *, System.Int32 >)loader.GetProcAddress("HAL_InitializeEncoder");
            HAL_ResetEncoderFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, void >)loader.GetProcAddress("HAL_ResetEncoder");
            HAL_SetEncoderIndexSourceFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, Hal.AnalogTriggerType, Hal.EncoderIndexingType, int *, void >)loader.GetProcAddress("HAL_SetEncoderIndexSource");
            HAL_SetEncoderMaxPeriodFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Double, int *, void >)loader.GetProcAddress("HAL_SetEncoderMaxPeriod");
            HAL_SetEncoderMinRateFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Double, int *, void >)loader.GetProcAddress("HAL_SetEncoderMinRate");
            HAL_SetEncoderReverseDirectionFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetEncoderReverseDirection");
            HAL_SetEncoderSamplesToAverageFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetEncoderSamplesToAverage");
            HAL_SetEncoderSimDeviceFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, void >)loader.GetProcAddress("HAL_SetEncoderSimDevice");
        }

        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_FreeEncoderFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_FreeEncoder(int encoderHandle)
        {
            int status = 0;
            HAL_FreeEncoderFunc(encoderHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetEncoderFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetEncoder(int encoderHandle)
        {
            int status = 0;
            var retVal = HAL_GetEncoderFunc(encoderHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, double> HAL_GetEncoderDecodingScaleFactorFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetEncoderDecodingScaleFactor(int encoderHandle)
        {
            int status = 0;
            var retVal = HAL_GetEncoderDecodingScaleFactorFunc(encoderHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetEncoderDirectionFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetEncoderDirection(int encoderHandle)
        {
            int status = 0;
            var retVal = HAL_GetEncoderDirectionFunc(encoderHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, double> HAL_GetEncoderDistanceFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetEncoderDistance(int encoderHandle)
        {
            int status = 0;
            var retVal = HAL_GetEncoderDistanceFunc(encoderHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, double> HAL_GetEncoderDistancePerPulseFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetEncoderDistancePerPulse(int encoderHandle)
        {
            int status = 0;
            var retVal = HAL_GetEncoderDistancePerPulseFunc(encoderHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetEncoderEncodingScaleFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetEncoderEncodingScale(int encoderHandle)
        {
            int status = 0;
            var retVal = HAL_GetEncoderEncodingScaleFunc(encoderHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, EncoderEncodingType> HAL_GetEncoderEncodingTypeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EncoderEncodingType HAL_GetEncoderEncodingType(int encoderHandle)
        {
            int status = 0;
            var retVal = HAL_GetEncoderEncodingTypeFunc(encoderHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetEncoderFPGAIndexFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetEncoderFPGAIndex(int encoderHandle)
        {
            int status = 0;
            var retVal = HAL_GetEncoderFPGAIndexFunc(encoderHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, double> HAL_GetEncoderPeriodFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetEncoderPeriod(int encoderHandle)
        {
            int status = 0;
            var retVal = HAL_GetEncoderPeriodFunc(encoderHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, double> HAL_GetEncoderRateFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetEncoderRate(int encoderHandle)
        {
            int status = 0;
            var retVal = HAL_GetEncoderRateFunc(encoderHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetEncoderRawFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetEncoderRaw(int encoderHandle)
        {
            int status = 0;
            var retVal = HAL_GetEncoderRawFunc(encoderHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetEncoderSamplesToAverageFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetEncoderSamplesToAverage(int encoderHandle)
        {
            int status = 0;
            var retVal = HAL_GetEncoderSamplesToAverageFunc(encoderHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, AnalogTriggerType, int, AnalogTriggerType, int, EncoderEncodingType, int*, int> HAL_InitializeEncoderFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_InitializeEncoder(int digitalSourceHandleA, AnalogTriggerType analogTriggerTypeA, int digitalSourceHandleB, AnalogTriggerType analogTriggerTypeB, int reverseDirection, EncoderEncodingType encodingType)
        {
            int status = 0;
            var retVal = HAL_InitializeEncoderFunc(digitalSourceHandleA, analogTriggerTypeA, digitalSourceHandleB, analogTriggerTypeB, reverseDirection, encodingType, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_ResetEncoderFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_ResetEncoder(int encoderHandle)
        {
            int status = 0;
            HAL_ResetEncoderFunc(encoderHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, AnalogTriggerType, EncoderIndexingType, int*, void> HAL_SetEncoderIndexSourceFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetEncoderIndexSource(int encoderHandle, int digitalSourceHandle, AnalogTriggerType analogTriggerType, EncoderIndexingType type)
        {
            int status = 0;
            HAL_SetEncoderIndexSourceFunc(encoderHandle, digitalSourceHandle, analogTriggerType, type, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, double, int*, void> HAL_SetEncoderMaxPeriodFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetEncoderMaxPeriod(int encoderHandle, double maxPeriod)
        {
            int status = 0;
            HAL_SetEncoderMaxPeriodFunc(encoderHandle, maxPeriod, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, double, int*, void> HAL_SetEncoderMinRateFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetEncoderMinRate(int encoderHandle, double minRate)
        {
            int status = 0;
            HAL_SetEncoderMinRateFunc(encoderHandle, minRate, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetEncoderReverseDirectionFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetEncoderReverseDirection(int encoderHandle, int reverseDirection)
        {
            int status = 0;
            HAL_SetEncoderReverseDirectionFunc(encoderHandle, reverseDirection, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetEncoderSamplesToAverageFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetEncoderSamplesToAverage(int encoderHandle, int samplesToAverage)
        {
            int status = 0;
            HAL_SetEncoderSamplesToAverageFunc(encoderHandle, samplesToAverage, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, void> HAL_SetEncoderSimDeviceFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetEncoderSimDevice(int handle, int device)
        {
            HAL_SetEncoderSimDeviceFunc(handle, device);
        }



    }
}
