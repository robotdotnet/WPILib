using CsCore.Handles;
using CsCore.Natives;

namespace CsCore;

public class UsbCamera : VideoCamera
{
    private static CsSource CreateUsbCameraDev(string name, int dev)
    {
        var handle = CsNative.CreateUsbCamera(name, dev, out var status);
        VideoException.ThrowIfFailed(status);
        return handle;
    }

    private static CsSource CreateUsbCameraPath(string name, string path)
    {
        var handle = CsNative.CreateUsbCamera(name, path, out var status);
        VideoException.ThrowIfFailed(status);
        return handle;
    }

    public UsbCamera(string name, int dev) : base(CreateUsbCameraDev(name, dev))
    {
    }

    public UsbCamera(string name, string path) : base(CreateUsbCameraPath(name, path))
    {
    }

    public static UsbCameraInfo[] EnumerateUsbCameras()
    {
        var info = CsNative.EnumerateUsbCameras(out var status);
        VideoException.ThrowIfFailed(status);
        return info;
    }

    public string Path
    {
        get
        {
            CsNative.GetUsbCameraPath(Handle, out var path, out var status);
            VideoException.ThrowIfFailed(status);
            return path;
        }
        set
        {
            CsNative.SetUsbCameraPath(Handle, value, out var status);
            VideoException.ThrowIfFailed(status);
        }
    }

    public UsbCameraInfo Info
    {
        get
        {
            var info = CsNative.GetUsbCameraInfo(Handle, out var status);
            VideoException.ThrowIfFailed(status);
            return info;
        }
    }

    public void SetConnectVerbose(int level)
    {
        var property = CsNative.GetSourceProperty(Handle, "connect_verbose"u8, out var status);
        VideoException.ThrowIfFailed(status);
        CsNative.SetProperty(property, level, out status);
        VideoException.ThrowIfFailed(status);
    }
}
