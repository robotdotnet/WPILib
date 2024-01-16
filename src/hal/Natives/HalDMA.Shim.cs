using WPIHal.Handles;

namespace WPIHal.Natives;

public static unsafe partial class HalDMA
{
    public static void AddDMAAnalogAccumulator(HalDMAHandle handle, HalAnalogInputHandle aInHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        AddDMAAnalogAccumulatorRefShim(handle, aInHandle, ref status);
    }
    public static void AddDMAAnalogInput(HalDMAHandle handle, HalAnalogInputHandle aInHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        AddDMAAnalogInputRefShim(handle, aInHandle, ref status);
    }
    public static void AddDMAAveragedAnalogInput(HalDMAHandle handle, HalAnalogInputHandle aInHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        AddDMAAveragedAnalogInputRefShim(handle, aInHandle, ref status);
    }
    public static void AddDMACounter(HalDMAHandle handle, HalCounterHandle counterHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        AddDMACounterRefShim(handle, counterHandle, ref status);
    }
    public static void AddDMACounterPeriod(HalDMAHandle handle, HalCounterHandle counterHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        AddDMACounterPeriodRefShim(handle, counterHandle, ref status);
    }
    public static void AddDMADigitalSource(HalDMAHandle handle, HalDigitalHandle digitalSourceHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        AddDMADigitalSourceRefShim(handle, digitalSourceHandle, ref status);
    }
    public static void AddDMADigitalSource(HalDMAHandle handle, HalAnalogTriggerHandle analogTriggerHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        AddDMADigitalSourceRefShim(handle, analogTriggerHandle, ref status);
    }
    public static void AddDMADutyCycle(HalDMAHandle handle, HalDutyCycleHandle dutyCycleHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        AddDMADutyCycleRefShim(handle, dutyCycleHandle, ref status);
    }
    public static void AddDMAEncoder(HalDMAHandle handle, HalEncoderHandle encoderHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        AddDMAEncoderRefShim(handle, encoderHandle, ref status);
    }
    public static void AddDMAEncoderPeriod(HalDMAHandle handle, HalEncoderHandle encoderHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        AddDMAEncoderPeriodRefShim(handle, encoderHandle, ref status);
    }
    public static void GetDMASampleAnalogAccumulator(in DMASample dmaSample, HalAnalogInputHandle aInHandle, out long count, out long value, out HalStatus status)
    {
        status = HalStatus.Ok;
        GetDMASampleAnalogAccumulatorRefShim(dmaSample, aInHandle, out count, out value, ref status);
    }
    public static int GetDMASampleAnalogInputRaw(in DMASample dmaSample, HalAnalogInputHandle aInHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetDMASampleAnalogInputRawRefShim(dmaSample, aInHandle, ref status);
    }
    public static int GetDMASampleAveragedAnalogInputRaw(in DMASample dmaSample, HalAnalogInputHandle aInHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetDMASampleAveragedAnalogInputRawRefShim(dmaSample, aInHandle, ref status);
    }
    public static int GetDMASampleCounter(in DMASample dmaSample, HalCounterHandle counterHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetDMASampleCounterRefShim(dmaSample, counterHandle, ref status);
    }
    public static int GetDMASampleCounterPeriod(in DMASample dmaSample, HalCounterHandle counterHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetDMASampleCounterPeriodRefShim(dmaSample, counterHandle, ref status);
    }
    public static int GetDMASampleDigitalSource(in DMASample dmaSample, HalDigitalHandle dSourceHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetDMASampleDigitalSourceRefShim(dmaSample, dSourceHandle, ref status);
    }
    public static int GetDMASampleDigitalSource(in DMASample dmaSample, HalAnalogTriggerHandle dSourceHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetDMASampleDigitalSourceRefShim(dmaSample, dSourceHandle, ref status);
    }
    public static int GetDMASampleDutyCycleOutputRaw(in DMASample dmaSample, HalDutyCycleHandle dutyCycleHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetDMASampleDutyCycleOutputRawRefShim(dmaSample, dutyCycleHandle, ref status);
    }
    public static int GetDMASampleEncoderPeriodRaw(in DMASample dmaSample, HalEncoderHandle encoderHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetDMASampleEncoderPeriodRawRefShim(dmaSample, encoderHandle, ref status);
    }
    public static int GetDMASampleEncoderRaw(in DMASample dmaSample, HalEncoderHandle encoderHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetDMASampleEncoderRawRefShim(dmaSample, encoderHandle, ref status);
    }
    public static ulong GetDMASampleTime(in DMASample dmaSample, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetDMASampleTimeRefShim(dmaSample, ref status);
    }
    public static HalDMAHandle InitializeDMA(out HalStatus status)
    {
        status = HalStatus.Ok;
        return InitializeDMARefShim(ref status);
    }
    public static unsafe DMAReadStatus ReadDMADirect(void* dmaPointer, out DMASample dmaSample, double timeoutSeconds, out int remainingOut, out HalStatus status)
    {
        status = HalStatus.Ok;
        return ReadDMADirectRefShim(dmaPointer, out dmaSample, timeoutSeconds, out remainingOut, ref status);
    }
    public static unsafe DMAReadStatus ReadDMA(HalDMAHandle handle, out DMASample dmaSample, double timeoutSeconds, out int remainingOut, out HalStatus status)
    {
        status = HalStatus.Ok;
        return ReadDMARefShim(handle, out dmaSample, timeoutSeconds, out remainingOut, ref status);
    }
    internal static int SetDMAExternalTrigger(HalDMAHandle handle, HalDigitalHandle digitalSourceHandle, AnalogTriggerType analogTriggerType, int rising, int falling, out HalStatus status)
    {
        status = HalStatus.Ok;
        return SetDMAExternalTriggerRefShim(handle, digitalSourceHandle, analogTriggerType, rising, falling, ref status);
    }
    public static int SetDMAExternalTrigger(HalDMAHandle handle, HalAnalogTriggerHandle digitalSourceHandle, AnalogTriggerType analogTriggerType, int rising, int falling, out HalStatus status)
    {
        status = HalStatus.Ok;
        return SetDMAExternalTriggerRefShim(handle, digitalSourceHandle, analogTriggerType, rising, falling, ref status);
    }
    public static void SetDMATimedTrigger(HalDMAHandle handle, double periodSeconds, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetDMATimedTriggerRefShim(handle, periodSeconds, ref status);
    }
    public static void SetDMAPause(HalDMAHandle handle, int pause, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetDMAPauseRefShim(handle, pause, ref status);
    }
    public static void StartDMA(HalDMAHandle handle, int queueDepth, out HalStatus status)
    {
        status = HalStatus.Ok;
        StartDMARefShim(handle, queueDepth, ref status);
    }
    public static void StopDMA(HalDMAHandle handle, out HalStatus status)
    {
        status = HalStatus.Ok;
        StopDMARefShim(handle, ref status);
    }
}
