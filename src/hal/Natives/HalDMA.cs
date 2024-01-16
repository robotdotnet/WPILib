using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal.Handles;

namespace WPIHal.Natives;

public static partial class HalDMA
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_AddDMAAnalogAccumulator")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void AddDMAAnalogAccumulatorRefShim(HalDMAHandle handle, HalAnalogInputHandle aInHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_AddDMAAnalogInput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void AddDMAAnalogInputRefShim(HalDMAHandle handle, HalAnalogInputHandle aInHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_AddDMAAveragedAnalogInput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void AddDMAAveragedAnalogInputRefShim(HalDMAHandle handle, HalAnalogInputHandle aInHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_AddDMACounter")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void AddDMACounterRefShim(HalDMAHandle handle, HalCounterHandle counterHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_AddDMACounterPeriod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void AddDMACounterPeriodRefShim(HalDMAHandle handle, HalCounterHandle counterHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_AddDMADigitalSource")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void AddDMADigitalSourceRefShim(HalDMAHandle handle, HalDigitalHandle digitalSourceHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_AddDMADigitalSource")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void AddDMADigitalSourceRefShim(HalDMAHandle handle, HalAnalogTriggerHandle analogTriggerHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_AddDMADutyCycle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void AddDMADutyCycleRefShim(HalDMAHandle handle, HalDutyCycleHandle dutyCycleHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_AddDMAEncoder")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void AddDMAEncoderRefShim(HalDMAHandle handle, HalEncoderHandle encoderHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_AddDMAEncoderPeriod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void AddDMAEncoderPeriodRefShim(HalDMAHandle handle, HalEncoderHandle encoderHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_FreeDMA")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeDMA(HalDMAHandle handle);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetDMADirectPointer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void* GetDMADirectPointer(HalDMAHandle handle);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetDMASampleAnalogAccumulator")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void GetDMASampleAnalogAccumulatorRefShim(in DMASample dmaSample, HalAnalogInputHandle aInHandle, out long count, out long value, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetDMASampleAnalogInputRaw")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetDMASampleAnalogInputRawRefShim(in DMASample dmaSample, HalAnalogInputHandle aInHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetDMASampleAveragedAnalogInputRaw")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetDMASampleAveragedAnalogInputRawRefShim(in DMASample dmaSample, HalAnalogInputHandle aInHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetDMASampleCounter")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetDMASampleCounterRefShim(in DMASample dmaSample, HalCounterHandle counterHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetDMASampleCounterPeriod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetDMASampleCounterPeriodRefShim(in DMASample dmaSample, HalCounterHandle counterHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetDMASampleDigitalSource")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetDMASampleDigitalSourceRefShim(in DMASample dmaSample, HalDigitalHandle dSourceHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetDMASampleDigitalSource")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetDMASampleDigitalSourceRefShim(in DMASample dmaSample, HalAnalogTriggerHandle dSourceHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetDMASampleDutyCycleOutputRaw")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetDMASampleDutyCycleOutputRawRefShim(in DMASample dmaSample, HalDutyCycleHandle dutyCycleHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetDMASampleEncoderPeriodRaw")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetDMASampleEncoderPeriodRawRefShim(in DMASample dmaSample, HalEncoderHandle encoderHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetDMASampleEncoderRaw")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetDMASampleEncoderRawRefShim(in DMASample dmaSample, HalEncoderHandle encoderHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetDMASampleTime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial ulong GetDMASampleTimeRefShim(in DMASample dmaSample, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_InitializeDMA")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial HalDMAHandle InitializeDMARefShim(ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_ReadDMADirect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static unsafe partial DMAReadStatus ReadDMADirectRefShim(void* dmaPointer, out DMASample dmaSample, double timeoutSeconds, out int remainingOut, ref HalStatus status);


    [LibraryImport("wpiHal", EntryPoint = "HAL_ReadDMA")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static unsafe partial DMAReadStatus ReadDMARefShim(HalDMAHandle handle, out DMASample dmaSample, double timeoutSeconds, out int remainingOut, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetDMAExternalTrigger")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int SetDMAExternalTriggerRefShim(HalDMAHandle handle, HalDigitalHandle digitalSourceHandle, AnalogTriggerType analogTriggerType, int rising, int falling, ref HalStatus status);

    public static int SetDMAExternalTrigger(HalDMAHandle handle, HalDigitalHandle digitalSourceHandle, int rising, int falling, out HalStatus status)
    {
        return SetDMAExternalTrigger(handle, digitalSourceHandle, AnalogTriggerType.State, rising, falling, out status);
    }

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetDMAExternalTrigger")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int SetDMAExternalTriggerRefShim(HalDMAHandle handle, HalAnalogTriggerHandle digitalSourceHandle, AnalogTriggerType analogTriggerType, int rising, int falling, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetDMATimedTrigger")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetDMATimedTriggerRefShim(HalDMAHandle handle, double periodSeconds, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetDMAPause")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetDMAPauseRefShim(HalDMAHandle handle, int pause, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_StartDMA")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void StartDMARefShim(HalDMAHandle handle, int queueDepth, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_StopDMA")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void StopDMARefShim(HalDMAHandle handle, ref HalStatus status);


}
