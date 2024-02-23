using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using CsCore.Natives;
using WPIUtil;
using WPIUtil.Marshal;

namespace CsCore;

[NativeMarshalling(typeof(VideoModeMarshaller))]
[StructLayout(LayoutKind.Auto)]
public record struct VideoMode(PixelFormat PixelFormat, int Width, int Height, int Fps) : INativeArrayFree<VideoModeMarshaller.NativeVideoMode>
{
    public static unsafe void FreeArray(VideoModeMarshaller.NativeVideoMode* array, int len)
    {
        CsNative.FreeEnumeratedVideoModes(array, len);
    }
}

[CustomMarshaller(typeof(VideoMode), MarshalMode.Default, typeof(VideoModeMarshaller))]
public static unsafe class VideoModeMarshaller
{
    public static NativeVideoMode ConvertToUnmanaged(VideoMode managed)
    {
        return new NativeVideoMode
        {
            pixelFormat = managed.PixelFormat,
            width = managed.Width,
            height = managed.Height,
            fps = managed.Fps
        };
    }

    public static VideoMode ConvertToManaged(NativeVideoMode unmanaged)
    {
        return new VideoMode
        {
            PixelFormat = unmanaged.pixelFormat,
            Width = unmanaged.width,
            Height = unmanaged.height,
            Fps = unmanaged.fps,
        };
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NativeVideoMode
    {
        public PixelFormat pixelFormat;
        public int width;
        public int height;
        public int fps;
    }
}
