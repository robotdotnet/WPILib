using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using CsCore.Handles;
using WPIUtil;
using WPIUtil.Marshal;

namespace CsCore.Natives;

public static unsafe partial class CsNative
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

    [LibraryImport("cscore", EntryPoint = "CS_GetSourceVideoMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetSourceVideoMode(CsSource source, out VideoMode mode, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetSourceVideoMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool SetSourceVideoMode(CsSource source, in VideoMode mode, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetSourceVideoModeDiscrete")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool SetSourceVideoMode(CsSource source, PixelFormat pixelFormat, int width, int height, int fps, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetSourcePixelFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool SetSourcePixelFormat(CsSource source, PixelFormat pixelFormat, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetSourceResolution")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool SetSourceResolution(CsSource source, int width, int height, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetSourceFPS")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool SetSourceFps(CsSource source, int fps, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetSourceConfigJson", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool SetSourceConfigJson(CsSource source, string config, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetSourceConfigJson")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(NullTerminatedStringMarshaller<CsStringFree>))]
    public static partial string? GetSourceConfigJson(CsSource source, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_EnumerateSourceVideoModes")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CustomFreeArrayMarshaller<,>), CountElementName = nameof(count))]
    public static partial VideoMode[] EnumerateSourceVideoModes(CsSource source, out int count, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_EnumerateSourceSinks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CustomFreeArrayMarshaller<,>), CountElementName = nameof(count))]
    public static partial CsSink[] EnumerateSourceSinks(CsSource source, out int count, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_CopySource")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial CsSource CopySource(CsSource source, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_ReleaseSource")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ReleaseSource(CsSource source, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetCameraBrightness")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetCameraBrightness(CsSource source, int brightness, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetCameraBrightness")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetCameraBrightness(CsSource source, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetCameraWhiteBalanceAuto")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetCameraWhiteBalanceAuto(CsSource source, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetCameraWhiteBalanceHoldCurrent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetCameraWhiteBalanceHoldCurrent(CsSource source, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetCameraWhiteBalanceManual")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetCameraWhiteBalanceManual(CsSource source, int value, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetCameraExposureAuto")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetCameraExposureAuto(CsSource source, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetCameraExposureHoldCurrent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetCameraExposureHoldCurrent(CsSource source, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetCameraExposureManual")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetCameraExposureManual(CsSource source, int value, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetUsbCameraPath", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetUsbCameraPath(CsSource source, string path, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetUsbCameraPath")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(NullTerminatedStringMarshaller<CsStringFree>))]
    public static partial string? GetUsbCameraPath(CsSource source, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetUsbCameraInfo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial UsbCameraInfo GetUsbCameraInfo(CsSource source, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetHttpCameraKind")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HttpCameraKind GetHttpCameraKind(CsSource source, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetHttpCameraUrls", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetHttpCameraUrls(CsSource source, ReadOnlySpan<string> urls, int count, out StatusValue status);

    public static void SetHttpCameraUrls(CsSource source, ReadOnlySpan<string> urls, out StatusValue status)
    {
        SetHttpCameraUrls(source, urls, urls.Length, out status);
    }

    [LibraryImport("cscore", EntryPoint = "CS_GetHttpCameraUrls")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CsEnumPropertyArrayMarshaller<,>), CountElementName = nameof(count))]
    [return: MarshalUsing(typeof(NullTerminatedStringMarshaller<CsEnumPropertyStringFree>), ElementIndirectionDepth = 1)]
    public static partial string[] GetHttpCameraUrls(CsSource source, out int count, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_NotifySourceError", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void NotifySourceError(CsSource source, string msg, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetSourceConnected")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSourceConnected(CsSource source, [MarshalAs(UnmanagedType.I4)] bool connected, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetSourceDescription", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSourceDescription(CsSource source, string description, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_CreateSourceProperty", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial CsProperty CreateSourceProperty(CsSource source, string name, PropertyKind kind, int minimum, int maximum, int step, int defaultValue, int value, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetSourceEnumPropertyChoices", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetSourceEnumPropertyChoices(CsSource source, CsProperty property, ReadOnlySpan<string> choices, int count, out StatusValue status);

    public static void SetSourceEnumPropertyChoices(CsSource source, CsProperty property, ReadOnlySpan<string> choices, out StatusValue status)
    {
        SetSourceEnumPropertyChoices(source, property, choices, choices.Length, out status);
    }

    [LibraryImport("cscore", EntryPoint = "CS_CreateMjpegServer", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial CsSink CreateMjpegServer(string name, string listenAddress, int port, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_CreateCvSink", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial CsSink CreateCvSink(string name, PixelFormat pixelFormat, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_CreateCvSink", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial CsSink CreateCvSinkCallback(string name, PixelFormat pixelFormat, void* data, delegate* unmanaged[Cdecl]<void*, ulong, void> processFrame, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetSinkKind")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SinkKind GetSinkKind(CsSink sink, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetSinkName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(NullTerminatedStringMarshaller<CsStringFree>))]
    public static partial string GetSinkName(CsSink sink, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetSinkDescription")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(NullTerminatedStringMarshaller<CsStringFree>))]
    public static partial string GetSinkDescription(CsSink sink, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetSinkProperty", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial CsProperty GetSinkProperty(CsSink sink, string name, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_EnumerateSinkProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CustomFreeArrayMarshaller<,>), CountElementName = "count")]
    public static partial CsProperty[] EnumerateSinkProperties(CsSink sink, out int count, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetSinkSource")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSinkSource(CsSink sink, CsSource source, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetSinkSourceProperty", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial CsProperty GetSinkSourceProperty(CsSink sink, string name, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetSinkConfigJson", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool SetSinkConfigJson(CsSink sink, string config, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetSinkConfigJson")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(NullTerminatedStringMarshaller<CsStringFree>))]
    public static partial string? GetSinkConfigJson(CsSink sink, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_CopySink")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial CsSink CopySink(CsSink sink, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_ReleaseSink")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial CsSink ReleaseSink(CsSink sink, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetMjpegServerListenAddress")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(NullTerminatedStringMarshaller<CsStringFree>))]
    public static partial string? GetMjpegServerListenAddress(CsSink sink, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetMjpegServerPort")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetMjpegServerPort(CsSink sink, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetSinkDescription", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSinkDescription(CsSink sink, string description, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetSinkError")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(NullTerminatedStringMarshaller<CsStringFree>))]
    public static partial string? GetSinkError(CsSink sink, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetSinkEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetSinkEnabled(CsSink sink, [MarshalAs(UnmanagedType.I4)] bool enabled, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_CreateListenerPoller")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial CsListenerPoller CreateListenerPoller();

    [LibraryImport("cscore", EntryPoint = "CS_DestroyListenerPoller")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyListenerPoller(CsListenerPoller poller);

    [LibraryImport("cscore", EntryPoint = "CS_AddPolledListener")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial CsListener AddPolledListener(CsListenerPoller poller, EventKind eventMask, [MarshalAs(UnmanagedType.I4)] bool immediateNotify, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_PollListener")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CustomFreeArrayMarshaller<,>), CountElementName = "count")]
    public static partial CsEvent[] PollListener(CsListenerPoller poller, out int count);

    [LibraryImport("cscore", EntryPoint = "CS_PollListenerTimeout")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CustomFreeArrayMarshaller<,>), CountElementName = "count")]
    public static partial CsEvent[] PollListener(CsListenerPoller poller, out int count, double timeout, [MarshalAs(UnmanagedType.I4)] out bool timedOut);

    [LibraryImport("cscore", EntryPoint = "CS_FreeEvents")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeEvents(CsEventMarshaller.NativeCsEvent* arr, int count);

    [LibraryImport("cscore", EntryPoint = "CS_CancelPollListener")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelPollListener(CsListenerPoller poller);

    [LibraryImport("cscore", EntryPoint = "CS_NotifierDestroyed")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool IsNotifierDestroyed();

    [LibraryImport("cscore", EntryPoint = "CS_SetTelemetryPeriod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetTelemetryPeriod(double seconds);

    [LibraryImport("cscore", EntryPoint = "CS_GetTelemetryElapsedTime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double CS_GetTelemetryElapsedTime();

    [LibraryImport("cscore", EntryPoint = "CS_GetTelemetryValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial long GetTelemetryValue(CsSource source, TelemetryKind kind, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetLogger")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetLogger(delegate* unmanaged[Cdecl]<uint, byte*, uint, byte*, void> func, uint min_level);

    [LibraryImport("cscore", EntryPoint = "CS_SetDefaultLogger")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetDefaultLogger(uint min_level);

    [LibraryImport("cscore", EntryPoint = "CS_Shutdown")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CS_Shutdown();

    [LibraryImport("cscore", EntryPoint = "CS_EnumerateUsbCameras")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CustomFreeArrayMarshaller<,>), CountElementName = nameof(count))]
    public static partial UsbCameraInfo[] EnumerateUsbCameras(out int count, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_EnumerateSources")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CustomFreeArrayMarshaller<,>), CountElementName = nameof(count))]
    public static partial CsSource[] EnumerateSources(out int count, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_EnumerateSinks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CustomFreeArrayMarshaller<,>), CountElementName = nameof(count))]
    public static partial CsSink[] EnumerateSinks(out int count, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetHostname")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(NullTerminatedStringMarshaller<CsStringFree>))]
    public static partial string? GetHostname();

    [LibraryImport("cscore", EntryPoint = "CS_GetNetworkInterfaces")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CsEnumPropertyArrayMarshaller<,>), CountElementName = nameof(count))]
    [return: MarshalUsing(typeof(NullTerminatedStringMarshaller<NetworkInterfacesStringFree>), ElementIndirectionDepth = 1)]
    public static partial string[] GetNetworkInterfaces(out int count);
}
