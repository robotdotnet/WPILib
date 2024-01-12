using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WPIHal;

[NativeMarshalling(typeof(ControlWordMarshaller))]
[StructLayout(LayoutKind.Auto)]
public record struct ControlWord(bool Enabled, bool Autonomous, bool Test, bool EStop, bool FmsAttached, bool DsAttached);

[CustomMarshaller(typeof(ControlWord), MarshalMode.Default, typeof(ControlWordMarshaller))]
public static class ControlWordMarshaller
{
    public static NativeControlWord ConvertToUnmanaged(ControlWord managed)
    {
        throw new System.NotImplementedException();
    }

    public static ControlWord ConvertToManaged(NativeControlWord unmanaged)
    {
        throw new System.NotImplementedException();
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NativeControlWord
    {
        public uint word;
    }
}
