namespace WPIUtil.Natives;

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using Handle = int;

public static partial class SynchronizationNative
{
    [LibraryImport("wpiutil", EntryPoint = "WPI_CreateEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Handle CreateEvent([MarshalAs(UnmanagedType.I4)] bool manualReset, [MarshalAs(UnmanagedType.I4)] bool initialState);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DestroyEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyEvent(Handle handle);

    [LibraryImport("wpiutil", EntryPoint = "WPI_SetEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetEvent(Handle handle);

    [LibraryImport("wpiutil", EntryPoint = "WPI_ResetEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetEvent(Handle handle);

    [LibraryImport("wpiutil", EntryPoint = "WPI_CreateSemaphore")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Handle CreateSemaphore(int initialCount, int maximumCount);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DestroySemaphore")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroySemaphore(Handle handle);

    [LibraryImport("wpiutil", EntryPoint = "WPI_ReleaseSemaphore")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U4)]
    public static unsafe partial bool ReleaseSemaphore(Handle handle, int releaseCount, out int prevCount);

    [LibraryImport("wpiutil", EntryPoint = "WPI_WaitForObject")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U4)]
    public static partial bool WaitForObject(Handle handle);

    [LibraryImport("wpiutil", EntryPoint = "WPI_WaitForObjectTimeout")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U4)]
    public static unsafe partial bool WaitForObjectTimeout(Handle handle, double timeout, [MarshalAs(UnmanagedType.I4)] out bool timedOut);

    [LibraryImport("wpiutil", EntryPoint = "WPI_WaitForObjects")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U4)]
    public static unsafe partial bool WaitForObjects([MarshalUsing(CountElementName = nameof(handlesCount))] ReadOnlySpan<Handle> handles, int handlesCount, [MarshalUsing(CountElementName = nameof(handlesCount))] Span<Handle> signaled);

    [LibraryImport("wpiutil", EntryPoint = "WPI_WaitForObjectsTimeout")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U4)]
    public static unsafe partial bool WaitForObjectsTimeout([MarshalUsing(CountElementName = nameof(handlesCount))] ReadOnlySpan<Handle> handles, int handlesCount, [MarshalUsing(CountElementName = nameof(handlesCount))] Span<Handle> signaled, double timeout, [MarshalAs(UnmanagedType.I4)] out bool timedOut);

    [LibraryImport("wpiutil", EntryPoint = "WPI_CreateSemaphore")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CreateSignalObject(Handle handle, [MarshalAs(UnmanagedType.I4)] bool manualReset, [MarshalAs(UnmanagedType.I4)] bool InitialState);

    [LibraryImport("wpiutil", EntryPoint = "WPI_SetSignalObject")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSignalObject(Handle handle);

    [LibraryImport("wpiutil", EntryPoint = "WPI_ResetSignalObject")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetSignalObject(Handle handle);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DestroySignalObject")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroySignalObject(Handle handle);
}
