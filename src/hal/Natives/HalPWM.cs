using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal.Handles;
using WPIUtil;

namespace WPIHal.Natives;

public static partial class HalPWM
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_CheckPWMChannel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int CheckPWMChannel(int channel);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_FreePWMPort")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreePWMPort(HalDigitalHandle pwmPortHandle, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetPWMCycleStartTime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ulong GetPWMCycleStartTime(out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetPWMEliminateDeadband")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetPWMEliminateDeadband(HalDigitalHandle pwmPortHandle, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetPWMLoopTiming")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetPWMLoopTiming(out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetPWMPosition")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetPWMPosition(HalDigitalHandle pwmPortHandle, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetPWMSpeed")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetPWMSpeed(HalDigitalHandle pwmPortHandle, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_InitializePWMPort", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalDigitalHandle InitializePWMPort(HalPortHandle portHandle, string allocationLocation, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_LatchPWMZero")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void LatchPWMZero(HalDigitalHandle pwmPortHandle, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_SetPWMConfigMicroseconds")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetPWMConfigMicroseconds(HalDigitalHandle pwmPortHandle, int maxPwm, int deadbandMaxPwm, int centerPwm, int deadbandMinPwm, int minPwm, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_SetPWMDisabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetPWMDisabled(HalDigitalHandle pwmPortHandle, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_SetPWMEliminateDeadband")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetPWMEliminateDeadband(HalDigitalHandle pwmPortHandle, int eliminateDeadband, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_SetPWMPeriodScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetPWMPeriodScale(HalDigitalHandle pwmPortHandle, int squelchMask, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_SetPWMPosition")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetPWMPosition(HalDigitalHandle pwmPortHandle, double position, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_SetPWMSpeed")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetPWMSpeed(HalDigitalHandle pwmPortHandle, double speed, out HalStatus status);


}
