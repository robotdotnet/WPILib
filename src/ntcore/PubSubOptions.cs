using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using NetworkTables.Handles;

namespace NetworkTables;

[NativeMarshalling(typeof(PubSubOptionsMarshaller))]
[StructLayout(LayoutKind.Auto)]
public record struct PubSubOptions(uint PollStorage, double Periodic, NtPublisher ExcludePublisher, bool SendAll, bool TopicsOnly, bool PrefixMatch, bool KeepDuplicates, bool DisableRemote, bool DisableLocal, bool ExcludeSelf, bool Hidden)
{
    public static PubSubOptions None => new();

    public uint PollSize
    {
        get => PollStorage;
        set => PollStorage = value;
    }
}

[CustomMarshaller(typeof(PubSubOptions), MarshalMode.Default, typeof(PubSubOptionsMarshaller))]
public static unsafe class PubSubOptionsMarshaller
{
    public static NativePubSubOptions ConvertToUnmanaged(in PubSubOptions managed)
    {
        return new NativePubSubOptions
        {
            structSize = (uint)sizeof(NativePubSubOptions),
            pollStorage = managed.PollStorage,
            periodic = managed.Periodic,
            excludePublisher = managed.ExcludePublisher.Handle,
            sendAll = managed.SendAll ? 1 : 0,
            topicsOnly = managed.TopicsOnly ? 1 : 0,
            prefixMatch = managed.PrefixMatch ? 1 : 0,
            keepDuplicates = managed.KeepDuplicates ? 1 : 0,
            disableRemote = managed.DisableRemote ? 1 : 0,
            disableLocal = managed.DisableLocal ? 1 : 0,
            excludeSelf = managed.ExcludeSelf ? 1 : 0,
            hidden = managed.Hidden ? 1 : 0,
        };
    }

    public static PubSubOptions ConvertToManaged(in NativePubSubOptions unmanaged)
    {
        return new PubSubOptions
        {
            PollStorage = unmanaged.pollStorage,
            Periodic = unmanaged.periodic,
            ExcludePublisher = new NtPublisher(unmanaged.excludePublisher),
            SendAll = unmanaged.sendAll != 0,
            TopicsOnly = unmanaged.topicsOnly != 0,
            PrefixMatch = unmanaged.prefixMatch != 0,
            KeepDuplicates = unmanaged.keepDuplicates != 0,
            DisableRemote = unmanaged.disableRemote != 0,
            DisableLocal = unmanaged.disableLocal != 0,
            ExcludeSelf = unmanaged.excludeSelf != 0,
            Hidden = unmanaged.hidden != 0,
        };
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NativePubSubOptions
    {
        public uint structSize;
        public uint pollStorage;
        public double periodic;
        public int excludePublisher;
        public int sendAll;
        public int topicsOnly;
        public int prefixMatch;
        public int keepDuplicates;
        public int disableRemote;
        public int disableLocal;
        public int excludeSelf;
        public int hidden;
    }
}
