using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal.Handles;
using WPIUtil;

namespace WPIHal.Natives;

public static partial class HalDIO
{
    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_AllocateDigitalPWM")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalDigitalPWMHandle AllocateDigitalPWM(out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_CheckDIOChannel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int CheckDIOChannel(int channel);

    [LibraryImport("wpiHal", EntryPoint = "HAL_FreeDIOPort")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeDIOPort(HalDigitalHandle dioPortHandle);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_FreeDigitalPWM")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeDigitalPWM(HalDigitalPWMHandle pwmGenerator, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetDIO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetDIO(HalDigitalHandle dioPortHandle, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetDIODirection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetDIODirection(HalDigitalHandle dioPortHandle, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetFilterPeriod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long GetFilterPeriod(int filterIndex, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetFilterSelect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetFilterSelect(HalDigitalHandle dioPortHandle, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_InitializeDIOPort", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalDigitalHandle InitializeDIOPort(HalPortHandle portHandle, int input, string allocationLocation, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_IsAnyPulsing")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IsAnyPulsing(out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_IsPulsing")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IsPulsing(HalDigitalHandle dioPortHandle, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_Pulse")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Pulse(HalDigitalHandle dioPortHandle, double pulseLengthSeconds, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_SetDIOSimDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetDIOSimDevice(HalDigitalHandle handle, HalSimDeviceHandle device);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_SetDIODirection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetDIODirection(HalDigitalHandle dioPortHandle, int input, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_SetDigitalPWMDutyCycle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetDigitalPWMDutyCycle(HalDigitalPWMHandle pwmGenerator, double dutyCycle, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_SetDigitalPWMOutputChannel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetDigitalPWMOutputChannel(HalDigitalPWMHandle pwmGenerator, int channel, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_SetDigitalPWMRate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetDigitalPWMRate(double rate, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_SetFilterPeriod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetFilterPeriod(int filterIndex, long value, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_SetFilterSelect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetFilterSelect(HalDigitalHandle dioPortHandle, int filterIndex, out HalStatus status);
}
