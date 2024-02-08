using CsCore.Handles;
using CsCore.Natives;

namespace CsCore;

public class MJpegServer : VideoSink
{
    private static CsSink CreateMJpegServer(string name, string listenAddress, int port)
    {
        var ret = CsNative.CreateMjpegServer(name, listenAddress, port, out var status);
        VideoException.ThrowIfFailed(status);
        return ret;
    }

    public MJpegServer(string name, string listenAddress, int port) : base(CreateMJpegServer(name, listenAddress, port))
    {
    }

    public MJpegServer(string name, int port) : base(CreateMJpegServer(name, "", port))
    {
    }

    public string ListenAddress
    {
        get
        {
            CsNative.GetMjpegServerListenAddress(Handle, out var address, out var status);
            VideoException.ThrowIfFailed(status);
            return address;
        }
    }

    public int Port
    {
        get
        {
            int port = CsNative.GetMjpegServerPort(Handle, out var status);
            VideoException.ThrowIfFailed(status);
            return port;
        }
    }

    public void SetResolution(int width, int height)
    {
        CsProperty prop = CsNative.GetSinkProperty(Handle, "width"u8, out var status);
        VideoException.ThrowIfFailed(status);
        CsNative.SetProperty(prop, width, out status);
        VideoException.ThrowIfFailed(status);

        prop = CsNative.GetSinkProperty(Handle, "height"u8, out status);
        VideoException.ThrowIfFailed(status);
        CsNative.SetProperty(prop, height, out status);
        VideoException.ThrowIfFailed(status);
    }

    public void SetFPS(int fps)
    {
        CsProperty prop = CsNative.GetSinkProperty(Handle, "fps"u8, out var status);
        VideoException.ThrowIfFailed(status);
        CsNative.SetProperty(prop, fps, out status);
        VideoException.ThrowIfFailed(status);
    }

    public void SetCompression(int quality)
    {
        CsProperty prop = CsNative.GetSinkProperty(Handle, "compression"u8, out var status);
        VideoException.ThrowIfFailed(status);
        CsNative.SetProperty(prop, quality, out status);
        VideoException.ThrowIfFailed(status);
    }

    public void SetDefaultCompression(int quality)
    {
        CsProperty prop = CsNative.GetSinkProperty(Handle, "default_compression"u8, out var status);
        VideoException.ThrowIfFailed(status);
        CsNative.SetProperty(prop, quality, out status);
        VideoException.ThrowIfFailed(status);
    }
}
