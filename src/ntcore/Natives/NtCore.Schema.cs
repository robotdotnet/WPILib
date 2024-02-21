using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NetworkTables.Handles;
using WPIUtil;

namespace NetworkTables.Natives;

public static unsafe partial class NtCore
{
    [LibraryImport("ntcore", EntryPoint = "NT_HasSchema")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool HasSchema(NtInst inst, WpiString name);

    [LibraryImport("ntcore", EntryPoint = "NT_AddSchema")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void AddSchema(NtInst inst, WpiString name, WpiString type, ReadOnlySpan<byte> schema, nuint schemaSize);

    public static void AddSchema(NtInst inst, WpiString name, WpiString type, ReadOnlySpan<byte> schema)
    {
        AddSchema(inst, name, type, schema, (nuint)schema.Length);
    }
}
