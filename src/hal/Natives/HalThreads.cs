using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIUtil;

namespace WPIHal.Natives;

public static partial class HalThreads
{
    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetCurrentThreadPriority")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetCurrentThreadPriority([MarshalAs(UnmanagedType.I4)] out bool isRealTime, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetThreadPriority")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    internal static unsafe partial bool GetThreadPriority(void* handle, [MarshalAs(UnmanagedType.I4)] out bool isRealTime, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_SetCurrentThreadPriority")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool SetCurrentThreadPriority([MarshalAs(UnmanagedType.I4)] bool realTime, [MarshalAs(UnmanagedType.I4)] bool priority, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_SetThreadPriority")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    internal static unsafe partial bool SetThreadPriority(void* handle, [MarshalAs(UnmanagedType.I4)] bool realTime, [MarshalAs(UnmanagedType.I4)] bool priority, out HalStatus status);


}
