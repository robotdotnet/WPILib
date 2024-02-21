using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using CommunityToolkit.Diagnostics;
using NetworkTables.Handles;
using WPIUtil;
using WPIUtil.Marshal;

namespace NetworkTables.Natives;

public static partial class NtCore
{
    [LibraryImport("ntcore", EntryPoint = "NT_GetNetworkMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial NetworkMode GetNetworkMode(NtInst inst);

    [LibraryImport("ntcore", EntryPoint = "NT_StartLocal")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void StartLocal(NtInst inst);

    [LibraryImport("ntcore", EntryPoint = "NT_StopLocal")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void StopLocal(NtInst inst);

    [LibraryImport("ntcore", EntryPoint = "NT_StartServer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void StartServer(NtInst inst, WpiString persistFilename, WpiString listenAddres, uint port3, uint port4);

    [LibraryImport("ntcore", EntryPoint = "NT_StopServer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void StopServer(NtInst inst);

    [LibraryImport("ntcore", EntryPoint = "NT_StartClient3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void StartClient3(NtInst inst, WpiString identity);

    [LibraryImport("ntcore", EntryPoint = "NT_StartClient4")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void StartClient4(NtInst inst, WpiString identity);

    [LibraryImport("ntcore", EntryPoint = "NT_StopClient")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void StopClient(NtInst inst);

    [LibraryImport("ntcore", EntryPoint = "NT_SetServer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetServer(NtInst inst, WpiString serverName, uint port);

    [LibraryImport("ntcore", EntryPoint = "NT_SetServerMulti")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetServerMulti(NtInst inst, nuint count, [MarshalUsing(typeof(WpiStringMarshaller), ElementIndirectionDepth = 1)] ReadOnlySpan<string> serverNames, ReadOnlySpan<uint> ports);

    public static void SetServerMulti(NtInst inst, ReadOnlySpan<string> serverNames, ReadOnlySpan<uint> ports)
    {
        if (serverNames.Length != ports.Length)
        {
            ThrowHelper.ThrowArgumentException($"{nameof(serverNames)} and {nameof(ports)} must have identical lengths");
        }
        SetServerMulti(inst, (nuint)serverNames.Length, serverNames, ports);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_SetServerTeam")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetServerTeam(NtInst inst, uint team, uint port);

    [LibraryImport("ntcore", EntryPoint = "NT_Disconnect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Disconnect(NtInst inst);

    [LibraryImport("ntcore", EntryPoint = "NT_StartDSClient")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void StartDSClient(NtInst inst, uint port);

    [LibraryImport("ntcore", EntryPoint = "NT_StopDSClient")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void StopDSClient(NtInst inst);

    [LibraryImport("ntcore", EntryPoint = "NT_FlushLocal")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FlushLocal(NtInst inst);

    [LibraryImport("ntcore", EntryPoint = "NT_Flush")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Flush(NtInst inst);

    [LibraryImport("ntcore", EntryPoint = "NT_GetConnections")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(ManagedFreeArrayMarshaller<,>), CountElementName = nameof(count))]
    internal static partial ConnectionInfo[] GetConnections(NtInst inst, out nuint count);

    public static ConnectionInfo[] GetConnections(NtInst inst)
    {
        return GetConnections(inst, out var _);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_IsConnected")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool IsConnected(NtInst inst);

    [LibraryImport("ntcore", EntryPoint = "NT_GetServerTimeOffset")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial long GetServerTimeOffset(NtInst inst, [MarshalAs(UnmanagedType.I4)] out bool valid);

    public static long? GetServerTimeOffset(NtInst inst)
    {
        long offset = GetServerTimeOffset(inst, out var valid);
        return valid ? offset : null;
    }
}
