using System;
using WPIUtil.Natives;

namespace WPIUtil;

public enum PixelFormat
{
    Unknown = 0,
    Mjpeg,
    Yuyv,
    Rgb565,
    Bgr,
    Gray,
    Y16,
    Uyvy
}

public sealed class RawFrame : IDisposable
{
    private RawFrameRaw internalFrame = new();

    public void Dispose()
    {
        RawFrameNative.FreeRawFrameData(ref internalFrame);
    }

    public void Reserve(nuint size)
    {
        RawFrameNative.AllocateRawFrameData(ref internalFrame, size);
    }

    public ref RawFrameRaw Frame => ref internalFrame;
}
