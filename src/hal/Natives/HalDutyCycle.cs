using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal.Handles;
using WPIUtil;

namespace WPIHal.Natives;

public static partial class HalDutyCycle
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_FreeDutyCycle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeDutyCycle(HalDutyCycleHandle dutyCycleHandle);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetDutyCycleFPGAIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetDutyCycleFPGAIndex(HalDutyCycleHandle dutyCycleHandle, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetDutyCycleFrequency")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetDutyCycleFrequency(HalDutyCycleHandle dutyCycleHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_InitializeDutyCycle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalDutyCycleHandle InitializeDutyCycle(HalDigitalHandle digitalSourceHandle, AnalogTriggerType triggerType, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    public static HalDutyCycleHandle InitializeDutyCycle(HalDigitalHandle digitalSourceHandle, out HalStatus status)
    {
        return InitializeDutyCycle(digitalSourceHandle, AnalogTriggerType.State, out status);
    }

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_InitializeDutyCycle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalDutyCycleHandle InitializeDutyCycle(HalAnalogTriggerHandle analogTriggerHandle, AnalogTriggerType triggerType, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_SetDutyCycleSimDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetDutyCycleSimDevice(HalDutyCycleHandle handle, HalSimDeviceHandle device);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetDutyCycleOutput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetDutyCycleOutput(HalDutyCycleHandle dutyCycleHandle, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetDutyCycleHighTime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetDutyCycleHighTime(HalDutyCycleHandle dutyCycleHandle, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetDutyCycleOutputScaleFactor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetDutyCycleOutputScaleFactor(HalDutyCycleHandle dutyCycleHandle, out HalStatus status);
}
