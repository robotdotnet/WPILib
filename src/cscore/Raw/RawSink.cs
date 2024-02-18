using System;
using CsCore.Handles;
using CsCore.Natives;
using WPIUtil;

namespace CsCore.Raw;

public class RawSink(string name) : ImageSink(CreateRawSink(name))
{
    private static CsSink CreateRawSink(string name)
    {
        var sink = CsNative.CreateRawSink(name, out var status);
        VideoException.ThrowIfFailed(status);
        return sink;
    }

    public long GrabFrame(RawFrameReader frame)
    {
        return GrabFrame(frame, TimeSpan.FromSeconds(0.225));
    }

    public long GrabFrame(RawFrameReader frame, TimeSpan timeout)
    {
        var time = CsNative.GrabRawSinkFrame(Handle, frame, timeout.TotalSeconds, out var status);
        VideoException.ThrowIfFailed(status);
        return (long)time;
    }

    public long GrabFrameNoTimeout(RawFrameReader frame)
    {
        var time = CsNative.GrabRawSinkFrame(Handle, frame, out var status);
        VideoException.ThrowIfFailed(status);
        return (long)time;
    }
}