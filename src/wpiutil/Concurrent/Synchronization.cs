using CommunityToolkit.Diagnostics;
using UnitsNet;
using WPIUtil.Handles;
using WPIUtil.Natives;

namespace WPIUtil.Concurrent;



public static class Synchronization
{
    public static SynchronizationResult WaitForObject(WpiEvent handle)
    {
        return WaitForObject(handle.Handle);
    }

    public static SynchronizationResult WaitForObject<T>(T handle) where T : IWpiSynchronizationHandle
    {
        return WaitForObject(handle.Handle);
    }

    public static SynchronizationResult WaitForObject(int handle)
    {
        return SynchronizationNative.WaitForObject(handle) ? SynchronizationResult.Signaled : SynchronizationResult.Cancelled;
    }

    public static SynchronizationResult WaitForObject(WpiEvent handle, Duration timeout)
    {
        return WaitForObject(handle.Handle, timeout);
    }

    public static SynchronizationResult WaitForObject<T>(T handle, Duration timeout) where T : IWpiSynchronizationHandle
    {
        return WaitForObject(handle.Handle, timeout);
    }

    public static SynchronizationResult WaitForObject(int handle, Duration timeout)
    {
        bool signaled = SynchronizationNative.WaitForObjectTimeout(handle, timeout.Seconds, out var timedOut);
        if (signaled)
        {
            return SynchronizationResult.Signaled;
        }
        else if (timedOut)
        {
            return SynchronizationResult.TimedOut;
        }
        else
        {
            return SynchronizationResult.Cancelled;
        }
    }

    public static ReadOnlySpan<int> WaitForObjects(ReadOnlySpan<int> handles, Span<int> signaled)
    {
        if (signaled.Length < handles.Length)
        {
            ThrowHelper.ThrowArgumentOutOfRangeException($"{nameof(signaled)} must have at least as much capacity as {nameof(handles)}");
        }

        int written = SynchronizationNative.WaitForObjects(handles, handles.Length, signaled);
        return signaled[..written];
    }

    public static ReadOnlySpan<int> WaitForObjects(ReadOnlySpan<int> handles, Span<int> signaled, Duration timeout, out bool timedOut)
    {
        if (signaled.Length < handles.Length)
        {
            ThrowHelper.ThrowArgumentOutOfRangeException($"{nameof(signaled)} must have at least as much capacity as {nameof(handles)}");
        }

        int written = SynchronizationNative.WaitForObjectsTimeout(handles, handles.Length, signaled, timeout.Seconds, out timedOut);
        return signaled[..written];
    }
}
