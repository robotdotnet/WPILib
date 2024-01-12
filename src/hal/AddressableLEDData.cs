using System.Runtime.InteropServices;

namespace WPIHal;

[StructLayout(LayoutKind.Sequential)]
public struct AddressableLEDData
{
    private byte b;
    private byte g;
    private byte r;
    private byte padding;

    public byte B { readonly get => b; set => b = value; }
    public byte G { readonly get => g; set => g = value; }
    public byte R { readonly get => r; set => r = value; }
    public byte Padding { readonly get => padding; set => padding = value; }
}
