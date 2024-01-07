using System.Collections.Generic;
using System.Runtime.InteropServices.Marshalling;
using WPINet.Natives;

namespace WPINet;

[NativeMarshalling(typeof(ServiceDataMarshaller))]
public readonly record struct ServiceData(uint Ipv4Address, int Port, string ServiceName, string HostName, (string Key, string Value)[] Txt);
