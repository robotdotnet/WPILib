using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal;
using WPIHal.Handles;

namespace Hal.Natives;

public static partial class HalThreads
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetCurrentThreadPriority")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetCurrentThreadPriority(int* isRealTime, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetThreadPriority")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetThreadPriority(NativeThreadHandle handle, int* isRealTime, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetCurrentThreadPriority")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetCurrentThreadPriority(int realTime, int priority, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetThreadPriority")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetThreadPriority(NativeThreadHandle handle, int realTime, int priority, out HalStatus status);


}
