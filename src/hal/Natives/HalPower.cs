using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIUtil;

namespace WPIHal.Natives;

public static partial class HalPower
{
    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetUserActive3V3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetUserActive3V3(out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetUserActive5V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetUserActive5V(out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetUserActive6V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetUserActive6V(out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetUserCurrent3V3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetUserCurrent3V3(out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetUserCurrent5V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetUserCurrent5V(out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetUserCurrent6V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetUserCurrent6V(out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetUserCurrentFaults3V3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetUserCurrentFaults3V3(out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetUserCurrentFaults5V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetUserCurrentFaults5V(out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetUserCurrentFaults6V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetUserCurrentFaults6V(out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetUserVoltage3V3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetUserVoltage3V3(out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetUserVoltage5V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetUserVoltage5V(out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetUserVoltage6V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetUserVoltage6V(out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetVinCurrent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetVinCurrent(out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetVinVoltage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetVinVoltage(out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetCPUTemp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetCPUTemp(out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetBrownoutVoltage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetBrownoutVoltage(out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_SetBrownoutVoltage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetBrownoutVoltage(double voltage, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_SetUserRailEnabled3V3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetUserRailEnabled3V3([MarshalAs(UnmanagedType.I4)] bool enabled, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_SetUserRailEnabled5V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetUserRailEnabled5V([MarshalAs(UnmanagedType.I4)] bool enabled, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_SetUserRailEnabled6V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetUserRailEnabled6V([MarshalAs(UnmanagedType.I4)] bool enabled, out HalStatus status);
}
