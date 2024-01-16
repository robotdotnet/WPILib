using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WPIHal.Natives;

public static partial class HalPower
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetUserActive3V3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetUserActive3V3RefShim(ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetUserActive5V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetUserActive5VRefShim(ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetUserActive6V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetUserActive6VRefShim(ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetUserCurrent3V3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial double GetUserCurrent3V3RefShim(ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetUserCurrent5V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial double GetUserCurrent5VRefShim(ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetUserCurrent6V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial double GetUserCurrent6VRefShim(ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetUserCurrentFaults3V3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetUserCurrentFaults3V3RefShim(ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetUserCurrentFaults5V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetUserCurrentFaults5VRefShim(ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetUserCurrentFaults6V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetUserCurrentFaults6VRefShim(ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetUserVoltage3V3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial double GetUserVoltage3V3RefShim(ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetUserVoltage5V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial double GetUserVoltage5VRefShim(ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetUserVoltage6V")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial double GetUserVoltage6VRefShim(ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetVinCurrent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial double GetVinCurrentRefShim(ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetVinVoltage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial double GetVinVoltageRefShim(ref HalStatus status);


}
