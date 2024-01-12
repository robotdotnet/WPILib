using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal;
using WPIHal.Handles;

namespace WPIHal.Natives;

public static partial class HalThreads
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetCurrentThreadPriority")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetCurrentThreadPriorityRefShim(out int isRealTime, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetThreadPriority")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static unsafe partial int GetThreadPriorityRefShim(void* handle, out int isRealTime, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetCurrentThreadPriority")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int SetCurrentThreadPriorityRefShim(int realTime, int priority, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetThreadPriority")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static unsafe partial int SetThreadPriorityRefShim(void* handle, int realTime, int priority, ref HalStatus status);


}
