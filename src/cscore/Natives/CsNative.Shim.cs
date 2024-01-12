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
    public static PropertyKind GetPropertyKind(CsProperty property, out StatusValue status)
    {
        status = StatusValue.Ok;
        return GetPropertyKindRefShim(property, ref status);
    }
    public static string? GetPropertyName(CsProperty property, out StatusValue status)
    {
        status = StatusValue.Ok;
        return GetPropertyNameRefShim(property, ref status);
    }
    public static int GetProperty(CsProperty property, out StatusValue status)
    {
        status = StatusValue.Ok;
        return GetPropertyRefShim(property, ref status);
    }
    public static int GetPropertyMin(CsProperty property, out StatusValue status)
    {
        status = StatusValue.Ok;
        return GetPropertyMinRefShim(property, ref status);
    }
    public static int GetPropertyMax(CsProperty property, out StatusValue status)
    {
        status = StatusValue.Ok;
        return GetPropertyMaxRefShim(property, ref status);
    }
    public static int GetPropertyStep(CsProperty property, out StatusValue status)
    {
        status = StatusValue.Ok;
        return GetPropertyStepRefShim(property, ref status);
    }
    public static int GetPropertyDefault(CsProperty property, out StatusValue status)
    {
        status = StatusValue.Ok;
        return GetPropertyDefaultRefShim(property, ref status);
    }
    public static string GetStringProperty(CsProperty property, out StatusValue status)
    {
        status = StatusValue.Ok;
        return GetStringPropertyRefShim(property, ref status);
    }
    public static void SetStringProperty(CsProperty property, [MarshalUsing(typeof(Utf8StringMarshaller))] string value, out StatusValue status)
    {
        status = StatusValue.Ok;
        SetStringPropertyRefShim(property, value, ref status);
    }
    public static string[] GetEnumPropertyChoices(CsProperty property, out int count, out StatusValue status)
    {
        status = StatusValue.Ok;
        return GetEnumPropertyChoicesRefShim(property, out count, ref status);
    }
    public static CsSource CreateUsbCamera(string name, int dev, out StatusValue status)
    {
        status = StatusValue.Ok;
        return CreateUsbCameraRefShim(name, dev, ref status);
    }
    public static CsSource CreateUsbCamera(string name, string path, out StatusValue status)
    {
        status = StatusValue.Ok;
        return CreateUsbCameraRefShim(name, path, ref status);
    }
    public static CsSource CreateHttpCamera(string name, string url, HttpCameraKind kind, out StatusValue status)
    {
        status = StatusValue.Ok;
        return CreateHttpCameraRefShim(name, url, kind, ref status);
    }
    internal static CsSource CreateHttpCamera(string name, ReadOnlySpan<string> urls, int count, HttpCameraKind kind, out StatusValue status)
    {
        status = StatusValue.Ok;
        return CreateHttpCameraRefShim(name, urls, count, kind, ref status);
    }
    public static CsSource CreateCvSource(string name, in VideoMode mode, out StatusValue status)
    {
        status = StatusValue.Ok;
        return CreateCvSourceRefShim(name, mode, ref status);
    }
    public static SourceKind GetSourceKind(CsSource source, out StatusValue status)
    {
        status = StatusValue.Ok;
        return GetSourceKindRefShim(source, ref status);
    }
    public static string GetSourceName(CsSource source, out StatusValue status)
    {
        status = StatusValue.Ok;
        return GetSourceNameRefShim(source, ref status);
    }
    public static string GetSourceDescription(CsSource source, out StatusValue status)
    {
        status = StatusValue.Ok;
        return GetSourceDescriptionRefShim(source, ref status);
    }
    public static ulong GetSourceLastFrameTime(CsSource source, out StatusValue status)
    {
        status = StatusValue.Ok;
        return GetSourceLastFrameTimeRefShim(source, ref status);
    }
    public static void SetSourceConnectionStrategy(CsSource source, ConnectionStrategy strategy, out StatusValue status)
    {
        status = StatusValue.Ok;
        SetSourceConnectionStrategyRefShim(source, strategy, ref status);
    }
    public static bool IsSourceConnected(CsSource source, out StatusValue status)
    {
        status = StatusValue.Ok;
        return IsSourceConnectedRefShim(source, ref status);
    }
    public static bool IsSourceEnabled(CsSource source, out StatusValue status)
    {
        status = StatusValue.Ok;
        return IsSourceEnabledRefShim(source, ref status);
    }
    public static CsProperty GetSourceProperty(CsSource source, string name, out StatusValue status)
    {
        status = StatusValue.Ok;
        return GetSourcePropertyRefShim(source, name, ref status);
    }
    public static CsProperty[] EnumerateSourceProperties(CsSource source, out int count, out StatusValue status)
    {
        status = StatusValue.Ok;
        return EnumerateSourcePropertiesRefShim(source, out count, ref status);
    }
    public static void GetSourceVideoMode(CsSource source, out VideoMode mode, out StatusValue status)
    {
        status = StatusValue.Ok;
        GetSourceVideoModeRefShim(source, out mode, ref status);
    }
    public static bool SetSourceVideoMode(CsSource source, in VideoMode mode, out StatusValue status)
    {
        status = StatusValue.Ok;
        return SetSourceVideoModeRefShim(source, mode, ref status);
    }
    public static bool SetSourceVideoMode(CsSource source, PixelFormat pixelFormat, int width, int height, int fps, out StatusValue status)
    {
        status = StatusValue.Ok;
        return SetSourceVideoModeRefShim(source, pixelFormat, width, height, fps, ref status);
    }
    public static bool SetSourcePixelFormat(CsSource source, PixelFormat pixelFormat, out StatusValue status)
    {
        status = StatusValue.Ok;
        return SetSourcePixelFormatRefShim(source, pixelFormat, ref status);
    }
    public static bool SetSourceResolution(CsSource source, int width, int height, out StatusValue status)
    {
        status = StatusValue.Ok;
        return SetSourceResolutionRefShim(source, width, height, ref status);
    }
    public static bool SetSourceFps(CsSource source, int fps, out StatusValue status)
    {
        status = StatusValue.Ok;
        return SetSourceFpsRefShim(source, fps, ref status);
    }
    public static bool SetSourceConfigJson(CsSource source, string config, out StatusValue status)
    {
        status = StatusValue.Ok;
        return SetSourceConfigJsonRefShim(source, config, ref status);
    }
    public static string? GetSourceConfigJson(CsSource source, out StatusValue status)
    {
        status = StatusValue.Ok;
        return GetSourceConfigJsonRefShim(source, ref status);
    }
    public static VideoMode[] EnumerateSourceVideoModes(CsSource source, out int count, out StatusValue status)
    {
        status = StatusValue.Ok;
        return EnumerateSourceVideoModesRefShim(source, out count, ref status);
    }
    public static CsSink[] EnumerateSourceSinks(CsSource source, out int count, out StatusValue status)
    {
        status = StatusValue.Ok;
        return EnumerateSourceSinksRefShim(source, out count, ref status);
    }
    public static CsSource CopySource(CsSource source, out StatusValue status)
    {
        status = StatusValue.Ok;
        return CopySourceRefShim(source, ref status);
    }
    public static void ReleaseSource(CsSource source, out StatusValue status)
    {
        status = StatusValue.Ok;
        ReleaseSourceRefShim(source, ref status);
    }
    public static void SetCameraBrightness(CsSource source, int brightness, out StatusValue status)
    {
        status = StatusValue.Ok;
        SetCameraBrightnessRefShim(source, brightness, ref status);
    }
    public static int GetCameraBrightness(CsSource source, out StatusValue status)
    {
        status = StatusValue.Ok;
        return GetCameraBrightnessRefShim(source, ref status);
    }
    public static void SetCameraWhiteBalanceAuto(CsSource source, out StatusValue status)
    {
        status = StatusValue.Ok;
        SetCameraWhiteBalanceAutoRefShim(source, ref status);
    }
    public static void SetCameraWhiteBalanceHoldCurrent(CsSource source, out StatusValue status)
    {
        status = StatusValue.Ok;
        SetCameraWhiteBalanceHoldCurrentRefShim(source, ref status);
    }
    public static void SetCameraWhiteBalanceManual(CsSource source, int value, out StatusValue status)
    {
        status = StatusValue.Ok;
        SetCameraWhiteBalanceManualRefShim(source, value, ref status);
    }
    public static void SetCameraExposureAuto(CsSource source, out StatusValue status)
    {
        status = StatusValue.Ok;
        SetCameraExposureAutoRefShim(source, ref status);
    }
    public static void SetCameraExposureHoldCurrent(CsSource source, out StatusValue status)
    {
        status = StatusValue.Ok;
        SetCameraExposureHoldCurrentRefShim(source, ref status);
    }
    public static void SetCameraExposureManual(CsSource source, int value, out StatusValue status)
    {
        status = StatusValue.Ok;
        SetCameraExposureManualRefShim(source, value, ref status);
    }
    public static void SetUsbCameraPath(CsSource source, string path, out StatusValue status)
    {
        status = StatusValue.Ok;
        SetUsbCameraPathRefShim(source, path, ref status);
    }
    public static string? GetUsbCameraPath(CsSource source, out StatusValue status)
    {
        status = StatusValue.Ok;
        return GetUsbCameraPathRefShim(source, ref status);
    }
    public static UsbCameraInfo GetUsbCameraInfo(CsSource source, out StatusValue status)
    {
        status = StatusValue.Ok;
        return GetUsbCameraInfoRefShim(source, ref status);
    }
    public static HttpCameraKind GetHttpCameraKind(CsSource source, out StatusValue status)
    {
        status = StatusValue.Ok;
        return GetHttpCameraKindRefShim(source, ref status);
    }
    internal static void SetHttpCameraUrls(CsSource source, ReadOnlySpan<string> urls, int count, out StatusValue status)
    {
        status = StatusValue.Ok;
        SetHttpCameraUrlsRefShim(source, urls, count, ref status);
    }
    public static string[] GetHttpCameraUrls(CsSource source, out int count, out StatusValue status)
    {
        status = StatusValue.Ok;
        return GetHttpCameraUrlsRefShim(source, out count, ref status);
    }
    public static void NotifySourceError(CsSource source, string msg, out StatusValue status)
    {
        status = StatusValue.Ok;
        NotifySourceErrorRefShim(source, msg, ref status);
    }
    public static void SetSourceConnected(CsSource source, bool connected, out StatusValue status)
    {
        status = StatusValue.Ok;
        SetSourceConnectedRefShim(source, connected, ref status);
    }
    public static void SetSourceDescription(CsSource source, string description, out StatusValue status)
    {
        status = StatusValue.Ok;
        SetSourceDescriptionRefShim(source, description, ref status);
    }
    public static CsProperty CreateSourceProperty(CsSource source, string name, PropertyKind kind, int minimum, int maximum, int step, int defaultValue, int value, out StatusValue status)
    {
        status = StatusValue.Ok;
        return CreateSourcePropertyRefShim(source, name, kind, minimum, maximum, step, defaultValue, value, ref status);
    }
    internal static void SetSourceEnumPropertyChoices(CsSource source, CsProperty property, ReadOnlySpan<string> choices, int count, out StatusValue status)
    {
        status = StatusValue.Ok;
        SetSourceEnumPropertyChoicesRefShim(source, property, choices, count, ref status);
    }
    public static CsSink CreateMjpegServer(string name, string listenAddress, int port, out StatusValue status)
    {
        status = StatusValue.Ok;
        return CreateMjpegServerRefShim(name, listenAddress, port, ref status);
    }
    public static CsSink CreateCvSink(string name, PixelFormat pixelFormat, out StatusValue status)
    {
        status = StatusValue.Ok;
        return CreateCvSinkRefShim(name, pixelFormat, ref status);
    }
    public static CsSink CreateCvSinkCallback(string name, PixelFormat pixelFormat, void* data, delegate* unmanaged[Cdecl]<void*, ulong, void> processFrame, out StatusValue status)
    {
        status = StatusValue.Ok;
        return CreateCvSinkCallbackRefShim(name, pixelFormat, data, processFrame, ref status);
    }
    public static SinkKind GetSinkKind(CsSink sink, out StatusValue status)
    {
        status = StatusValue.Ok;
        return GetSinkKindRefShim(sink, ref status);
    }
    public static string GetSinkName(CsSink sink, out StatusValue status)
    {
        status = StatusValue.Ok;
        return GetSinkNameRefShim(sink, ref status);
    }
    public static string GetSinkDescription(CsSink sink, out StatusValue status)
    {
        status = StatusValue.Ok;
        return GetSinkDescriptionRefShim(sink, ref status);
    }
    public static CsProperty GetSinkProperty(CsSink sink, string name, out StatusValue status)
    {
        status = StatusValue.Ok;
        return GetSinkPropertyRefShim(sink, name, ref status);
    }
    public static CsProperty[] EnumerateSinkProperties(CsSink sink, out int count, out StatusValue status)
    {
        status = StatusValue.Ok;
        return EnumerateSinkPropertiesRefShim(sink, out count, ref status);
    }
    public static void SetSinkSource(CsSink sink, CsSource source, out StatusValue status)
    {
        status = StatusValue.Ok;
        SetSinkSourceRefShim(sink, source, ref status);
    }
    public static CsProperty GetSinkSourceProperty(CsSink sink, string name, out StatusValue status)
    {
        status = StatusValue.Ok;
        return GetSinkSourcePropertyRefShim(sink, name, ref status);
    }
    public static bool SetSinkConfigJson(CsSink sink, string config, out StatusValue status)
    {
        status = StatusValue.Ok;
        return SetSinkConfigJsonRefShim(sink, config, ref status);
    }
    public static string? GetSinkConfigJson(CsSink sink, out StatusValue status)
    {
        status = StatusValue.Ok;
        return GetSinkConfigJsonRefShim(sink, ref status);
    }
    public static CsSink CopySink(CsSink sink, out StatusValue status)
    {
        status = StatusValue.Ok;
        return CopySinkRefShim(sink, ref status);
    }
    public static CsSink ReleaseSink(CsSink sink, out StatusValue status)
    {
        status = StatusValue.Ok;
        return ReleaseSinkRefShim(sink, ref status);
    }
    public static string? GetMjpegServerListenAddress(CsSink sink, out StatusValue status)
    {
        status = StatusValue.Ok;
        return GetMjpegServerListenAddressRefShim(sink, ref status);
    }
    public static int GetMjpegServerPort(CsSink sink, out StatusValue status)
    {
        status = StatusValue.Ok;
        return GetMjpegServerPortRefShim(sink, ref status);
    }
    public static void SetSinkDescription(CsSink sink, string description, out StatusValue status)
    {
        status = StatusValue.Ok;
        SetSinkDescriptionRefShim(sink, description, ref status);
    }
    public static string? GetSinkError(CsSink sink, out StatusValue status)
    {
        status = StatusValue.Ok;
        return GetSinkErrorRefShim(sink, ref status);
    }
    public static int SetSinkEnabled(CsSink sink, bool enabled, out StatusValue status)
    {
        status = StatusValue.Ok;
        return SetSinkEnabledRefShim(sink, enabled, ref status);
    }
    public static CsListener AddPolledListener(CsListenerPoller poller, EventKind eventMask, bool immediateNotify, out StatusValue status)
    {
        status = StatusValue.Ok;
        return AddPolledListenerRefShim(poller, eventMask, immediateNotify, ref status);
    }
    internal static long GetTelemetryValue(CsSource source, TelemetryKind kind, out StatusValue status)
    {
        status = StatusValue.Ok;
        return GetTelemetryValueRefShim(source, kind, ref status);
    }
    public static UsbCameraInfo[] EnumerateUsbCameras(out int count, out StatusValue status)
    {
        status = StatusValue.Ok;
        return EnumerateUsbCamerasRefShim(out count, ref status);
    }
    public static CsSource[] EnumerateSources(out int count, out StatusValue status)
    {
        status = StatusValue.Ok;
        return EnumerateSourcesRefShim(out count, ref status);
    }
    public static CsSink[] EnumerateSinks(out int count, out StatusValue status)
    {
        status = StatusValue.Ok;
        return EnumerateSinksRefShim(out count, ref status);
    }
}
