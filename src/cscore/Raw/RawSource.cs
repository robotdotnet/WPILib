using CsCore.Handles;
using CsCore.Natives;
using WPIUtil;

namespace CsCore.Raw;

public class RawSource(string name, VideoMode mode) : ImageSource(CreateRawSource(name, in mode))
{
    private static CsSource CreateRawSource(string name, ref readonly VideoMode mode)
    {
        var source = CsNative.CreateRawSource(name, mode);

        return source;
    }

    public RawSource(string name, PixelFormat pixelFormat, int width, int height, int fps) : this(name, new VideoMode
    {
        PixelFormat = pixelFormat,
        Width = width,
        Height = height,
        Fps = fps
    })
    { }

    public void PutFrame(RawFrameWriter image)
    {
        CsNative.PutRawSourceFrame(Handle, image);

    }
}
