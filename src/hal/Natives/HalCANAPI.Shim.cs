using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using WPIHal;
using WPIHal.Handles;

namespace WPIHal.Natives;

public static unsafe partial class HalCANAPI
{
    public static HalCANHandle InitializeCAN(CANManufacturer manufacturer, int deviceId, CANDeviceType deviceType, out HalStatus status)
    {
        status = HalStatus.Ok;
        return InitializeCANRefShim(manufacturer, deviceId, deviceType, ref status);
    }
    internal static void ReadCANPacketLatest(HalCANHandle handle, int apiId, Span<byte> data, out int length, out ulong receivedTimestamp, out HalStatus status)
    {
        status = HalStatus.Ok;
        ReadCANPacketLatestRefShim(handle, apiId, data, out length, out receivedTimestamp, ref status);
    }
    internal static void ReadCANPacketNew(HalCANHandle handle, int apiId, Span<byte> data, out int length, out ulong receivedTimestamp, out HalStatus status)
    {
        status = HalStatus.Ok;
        ReadCANPacketNewRefShim(handle, apiId, data, out length, out receivedTimestamp, ref status);
    }
    internal static void ReadCANPacketTimeout(HalCANHandle handle, int apiId, Span<byte> data, out int length, out ulong receivedTimestamp, int timeoutMs, out HalStatus status)
    {
        status = HalStatus.Ok;
        ReadCANPacketTimeoutRefShim(handle, apiId, data, out length, out receivedTimestamp, timeoutMs, ref status);
    }
    public static void StopCANPacketRepeating(HalCANHandle handle, int apiId, out HalStatus status)
    {
        status = HalStatus.Ok;
        StopCANPacketRepeatingRefShim(handle, apiId, ref status);
    }
    internal static void WriteCANPacket(HalCANHandle handle, ReadOnlySpan<byte> data, int length, int apiId, out HalStatus status)
    {
        status = HalStatus.Ok;
        WriteCANPacketRefShim(handle, data, length, apiId, ref status);
    }
    internal static void WriteCANPacketRepeating(HalCANHandle handle, ReadOnlySpan<byte> data, int length, int apiId, int repeatMs, out HalStatus status)
    {
        status = HalStatus.Ok;
        WriteCANPacketRepeatingRefShim(handle, data, length, apiId, repeatMs, ref status);
    }
    public static void WriteCANRTRFrame(HalCANHandle handle, int length, int apiId, out HalStatus status)
    {
        status = HalStatus.Ok;
        WriteCANRTRFrameRefShim(handle, length, apiId, ref status);
    }
}
