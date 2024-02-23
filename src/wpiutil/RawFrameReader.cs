using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Natives;

namespace WPIUtil;

[NativeMarshalling(typeof(RawFrameReaderMarshaller))]
public sealed class RawFrameReader : IDisposable
{
    private NativeRawFrame internalFrame;

    public void Dispose()
    {
        NativeRawFrame.FreeRawFrameData(ref internalFrame);
    }

    public ref NativeRawFrame RawFrame => ref internalFrame;

    public unsafe ReadOnlySpan<byte> Data => new(internalFrame.data, checked((int)internalFrame.size));

    public void SetInfo(int width, int height, int stride, PixelFormat pixelFormat)
    {
        Height = height;
        Width = width;
        Stride = stride;
        PixelFormat = pixelFormat;
    }

    public int Height
    {
        get => internalFrame.height;
        set => internalFrame.height = value;
    }

    public int Width
    {
        get => internalFrame.width;
        set => internalFrame.width = value;
    }

    public int Stride
    {
        get => internalFrame.stride;
        set => internalFrame.stride = value;
    }

    public PixelFormat PixelFormat
    {
        get => internalFrame.pixelFormat;
        set => internalFrame.pixelFormat = value;
    }

    public unsafe RawFrameReader CreateCopy()
    {
        RawFrameReader ret = new RawFrameReader();
        ret.SetInfo(Width, Height, Stride, PixelFormat);
        NativeRawFrame.AllocateRawFrameData(ref ret.RawFrame, internalFrame.size);
        ret.internalFrame.size = internalFrame.size;
        new ReadOnlySpan<byte>(internalFrame.data, checked((int)internalFrame.size)).CopyTo(new Span<byte>(ret.internalFrame.data, (int)ret.internalFrame.size));
        return ret;
    }

    public unsafe RawFrameWriter ToWriter()
    {
        return new RawFrameWriter()
        {
            Data = new ReadOnlySpan<byte>(internalFrame.data, checked((int)internalFrame.size)),
            Width = Width,
            Height = Height,
            Stride = Stride,
            PixelFormat = PixelFormat,
        };
    }
}

[CustomMarshaller(typeof(RawFrameReader), MarshalMode.ManagedToUnmanagedIn, typeof(RawFrameReaderMarshaller))]
public static class RawFrameReaderMarshaller
{
    public static ref NativeRawFrame GetPinnableReference(RawFrameReader managed)
    {
        return ref managed.RawFrame;
    }

    public static unsafe NativeRawFrame* ConvertToUnmanaged(RawFrameReader managed)
    {
        throw new NotSupportedException();
    }
}
