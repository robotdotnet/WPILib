using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace NetworkTables;

[NativeMarshalling(typeof(PubSubOptionsMarshaller))]
[StructLayout(LayoutKind.Auto)]
public record struct PubSubOptions(uint StructSize, uint PollSize, double Periodic, bool ExcludePublisher, bool SendAll, bool TopicsOnly, bool PrefixMatch, bool KeepDuplicates, bool DisableRemote, bool DisableLocal, bool ExcludeSelf)
{
    public static PubSubOptions None => new();
}

[CustomMarshaller(typeof(PubSubOptions), MarshalMode.Default, typeof(PubSubOptionsMarshaller))]
public static unsafe class PubSubOptionsMarshaller
{
    public static NativePubSubOptions ConvertToUnmanaged(in PubSubOptions managed)
    {
        return new NativePubSubOptions
        {
            structSize = managed.StructSize,
            pollSize = managed.PollSize,
            periodic = managed.Periodic,
            excludePublisher = managed.ExcludePublisher ? 1 : 0,
            sendAll = managed.SendAll ? 1 : 0,
            topicsOnly = managed.TopicsOnly ? 1 : 0,
            prefixMatch = managed.PrefixMatch ? 1 : 0,
            keepDuplicates = managed.KeepDuplicates ? 1 : 0,
            disableRemote = managed.DisableRemote ? 1 : 0,
            disableLocal = managed.DisableLocal ? 1 : 0,
            excludeSelf = managed.ExcludeSelf ? 1 : 0,
        };
    }

    public static PubSubOptions ConvertToManaged(in NativePubSubOptions unmanaged)
    {
        return new PubSubOptions
        {
            StructSize = unmanaged.structSize,
            PollSize = unmanaged.pollSize,
            Periodic = unmanaged.periodic,
            ExcludePublisher = unmanaged.excludePublisher != 0,
            SendAll = unmanaged.sendAll != 0,
            TopicsOnly = unmanaged.topicsOnly != 0,
            PrefixMatch = unmanaged.prefixMatch != 0,
            KeepDuplicates = unmanaged.keepDuplicates != 0,
            DisableRemote = unmanaged.disableRemote != 0,
            DisableLocal = unmanaged.disableLocal != 0,
            ExcludeSelf = unmanaged.excludeSelf != 0,
        };
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NativePubSubOptions
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
}
