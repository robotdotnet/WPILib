using System;
using System.Buffers;
using CsCore.Handles;
using CsCore.Natives;

namespace CsCore;

public abstract class ImageSource(CsSource handle) : VideoSource(handle)
{
    public void NotifyError(string msg)
    {
        CsNative.NotifySourceError(Handle, msg, out var status);
        VideoException.ThrowIfFailed(status);
    }

    public bool Connected
    {
        set
        {
            CsNative.SetSourceConnected(Handle, value, out var status);
            VideoException.ThrowIfFailed(status);
        }
    }

    public new string Description
    {
        set
        {
            CsNative.SetSourceDescription(Handle, value, out var status);
            VideoException.ThrowIfFailed(status);
        }
    }

    public VideoProperty CreateProperty(string name, PropertyKind kind, int minimum, int maximum, int step, int defaultValue, int value)
    {
        var property = CsNative.CreateSourceProperty(Handle, name, kind, minimum, maximum, step, defaultValue, value, out var status);
        VideoException.ThrowIfFailed(status);
        return new VideoProperty(property);
    }

    public VideoProperty CreateIntegerProperty(string name, int minimum, int maximum, int step, int defaultValue, int value)
    {
        var property = CsNative.CreateSourceProperty(Handle, name, PropertyKind.Integer, minimum, maximum, step, defaultValue, value, out var status);
        VideoException.ThrowIfFailed(status);
        return new VideoProperty(property);
    }

    public VideoProperty CreateBooleanProperty(string name, bool defaultValue, bool value)
    {
        var property = CsNative.CreateSourceProperty(Handle, name, PropertyKind.Boolean, 0, 1, 1, defaultValue ? 1 : 0, value ? 1 : 0, out var status);
        VideoException.ThrowIfFailed(status);
        return new VideoProperty(property);
    }

    public VideoProperty CreateStringProperty(string name, string value)
    {
        var property = CsNative.CreateSourceProperty(Handle, name, PropertyKind.String, 0, 0, 0, 0, 0, out var status);
        VideoException.ThrowIfFailed(status);
        return new VideoProperty(property)
        {
            StringValue = value
        };
    }

    public void SetEnumPropertyChoices(VideoProperty property, ReadOnlySpan<string> choices)
    {
        CsNative.SetSourceEnumPropertyChoices(Handle, property.Handle, choices, out var status);
        VideoException.ThrowIfFailed(status);
    }
}
