using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace NetworkTables.Natives;

[NativeMarshalling(typeof(NtTopicInfoMarshaller))]
[StructLayout(LayoutKind.Auto)]
public record struct TopicInfo(int TopicHandle, string Name, NetworkTableType Type, string TypeStr, string Properties);
