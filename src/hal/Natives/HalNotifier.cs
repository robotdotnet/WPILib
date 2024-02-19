using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal.Handles;

namespace WPIHal.Natives;

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

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetNotifierName", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetNotifierName(HalNotifierHandle notifierHandle, string name, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_StopNotifier")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void StopNotifier(HalNotifierHandle notifierHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_UpdateNotifierAlarm")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void UpdateNotifierAlarm(HalNotifierHandle notifierHandle, ulong triggerTime, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetNotifierThreadPriority")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetNotifierThreadPriority(int realTime, int priority, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_WaitForNotifierAlarm")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ulong WaitForNotifierAlarm(HalNotifierHandle notifierHandle, out HalStatus status);
}
