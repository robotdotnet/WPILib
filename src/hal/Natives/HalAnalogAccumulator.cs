using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal;
using WPIHal.Handles;

namespace WPIHal.Natives;

public static partial class HalAnalogAccumulator
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetAccumulatorCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial long GetAccumulatorCountRefShim(HalAnalogInputHandle analogPortHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetAccumulatorOutput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void GetAccumulatorOutputRefShim(HalAnalogInputHandle analogPortHandle, out long value, out long count, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetAccumulatorValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial long GetAccumulatorValueRefShim(HalAnalogInputHandle analogPortHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_InitAccumulator")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void InitAccumulatorRefShim(HalAnalogInputHandle analogPortHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_IsAccumulatorChannel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int IsAccumulatorChannelRefShim(HalAnalogInputHandle analogPortHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_ResetAccumulator")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void ResetAccumulatorRefShim(HalAnalogInputHandle analogPortHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetAccumulatorCenter")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetAccumulatorCenterRefShim(HalAnalogInputHandle analogPortHandle, int center, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetAccumulatorDeadband")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetAccumulatorDeadbandRefShim(HalAnalogInputHandle analogPortHandle, int deadband, ref HalStatus status);


}
