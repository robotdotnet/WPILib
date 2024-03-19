using CsCore.Handles;
using CsCore.Natives;
using UnitsNet;
using UnitsNet.NumberExtensions.NumberToDuration;
using WPIUtil;

namespace CsCore.Raw;

public class RawSink(string name) : ImageSink(CreateRawSink(name))
{
    private static CsSink CreateRawSink(string name)
    {
        var sink = CsNative.CreateRawSink(name);

        return sink;
    }

    public long GrabFrame(RawFrameReader frame)
    {
        return GrabFrame(frame, 0.225.Seconds());
    }

    public long GrabFrame(RawFrameReader frame, Duration timeout)
    {
        var time = CsNative.GrabRawSinkFrame(Handle, frame, timeout.Seconds);

        return (long)time;
    }

    public long GrabFrameNoTimeout(RawFrameReader frame)
    {
        var time = CsNative.GrabRawSinkFrame(Handle, frame);

        return (long)time;
    }
}
