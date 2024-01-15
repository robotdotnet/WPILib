using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;
using WPIUtil.Marshal;

namespace WPIUtil.Natives;

public struct OpaqueDataLog { }

[StructLayout(LayoutKind.Sequential)]
public unsafe struct DataLogString : IStringLengthPair
{
    public byte* str;
    public nuint len;

    public byte* Ptr { readonly get => str; set => str = value; }
    public nuint Len { readonly get => len; set => len = value; }
}

public static partial class DataLogNative
{
    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_Create", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial OpaqueDataLog* DataLogCreate(string? dir, string? filename, double period, string? extraHeader);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_Create_Func", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial OpaqueDataLog* DataLogCreateFunc(delegate* unmanaged[Cdecl]<void*, byte*, nuint, void> write, void* ptr, double period, string? extraHeader);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_Release")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogRelease(OpaqueDataLog* datalog);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_SetFilename", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogSetFilename(OpaqueDataLog* datalog, string filename);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_Flush")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogFlush(OpaqueDataLog* datalog);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_Pause")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogPause(OpaqueDataLog* datalog);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_Resume")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogResume(OpaqueDataLog* datalog);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_Stop")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogStop(OpaqueDataLog* datalog);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_Start", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial DataLogEntryHandle DataLogStart(OpaqueDataLog* datalog, string name, string type, string metadata, ulong timestamp);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_Finish")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogFinish(OpaqueDataLog* datalog, DataLogEntryHandle entry, ulong timestamp);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_SetMetadata", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogSetMetadata(OpaqueDataLog* datalog, DataLogEntryHandle entry, string metadata, ulong timestamp);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_AppendRaw")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static unsafe partial void DataLogAppend(OpaqueDataLog* datalog, DataLogEntryHandle entry, ReadOnlySpan<byte> raw, nuint len, ulong timestamp);

    public static unsafe void DataLogAppend(OpaqueDataLog* datalog, DataLogEntryHandle entry, ReadOnlySpan<byte> raw, ulong timestamp)
    {
        DataLogAppend(datalog, entry, raw, (nuint)raw.Length, timestamp);
    }

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_AppendBoolean")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogAppend(OpaqueDataLog* datalog, DataLogEntryHandle entry, [MarshalAs(UnmanagedType.I4)] bool value, ulong timestamp);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_AppendInteger")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogAppend(OpaqueDataLog* datalog, DataLogEntryHandle entry, long value, ulong timestamp);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_AppendFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogAppend(OpaqueDataLog* datalog, DataLogEntryHandle entry, float value, ulong timestamp);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_AppendDouble")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogAppend(OpaqueDataLog* datalog, DataLogEntryHandle entry, double value, ulong timestamp);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_AppendString")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogAppend(OpaqueDataLog* datalog, DataLogEntryHandle entry, byte* value, nuint len, ulong timestamp);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_AppendString")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogAppendStringSpan(OpaqueDataLog* datalog, DataLogEntryHandle entry, ReadOnlySpan<byte> value, nuint len, ulong timestamp);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_AppendBooleanArray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogAppend(OpaqueDataLog* datalog, DataLogEntryHandle entry, [MarshalUsing(typeof(BoolToIntMarshaller), ElementIndirectionDepth = 1)] ReadOnlySpan<bool> value, nuint len, ulong timestamp);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_AppendIntegerArray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogAppend(OpaqueDataLog* datalog, DataLogEntryHandle entry, ReadOnlySpan<long> value, nuint len, ulong timestamp);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_AppendFloatArray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogAppend(OpaqueDataLog* datalog, DataLogEntryHandle entry, ReadOnlySpan<float> value, nuint len, ulong timestamp);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_AppendDoubleArray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogAppend(OpaqueDataLog* datalog, DataLogEntryHandle entry, ReadOnlySpan<double> value, nuint len, ulong timestamp);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_AppendStringArray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogAppend(OpaqueDataLog* datalog, DataLogEntryHandle entry, [MarshalUsing(typeof(StringLengthPairMarshaller<DataLogString>), ElementIndirectionDepth = 1)] ReadOnlySpan<string> value, nuint len, ulong timestamp);
}
