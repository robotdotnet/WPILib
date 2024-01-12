using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal;
using WPIHal.Handles;

namespace Hal.Natives;

public static partial class HalDMA
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_AddDMAAnalogAccumulator")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void AddDMAAnalogAccumulator(HalDMAHandle handle, HalAnalogInputHandle aInHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_AddDMAAnalogInput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void AddDMAAnalogInput(HalDMAHandle handle, HalAnalogInputHandle aInHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_AddDMAAveragedAnalogInput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void AddDMAAveragedAnalogInput(HalDMAHandle handle, HalAnalogInputHandle aInHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_AddDMACounter")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void AddDMACounter(HalDMAHandle handle, HalCounterHandle counterHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_AddDMACounterPeriod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void AddDMACounterPeriod(HalDMAHandle handle, HalCounterHandle counterHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_AddDMADigitalSource")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void AddDMADigitalSource(HalDMAHandle handle, HalDigitalHandle digitalSourceHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_AddDMADigitalSource")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void AddDMADigitalSource(HalDMAHandle handle, HalAnalogTriggerHandle analogTriggerHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_AddDMADutyCycle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void AddDMADutyCycle(HalDMAHandle handle, HalDutyCycleHandle dutyCycleHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_AddDMAEncoder")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void AddDMAEncoder(HalDMAHandle handle, HalEncoderHandle encoderHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_AddDMAEncoderPeriod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void AddDMAEncoderPeriod(HalDMAHandle handle, HalEncoderHandle encoderHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_FreeDMA")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeDMA(HalDMAHandle handle);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetDMADirectPointer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void* GetDMADirectPointer(HalDMAHandle handle);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetDMASampleAnalogAccumulator")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetDMASampleAnalogAccumulator(in DMASample dmaSample, HalAnalogInputHandle aInHandle, out long count, out long value, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetDMASampleAnalogInputRaw")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetDMASampleAnalogInputRaw(in DMASample dmaSample, HalAnalogInputHandle aInHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetDMASampleAveragedAnalogInputRaw")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetDMASampleAveragedAnalogInputRaw(in DMASample dmaSample, HalAnalogInputHandle aInHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetDMASampleCounter")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetDMASampleCounter(in DMASample dmaSample, HalCounterHandle counterHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetDMASampleCounterPeriod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetDMASampleCounterPeriod(in DMASample dmaSample, HalCounterHandle counterHandle, out HalStatus status);

    //     [LibraryImport("wpiHal", EntryPoint = "HAL_GetDMASampleDigitalSource")]
    //     [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    //    public static partial  int GetDMASampleDigitalSource( in DMASample dmaSample, HALHANDLETODO dSourceHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetDMASampleDutyCycleOutputRaw")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetDMASampleDutyCycleOutputRaw(in DMASample dmaSample, HalDutyCycleHandle dutyCycleHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetDMASampleEncoderPeriodRaw")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetDMASampleEncoderPeriodRaw(in DMASample dmaSample, HalEncoderHandle encoderHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetDMASampleEncoderRaw")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetDMASampleEncoderRaw(in DMASample dmaSample, HalEncoderHandle encoderHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetDMASampleTime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ulong GetDMASampleTime(in DMASample dmaSample, out HalStatus status);

    //     [LibraryImport("wpiHal", EntryPoint = "Use HAL_GetDutyCycleOutputScaleFactor to scale this to a percentage. * * @param[in] dmaSample the sample to read from * @param[in] dutyCycleHandle the duty cycle handle * @param[out] status Error status variable. 0 on success. * @return raw duty cycle input data */ int HAL_GetDMASampleDutyCycleOutputRaw")]
    //     [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    //    public static partial  * Use GetDutyCycleOutputScaleFactor to scale this to a percentage. * * @param[in] dmaSample the sample to read from * @param[in] dutyCycleHandle the duty cycle handle * @param[out] status Error status variable. 0 on success. * @return raw duty cycle input data */ int GetDMASampleDutyCycleOutputRaw( in DMASample dmaSample, HalDutyCycleHandle dutyCycleHandle, out HalStatus status);

    //     [LibraryImport("wpiHal", EntryPoint = "Use HAL_GetDutyCycleOutputScaleFactor to scale this to a percentage. * * @param[in] dmaSample the sample to read from * @param[in] dutyCycleHandle the duty cycle handle * @param[out] status Error status variable. 0 on success. * @return raw duty cycle input data */ int HAL_GetDMASampleDutyCycleOutputRaw")]
    //     [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    //    public static partial  * Use GetDutyCycleOutputScaleFactor to scale this to a percentage. * * @param[in] dmaSample the sample to read from * @param[in] dutyCycleHandle the duty cycle handle * @param[out] status Error status variable. 0 on success. * @return raw duty cycle input data */ int GetDMASampleDutyCycleOutputRaw( in DMASample dmaSample, HalDutyCycleHandle dutyCycleHandle, out HalStatus status);

    //     [LibraryImport("wpiHal", EntryPoint = "This is in the same time domain as HAL_GetFPGATime")]
    //     [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    //    public static partial  * This is in the same time domain as GetFPGATime(). * * @param[in] dmaSample the sample to read from * @param[out] status Error status variable. 0 on success. * @return timestamp in microseconds since FPGA Initialization */ ulong GetDMASampleTime( in DMASample dmaSample, out HalStatus status);

    //     [LibraryImport("wpiHal", EntryPoint = "HAL_InitializeDMA")]
    //     [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    //    public static partial  HalDMAHandle InitializeDMA(out HalStatus status);

    //     [LibraryImport("wpiHal", EntryPoint = "See HAL_ReadDMA for full documentation. * * @param[in] dmaPointer     direct DMA pointer * @param[in] dmaSample      the sample object to place data into * @param[in] timeoutSeconds the time to wait for data to be queued before *                           timing out * @param[in] remainingOut   the number of samples remaining in the queue * @param[out] status        Error status variable. 0 on success. */ enum HAL_DMAReadStatus HAL_ReadDMADirect")]
    //     [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    //    public static partial  * See ReadDMA for full documentation. * * @param[in] dmaPointer     direct DMA pointer * @param[in] dmaSample      the sample object to place data into * @param[in] timeoutSeconds the time to wait for data to be queued before *                           timing out * @param[in] remainingOut   the number of samples remaining in the queue * @param[out] status        Error status variable. 0 on success. */ enum DMAReadStatus ReadDMADirect(void* dmaPointer, in DMASample dmaSample, double timeoutSeconds, int* remainingOut, out HalStatus status);

    //     [LibraryImport("wpiHal", EntryPoint = "HAL_DMAReadStatus HAL_ReadDMADirect")]
    //     [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    //    public static partial  enum DMAReadStatus ReadDMADirect(void* dmaPointer, in DMASample dmaSample, double timeoutSeconds, int* remainingOut, out HalStatus status);

    //     [LibraryImport("wpiHal", EntryPoint = "HAL_SetDMAExternalTrigger")]
    //     [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    //    public static partial  int SetDMAExternalTrigger(HalDMAHandle handle, HALHANDLETODO digitalSourceHandle, AnalogTriggerType analogTriggerType, int rising, int falling, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetDMAPause")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetDMAPause(HalDMAHandle handle, int pause, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_StartDMA")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void StartDMA(HalDMAHandle handle, int queueDepth, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_StopDMA")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void StopDMA(HalDMAHandle handle, out HalStatus status);


}
