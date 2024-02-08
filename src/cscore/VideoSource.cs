using System;
using System.Collections.Generic;
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
            var kind = CsNative.GetSourceKind(Handle, out var status);
            VideoException.ThrowIfFailed(status);
            return kind;
        }
    }

    public string Name
    {
        get
        {
            CsNative.GetSourceName(Handle, out var name, out var status);
            VideoException.ThrowIfFailed(status);
            return name;
        }
    }

    public string Description
    {
        get
        {
            CsNative.GetSourceDescription(Handle, out var description, out var status);
            VideoException.ThrowIfFailed(status);
            return description;
        }
    }

    public long LastFrameTime
    {
        get
        {
            var frameTime = CsNative.GetSourceLastFrameTime(Handle, out var status);
            VideoException.ThrowIfFailed(status);
            return (long)frameTime;
        }
    }

    public ConnectionStrategy ConnectionStrategy
    {
        set
        {
            CsNative.SetSourceConnectionStrategy(Handle, value, out var status);
            VideoException.ThrowIfFailed(status);
        }
    }

    public bool IsConnected
    {
        get
        {
            var isConnected = CsNative.IsSourceConnected(Handle, out var status);
            VideoException.ThrowIfFailed(status);
            return isConnected;
        }
    }

    public bool IsEnabled
    {
        get
        {
            var isEnabled = CsNative.IsSourceEnabled(Handle, out var status);
            VideoException.ThrowIfFailed(status);
            return isEnabled;
        }
    }

    public VideoMode VideoMode
    {
        get
        {
            CsNative.GetSourceVideoMode(Handle, out var mode, out var status);
            VideoException.ThrowIfFailed(status);
            return mode;
        }
        set
        {
            CsNative.SetSourceVideoMode(Handle, value, out var status);
            VideoException.ThrowIfFailed(status);
        }
    }

    public bool SetConfigJson(string config)
    {
        var ret = CsNative.SetSourceConfigJson(Handle, config, out var status);
        VideoException.ThrowIfFailed(status);
        return ret;
    }

    public string GetConfigJson()
    {
        CsNative.GetSourceConfigJson(Handle, out var str, out var status);
        VideoException.ThrowIfFailed(status);
        return str;
    }

    public double GetActualFPS()
    {
        var avg = CsNative.GetTelemetryAverageValue(Handle, TelemetryKind.SourceFramesReceived, out var status);
        VideoException.ThrowIfFailed(status);
        return avg;
    }

    public double GetActualDataRate()
    {
        var avg = CsNative.GetTelemetryAverageValue(Handle, TelemetryKind.SourceBytesReceived, out var status);
        VideoException.ThrowIfFailed(status);
        return avg;
    }

    public VideoMode[] EnumerateVideoModes()
    {
        var modes = CsNative.EnumerateSourceVideoModes(Handle, out var status);
        VideoException.ThrowIfFailed(status);
        return modes;
    }

    public VideoSink[] EnumerateSinks()
    {
        var handles = CsNative.EnumerateSourceSinks(Handle, out var status);
        VideoException.ThrowIfFailed(status);
        VideoSink[] rv = new VideoSink[handles.Length];
        for (int i = 0; i < handles.Length; i++)
        {
            rv[i] = new VideoSink(handles[i]);
        }
        return rv;
    }

    public VideoProperty GetProperty(string name)
    {
        var property = CsNative.GetSourceProperty(Handle, name, out var status);
        VideoException.ThrowIfFailed(status);
        return new VideoProperty(property);
    }

    public VideoProperty[] EnumerateProperties()
    {
        var handles = CsNative.EnumerateSourceProperties(Handle, out var status);
        VideoException.ThrowIfFailed(status);
        VideoProperty[] rv = new VideoProperty[handles.Length];
        for (int i = 0; i < handles.Length; i++)
        {
            rv[i] = new VideoProperty(handles[i]);
        }
        return rv;
    }

    public static VideoSource[] EnumerateSources()
    {
        var handles = CsNative.EnumerateSources(out var status);
        VideoException.ThrowIfFailed(status);
        VideoSource[] rv = new VideoSource[handles.Length];
        for (int i = 0; i < handles.Length; i++)
        {
            rv[i] = new VideoSource(handles[i]);
        }
        return rv;
    }

    public void Dispose()
    {
        if (Handle.Handle != 0)
        {
            CsNative.ReleaseSource(Handle, out var status);
            VideoException.ThrowIfFailed(status);
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
