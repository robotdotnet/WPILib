using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using NetworkTables.Natives;

namespace NetworkTables;

[NativeMarshalling(typeof(NtPubSubOptionsMarshaller))]
[StructLayout(LayoutKind.Auto)]
public record struct PubSubOptions(uint StructSize, uint PollSize, double Periodic, bool ExcludePublisher, bool SendAll, bool TopicsOnly, bool PrefixMatch, bool KeepDuplicates, bool DisableRemote, bool DisableLocal, bool ExcludeSelf);
