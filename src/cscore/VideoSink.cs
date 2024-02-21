using CsCore.Handles;
using CsCore.Natives;

namespace CsCore;

public class VideoSink : IDisposable, IEquatable<VideoSink?>
{
    public CsSink Handle { get; private set; }

    public bool IsValid => Handle.Handle != 0;

    protected internal VideoSink(CsSink handle)
    {
        Handle = handle;
    }

    public SinkKind Kind
    {
        get
        {
            var kind = CsNative.GetSinkKind(Handle);

            return kind;
        }
    }

    public string Name
    {
        get
        {
            CsNative.GetSinkName(Handle, out var name);

            return name;
        }
    }

    public string Description
    {
        get
        {
            CsNative.GetSinkDescription(Handle, out var description);

            return description;
        }
    }

    public bool SetConfigJson(string config)
    {
        var ret = CsNative.SetSinkConfigJson(Handle, config);

        return ret;
    }

    public string GetConfigJson()
    {
        CsNative.GetSinkConfigJson(Handle, out var str);

        return str;
    }

    public VideoSource? Source
    {
        get
        {
            var sourceHandle = CsNative.GetSinkSource(Handle);

            if (sourceHandle.Handle == 0)
            {
                return null;
            }
            return new VideoSource(sourceHandle);
        }
        set
        {
            CsSource sourceHandle = default;
            if (value is not null)
            {
                sourceHandle = value.Handle;
            }
            CsNative.SetSinkSource(Handle, sourceHandle);

        }
    }

    public VideoProperty GetProperty(string name)
    {
        var property = CsNative.GetSinkProperty(Handle, name);

        return new VideoProperty(property);
    }

    public VideoProperty[] EnumerateProperties()
    {
        var handles = CsNative.EnumerateSinkProperties(Handle);

        VideoProperty[] rv = new VideoProperty[handles.Length];
        for (int i = 0; i < handles.Length; i++)
        {
            rv[i] = new VideoProperty(handles[i]);
        }
        return rv;
    }

    public VideoProperty GetSourceProperty(string name)
    {
        var property = CsNative.GetSinkSourceProperty(Handle, name);

        return new VideoProperty(property);
    }

    public static VideoSink[] EnumerateSinks()
    {
        var handles = CsNative.EnumerateSinks();

        VideoSink[] rv = new VideoSink[handles.Length];
        for (int i = 0; i < handles.Length; i++)
        {
            rv[i] = new VideoSink(handles[i]);
        }
        return rv;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        if (Handle.Handle != 0)
        {
            CsNative.ReleaseSink(Handle);

        }
        Handle = default;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as VideoSink);
    }

    public bool Equals(VideoSink? other)
    {
        return other is not null &&
               Handle.Equals(other.Handle);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Handle);
    }

    public static bool operator ==(VideoSink? left, VideoSink? right)
    {
        return EqualityComparer<VideoSink>.Default.Equals(left, right);
    }

    public static bool operator !=(VideoSink? left, VideoSink? right)
    {
        return !(left == right);
    }
}
