global using unsafe DataLogHandle = void*;

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WPIUtil.Natives;

public unsafe struct DataLogString
{
    public byte* str;
    public nuint len;
}

public static partial class DataLogNative
{
    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_Create", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial DataLogHandle DataLogCreate(string? dir, string? filename, double period, string? extraHeader);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_Create_Func", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial DataLogHandle DataLogCreateFunc(delegate* unmanaged[Cdecl]<void*, byte*, nuint, void> write, void* ptr, double period, string? extraHeader);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_Release")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogRelease(DataLogHandle datalog);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_SetFilename", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogSetFilename(DataLogHandle datalog, string filename);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_Flush")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogFlush(DataLogHandle datalog);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_Pause")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogPause(DataLogHandle datalog);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_Resume")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogResume(DataLogHandle datalog);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_Stop")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogStop(DataLogHandle datalog);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_Start", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial EntryHandle DataLogStart(DataLogHandle datalog, string name, string type, string metadata, ulong timestamp);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_Finish")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogFinish(DataLogHandle datalog, EntryHandle entry, ulong timestamp);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_SetMetadata", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogSetMetadata(DataLogHandle datalog, EntryHandle entry, string metadata, ulong timestamp);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_AppendRaw")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogAppend(DataLogHandle datalog, EntryHandle entry, ReadOnlySpan<byte> raw, nuint len, ulong timestamp);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_AppendBoolean")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogAppend(DataLogHandle datalog, EntryHandle entry, [MarshalAs(UnmanagedType.I4)] bool value, ulong timestamp);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_AppendInteger")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogAppend(DataLogHandle datalog, EntryHandle entry, long value, ulong timestamp);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_AppendFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogAppend(DataLogHandle datalog, EntryHandle entry, float value, ulong timestamp);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_AppendDouble")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogAppend(DataLogHandle datalog, EntryHandle entry, double value, ulong timestamp);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_AppendString")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogAppend(DataLogHandle datalog, EntryHandle entry, byte* value, nuint len, ulong timestamp);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_AppendString")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogAppendStringSpan(DataLogHandle datalog, EntryHandle entry, ReadOnlySpan<byte> value, nuint len, ulong timestamp);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_AppendBooleanArray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogAppend(DataLogHandle datalog, EntryHandle entry, [MarshalUsing(typeof(BoolToIntMarshaller), ElementIndirectionDepth = 1)] ReadOnlySpan<bool> value, nuint len, ulong timestamp);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_AppendIntegerArray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogAppend(DataLogHandle datalog, EntryHandle entry, ReadOnlySpan<long> value, nuint len, ulong timestamp);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_AppendFloatArray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogAppend(DataLogHandle datalog, EntryHandle entry, ReadOnlySpan<float> value, nuint len, ulong timestamp);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_AppendDoubleArray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogAppend(DataLogHandle datalog, EntryHandle entry, ReadOnlySpan<double> value, nuint len, ulong timestamp);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_AppendStringArray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DataLogAppend(DataLogHandle datalog, EntryHandle entry, [MarshalUsing(typeof(DataLogStringMarshaller), ElementIndirectionDepth = 1)] ReadOnlySpan<string> value, nuint len, ulong timestamp);
}
