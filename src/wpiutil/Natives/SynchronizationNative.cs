using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CommunityToolkit.Diagnostics;
using WPIUtil.Handles;

namespace WPIUtil.Natives;

public static partial class SynchronizationNative
{
    [LibraryImport("wpiutil", EntryPoint = "WPI_CreateEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial WpiEventHandle CreateEvent([MarshalAs(UnmanagedType.I4)] bool manualReset, [MarshalAs(UnmanagedType.I4)] bool initialState);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DestroyEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyEvent(WpiEventHandle handle);

    [LibraryImport("wpiutil", EntryPoint = "WPI_SetEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetEvent(WpiEventHandle handle);

    [LibraryImport("wpiutil", EntryPoint = "WPI_ResetEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetEvent(WpiEventHandle handle);

    [LibraryImport("wpiutil", EntryPoint = "WPI_CreateSemaphore")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial WpiSemaphoreHandle CreateSemaphore(int initialCount, int maximumCount);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DestroySemaphore")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroySemaphore(WpiSemaphoreHandle handle);

    [LibraryImport("wpiutil", EntryPoint = "WPI_ReleaseSemaphore")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U4)]
    public static unsafe partial bool ReleaseSemaphore(WpiSemaphoreHandle handle, int releaseCount, out int prevCount);

    [LibraryImport("wpiutil", EntryPoint = "WPI_WaitForObject")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U4)]
    public static partial bool WaitForObject(int handle);

    [LibraryImport("wpiutil", EntryPoint = "WPI_WaitForObjectTimeout")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U4)]
    public static unsafe partial bool WaitForObjectTimeout(int handle, double timeout, [MarshalAs(UnmanagedType.I4)] out bool timedOut);

    [LibraryImport("wpiutil", EntryPoint = "WPI_WaitForObjects")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial int WaitForObjects(ReadOnlySpan<int> handles, int handlesCount, Span<int> signaled);

    public static unsafe ReadOnlySpan<int> WaitForObjects(ReadOnlySpan<int> handles, Span<int> signaled)
    {
        if (handles.Length > signaled.Length)
        {
            ThrowHelper.ThrowArgumentException("Handles must have a length larger then signaled");
        }
        int numSignaled = WaitForObjects(handles, handles.Length, signaled);
        return signaled[..numSignaled];
    }

    [LibraryImport("wpiutil", EntryPoint = "WPI_WaitForObjectsTimeout")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial int WaitForObjectsTimeout(ReadOnlySpan<int> handles, int handlesCount, Span<int> signaled, double timeout, [MarshalAs(UnmanagedType.I4)] out bool timedOut);

    [LibraryImport("wpiutil", EntryPoint = "WPI_CreateSemaphore")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CreateSignalObject(int handle, [MarshalAs(UnmanagedType.I4)] bool manualReset, [MarshalAs(UnmanagedType.I4)] bool InitialState);

    [LibraryImport("wpiutil", EntryPoint = "WPI_SetSignalObject")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSignalObject(int handle);

    [LibraryImport("wpiutil", EntryPoint = "WPI_ResetSignalObject")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetSignalObject(int handle);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DestroySignalObject")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroySignalObject(int handle);
}
