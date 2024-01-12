using Hal.Natives;
using WPIUtil.Marshal;

namespace WPIHal;

public class JoystickNameStringFree : INullTerminatedStringFree<byte>
{
    public static unsafe void FreeString(byte* ptr)
    {
        HalDriverStation.FreeJoystickName(ptr);
    }
}
