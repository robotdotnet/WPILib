using System;
using CommunityToolkit.Diagnostics;
using WPIUtil.Natives;

namespace WPIUtil.Concurrent;

public static class Synchronization
{
    public static SynchronizationResult WaitForObject(int handle)
    {
        return SynchronizationNative.WaitForObject(handle) ? SynchronizationResult.Signaled : SynchronizationResult.Cancelled;
    }

    public static SynchronizationResult WaitForObject(int handle, TimeSpan timeout)
    {
        bool signaled = SynchronizationNative.WaitForObjectTimeout(handle, timeout.TotalSeconds, out var timedOut);
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

    public static ReadOnlySpan<int> WaitForObjects(ReadOnlySpan<int> handles, Span<int> signaled, TimeSpan timeout, out bool timedOut)
    {
        if (signaled.Length < handles.Length)
        {
            ThrowHelper.ThrowArgumentOutOfRangeException($"{nameof(signaled)} must have at least as much capacity as {nameof(handles)}");
        }

        int written = SynchronizationNative.WaitForObjectsTimeout(handles, handles.Length, signaled, timeout.TotalSeconds, out timedOut);
        return signaled[..written];
    }
}
