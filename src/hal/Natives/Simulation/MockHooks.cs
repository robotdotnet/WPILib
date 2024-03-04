using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using unsafe HALSIM_SendConsoleLineHandler = delegate* unmanaged[Cdecl]<byte*, int>;
using unsafe HALSIM_SendErrorHandler = delegate* unmanaged[Cdecl]<int, int, int, byte*, byte*, byte*, int, int>;
using unsafe HALSIM_SimPeriodicCallback = delegate* unmanaged[Cdecl]<void*, void>;

namespace WPIHal.Natives.Simulation;

public static unsafe partial class HalMockHooks
{
    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRuntimeType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetRuntimeType(WPIHal.RuntimeType type);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_WaitForProgramStart")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void WaitForProgramStart();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetProgramStarted")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetProgramStarted();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetProgramStarted")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetProgramStarted();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RestartTiming")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RestartTiming();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_PauseTiming")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void PauseTiming();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_ResumeTiming")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResumeTiming();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_IsTimingPaused")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool IsTimingPaused();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_StepTiming")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void StepTiming(ulong delta);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_StepTimingAsync")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void StepTimingAsync(ulong delta);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetSendError")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSendError(HALSIM_SendErrorHandler handler);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetSendConsoleLine")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSendConsoleLine(HALSIM_SendConsoleLineHandler handler);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterSimPeriodicBeforeCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterSimPeriodicBeforeCallback(HALSIM_SimPeriodicCallback callback, void* param);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelSimPeriodicBeforeCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelSimPeriodicBeforeCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterSimPeriodicAfterCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterSimPeriodicAfterCallback(HALSIM_SimPeriodicCallback callback, void* param);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelSimPeriodicAfterCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelSimPeriodicAfterCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAllSimPeriodicCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelAllSimPeriodicCallbacks();

}
