using CsCore.Handles;
using CsCore.Natives;

namespace CsCore;

public class VideoCamera : VideoSource
{
    protected internal VideoCamera(CsSource handle) : base(handle)
    {

    }

    public int Brightness
    {
        get
        {
            int brightness = CsNative.GetCameraBrightness(Handle, out var status);
            VideoException.ThrowIfFailed(status);
            return brightness;
        }
        set
        {
            CsNative.SetCameraBrightness(Handle, value, out var status);
            VideoException.ThrowIfFailed(status);
        }
    }

    public void SetWhiteBalanceAuto()
    {
        CsNative.SetCameraWhiteBalanceAuto(Handle, out var status);
        VideoException.ThrowIfFailed(status);
    }

    public void SetWhiteBalanceHoldCurrent()
    {
        CsNative.SetCameraWhiteBalanceHoldCurrent(Handle, out var status);
        VideoException.ThrowIfFailed(status);
    }

    public void SetWhiteBalanceManual(int value)
    {
        CsNative.SetCameraWhiteBalanceManual(Handle, value, out var status);
        VideoException.ThrowIfFailed(status);
    }

    public void SetExposureAuto()
    {
        CsNative.SetCameraExposureAuto(Handle, out var status);
        VideoException.ThrowIfFailed(status);
    }

    public void SetExposureHoldCurrent()
    {
        CsNative.SetCameraExposureHoldCurrent(Handle, out var status);
        VideoException.ThrowIfFailed(status);
    }

    public void SetExposureManual(int value)
    {
        CsNative.SetCameraExposureManual(Handle, value, out var status);
        VideoException.ThrowIfFailed(status);
    }
}
