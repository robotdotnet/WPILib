using System.Runtime.InteropServices;

namespace WPIHal;

[StructLayout(LayoutKind.Sequential)]
public readonly struct JoystickButtons
{
    private readonly uint buttons;
    private readonly byte count;

    public int Count => count;

    public uint Buttons => buttons;

    public bool? this[int index]
    {
        get
        {
            if (index < count)
            {
                return (buttons & index) != 0;
            }
            else
            {
                return null;
            }
        }
    }
}
