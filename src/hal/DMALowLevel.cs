
using Hal.Natives;
using WPIUtil.ILGeneration;

namespace Hal
{

    public static unsafe class DMALowLevel
    {
        internal static DMALowLevelNative lowLevel = null!;

        internal static void InitializeNatives(IFunctionPointerLoader loader)
        {
            lowLevel = new(loader);
        }

        public static void AddAnalogAccumulator(int handle, int aInHandle)
        {
            lowLevel.HAL_AddDMAAnalogAccumulator(handle, aInHandle);
        }

        public static void AddAnalogInput(int handle, int aInHandle)
        {
            lowLevel.HAL_AddDMAAnalogInput(handle, aInHandle);
        }

        public static void AddAveragedAnalogInput(int handle, int aInHandle)
        {
            lowLevel.HAL_AddDMAAveragedAnalogInput(handle, aInHandle);
        }

        public static void AddCounter(int handle, int counterHandle)
        {
            lowLevel.HAL_AddDMACounter(handle, counterHandle);
        }

        public static void AddCounterPeriod(int handle, int counterHandle)
        {
            lowLevel.HAL_AddDMACounterPeriod(handle, counterHandle);
        }

        public static void AddDigitalSource(int handle, int digitalSourceHandle)
        {
            lowLevel.HAL_AddDMADigitalSource(handle, digitalSourceHandle);
        }

        public static void AddDutyCycle(int handle, int dutyCycleHandle)
        {
            lowLevel.HAL_AddDMADutyCycle(handle, dutyCycleHandle);
        }

        public static void AddEncoder(int handle, int encoderHandle)
        {
            lowLevel.HAL_AddDMAEncoder(handle, encoderHandle);
        }

        public static void AddEncoderPeriod(int handle, int encoderHandle)
        {
            lowLevel.HAL_AddDMAEncoderPeriod(handle, encoderHandle);
        }

        public static void Free(int handle)
        {
            lowLevel.HAL_FreeDMA(handle);
        }

        public static void* GetDirectPointer(int handle)
        {
            return lowLevel.HAL_GetDMADirectPointer(handle);
        }

        public static void GetSampleAnalogAccumulator(DMASample* dmaSample, int aInHandle, long* count, long* value)
        {
            lowLevel.HAL_GetDMASampleAnalogAccumulator(dmaSample, aInHandle, count, value);
        }

        public static int GetSampleAnalogInputRaw(DMASample* dmaSample, int aInHandle)
        {
            return lowLevel.HAL_GetDMASampleAnalogInputRaw(dmaSample, aInHandle);
        }

        public static int GetSampleAveragedAnalogInputRaw(DMASample* dmaSample, int aInHandle)
        {
            return lowLevel.HAL_GetDMASampleAveragedAnalogInputRaw(dmaSample, aInHandle);
        }

        public static int GetSampleCounter(DMASample* dmaSample, int counterHandle)
        {
            return lowLevel.HAL_GetDMASampleCounter(dmaSample, counterHandle);
        }

        public static int GetSampleCounterPeriod(DMASample* dmaSample, int counterHandle)
        {
            return lowLevel.HAL_GetDMASampleCounterPeriod(dmaSample, counterHandle);
        }

        public static int GetSampleDigitalSource(DMASample* dmaSample, int dSourceHandle)
        {
            return lowLevel.HAL_GetDMASampleDigitalSource(dmaSample, dSourceHandle);
        }

        public static int GetSampleDutyCycleOutputRaw(DMASample* dmaSample, int dutyCycleHandle)
        {
            return lowLevel.HAL_GetDMASampleDutyCycleOutputRaw(dmaSample, dutyCycleHandle);
        }

        public static int GetSampleEncoderPeriodRaw(DMASample* dmaSample, int encoderHandle)
        {
            return lowLevel.HAL_GetDMASampleEncoderPeriodRaw(dmaSample, encoderHandle);
        }

        public static int GetSampleEncoderRaw(DMASample* dmaSample, int encoderHandle)
        {
            return lowLevel.HAL_GetDMASampleEncoderRaw(dmaSample, encoderHandle);
        }

        public static ulong GetSampleTime(DMASample* dmaSample)
        {
            return lowLevel.HAL_GetDMASampleTime(dmaSample);
        }

        public static int Initialize()
        {
            return lowLevel.HAL_InitializeDMA();
        }

        public static DMAReadStatus ReadDirect(void* dmaPointer, DMASample* dmaSample, int timeoutMs, int* remainingOut)
        {
            return lowLevel.HAL_ReadDMADirect(dmaPointer, dmaSample, timeoutMs, remainingOut);
        }

        public static void SetExternalTrigger(int handle, int digitalSourceHandle, AnalogTriggerType analogTriggerType, int rising, int falling)
        {
            lowLevel.HAL_SetDMAExternalTrigger(handle, digitalSourceHandle, analogTriggerType, rising, falling);
        }

        public static void SetPause(int handle, int pause)
        {
            lowLevel.HAL_SetDMAPause(handle, pause);
        }

        public static void SetRate(int handle, int cycles)
        {
            lowLevel.HAL_SetDMARate(handle, cycles);
        }

        public static void Start(int handle, int queueDepth)
        {
            lowLevel.HAL_StartDMA(handle, queueDepth);
        }

        public static void Stop(int handle)
        {
            lowLevel.HAL_StopDMA(handle);
        }

    }
}
