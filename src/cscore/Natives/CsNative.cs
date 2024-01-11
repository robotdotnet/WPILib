using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using CsCore.Handles;
using WPIUtil.Marshal;

namespace CsCore.Natives;

public static unsafe partial class CsNatives
{
    [LibraryImport("cscore", EntryPoint = "CS_GetPropertyKind")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial PropertyKind GetPropertyKind(CsProperty property, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetPropertyKind")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(NullTerminatedStringMarshaller<CsStringFree>))]
    public static partial string? GetPropertyName(CsProperty property, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetProperty(CsProperty property, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetPropertyMin")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetPropertyMin(CsProperty property, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetPropertyMax")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetPropertyMax(CsProperty property, out StatusValue status);


    [LibraryImport("cscore", EntryPoint = "CS_GetPropertyStep")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetPropertyStep(CsProperty property, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetPropertyDefault")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetPropertyDefault(CsProperty property, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetStringProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(NullTerminatedStringMarshaller<CsStringFree>))]
    public static partial string GetStringProperty(CsProperty property, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetStringProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetStringProperty(CsProperty property, [MarshalUsing(typeof(Utf8StringMarshaller))] string value, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetEnumPropertyChoices")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CsEnumPropertyArrayMarshaller<,>), CountElementName = nameof(count))]
    [return: MarshalUsing(typeof(NullTerminatedStringMarshaller<CsEnumPropertyStringFree>), ElementIndirectionDepth = 1)]
    public static partial string[] GetEnumPropertyChoices(CsProperty property, out int count, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_CreateUsbCameraDev", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial CsSource CreateUsbCamera(string name, int dev, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_CreateUsbCameraPath", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial CsSource CreateUsbCamera(string name, string path, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_CreateHttpCamera", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial CsSource CreateHttpCamera(string name, string url, HttpCameraKind kind, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_CreateHttpCameraMulti", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial CsSource CreateHttpCamera(string name, ReadOnlySpan<string> urls, int count, HttpCameraKind kind, out StatusValue status);

    public static CsSource CreateHttpCamera(string name, ReadOnlySpan<string> urls, HttpCameraKind kind, out StatusValue status)
    {
        return CreateHttpCamera(name, urls, urls.Length, kind, out status);
    }

    [LibraryImport("cscore", EntryPoint = "CS_CreateCvSource", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial CsSource CreateCvSource(string name, in VideoMode mode, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetSourceKind")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SourceKind GetSourceKind(CsSource source, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetSourceName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(NullTerminatedStringMarshaller<CsStringFree>))]
    public static partial string GetSourceName(CsSource source, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetSourceDescription")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(NullTerminatedStringMarshaller<CsStringFree>))]
    public static partial string GetSourceDescription(CsSource source, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetSourceLastFrameTime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ulong GetSourceLastFrameTime(CsSource source, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetSourceConnectionStrategy")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSourceConnectionStrategy(CsSource source, ConnectionStrategy strategy, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_IsSourceConnected")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool IsSourceConnected(CsSource source, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_IsSourceEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool IsSourceEnabled(CsSource source, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetSourceProperty", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial CsProperty GetSourceProperty(CsSource source, string name, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_EnumerateSourceProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CustomFreeArrayMarshaller<,>), CountElementName = "count")]
    public static partial CsProperty[] EnumerateSourceProperties(CsSource source, out int count, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_EnumerateSources")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CustomFreeArrayMarshaller<,>), CountElementName = nameof(count))]
    public static partial CsSource[] EnumerateSources(out int count, out StatusValue status);
}
