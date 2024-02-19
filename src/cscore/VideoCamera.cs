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
            int brightness = CsNative.GetCameraBrightness(Handle);

            return brightness;
        }
        set
        {
            CsNative.SetCameraBrightness(Handle, value);

        }
    }

    public void SetWhiteBalanceAuto()
    {
        CsNative.SetCameraWhiteBalanceAuto(Handle);

    }

    public void SetWhiteBalanceHoldCurrent()
    {
        CsNative.SetCameraWhiteBalanceHoldCurrent(Handle);

    }

    public void SetWhiteBalanceManual(int value)
    {
        CsNative.SetCameraWhiteBalanceManual(Handle, value);

    }

    public void SetExposureAuto()
    {
        CsNative.SetCameraExposureAuto(Handle);

    }

    public void SetExposureHoldCurrent()
    {
        CsNative.SetCameraExposureHoldCurrent(Handle);

    }

    public void SetExposureManual(int value)
    {
        CsNative.SetCameraExposureManual(Handle, value);

    }
}
