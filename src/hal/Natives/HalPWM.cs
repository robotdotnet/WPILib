using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal.Handles;

namespace WPIHal.Natives;

public static partial class HalPWM
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_CheckPWMChannel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int CheckPWMChannel(int channel);

    [LibraryImport("wpiHal", EntryPoint = "HAL_FreePWMPort")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void FreePWMPortRefShim(HalDigitalHandle pwmPortHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetPWMCycleStartTime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial ulong GetPWMCycleStartTimeRefShim(ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetPWMEliminateDeadband")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetPWMEliminateDeadbandRefShim(HalDigitalHandle pwmPortHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetPWMLoopTiming")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetPWMLoopTimingRefShim(ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetPWMPosition")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial double GetPWMPositionRefShim(HalDigitalHandle pwmPortHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetPWMSpeed")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial double GetPWMSpeedRefShim(HalDigitalHandle pwmPortHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_InitializePWMPort", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial HalDigitalHandle InitializePWMPortRefShim(HalPortHandle portHandle, string allocationLocation, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_LatchPWMZero")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void LatchPWMZeroRefShim(HalDigitalHandle pwmPortHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetPWMConfigMicroseconds")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetPWMConfigMicrosecondsRefShim(HalDigitalHandle pwmPortHandle, int maxPwm, int deadbandMaxPwm, int centerPwm, int deadbandMinPwm, int minPwm, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetPWMDisabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetPWMDisabledRefShim(HalDigitalHandle pwmPortHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetPWMEliminateDeadband")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetPWMEliminateDeadbandRefShim(HalDigitalHandle pwmPortHandle, int eliminateDeadband, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetPWMPeriodScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetPWMPeriodScaleRefShim(HalDigitalHandle pwmPortHandle, int squelchMask, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetPWMPosition")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetPWMPositionRefShim(HalDigitalHandle pwmPortHandle, double position, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetPWMSpeed")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetPWMSpeedRefShim(HalDigitalHandle pwmPortHandle, double speed, ref HalStatus status);


}
