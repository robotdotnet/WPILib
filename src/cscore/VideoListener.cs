using CsCore.Handles;
using CsCore.Natives;

namespace CsCore;

public class VideoListener : IDisposable, IEquatable<VideoListener?>
{
    private static readonly object s_listenerLock = new();
    private static readonly Dictionary<CsListener, Action<VideoEvent>> s_listeners = [];
    private static Thread? s_thread;
    private static CsListenerPoller s_poller;

    private static void StartThread()
    {
        s_thread = new Thread(() =>
        {
            bool wasInterrupted = false;
            while (true)
            {
                VideoEvent[] events = CsNative.PollListener(s_poller);
                if (events.Length == 0)
                {
                    // don't try to destroy poller, as its handle is likely no longer valid
                    wasInterrupted = true;
                    break;
                }
                foreach (var videoEvent in events)
                {
                    Action<VideoEvent>? listener = null;
                    lock (s_listenerLock)
                    {
                        s_listeners.TryGetValue(videoEvent.Listener, out listener);
                    }
                    if (listener is not null)
                    {
                        try
                        {
                            listener(videoEvent);
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Unhandled exception during listener callback: {ex}");
                        }
                    }


                }
            }

            lock (s_listenerLock)
            {
                if (!wasInterrupted)
                {
                    CsNative.DestroyListenerPoller(s_poller);
                }
                s_poller = default;
                s_thread = null;
            }
        })
        {
            Name = "VideoListener",
            IsBackground = true
        };
        s_thread.Start();
    }

    public CsListener Handle { get; private set; }

    public VideoListener(Action<VideoEvent> listener, EventKind eventMask, bool immediateNotify)
    {
        lock (s_listenerLock)
        {
            if (s_poller.Handle == 0)
            {
                s_poller = CsNative.CreateListenerPoller();
                StartThread();
            }
            Handle = CsNative.AddPolledListener(s_poller, eventMask, immediateNotify);

            s_listeners.Add(Handle, listener);
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        if (Handle.Handle != 0)
        {
            lock (s_listenerLock)
            {
                s_listeners.Remove(Handle);
            }
            CsNative.RemoveListener(Handle);

        }
        Handle = default;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as VideoListener);
    }

    public bool Equals(VideoListener? other)
    {
        return other is not null &&
               Handle.Equals(other.Handle);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Handle);
    }

    public static bool operator ==(VideoListener? left, VideoListener? right)
    {
        return EqualityComparer<VideoListener>.Default.Equals(left, right);
    }

    public static bool operator !=(VideoListener? left, VideoListener? right)
    {
        return !(left == right);
    }
}
