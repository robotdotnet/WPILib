using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WPIHal;

[NativeMarshalling(typeof(JoystickDescriptorMarshaller))]
public readonly record struct JoystickDescriptor(bool IsXbox, int Type, string Name, int[] AxisTypes, int ButtonCount, int PovCount);

[CustomMarshaller(typeof(JoystickDescriptor), MarshalMode.ManagedToUnmanagedOut, typeof(JoystickDescriptorMarshaller))]
public static class JoystickDescriptorMarshaller
{
    public static JoystickDescriptor ConvertToManaged(NativeJoystickDescriptor unmanaged)
    {
        return new JoystickDescriptor
        {
            IsXbox = unmanaged.isXbox != 0,
            Type = unmanaged.type,
            Name = unmanaged.name.FromNullTerminatedString(),
            AxisTypes = unmanaged.axisTypes.FromRawBytes(unmanaged.axisCount),
            ButtonCount = unmanaged.buttonCount,
            PovCount = unmanaged.povCount,
        };
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NativeJoystickDescriptor
    {
        [System.Runtime.CompilerServices.InlineArray(256)]
        public struct NameBuffer
        {
            private byte _element0;

            public readonly unsafe string FromNullTerminatedString()
            {
                ReadOnlySpan<byte> thisSpan = this;
                fixed (byte* b = thisSpan)
                {
                    return Marshal.PtrToStringUTF8((nint)b)!;
                }
            }
        }

        [System.Runtime.CompilerServices.InlineArray(12)]
        public struct AxesTypeBuffer
        {
            private byte _element0;

            public readonly unsafe int[] FromRawBytes(int length)
            {
                int[] ret = new int[int.Min(length, 64)];
                for (int i = 0; i < ret.Length; i++)
                {
                    ret[i] = this[i];
                }
                return ret;
            }
        }

        public byte isXbox;
        public byte type;
        public NameBuffer name;
        public byte axisCount;
        public AxesTypeBuffer axisTypes;
        public byte buttonCount;
        public byte povCount;

    }
}
