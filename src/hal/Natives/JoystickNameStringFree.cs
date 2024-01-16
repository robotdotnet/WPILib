using WPIUtil.Marshal;

namespace WPIHal.Natives;

public class JoystickNameStringFree : INullTerminatedStringFree<byte>
{
    public static unsafe void FreeString(byte* ptr)
    {
        HalDriverStation.FreeJoystickName(ptr);
    }
}
