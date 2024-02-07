global using MulticastServiceResolverHandle = uint;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Marshal;

namespace WPINet.Natives;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct ServiceDataRaw
{
    public uint ipv4Address;
    public int port;
    public byte* serviceName;
    public byte* hostName;
    public int txtCount;
    public byte** txtKeys;
    public byte** txtValues;
}

public static partial class MulticastServiceResolver
{
    [LibraryImport("wpinet", EntryPoint = "WPI_CreateMulticastServiceResolver", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial MulticastServiceResolverHandle CreateMulticastServiceResolver(string serviceType);

    [LibraryImport("wpinet", EntryPoint = "WPI_FreeMulticastServiceResolver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeMulticastServiceResolver(MulticastServiceResolverHandle handle);

    [LibraryImport("wpinet", EntryPoint = "WPI_StartMulticastServiceResolver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void StartMulticastServiceResolver(MulticastServiceResolverHandle handle);

    [LibraryImport("wpinet", EntryPoint = "WPI_StopMulticastServiceResolver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void StopMulticastServiceResolver(MulticastServiceResolverHandle handle);

    [LibraryImport("wpinet", EntryPoint = "WPI_GetMulticastServiceResolverHasImplementation")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetMulticastServiceResolverHasImplementation(MulticastServiceResolverHandle handle);

    [LibraryImport("wpinet", EntryPoint = "WPI_GetMulticastServiceResolverEventHandle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint GetMulticastServiceResolverEventHandle(MulticastServiceResolverHandle handle);

    [LibraryImport("wpinet", EntryPoint = "WPI_GetMulticastServiceResolverData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(ManagedFreeArrayMarshaller<,>), CountElementName = nameof(dataCount))]
    public static unsafe partial ServiceData[] GetMulticastServiceResolverData(MulticastServiceResolverHandle handle, out int dataCount);

    [LibraryImport("wpinet", EntryPoint = "WPI_FreeMulticastServiceResolverData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void FreeMulticastServiceResolverData(ServiceDataRaw* serviceData, int dataCount);
}
