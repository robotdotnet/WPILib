using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal;
using WPIHal.Handles;

namespace WPIHal.Natives;

public static partial class HalDutyCycle
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_FreeDutyCycle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeDutyCycle(HalDutyCycleHandle dutyCycleHandle);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetDutyCycleFPGAIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetDutyCycleFPGAIndexRefShim(HalDutyCycleHandle dutyCycleHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetDutyCycleFrequency")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetDutyCycleFrequencyRefShim(HalDutyCycleHandle dutyCycleHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_InitializeDutyCycle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial HalDutyCycleHandle InitializeDutyCycleRefShim(HalDigitalHandle digitalSourceHandle, AnalogTriggerType triggerType, ref HalStatus status);

    public static HalDutyCycleHandle InitializeDutyCycle(HalDigitalHandle digitalSourceHandle, out HalStatus status)
    {
        return InitializeDutyCycle(digitalSourceHandle, AnalogTriggerType.State, out status);
    }

    [LibraryImport("wpiHal", EntryPoint = "HAL_InitializeDutyCycle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial HalDutyCycleHandle InitializeDutyCycleRefShim(HalAnalogTriggerHandle analogTriggerHandle, AnalogTriggerType triggerType, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetDutyCycleSimDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetDutyCycleSimDevice(HalDutyCycleHandle handle, HalSimDeviceHandle device);


}
