using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal.Handles;

namespace WPIHal.Natives;

public static partial class HalNotifier
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_CancelNotifierAlarm")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void CancelNotifierAlarmRefShim(HalNotifierHandle notifierHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_CleanNotifier")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void CleanNotifierRefShim(HalNotifierHandle notifierHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_InitializeNotifier")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial HalNotifierHandle InitializeNotifierRefShim(ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetNotifierName", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetNotifierNameRefShim(HalNotifierHandle notifierHandle, string name, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_StopNotifier")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void StopNotifierRefShim(HalNotifierHandle notifierHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_UpdateNotifierAlarm")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void UpdateNotifierAlarmRefShim(HalNotifierHandle notifierHandle, ulong triggerTime, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetNotifierThreadPriority")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int SetNotifierThreadPriorityRefShim(int realTime, int priority, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_WaitForNotifierAlarm")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial ulong WaitForNotifierAlarmRefShim(HalNotifierHandle notifierHandle, ref HalStatus status);
}
