using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal;
using WPIHal.Handles;

namespace Hal.Natives;

public static partial class HalDutyCycle
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_FreeDutyCycle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeDutyCycle(HalDutyCycleHandle dutyCycleHandle);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetDutyCycleFPGAIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetDutyCycleFPGAIndex(HalDutyCycleHandle dutyCycleHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetDutyCycleFrequency")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetDutyCycleFrequency(HalDutyCycleHandle dutyCycleHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_InitializeDutyCycle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalDutyCycleHandle InitializeDutyCycle(HALHANDLETODO digitalSourceHandle, AnalogTriggerType triggerType, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetDutyCycleSimDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetDutyCycleSimDevice(HalDutyCycleHandle handle, HalSimDeviceHandle device);


}
