using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WPIHal;

[NativeMarshalling(typeof(JoystickButtonsMarshaller))]
[StructLayout(LayoutKind.Auto)]
public readonly record struct JoystickButtons(uint Buttons, int Count)
{
    public bool? this[int index]
    {
        get
        {
            if (index < Count)
            {
                return (Buttons & index) != 0;
            }
            else
            {
                return null;
            }
        }
    }
}

[CustomMarshaller(typeof(JoystickButtons), MarshalMode.ManagedToUnmanagedOut, typeof(JoystickButtonsMarshaller))]
public static class JoystickButtonsMarshaller
{
    public static JoystickButtons ConvertToManaged(NativeJoystickButtons unmanaged)
    {
        return new JoystickButtons
        {
            Buttons = unmanaged.buttons,
            Count = unmanaged.count,
        };
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NativeJoystickButtons
    {
        public uint buttons;
        public byte count;
    }
}
