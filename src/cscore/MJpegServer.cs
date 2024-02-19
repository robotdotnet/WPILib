using CsCore.Handles;
using CsCore.Natives;

namespace CsCore;

public class MJpegServer : VideoSink
{
    private static CsSink CreateMJpegServer(string name, string listenAddress, int port)
    {
        var ret = CsNative.CreateMjpegServer(name, listenAddress, port);

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
            CsNative.GetMjpegServerListenAddress(Handle, out var address);
            return address;
        }
    }

    public int Port
    {
        get
        {
            int port = CsNative.GetMjpegServerPort(Handle);
            return port;
        }
    }

    public void SetResolution(int width, int height)
    {
        CsProperty prop = CsNative.GetSinkProperty(Handle, "width"u8);
        CsNative.SetProperty(prop, width);
        prop = CsNative.GetSinkProperty(Handle, "height"u8);
        CsNative.SetProperty(prop, height);
    }

    public void SetFPS(int fps)
    {
        CsProperty prop = CsNative.GetSinkProperty(Handle, "fps"u8);
        CsNative.SetProperty(prop, fps);
    }

    public void SetCompression(int quality)
    {
        CsProperty prop = CsNative.GetSinkProperty(Handle, "compression"u8);
        CsNative.SetProperty(prop, quality);
    }

    public void SetDefaultCompression(int quality)
    {
        CsProperty prop = CsNative.GetSinkProperty(Handle, "default_compression"u8);
        CsNative.SetProperty(prop, quality);

    }
}
