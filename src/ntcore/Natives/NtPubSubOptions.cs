using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace NetworkTables.Natives;

[CustomMarshaller(typeof(PubSubOptions), MarshalMode.Default, typeof(NtPubSubOptionsMarshaller))]
public static unsafe class NtPubSubOptionsMarshaller
{
    public static NtPubSubOptions ConvertToUnmanaged(in PubSubOptions managed)
    {
        throw new System.NotImplementedException();
    }

    public static PubSubOptions ConvertToManaged(in NtPubSubOptions unmanaged)
    {
        throw new System.NotImplementedException();
    }
}

[StructLayout(LayoutKind.Sequential)]
public struct NtPubSubOptions
{
    public uint structSize;
    public uint pollSize;
    public double periodic;
    public int excludePublisher;
    public int sendAll;
    public int topicsOnly;
    public int prefixMatch;
    public int keepDuplicates;
    public int disableRemote;
    public int disableLocal;
    public int excludeSelf;

}
