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
    internal static partial PropertyKind GetPropertyKindRefShim(CsProperty property, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetPropertyKind")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void GetPropertyNameRefShim(CsProperty property, [MarshalUsing(typeof(WpiStringMarshaller))] out string name, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetPropertyRefShim(CsProperty property, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetPropertyMin")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetPropertyMinRefShim(CsProperty property, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetPropertyMax")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetPropertyMaxRefShim(CsProperty property, ref StatusValue status);


    [LibraryImport("cscore", EntryPoint = "CS_GetPropertyStep")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetPropertyStepRefShim(CsProperty property, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetPropertyDefault")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetPropertyDefaultRefShim(CsProperty property, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetStringProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void GetStringPropertyRefShim(CsProperty property, [MarshalUsing(typeof(WpiStringMarshaller))] out string value, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetStringProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetStringPropertyRefShim(CsProperty property, WpiString value, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetEnumPropertyChoices")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(UnmanagedFreeArrayMarshaller<,>), CountElementName = nameof(count))]
    [return: MarshalUsing(typeof(WpiStringMarshaller), ElementIndirectionDepth = 1)]
    internal static partial string[] GetEnumPropertyChoicesRefShim(CsProperty property, out int count, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_CreateUsbCameraDev")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial CsSource CreateUsbCameraRefShim(WpiString name, int dev, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_CreateUsbCameraPath")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial CsSource CreateUsbCameraRefShim(WpiString name, WpiString path, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_CreateHttpCamera")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial CsSource CreateHttpCameraRefShim(WpiString name, WpiString url, HttpCameraKind kind, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_CreateHttpCameraMulti")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial CsSource CreateHttpCameraRefShim(WpiString name, [MarshalUsing(typeof(WpiStringMarshaller), ElementIndirectionDepth = 1)] ReadOnlySpan<string> urls, int count, HttpCameraKind kind, ref StatusValue status);

    public static CsSource CreateHttpCamera(string name, ReadOnlySpan<string> urls, HttpCameraKind kind, out StatusValue status)
    {
        return CreateHttpCamera(name, urls, urls.Length, kind, out status);
    }

    [LibraryImport("cscore", EntryPoint = "CS_CreateCvSource")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial CsSource CreateCvSourceRefShim(WpiString name, in VideoMode mode, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetSourceKind")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SourceKind GetSourceKindRefShim(CsSource source, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetSourceName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void GetSourceNameRefShim(CsSource source, [MarshalUsing(typeof(WpiStringMarshaller))] out string name, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetSourceDescription")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void GetSourceDescriptionRefShim(CsSource source, [MarshalUsing(typeof(WpiStringMarshaller))] out string description, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetSourceLastFrameTime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial ulong GetSourceLastFrameTimeRefShim(CsSource source, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetSourceConnectionStrategy")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetSourceConnectionStrategyRefShim(CsSource source, ConnectionStrategy strategy, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_IsSourceConnected")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    internal static partial bool IsSourceConnectedRefShim(CsSource source, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_IsSourceEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    internal static partial bool IsSourceEnabledRefShim(CsSource source, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetSourceProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial CsProperty GetSourcePropertyRefShim(CsSource source, WpiString name, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_EnumerateSourceProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(ManagedFreeArrayMarshaller<,>), CountElementName = "count")]
    internal static partial CsProperty[] EnumerateSourcePropertiesRefShim(CsSource source, out int count, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetSourceVideoMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void GetSourceVideoModeRefShim(CsSource source, out VideoMode mode, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetSourceVideoMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    internal static partial bool SetSourceVideoModeRefShim(CsSource source, in VideoMode mode, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetSourceVideoModeDiscrete")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    internal static partial bool SetSourceVideoModeRefShim(CsSource source, PixelFormat pixelFormat, int width, int height, int fps, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetSourcePixelFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    internal static partial bool SetSourcePixelFormatRefShim(CsSource source, PixelFormat pixelFormat, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetSourceResolution")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    internal static partial bool SetSourceResolutionRefShim(CsSource source, int width, int height, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetSourceFPS")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    internal static partial bool SetSourceFpsRefShim(CsSource source, int fps, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetSourceConfigJson")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    internal static partial bool SetSourceConfigJsonRefShim(CsSource source, WpiString config, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetSourceConfigJson")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void GetSourceConfigJsonRefShim(CsSource source, [MarshalUsing(typeof(WpiStringMarshaller))] out string config, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_EnumerateSourceVideoModes")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(ManagedFreeArrayMarshaller<,>), CountElementName = nameof(count))]
    internal static partial VideoMode[] EnumerateSourceVideoModesRefShim(CsSource source, out int count, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_EnumerateSourceSinks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(ManagedFreeArrayMarshaller<,>), CountElementName = nameof(count))]
    internal static partial CsSink[] EnumerateSourceSinksRefShim(CsSource source, out int count, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_CopySource")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial CsSource CopySourceRefShim(CsSource source, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_ReleaseSource")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void ReleaseSourceRefShim(CsSource source, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetCameraBrightness")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetCameraBrightnessRefShim(CsSource source, int brightness, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetCameraBrightness")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetCameraBrightnessRefShim(CsSource source, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetCameraWhiteBalanceAuto")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetCameraWhiteBalanceAutoRefShim(CsSource source, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetCameraWhiteBalanceHoldCurrent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetCameraWhiteBalanceHoldCurrentRefShim(CsSource source, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetCameraWhiteBalanceManual")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetCameraWhiteBalanceManualRefShim(CsSource source, int value, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetCameraExposureAuto")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetCameraExposureAutoRefShim(CsSource source, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetCameraExposureHoldCurrent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetCameraExposureHoldCurrentRefShim(CsSource source, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetCameraExposureManual")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetCameraExposureManualRefShim(CsSource source, int value, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetUsbCameraPath")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetUsbCameraPathRefShim(CsSource source, WpiString path, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetUsbCameraPath")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void GetUsbCameraPathRefShim(CsSource source, [MarshalUsing(typeof(WpiStringMarshaller))] out string path, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetUsbCameraInfo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial UsbCameraInfo GetUsbCameraInfoRefShim(CsSource source, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetHttpCameraKind")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial HttpCameraKind GetHttpCameraKindRefShim(CsSource source, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetHttpCameraUrls")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetHttpCameraUrlsRefShim(CsSource source, [MarshalUsing(typeof(WpiStringMarshaller), ElementIndirectionDepth = 1)] ReadOnlySpan<string> urls, int count, ref StatusValue status);

    public static void SetHttpCameraUrls(CsSource source, ReadOnlySpan<string> urls, out StatusValue status)
    {
        SetHttpCameraUrls(source, urls, urls.Length, out status);
    }

    [LibraryImport("cscore", EntryPoint = "CS_GetHttpCameraUrls")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(UnmanagedFreeArrayMarshaller<,>), CountElementName = nameof(count))]
    [return: MarshalUsing(typeof(WpiStringMarshaller), ElementIndirectionDepth = 1)]
    internal static partial string[] GetHttpCameraUrlsRefShim(CsSource source, out int count, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_NotifySourceError")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void NotifySourceErrorRefShim(CsSource source, WpiString msg, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetSourceConnected")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetSourceConnectedRefShim(CsSource source, [MarshalAs(UnmanagedType.I4)] bool connected, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetSourceDescription")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetSourceDescriptionRefShim(CsSource source, WpiString description, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_CreateSourceProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial CsProperty CreateSourcePropertyRefShim(CsSource source, WpiString name, PropertyKind kind, int minimum, int maximum, int step, int defaultValue, int value, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetSourceEnumPropertyChoices")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetSourceEnumPropertyChoicesRefShim(CsSource source, CsProperty property, [MarshalUsing(typeof(WpiStringMarshaller), ElementIndirectionDepth = 1)] ReadOnlySpan<string> choices, int count, ref StatusValue status);

    public static void SetSourceEnumPropertyChoices(CsSource source, CsProperty property, ReadOnlySpan<string> choices, out StatusValue status)
    {
        SetSourceEnumPropertyChoices(source, property, choices, choices.Length, out status);
    }

    [LibraryImport("cscore", EntryPoint = "CS_CreateMjpegServer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial CsSink CreateMjpegServerRefShim(WpiString name, WpiString listenAddress, int port, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_CreateCvSink")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial CsSink CreateCvSinkRefShim(WpiString name, PixelFormat pixelFormat, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_CreateCvSink")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial CsSink CreateCvSinkCallbackRefShim(WpiString name, PixelFormat pixelFormat, void* data, delegate* unmanaged[Cdecl]<void*, ulong, void> processFrame, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetSinkKind")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SinkKind GetSinkKindRefShim(CsSink sink, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetSinkName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void GetSinkNameRefShim(CsSink sink, [MarshalUsing(typeof(WpiStringMarshaller))] out string name, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetSinkDescription")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void GetSinkDescriptionRefShim(CsSink sink, [MarshalUsing(typeof(WpiStringMarshaller))] out string description, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetSinkProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial CsProperty GetSinkPropertyRefShim(CsSink sink, WpiString name, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_EnumerateSinkProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(ManagedFreeArrayMarshaller<,>), CountElementName = "count")]
    internal static partial CsProperty[] EnumerateSinkPropertiesRefShim(CsSink sink, out int count, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetSinkSource")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetSinkSourceRefShim(CsSink sink, CsSource source, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetSinkSourceProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial CsProperty GetSinkSourcePropertyRefShim(CsSink sink, WpiString name, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetSinkConfigJson")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    internal static partial bool SetSinkConfigJsonRefShim(CsSink sink, WpiString config, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetSinkConfigJson")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void GetSinkConfigJsonRefShim(CsSink sink, [MarshalUsing(typeof(WpiStringMarshaller))] out string config, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_CopySink")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial CsSink CopySinkRefShim(CsSink sink, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_ReleaseSink")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial CsSink ReleaseSinkRefShim(CsSink sink, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetMjpegServerListenAddress")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void GetMjpegServerListenAddressRefShim(CsSink sink, [MarshalUsing(typeof(WpiStringMarshaller))] out string listenAddress, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetMjpegServerPort")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetMjpegServerPortRefShim(CsSink sink, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetSinkDescription")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetSinkDescriptionRefShim(CsSink sink, WpiString description, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetSinkError")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void GetSinkErrorRefShim(CsSink sink, [MarshalUsing(typeof(WpiStringMarshaller))] out string error, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_SetSinkEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int SetSinkEnabledRefShim(CsSink sink, [MarshalAs(UnmanagedType.I4)] bool enabled, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_CreateListenerPoller")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial CsListenerPoller CreateListenerPoller();

    [LibraryImport("cscore", EntryPoint = "CS_DestroyListenerPoller")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyListenerPoller(CsListenerPoller poller);

    [LibraryImport("cscore", EntryPoint = "CS_AddPolledListener")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial CsListener AddPolledListenerRefShim(CsListenerPoller poller, EventKind eventMask, [MarshalAs(UnmanagedType.I4)] bool immediateNotify, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_PollListener")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(ManagedFreeArrayMarshaller<,>), CountElementName = "count")]
    public static partial CsEvent[] PollListener(CsListenerPoller poller, out int count);

    [LibraryImport("cscore", EntryPoint = "CS_PollListenerTimeout")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(ManagedFreeArrayMarshaller<,>), CountElementName = "count")]
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
    public static partial double GetTelemetryElapsedTime();

    [LibraryImport("cscore", EntryPoint = "CS_GetTelemetryValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial long GetTelemetryValueRefShim(CsSource source, TelemetryKind kind, ref StatusValue status);

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
    [return: MarshalUsing(typeof(ManagedFreeArrayMarshaller<,>), CountElementName = nameof(count))]
    internal static partial UsbCameraInfo[] EnumerateUsbCamerasRefShim(out int count, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_EnumerateSources")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(ManagedFreeArrayMarshaller<,>), CountElementName = nameof(count))]
    internal static partial CsSource[] EnumerateSourcesRefShim(out int count, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_EnumerateSinks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(ManagedFreeArrayMarshaller<,>), CountElementName = nameof(count))]
    internal static partial CsSink[] EnumerateSinksRefShim(out int count, ref StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetHostname")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetHostname([MarshalUsing(typeof(WpiStringMarshaller))] out string value);

    [LibraryImport("cscore", EntryPoint = "CS_GetNetworkInterfaces")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(UnmanagedFreeArrayMarshaller<,>), CountElementName = nameof(count))]
    [return: MarshalUsing(typeof(WpiStringMarshaller), ElementIndirectionDepth = 1)]
    public static partial string[] GetNetworkInterfaces(out int count);
}
