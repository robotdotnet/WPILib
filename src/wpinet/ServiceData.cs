using System.Runtime.InteropServices.Marshalling;
using WPINet.Natives;
using WPIUtil.Marshal;

namespace WPINet;

[NativeMarshalling(typeof(ServiceDataMarshaller))]
public readonly record struct ServiceData(uint Ipv4Address, int Port, string ServiceName, string HostName, (string Key, string Value)[] Txt) : INativeArrayFree<ServiceDataRaw>
{
    public static unsafe void FreeArray(ServiceDataRaw* array, int len)
    {
        MulticastServiceResolver.FreeMulticastServiceResolverData(array, len);
    }
}
