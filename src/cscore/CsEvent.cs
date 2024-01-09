using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace CsCore;

[NativeMarshalling(typeof(CsEventMarshaller))]
[StructLayout(LayoutKind.Auto)]
public readonly struct CsEvent
{
    public EventKind Kind { get; init; }

}

[CustomMarshaller(typeof(CsEvent), MarshalMode.Default, typeof(CsEventMarshaller))]
public static unsafe class CsEventMarshaller
{

    public static NativeCsEvent ConvertToUnmanaged(in CsEvent managed)
    {
        throw new System.NotImplementedException();
    }

    public static CsEvent ConvertToManaged(in NativeCsEvent unmanaged)
    {
        throw new System.NotImplementedException();
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NativeCsEvent
    {
        public EventKind kind;
        public int source;
        public int sink;
        public byte* name;
        public VideoMode mode;
        public int property;
        public PropertyKind propertyKind;
        public int value;
        public byte* valueStr;
        public int listener;
    }
}
