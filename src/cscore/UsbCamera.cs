using CsCore.Handles;
using CsCore.Natives;

namespace CsCore;

public class UsbCamera : VideoCamera
{
    private static CsSource CreateUsbCameraDev(string name, int dev)
    {
        var handle = CsNative.CreateUsbCamera(name, dev);

        return handle;
    }

    private static CsSource CreateUsbCameraPath(string name, string path)
    {
        var handle = CsNative.CreateUsbCamera(name, path);

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
        var info = CsNative.EnumerateUsbCameras();

        return info;
    }

    public string Path
    {
        get
        {
            CsNative.GetUsbCameraPath(Handle, out var path);

            return path;
        }
        set
        {
            CsNative.SetUsbCameraPath(Handle, value);

        }
    }

    public UsbCameraInfo Info
    {
        get
        {
            var info = CsNative.GetUsbCameraInfo(Handle);

            return info;
        }
    }

    public void SetConnectVerbose(int level)
    {
        var property = CsNative.GetSourceProperty(Handle, "connect_verbose"u8);
        CsNative.SetProperty(property, level);
    }
}
