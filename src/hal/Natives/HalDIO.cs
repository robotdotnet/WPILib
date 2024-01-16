using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal.Handles;

namespace WPIHal.Natives;

public static partial class HalDIO
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_AllocateDigitalPWM")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial HalDigitalPWMHandle AllocateDigitalPWMRefShim(ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_CheckDIOChannel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int CheckDIOChannel(int channel);

    [LibraryImport("wpiHal", EntryPoint = "HAL_FreeDIOPort")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeDIOPort(HalDigitalHandle dioPortHandle);

    [LibraryImport("wpiHal", EntryPoint = "HAL_FreeDigitalPWM")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void FreeDigitalPWMRefShim(HalDigitalPWMHandle pwmGenerator, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetDIO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetDIORefShim(HalDigitalHandle dioPortHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetDIODirection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetDIODirectionRefShim(HalDigitalHandle dioPortHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetFilterPeriod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial long GetFilterPeriodRefShim(int filterIndex, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetFilterSelect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetFilterSelectRefShim(HalDigitalHandle dioPortHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_InitializeDIOPort", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial HalDigitalHandle InitializeDIOPortRefShim(HalPortHandle portHandle, int input, string allocationLocation, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_IsAnyPulsing")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int IsAnyPulsingRefShim(ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_IsPulsing")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int IsPulsingRefShim(HalDigitalHandle dioPortHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_Pulse")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void PulseRefShim(HalDigitalHandle dioPortHandle, double pulseLengthSeconds, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetDIOSimDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetDIOSimDevice(HalDigitalHandle handle, HalSimDeviceHandle device);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetDIODirection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetDIODirectionRefShim(HalDigitalHandle dioPortHandle, int input, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetDigitalPWMDutyCycle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetDigitalPWMDutyCycleRefShim(HalDigitalPWMHandle pwmGenerator, double dutyCycle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetDigitalPWMOutputChannel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetDigitalPWMOutputChannelRefShim(HalDigitalPWMHandle pwmGenerator, int channel, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetDigitalPWMRate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetDigitalPWMRateRefShim(double rate, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetFilterPeriod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetFilterPeriodRefShim(int filterIndex, long value, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetFilterSelect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetFilterSelectRefShim(HalDigitalHandle dioPortHandle, int filterIndex, ref HalStatus status);


}
