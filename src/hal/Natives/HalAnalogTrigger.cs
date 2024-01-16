using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal.Handles;

namespace WPIHal.Natives;

public static partial class HalAnalogTrigger
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_CleanAnalogTrigger")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void CleanAnalogTriggerRefShim(HalAnalogTriggerHandle analogTriggerHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetAnalogTriggerFPGAIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetAnalogTriggerFPGAIndexRefShim(HalAnalogTriggerHandle analogTriggerHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetAnalogTriggerInWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetAnalogTriggerInWindowRefShim(HalAnalogTriggerHandle analogTriggerHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetAnalogTriggerOutput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetAnalogTriggerOutputRefShim(HalAnalogTriggerHandle analogTriggerHandle, AnalogTriggerType type, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetAnalogTriggerTriggerState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetAnalogTriggerTriggerStateRefShim(HalAnalogTriggerHandle analogTriggerHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_InitializeAnalogTrigger")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial HalAnalogTriggerHandle InitializeAnalogTriggerRefShim(HalAnalogInputHandle portHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_InitializeAnalogTriggerDutyCycle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial HalAnalogTriggerHandle InitializeAnalogTriggerDutyCycleRefShim(HalDutyCycleHandle dutyCycleHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetAnalogTriggerAveraged")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetAnalogTriggerAveragedRefShim(HalAnalogTriggerHandle analogTriggerHandle, int useAveragedValue, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetAnalogTriggerFiltered")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetAnalogTriggerFilteredRefShim(HalAnalogTriggerHandle analogTriggerHandle, int useFilteredValue, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetAnalogTriggerLimitsVoltage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetAnalogTriggerLimitsVoltageRefShim(HalAnalogTriggerHandle analogTriggerHandle, double lower, double upper, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetAnalogTriggerLimitsRaw")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetAnalogTriggerLimitsRawRefShim(HalAnalogTriggerHandle analogTriggerHandle, int lower, int upper, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetAnalogTriggerLimitsDutyCycle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetAnalogTriggerLimitsDutyCycleRefShim(HalAnalogTriggerHandle analogTriggerHandle, double lower, double upper, ref HalStatus status);


}
