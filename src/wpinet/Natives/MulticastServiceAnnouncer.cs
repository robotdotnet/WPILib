global using MulticastServiceAnnouncerHandle = uint;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WPINet.Natives;

public static partial class MulticastServiceAnnouncer
{
    [LibraryImport("wpinet", EntryPoint = "WPI_CreateMulticastServiceAnnouncer", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial MulticastServiceAnnouncerHandle CreateMulticastServiceAnnouncer(string serviceName, string serviceType, int port, int txtCount, ReadOnlySpan<string> keys, ReadOnlySpan<string> values);

    [LibraryImport("wpinet", EntryPoint = "WPI_FreeMulticastServiceAnnouncer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeMulticastServiceAnnouncer(MulticastServiceAnnouncerHandle handle);

    [LibraryImport("wpinet", EntryPoint = "WPI_StartMulticastServiceAnnouncer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void StartMulticastServiceAnnouncer(MulticastServiceAnnouncerHandle handle);

    [LibraryImport("wpinet", EntryPoint = "WPI_StopMulticastServiceAnnouncer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void StopMulticastServiceAnnouncer(MulticastServiceAnnouncerHandle handle);

    [LibraryImport("wpinet", EntryPoint = "WPI_GetMulticastServiceAnnouncerHasImplementation")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetMulticastServiceAnnouncerHasImplementation(MulticastServiceAnnouncerHandle handle);
}
