using CsCore.Handles;
using CsCore.Natives;

namespace CsCore;

public class VideoSource : IDisposable, IEquatable<VideoSource?>
{
    public CsSource Handle { get; private set; }

    public bool IsValid => Handle.Handle != 0;

    protected internal VideoSource(CsSource handle)
    {
        Handle = handle;
    }

    public SourceKind Kind
    {
        get
        {
            var kind = CsNative.GetSourceKind(Handle);

            return kind;
        }
    }

    public string Name
    {
        get
        {
            CsNative.GetSourceName(Handle, out var name);

            return name;
        }
    }

    public string Description
    {
        get
        {
            CsNative.GetSourceDescription(Handle, out var description);

            return description;
        }
    }

    public long LastFrameTime
    {
        get
        {
            var frameTime = CsNative.GetSourceLastFrameTime(Handle);

            return (long)frameTime;
        }
    }

    public ConnectionStrategy ConnectionStrategy
    {
        set
        {
            CsNative.SetSourceConnectionStrategy(Handle, value);

        }
    }

    public bool IsConnected
    {
        get
        {
            var isConnected = CsNative.IsSourceConnected(Handle);

            return isConnected;
        }
    }

    public bool IsEnabled
    {
        get
        {
            var isEnabled = CsNative.IsSourceEnabled(Handle);

            return isEnabled;
        }
    }

    public VideoMode VideoMode
    {
        get
        {
            CsNative.GetSourceVideoMode(Handle, out var mode);

            return mode;
        }
        set
        {
            CsNative.SetSourceVideoMode(Handle, value);

        }
    }

    public bool SetConfigJson(string config)
    {
        return CsNative.SetSourceConfigJson(Handle, config);
    }

    public string GetConfigJson()
    {
        CsNative.GetSourceConfigJson(Handle, out var str);

        return str;
    }

    public double GetActualFPS()
    {
        var avg = CsNative.GetTelemetryAverageValue(Handle, TelemetryKind.SourceFramesReceived);

        return avg;
    }

    public double GetActualDataRate()
    {
        var avg = CsNative.GetTelemetryAverageValue(Handle, TelemetryKind.SourceBytesReceived);

        return avg;
    }

    public VideoMode[] EnumerateVideoModes()
    {
        var modes = CsNative.EnumerateSourceVideoModes(Handle);

        return modes;
    }

    public VideoSink[] EnumerateSinks()
    {
        var handles = CsNative.EnumerateSourceSinks(Handle);

        VideoSink[] rv = new VideoSink[handles.Length];
        for (int i = 0; i < handles.Length; i++)
        {
            rv[i] = new VideoSink(handles[i]);
        }
        return rv;
    }

    public VideoProperty GetProperty(string name)
    {
        var property = CsNative.GetSourceProperty(Handle, name);

        return new VideoProperty(property);
    }

    public VideoProperty[] EnumerateProperties()
    {
        var handles = CsNative.EnumerateSourceProperties(Handle);

        VideoProperty[] rv = new VideoProperty[handles.Length];
        for (int i = 0; i < handles.Length; i++)
        {
            rv[i] = new VideoProperty(handles[i]);
        }
        return rv;
    }

    public static VideoSource[] EnumerateSources()
    {
        var handles = CsNative.EnumerateSources();

        VideoSource[] rv = new VideoSource[handles.Length];
        for (int i = 0; i < handles.Length; i++)
        {
            rv[i] = new VideoSource(handles[i]);
        }
        return rv;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        if (Handle.Handle != 0)
        {
            CsNative.ReleaseSource(Handle);

        }
        Handle = default;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as VideoSource);
    }

    public bool Equals(VideoSource? other)
    {
        return other is not null &&
               Handle.Equals(other.Handle);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Handle);
    }

    public static bool operator ==(VideoSource? left, VideoSource? right)
    {
        return EqualityComparer<VideoSource>.Default.Equals(left, right);
    }

    public static bool operator !=(VideoSource? left, VideoSource? right)
    {
        return !(left == right);
    }
}
