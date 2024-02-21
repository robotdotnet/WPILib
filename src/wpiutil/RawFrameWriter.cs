using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Natives;

namespace WPIUtil;

[NativeMarshalling(typeof(RawFrameWriterMarshaller))]
[StructLayout(LayoutKind.Auto)]
public readonly ref struct RawFrameWriter
{
    public ReadOnlySpan<byte> Data { get; init; }
    public int Width { get; init; }
    public int Height { get; init; }
    public int Stride { get; init; }
    public PixelFormat PixelFormat { get; init; }
}
