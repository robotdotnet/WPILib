using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
using System;

namespace Hal.Natives
{
    public unsafe class DMALowLevelNative
    {
        public DMALowLevelNative(IFunctionPointerLoader loader)
        {
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader));
            }

            HAL_AddDMAAnalogAccumulatorFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_AddDMAAnalogAccumulator");
            HAL_AddDMAAnalogInputFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_AddDMAAnalogInput");
            HAL_AddDMAAveragedAnalogInputFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_AddDMAAveragedAnalogInput");
            HAL_AddDMACounterFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_AddDMACounter");
            HAL_AddDMACounterPeriodFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_AddDMACounterPeriod");
            HAL_AddDMADigitalSourceFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_AddDMADigitalSource");
            HAL_AddDMADutyCycleFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_AddDMADutyCycle");
            HAL_AddDMAEncoderFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_AddDMAEncoder");
            HAL_AddDMAEncoderPeriodFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_AddDMAEncoderPeriod");
            HAL_FreeDMAFunc = (delegate* unmanaged[Cdecl] < System.Int32, void >)loader.GetProcAddress("HAL_FreeDMA");
            HAL_GetDMADirectPointerFunc = (delegate* unmanaged[Cdecl] < System.Int32, void *>)loader.GetProcAddress("HAL_GetDMADirectPointer");
            HAL_GetDMASampleAnalogAccumulatorFunc = (delegate* unmanaged[Cdecl] < Hal.DMASample *, System.Int32, System.Int64 *, System.Int64 *, int *, void >)loader.GetProcAddress("HAL_GetDMASampleAnalogAccumulator");
            HAL_GetDMASampleAnalogInputRawFunc = (delegate* unmanaged[Cdecl] < Hal.DMASample *, System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_GetDMASampleAnalogInputRaw");
            HAL_GetDMASampleAveragedAnalogInputRawFunc = (delegate* unmanaged[Cdecl] < Hal.DMASample *, System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_GetDMASampleAveragedAnalogInputRaw");
            HAL_GetDMASampleCounterFunc = (delegate* unmanaged[Cdecl] < Hal.DMASample *, System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_GetDMASampleCounter");
            HAL_GetDMASampleCounterPeriodFunc = (delegate* unmanaged[Cdecl] < Hal.DMASample *, System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_GetDMASampleCounterPeriod");
            HAL_GetDMASampleDigitalSourceFunc = (delegate* unmanaged[Cdecl] < Hal.DMASample *, System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_GetDMASampleDigitalSource");
            HAL_GetDMASampleDutyCycleOutputRawFunc = (delegate* unmanaged[Cdecl] < Hal.DMASample *, System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_GetDMASampleDutyCycleOutputRaw");
            HAL_GetDMASampleEncoderPeriodRawFunc = (delegate* unmanaged[Cdecl] < Hal.DMASample *, System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_GetDMASampleEncoderPeriodRaw");
            HAL_GetDMASampleEncoderRawFunc = (delegate* unmanaged[Cdecl] < Hal.DMASample *, System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_GetDMASampleEncoderRaw");
            HAL_GetDMASampleTimeFunc = (delegate* unmanaged[Cdecl] < Hal.DMASample *, int *, System.UInt64 >)loader.GetProcAddress("HAL_GetDMASampleTime");
            HAL_InitializeDMAFunc = (delegate* unmanaged[Cdecl] < int *, System.Int32 >)loader.GetProcAddress("HAL_InitializeDMA");
            HAL_ReadDMADirectFunc = (delegate* unmanaged[Cdecl] < void *, Hal.DMASample *, System.Int32, System.Int32 *, int *, Hal.DMAReadStatus >)loader.GetProcAddress("HAL_ReadDMADirect");
            HAL_SetDMAExternalTriggerFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, Hal.AnalogTriggerType, System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetDMAExternalTrigger");
            HAL_SetDMAPauseFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetDMAPause");
            HAL_SetDMARateFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetDMARate");
            HAL_StartDMAFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_StartDMA");
            HAL_StopDMAFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, void >)loader.GetProcAddress("HAL_StopDMA");
        }

        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_AddDMAAnalogAccumulatorFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_AddDMAAnalogAccumulator(int handle, int aInHandle)
        {
            int status = 0;
            HAL_AddDMAAnalogAccumulatorFunc(handle, aInHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_AddDMAAnalogInputFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_AddDMAAnalogInput(int handle, int aInHandle)
        {
            int status = 0;
            HAL_AddDMAAnalogInputFunc(handle, aInHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_AddDMAAveragedAnalogInputFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_AddDMAAveragedAnalogInput(int handle, int aInHandle)
        {
            int status = 0;
            HAL_AddDMAAveragedAnalogInputFunc(handle, aInHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_AddDMACounterFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_AddDMACounter(int handle, int counterHandle)
        {
            int status = 0;
            HAL_AddDMACounterFunc(handle, counterHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_AddDMACounterPeriodFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_AddDMACounterPeriod(int handle, int counterHandle)
        {
            int status = 0;
            HAL_AddDMACounterPeriodFunc(handle, counterHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_AddDMADigitalSourceFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_AddDMADigitalSource(int handle, int digitalSourceHandle)
        {
            int status = 0;
            HAL_AddDMADigitalSourceFunc(handle, digitalSourceHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_AddDMADutyCycleFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_AddDMADutyCycle(int handle, int dutyCycleHandle)
        {
            int status = 0;
            HAL_AddDMADutyCycleFunc(handle, dutyCycleHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_AddDMAEncoderFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_AddDMAEncoder(int handle, int encoderHandle)
        {
            int status = 0;
            HAL_AddDMAEncoderFunc(handle, encoderHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_AddDMAEncoderPeriodFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_AddDMAEncoderPeriod(int handle, int encoderHandle)
        {
            int status = 0;
            HAL_AddDMAEncoderPeriodFunc(handle, encoderHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, void> HAL_FreeDMAFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_FreeDMA(int handle)
        {
            HAL_FreeDMAFunc(handle);
        }



        private readonly delegate* unmanaged[Cdecl]<int, void*> HAL_GetDMADirectPointerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void* HAL_GetDMADirectPointer(int handle)
        {
            return HAL_GetDMADirectPointerFunc(handle);
        }



        private readonly delegate* unmanaged[Cdecl]<DMASample*, int, long*, long*, int*, void> HAL_GetDMASampleAnalogAccumulatorFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_GetDMASampleAnalogAccumulator(DMASample* dmaSample, int aInHandle, long* count, long* value)
        {
            int status = 0;
            HAL_GetDMASampleAnalogAccumulatorFunc(dmaSample, aInHandle, count, value, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<DMASample*, int, int*, int> HAL_GetDMASampleAnalogInputRawFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetDMASampleAnalogInputRaw(DMASample* dmaSample, int aInHandle)
        {
            int status = 0;
            var retVal = HAL_GetDMASampleAnalogInputRawFunc(dmaSample, aInHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<DMASample*, int, int*, int> HAL_GetDMASampleAveragedAnalogInputRawFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetDMASampleAveragedAnalogInputRaw(DMASample* dmaSample, int aInHandle)
        {
            int status = 0;
            var retVal = HAL_GetDMASampleAveragedAnalogInputRawFunc(dmaSample, aInHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<DMASample*, int, int*, int> HAL_GetDMASampleCounterFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetDMASampleCounter(DMASample* dmaSample, int counterHandle)
        {
            int status = 0;
            var retVal = HAL_GetDMASampleCounterFunc(dmaSample, counterHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<DMASample*, int, int*, int> HAL_GetDMASampleCounterPeriodFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetDMASampleCounterPeriod(DMASample* dmaSample, int counterHandle)
        {
            int status = 0;
            var retVal = HAL_GetDMASampleCounterPeriodFunc(dmaSample, counterHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<DMASample*, int, int*, int> HAL_GetDMASampleDigitalSourceFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetDMASampleDigitalSource(DMASample* dmaSample, int dSourceHandle)
        {
            int status = 0;
            var retVal = HAL_GetDMASampleDigitalSourceFunc(dmaSample, dSourceHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<DMASample*, int, int*, int> HAL_GetDMASampleDutyCycleOutputRawFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetDMASampleDutyCycleOutputRaw(DMASample* dmaSample, int dutyCycleHandle)
        {
            int status = 0;
            var retVal = HAL_GetDMASampleDutyCycleOutputRawFunc(dmaSample, dutyCycleHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<DMASample*, int, int*, int> HAL_GetDMASampleEncoderPeriodRawFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetDMASampleEncoderPeriodRaw(DMASample* dmaSample, int encoderHandle)
        {
            int status = 0;
            var retVal = HAL_GetDMASampleEncoderPeriodRawFunc(dmaSample, encoderHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<DMASample*, int, int*, int> HAL_GetDMASampleEncoderRawFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetDMASampleEncoderRaw(DMASample* dmaSample, int encoderHandle)
        {
            int status = 0;
            var retVal = HAL_GetDMASampleEncoderRawFunc(dmaSample, encoderHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<DMASample*, int*, ulong> HAL_GetDMASampleTimeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong HAL_GetDMASampleTime(DMASample* dmaSample)
        {
            int status = 0;
            var retVal = HAL_GetDMASampleTimeFunc(dmaSample, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int*, int> HAL_InitializeDMAFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_InitializeDMA()
        {
            int status = 0;
            var retVal = HAL_InitializeDMAFunc(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<void*, DMASample*, int, int*, int*, DMAReadStatus> HAL_ReadDMADirectFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DMAReadStatus HAL_ReadDMADirect(void* dmaPointer, DMASample* dmaSample, int timeoutMs, int* remainingOut)
        {
            int status = 0;
            var retVal = HAL_ReadDMADirectFunc(dmaPointer, dmaSample, timeoutMs, remainingOut, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, AnalogTriggerType, int, int, int*, void> HAL_SetDMAExternalTriggerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetDMAExternalTrigger(int handle, int digitalSourceHandle, AnalogTriggerType analogTriggerType, int rising, int falling)
        {
            int status = 0;
            HAL_SetDMAExternalTriggerFunc(handle, digitalSourceHandle, analogTriggerType, rising, falling, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetDMAPauseFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetDMAPause(int handle, int pause)
        {
            int status = 0;
            HAL_SetDMAPauseFunc(handle, pause, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetDMARateFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetDMARate(int handle, int cycles)
        {
            int status = 0;
            HAL_SetDMARateFunc(handle, cycles, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_StartDMAFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_StartDMA(int handle, int queueDepth)
        {
            int status = 0;
            HAL_StartDMAFunc(handle, queueDepth, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_StopDMAFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_StopDMA(int handle)
        {
            int status = 0;
            HAL_StopDMAFunc(handle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



    }
}
