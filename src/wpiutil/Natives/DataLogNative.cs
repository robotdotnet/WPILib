using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;
using WPIUtil.Marshal;

namespace WPIUtil.Natives;

public struct OpaqueDataLog { }

public static partial class DataLogNative
{
    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_CreateWriter")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial OpaqueDataLog* Create(WpiString dir, WpiString filename);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_CreateBackgroundWriter")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial OpaqueDataLog* CreateBg(WpiString dir, WpiString filename, double period, WpiString extraHeader);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_CreateBackgroundWriter_Func")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial OpaqueDataLog* CreateBgFunc(delegate* unmanaged[Cdecl]<void*, byte*, nuint, void> write, void* ptr, double period, WpiString extraHeader);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_Release")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void Release(OpaqueDataLog* datalog);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_SetFilename")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SetFilename(OpaqueDataLog* datalog, WpiString filename);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_Flush")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void Flush(OpaqueDataLog* datalog);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_Pause")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void Pause(OpaqueDataLog* datalog);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_Resume")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void Resume(OpaqueDataLog* datalog);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_Stop")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void Stop(OpaqueDataLog* datalog);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_Start")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial DataLogEntryHandle Start(OpaqueDataLog* datalog, WpiString name, WpiString type, WpiString metadata, ulong timestamp);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_Finish")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void Finish(OpaqueDataLog* datalog, DataLogEntryHandle entry, ulong timestamp);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_SetMetadata")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SetMetadata(OpaqueDataLog* datalog, DataLogEntryHandle entry, WpiString metadata, ulong timestamp);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_AppendRaw")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static unsafe partial void AppendRaw(OpaqueDataLog* datalog, DataLogEntryHandle entry, ReadOnlySpan<byte> raw, nuint len, ulong timestamp);

    public static unsafe void AppendRaw(OpaqueDataLog* datalog, DataLogEntryHandle entry, ReadOnlySpan<byte> raw, ulong timestamp)
    {
        AppendRaw(datalog, entry, raw, (nuint)raw.Length, timestamp);
    }

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_AppendBoolean")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void AppendBoolean(OpaqueDataLog* datalog, DataLogEntryHandle entry, [MarshalAs(UnmanagedType.I4)] bool value, ulong timestamp);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_AppendInteger")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void AppendInteger(OpaqueDataLog* datalog, DataLogEntryHandle entry, long value, ulong timestamp);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_AppendFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void AppendFloat(OpaqueDataLog* datalog, DataLogEntryHandle entry, float value, ulong timestamp);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_AppendDouble")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void AppendDouble(OpaqueDataLog* datalog, DataLogEntryHandle entry, double value, ulong timestamp);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_AppendString")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void AppendString(OpaqueDataLog* datalog, DataLogEntryHandle entry, WpiString value, ulong timestamp);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_AppendBooleanArray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static unsafe partial void AppendBooleanArray(OpaqueDataLog* datalog, DataLogEntryHandle entry, [MarshalUsing(typeof(BoolToIntMarshaller), ElementIndirectionDepth = 1)] ReadOnlySpan<bool> value, nuint len, ulong timestamp);

    public static unsafe void AppendBooleanArray(OpaqueDataLog* datalog, DataLogEntryHandle entry, [MarshalUsing(typeof(BoolToIntMarshaller), ElementIndirectionDepth = 1)] ReadOnlySpan<bool> value, ulong timestamp)
    {
        AppendBooleanArray(datalog, entry, value, timestamp);
    }

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_AppendIntegerArray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static unsafe partial void AppendIntegerArray(OpaqueDataLog* datalog, DataLogEntryHandle entry, ReadOnlySpan<long> value, nuint len, ulong timestamp);

    public static unsafe void AppendIntegerArray(OpaqueDataLog* datalog, DataLogEntryHandle entry, ReadOnlySpan<long> value, ulong timestamp)
    {
        AppendIntegerArray(datalog, entry, value, timestamp);
    }

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_AppendFloatArray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static unsafe partial void AppendFloatArray(OpaqueDataLog* datalog, DataLogEntryHandle entry, ReadOnlySpan<float> value, nuint len, ulong timestamp);

    public static unsafe void AppendFloatArray(OpaqueDataLog* datalog, DataLogEntryHandle entry, ReadOnlySpan<float> value, ulong timestamp)
    {
        AppendFloatArray(datalog, entry, value, timestamp);
    }

    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_AppendDoubleArray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static unsafe partial void AppendDoubleArray(OpaqueDataLog* datalog, DataLogEntryHandle entry, ReadOnlySpan<double> value, nuint len, ulong timestamp);

    public static unsafe void AppendDoubleArray(OpaqueDataLog* datalog, DataLogEntryHandle entry, ReadOnlySpan<double> value, ulong timestamp)
    {
        AppendDoubleArray(datalog, entry, value, timestamp);
    }


    [LibraryImport("wpiutil", EntryPoint = "WPI_DataLog_AppendStringArray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static unsafe partial void AppendStringArray(OpaqueDataLog* datalog, DataLogEntryHandle entry, [MarshalUsing(typeof(WpiStringMarshaller), ElementIndirectionDepth = 1)] ReadOnlySpan<string> value, nuint len, ulong timestamp);

    public static unsafe void AppendStringArray(OpaqueDataLog* datalog, DataLogEntryHandle entry, ReadOnlySpan<string> value, ulong timestamp)
    {
        AppendStringArray(datalog, entry, value, timestamp);
    }

}
