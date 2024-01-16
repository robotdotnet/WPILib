using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NetworkTables.Handles;

namespace NetworkTables.Natives;

public static unsafe partial class NtCore
{
    [LibraryImport("ntcore", EntryPoint = "NT_HasSchema", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool HasSchema(NtInst inst, string name);

    [LibraryImport("ntcore", EntryPoint = "NT_AddSchema", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void AddSchema(NtInst inst, string name, string type, ReadOnlySpan<byte> schema, nuint schemaSize);

    public static void AddSchema(NtInst inst, string name, string type, ReadOnlySpan<byte> schema)
    {
        AddSchema(inst, name, type, schema, (nuint)schema.Length);
    }
}
