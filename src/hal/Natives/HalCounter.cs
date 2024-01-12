using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal;
using WPIHal.Handles;

namespace WPIHal.Natives;

public static partial class HalCounter
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_ClearCounterDownSource")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void ClearCounterDownSourceRefShim(HalCounterHandle counterHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_ClearCounterUpSource")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void ClearCounterUpSourceRefShim(HalCounterHandle counterHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_FreeCounter")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void FreeCounterRefShim(HalCounterHandle counterHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetCounterSamplesToAverage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetCounterSamplesToAverageRefShim(HalCounterHandle counterHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetCounterDirection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetCounterDirectionRefShim(HalCounterHandle counterHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetCounterPeriod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial double GetCounterPeriodRefShim(HalCounterHandle counterHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetCounterStopped")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetCounterStoppedRefShim(HalCounterHandle counterHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_InitializeCounter")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial HalCounterHandle InitializeCounterRefShim(CounterMode mode, out int index, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_ResetCounter")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void ResetCounterRefShim(HalCounterHandle counterHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetCounterAverageSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetCounterAverageSizeRefShim(HalCounterHandle counterHandle, int size, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetCounterDownSource")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetCounterDownSourceRefShim(HalCounterHandle counterHandle, HalDigitalHandle digitalSourceHandle, AnalogTriggerType analogTriggerType, ref HalStatus status);

    public static void SetCounterDownSource(HalCounterHandle counterHandle, HalDigitalHandle digitalSourceHandle, out HalStatus status)
    {
        SetCounterDownSource(counterHandle, digitalSourceHandle, AnalogTriggerType.InWindow, out status);
    }

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetCounterDownSource")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetCounterDownSourceRefShim(HalCounterHandle counterHandle, HalAnalogTriggerHandle analogTriggerSource, AnalogTriggerType analogTriggerType, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetCounterDownSourceEdge")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetCounterDownSourceEdgeRefShim(HalCounterHandle counterHandle, int risingEdge, int fallingEdge, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetCounterExternalDirectionMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetCounterExternalDirectionModeRefShim(HalCounterHandle counterHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetCounterMaxPeriod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetCounterMaxPeriodRefShim(HalCounterHandle counterHandle, double maxPeriod, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetCounterPulseLengthMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetCounterPulseLengthModeRefShim(HalCounterHandle counterHandle, double threshold, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetCounterReverseDirection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetCounterReverseDirectionRefShim(HalCounterHandle counterHandle, int reverseDirection, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetCounterSamplesToAverage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetCounterSamplesToAverageRefShim(HalCounterHandle counterHandle, int samplesToAverage, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetCounterSemiPeriodMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetCounterSemiPeriodModeRefShim(HalCounterHandle counterHandle, int highSemiPeriod, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetCounterUpDownMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetCounterUpDownModeRefShim(HalCounterHandle counterHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetCounterUpSource")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetCounterUpSourceRefShim(HalCounterHandle counterHandle, HalDigitalHandle digitalSourceHandle, AnalogTriggerType analogTriggerType, ref HalStatus status);

    public static void SetCounterUpSource(HalCounterHandle counterHandle, HalDigitalHandle digitalSourceHandle, out HalStatus status)
    {
        SetCounterUpSource(counterHandle, digitalSourceHandle, AnalogTriggerType.InWindow, out status);
    }

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetCounterUpSource")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetCounterUpSourceRefShim(HalCounterHandle counterHandle, HalAnalogTriggerHandle analogTriggerSource, AnalogTriggerType analogTriggerType, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetCounterUpSourceEdge")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetCounterUpSourceEdgeRefShim(HalCounterHandle counterHandle, int risingEdge, int fallingEdge, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetCounterUpdateWhenEmpty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetCounterUpdateWhenEmptyRefShim(HalCounterHandle counterHandle, int enabled, ref HalStatus status);


}
