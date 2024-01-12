using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal;
using WPIHal.Handles;

namespace WPIHal.Natives;

public static partial class HalAnalogInput
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_CheckAnalogInputChannel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int CheckAnalogInputChannel(int channel);

    [LibraryImport("wpiHal", EntryPoint = "HAL_CheckAnalogModule")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int CheckAnalogModule(int module);

    [LibraryImport("wpiHal", EntryPoint = "HAL_FreeAnalogInputPort")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeAnalogInputPort(HalAnalogInputHandle analogPortHandle);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetAnalogAverageBits")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetAnalogAverageBitsRefShim(HalAnalogInputHandle analogPortHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetAnalogAverageValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetAnalogAverageValueRefShim(HalAnalogInputHandle analogPortHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetAnalogAverageVoltage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial double GetAnalogAverageVoltageRefShim(HalAnalogInputHandle analogPortHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetAnalogLSBWeight")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetAnalogLSBWeightRefShim(HalAnalogInputHandle analogPortHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetAnalogOffset")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetAnalogOffsetRefShim(HalAnalogInputHandle analogPortHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetAnalogOversampleBits")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetAnalogOversampleBitsRefShim(HalAnalogInputHandle analogPortHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetAnalogSampleRate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial double GetAnalogSampleRateRefShim(ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetAnalogValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetAnalogValueRefShim(HalAnalogInputHandle analogPortHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetAnalogValueToVolts")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial double GetAnalogValueToVoltsRefShim(HalAnalogInputHandle analogPortHandle, int rawValue, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetAnalogVoltage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial double GetAnalogVoltageRefShim(HalAnalogInputHandle analogPortHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetAnalogVoltsToValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetAnalogVoltsToValueRefShim(HalAnalogInputHandle analogPortHandle, double voltage, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_InitializeAnalogInputPort", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial HalAnalogInputHandle InitializeAnalogInputPortRefShim(HalPortHandle portHandle, string allocationLocation, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetAnalogAverageBits")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetAnalogAverageBitsRefShim(HalAnalogInputHandle analogPortHandle, int bits, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetAnalogInputSimDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAnalogInputSimDevice(HalAnalogInputHandle handle, HalSimDeviceHandle device);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetAnalogOversampleBits")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetAnalogOversampleBitsRefShim(HalAnalogInputHandle analogPortHandle, int bits, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetAnalogSampleRate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetAnalogSampleRateRefShim(double samplesPerSecond, ref HalStatus status);


}
