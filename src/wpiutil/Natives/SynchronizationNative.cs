namespace WPIUtil.Natives;

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SynchronizationHandle = int;

public static partial class SynchronizationNative
{
    [LibraryImport("wpiutil", EntryPoint = "WPI_CreateEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SynchronizationHandle CreateEvent([MarshalAs(UnmanagedType.I4)] bool manualReset, [MarshalAs(UnmanagedType.I4)] bool initialState);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DestroyEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyEvent(SynchronizationHandle handle);

    [LibraryImport("wpiutil", EntryPoint = "WPI_SetEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetEvent(SynchronizationHandle handle);

    [LibraryImport("wpiutil", EntryPoint = "WPI_ResetEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetEvent(SynchronizationHandle handle);

    [LibraryImport("wpiutil", EntryPoint = "WPI_CreateSemaphore")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SynchronizationHandle CreateSemaphore(int initialCount, int maximumCount);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DestroySemaphore")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroySemaphore(SynchronizationHandle handle);

    [LibraryImport("wpiutil", EntryPoint = "WPI_ReleaseSemaphore")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U4)]
    public static unsafe partial bool ReleaseSemaphore(SynchronizationHandle handle, int releaseCount, out int prevCount);

    [LibraryImport("wpiutil", EntryPoint = "WPI_WaitForObject")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U4)]
    public static partial bool WaitForObject(SynchronizationHandle handle);

    [LibraryImport("wpiutil", EntryPoint = "WPI_WaitForObjectTimeout")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U4)]
    public static unsafe partial bool WaitForObjectTimeout(SynchronizationHandle handle, double timeout, [MarshalAs(UnmanagedType.I4)] out bool timedOut);

    [LibraryImport("wpiutil", EntryPoint = "WPI_WaitForObjects")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial int WaitForObjects(ReadOnlySpan<SynchronizationHandle> handles, int handlesCount, Span<SynchronizationHandle> signaled);

    [LibraryImport("wpiutil", EntryPoint = "WPI_WaitForObjectsTimeout")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial int WaitForObjectsTimeout(ReadOnlySpan<SynchronizationHandle> handles, int handlesCount, Span<SynchronizationHandle> signaled, double timeout, [MarshalAs(UnmanagedType.I4)] out bool timedOut);

    [LibraryImport("wpiutil", EntryPoint = "WPI_CreateSemaphore")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CreateSignalObject(SynchronizationHandle handle, [MarshalAs(UnmanagedType.I4)] bool manualReset, [MarshalAs(UnmanagedType.I4)] bool InitialState);

    [LibraryImport("wpiutil", EntryPoint = "WPI_SetSignalObject")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSignalObject(SynchronizationHandle handle);

    [LibraryImport("wpiutil", EntryPoint = "WPI_ResetSignalObject")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetSignalObject(SynchronizationHandle handle);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DestroySignalObject")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroySignalObject(SynchronizationHandle handle);
}
