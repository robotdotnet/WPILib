using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using CsCore.Handles;
using WPIUtil;
using WPIUtil.Marshal;

namespace CsCore.Natives;

public static partial class CsNative
{
    private const string StatusCheckCall = "global::CsCore.StatusValueExtensions.ThrowIfFailed({0});";
    private const string LibraryName = "cscore";

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_GetPropertyKind")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial PropertyKind GetPropertyKind(CsProperty property, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_GetPropertyKind")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetPropertyName(CsProperty property, [MarshalUsing(typeof(WpiStringMarshaller))] out string name, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_GetProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetProperty(CsProperty property, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_SetProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetProperty(CsProperty property, int value, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_GetPropertyMin")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetPropertyMin(CsProperty property, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_GetPropertyMax")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetPropertyMax(CsProperty property, out StatusValue status);


    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_GetPropertyStep")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetPropertyStep(CsProperty property, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_GetPropertyDefault")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetPropertyDefault(CsProperty property, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_GetStringProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetStringProperty(CsProperty property, [MarshalUsing(typeof(WpiStringMarshaller))] out string value, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_SetStringProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetStringProperty(CsProperty property, WpiString value, out StatusValue status);

    [LibraryImport(LibraryName, EntryPoint = "CS_GetEnumPropertyChoices")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(UnmanagedFreeArrayMarshaller<,>), CountElementName = nameof(count))]
    [return: MarshalUsing(typeof(WpiStringMarshaller), ElementIndirectionDepth = 1)]
    public static partial string[] GetEnumPropertyChoices(CsProperty property, out int count, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    public static string[] GetEnumPropertyChoices(CsProperty property, out StatusValue status)
    {
        return GetEnumPropertyChoices(property, out _, out status);
    }

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_CreateUsbCameraDev")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial CsSource CreateUsbCamera(WpiString name, int dev, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_CreateUsbCameraPath")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial CsSource CreateUsbCamera(WpiString name, WpiString path, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_CreateHttpCamera")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial CsSource CreateHttpCamera(WpiString name, WpiString url, HttpCameraKind kind, out StatusValue status);

    [LibraryImport(LibraryName, EntryPoint = "CS_CreateHttpCameraMulti")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial CsSource CreateHttpCamera(WpiString name, [MarshalUsing(typeof(WpiStringMarshaller), ElementIndirectionDepth = 1)] ReadOnlySpan<string> urls, int count, HttpCameraKind kind, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    public static CsSource CreateHttpCamera(string name, ReadOnlySpan<string> urls, HttpCameraKind kind, out StatusValue status)
    {
        return CreateHttpCamera(name, urls, urls.Length, kind, out status);
    }

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_CreateCvSource")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial CsSource CreateCvSource(WpiString name, in VideoMode mode, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_GetSourceKind")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SourceKind GetSourceKind(CsSource source, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_GetSourceName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetSourceName(CsSource source, [MarshalUsing(typeof(WpiStringMarshaller))] out string name, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_GetSourceDescription")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetSourceDescription(CsSource source, [MarshalUsing(typeof(WpiStringMarshaller))] out string description, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_GetSourceLastFrameTime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ulong GetSourceLastFrameTime(CsSource source, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_SetSourceConnectionStrategy")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSourceConnectionStrategy(CsSource source, ConnectionStrategy strategy, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_IsSourceConnected")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool IsSourceConnected(CsSource source, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_IsSourceEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool IsSourceEnabled(CsSource source, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_GetSourceProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial CsProperty GetSourceProperty(CsSource source, WpiString name, out StatusValue status);

    [LibraryImport(LibraryName, EntryPoint = "CS_EnumerateSourceProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(ManagedFreeArrayMarshaller<,>), CountElementName = "count")]
    public static partial CsProperty[] EnumerateSourceProperties(CsSource source, out int count, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    public static CsProperty[] EnumerateSourceProperties(CsSource source, out StatusValue status)
    {
        return EnumerateSourceProperties(source, out _, out status);
    }

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_GetSourceVideoMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetSourceVideoMode(CsSource source, out VideoMode mode, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_SetSourceVideoMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool SetSourceVideoMode(CsSource source, in VideoMode mode, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_SetSourceVideoModeDiscrete")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool SetSourceVideoMode(CsSource source, PixelFormat pixelFormat, int width, int height, int fps, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_SetSourcePixelFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool SetSourcePixelFormat(CsSource source, PixelFormat pixelFormat, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_SetSourceResolution")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool SetSourceResolution(CsSource source, int width, int height, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_SetSourceFPS")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool SetSourceFps(CsSource source, int fps, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_SetSourceConfigJson")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool SetSourceConfigJson(CsSource source, WpiString config, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_GetSourceConfigJson")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetSourceConfigJson(CsSource source, [MarshalUsing(typeof(WpiStringMarshaller))] out string config, out StatusValue status);

    [LibraryImport(LibraryName, EntryPoint = "CS_EnumerateSourceVideoModes")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(ManagedFreeArrayMarshaller<,>), CountElementName = nameof(count))]
    public static partial VideoMode[] EnumerateSourceVideoModes(CsSource source, out int count, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    public static VideoMode[] EnumerateSourceVideoModes(CsSource source, out StatusValue status)
    {
        return EnumerateSourceVideoModes(source, out status);
    }

    [LibraryImport(LibraryName, EntryPoint = "CS_EnumerateSourceSinks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(ManagedFreeArrayMarshaller<,>), CountElementName = nameof(count))]
    public static partial CsSink[] EnumerateSourceSinks(CsSource source, out int count, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    public static CsSink[] EnumerateSourceSinks(CsSource source, out StatusValue status)
    {
        return EnumerateSourceSinks(source, out _, out status);
    }

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_CopySource")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial CsSource CopySource(CsSource source, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_ReleaseSource")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ReleaseSource(CsSource source, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_SetCameraBrightness")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetCameraBrightness(CsSource source, int brightness, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_GetCameraBrightness")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetCameraBrightness(CsSource source, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_SetCameraWhiteBalanceAuto")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetCameraWhiteBalanceAuto(CsSource source, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_SetCameraWhiteBalanceHoldCurrent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetCameraWhiteBalanceHoldCurrent(CsSource source, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_SetCameraWhiteBalanceManual")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetCameraWhiteBalanceManual(CsSource source, int value, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_SetCameraExposureAuto")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetCameraExposureAuto(CsSource source, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_SetCameraExposureHoldCurrent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetCameraExposureHoldCurrent(CsSource source, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_SetCameraExposureManual")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetCameraExposureManual(CsSource source, int value, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_SetUsbCameraPath")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetUsbCameraPath(CsSource source, WpiString path, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_GetUsbCameraPath")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetUsbCameraPath(CsSource source, [MarshalUsing(typeof(WpiStringMarshaller))] out string path, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_GetUsbCameraInfo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial UsbCameraInfo GetUsbCameraInfo(CsSource source, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_GetHttpCameraKind")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HttpCameraKind GetHttpCameraKind(CsSource source, out StatusValue status);

    [LibraryImport(LibraryName, EntryPoint = "CS_SetHttpCameraUrls")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetHttpCameraUrls(CsSource source, [MarshalUsing(typeof(WpiStringMarshaller), ElementIndirectionDepth = 1)] ReadOnlySpan<string> urls, int count, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    public static void SetHttpCameraUrls(CsSource source, ReadOnlySpan<string> urls, out StatusValue status)
    {
        SetHttpCameraUrls(source, urls, urls.Length, out status);
    }

    [LibraryImport(LibraryName, EntryPoint = "CS_GetHttpCameraUrls")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(UnmanagedFreeArrayMarshaller<,>), CountElementName = nameof(count))]
    [return: MarshalUsing(typeof(WpiStringMarshaller), ElementIndirectionDepth = 1)]
    public static partial string[] GetHttpCameraUrls(CsSource source, out int count, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    public static string[] GetHttpCameraUrls(CsSource source, out StatusValue status)
    {
        return GetHttpCameraUrls(source, out _, out status);
    }

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_NotifySourceError")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void NotifySourceError(CsSource source, WpiString msg, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_SetSourceConnected")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSourceConnected(CsSource source, [MarshalAs(UnmanagedType.I4)] bool connected, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_SetSourceDescription")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSourceDescription(CsSource source, WpiString description, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_CreateSourceProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial CsProperty CreateSourceProperty(CsSource source, WpiString name, PropertyKind kind, int minimum, int maximum, int step, int defaultValue, int value, out StatusValue status);

    [LibraryImport(LibraryName, EntryPoint = "CS_SetSourceEnumPropertyChoices")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSourceEnumPropertyChoices(CsSource source, CsProperty property, [MarshalUsing(typeof(WpiStringMarshaller), ElementIndirectionDepth = 1)] ReadOnlySpan<string> choices, int count, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    public static void SetSourceEnumPropertyChoices(CsSource source, CsProperty property, ReadOnlySpan<string> choices, out StatusValue status)
    {
        SetSourceEnumPropertyChoices(source, property, choices, choices.Length, out status);
    }

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_CreateMjpegServer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial CsSink CreateMjpegServer(WpiString name, WpiString listenAddress, int port, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_CreateCvSink")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial CsSink CreateCvSink(WpiString name, PixelFormat pixelFormat, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_CreateCvSink")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial CsSink CreateCvSinkCallback(WpiString name, PixelFormat pixelFormat, void* data, delegate* unmanaged[Cdecl]<void*, ulong, void> processFrame, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_GetSinkKind")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SinkKind GetSinkKind(CsSink sink, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_GetSinkName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetSinkName(CsSink sink, [MarshalUsing(typeof(WpiStringMarshaller))] out string name, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_GetSinkDescription")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetSinkDescription(CsSink sink, [MarshalUsing(typeof(WpiStringMarshaller))] out string description, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_GetSinkProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial CsProperty GetSinkProperty(CsSink sink, WpiString name, out StatusValue status);

    [LibraryImport(LibraryName, EntryPoint = "CS_EnumerateSinkProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(ManagedFreeArrayMarshaller<,>), CountElementName = "count")]
    public static partial CsProperty[] EnumerateSinkProperties(CsSink sink, out int count, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    public static CsProperty[] EnumerateSinkProperties(CsSink sink, out StatusValue status)
    {
        return EnumerateSinkProperties(sink, out _, out status);
    }

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_SetSinkSource")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSinkSource(CsSink sink, CsSource source, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_GetSinkSourceProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial CsProperty GetSinkSourceProperty(CsSink sink, WpiString name, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_SetSinkConfigJson")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool SetSinkConfigJson(CsSink sink, WpiString config, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_GetSinkConfigJson")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetSinkConfigJson(CsSink sink, [MarshalUsing(typeof(WpiStringMarshaller))] out string config, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_GetSinkSource")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial CsSource GetSinkSource(CsSink sink, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_CopySink")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial CsSink CopySink(CsSink sink, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_ReleaseSink")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial CsSink ReleaseSink(CsSink sink, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_GetMjpegServerListenAddress")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetMjpegServerListenAddress(CsSink sink, [MarshalUsing(typeof(WpiStringMarshaller))] out string listenAddress, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_GetMjpegServerPort")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetMjpegServerPort(CsSink sink, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_SetSinkDescription")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSinkDescription(CsSink sink, WpiString description, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_GetSinkError")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetSinkError(CsSink sink, [MarshalUsing(typeof(WpiStringMarshaller))] out string error, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_SetSinkEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetSinkEnabled(CsSink sink, [MarshalAs(UnmanagedType.I4)] bool enabled, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_CreateListenerPoller")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial CsListenerPoller CreateListenerPoller();

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_DestroyListenerPoller")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyListenerPoller(CsListenerPoller poller);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_AddPolledListener")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial CsListener AddPolledListener(CsListenerPoller poller, EventKind eventMask, [MarshalAs(UnmanagedType.I4)] bool immediateNotify, out StatusValue status);

    [LibraryImport(LibraryName, EntryPoint = "CS_PollListener")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(ManagedFreeArrayMarshaller<,>), CountElementName = "count")]
    public static partial VideoEvent[] PollListener(CsListenerPoller poller, out int count);

    public static VideoEvent[] PollListener(CsListenerPoller poller)
    {
        return PollListener(poller, out _);
    }

    [LibraryImport(LibraryName, EntryPoint = "CS_PollListenerTimeout")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(ManagedFreeArrayMarshaller<,>), CountElementName = "count")]
    public static partial VideoEvent[] PollListener(CsListenerPoller poller, out int count, double timeout, [MarshalAs(UnmanagedType.I4)] out bool timedOut);

    public static VideoEvent[] PollListener(CsListenerPoller poller, double timeout, [MarshalAs(UnmanagedType.I4)] out bool timedOut)
    {
        return PollListener(poller, out _, timeout, out timedOut);
    }

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_FreeEvents")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void FreeEvents(VideoEventMarshaller.NativeCsEvent* arr, int count);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_CancelPollListener")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelPollListener(CsListenerPoller poller);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_RemoveListener")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RemoveListener(CsListener listener, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_NotifierDestroyed")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool IsNotifierDestroyed();

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_SetTelemetryPeriod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetTelemetryPeriod(double seconds);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_GetTelemetryElapsedTime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetTelemetryElapsedTime();

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_GetTelemetryValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long GetTelemetryValue(CsSource source, TelemetryKind kind, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_GetTelemetryAverageValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long GetTelemetryAverageValue(CsSource source, TelemetryKind kind, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_SetLogger")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SetLogger(delegate* unmanaged[Cdecl]<uint, byte*, uint, byte*, void> func, uint minLevel);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_SetDefaultLogger")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetDefaultLogger(uint minLevel);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_Shutdown")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Shutdown();

    [LibraryImport(LibraryName, EntryPoint = "CS_EnumerateUsbCameras")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(ManagedFreeArrayMarshaller<,>), CountElementName = nameof(count))]
    public static partial UsbCameraInfo[] EnumerateUsbCameras(out int count, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    public static UsbCameraInfo[] EnumerateUsbCameras(out StatusValue status)
    {
        return EnumerateUsbCameras(out _, out status);
    }


    [LibraryImport(LibraryName, EntryPoint = "CS_EnumerateSources")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(ManagedFreeArrayMarshaller<,>), CountElementName = nameof(count))]
    public static partial CsSource[] EnumerateSources(out int count, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    public static CsSource[] EnumerateSources(out StatusValue status)
    {
        return EnumerateSources(out _, out status);
    }

    [LibraryImport(LibraryName, EntryPoint = "CS_EnumerateSinks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(ManagedFreeArrayMarshaller<,>), CountElementName = nameof(count))]
    public static partial CsSink[] EnumerateSinks(out int count, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    public static CsSink[] EnumerateSinks(out StatusValue status)
    {
        return EnumerateSinks(out _, out status);
    }

    [LibraryImport(LibraryName, EntryPoint = "CS_GetHostname")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetHostname([MarshalUsing(typeof(WpiStringMarshaller))] out string value);

    [LibraryImport(LibraryName, EntryPoint = "CS_GetNetworkInterfaces")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(UnmanagedFreeArrayMarshaller<,>), CountElementName = nameof(count))]
    [return: MarshalUsing(typeof(WpiStringMarshaller), ElementIndirectionDepth = 1)]
    public static partial string[] GetNetworkInterfaces(out int count);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_GrabRawSinkFrame")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ulong GrabRawSinkFrame(CsSink sink, RawFrameReader rawImage, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_GrabRawSinkFrameTimeout")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ulong GrabRawSinkFrame(CsSink sink, RawFrameReader rawImage, double timeout, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_CreateRawSink")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial CsSink CreateRawSink(WpiString name, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_PutRawSourceFrame")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void PutRawSourceFrame(CsSource source, RawFrameWriter rawImage, out StatusValue status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport(LibraryName, EntryPoint = "CS_CreateRawSource")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial CsSource CreateRawSource(WpiString name, in VideoMode mode, out StatusValue status);
}
