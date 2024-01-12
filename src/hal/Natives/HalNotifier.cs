using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal;
using WPIHal.Handles;

namespace Hal.Natives;

public static partial class HalNotifier
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_CancelNotifierAlarm")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelNotifierAlarm(HalNotifierHandle notifierHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_CleanNotifier")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CleanNotifier(HalNotifierHandle notifierHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_InitializeNotifier")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalNotifierHandle InitializeNotifier(out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetNotifierName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetNotifierName(HalNotifierHandle notifierHandle, string name, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_StopNotifier")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void StopNotifier(HalNotifierHandle notifierHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_UpdateNotifierAlarm")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void UpdateNotifierAlarm(HalNotifierHandle notifierHandle, ulong triggerTime, out HalStatus status);

    // [LibraryImport("wpiHal", EntryPoint = "This will cause any call into HAL_WaitForNotifierAlarm to return with time = * 0. * * @param[in] notifierHandle the notifier handle * @param[out] status Error status variable. 0 on success. */ void HAL_StopNotifier")]
    // [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    // public static partial* This will cause any call into WaitForNotifierAlarm to return with time = *0. * *@param[in] notifierHandle the notifier handle * @param[out] status Error status variable. 0 on success. */ void StopNotifier(HalNotifierHandle notifierHandle, out HalStatus status);


}
