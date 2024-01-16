using System;
using WPIHal.Handles;

namespace WPIHal.Natives;

public static unsafe partial class HalAddressableLED
{
    public static HalAddressableLEDHandle InitializeAddressableLED(HalDigitalHandle outputPort, out HalStatus status)
    {
        status = HalStatus.Ok;
        return InitializeAddressableLEDRefShim(outputPort, ref status);
    }
    public static void SetAddressableLEDBitTiming(HalAddressableLEDHandle handle, int highTime0NanoSeconds, int lowTime0NanoSeconds, int highTime1NanoSeconds, int lowTime1NanoSeconds, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetAddressableLEDBitTimingRefShim(handle, highTime0NanoSeconds, lowTime0NanoSeconds, highTime1NanoSeconds, lowTime1NanoSeconds, ref status);
    }
    public static void SetAddressableLEDLength(HalAddressableLEDHandle handle, int length, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetAddressableLEDLengthRefShim(handle, length, ref status);
    }
    public static void SetAddressableLEDOutputPort(HalAddressableLEDHandle handle, HalDigitalHandle outputPort, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetAddressableLEDOutputPortRefShim(handle, outputPort, ref status);
    }
    public static void SetAddressableLEDSyncTime(HalAddressableLEDHandle handle, int syncTimeMicroSeconds, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetAddressableLEDSyncTimeRefShim(handle, syncTimeMicroSeconds, ref status);
    }
    public static void StartAddressableLEDOutput(HalAddressableLEDHandle handle, out HalStatus status)
    {
        status = HalStatus.Ok;
        StartAddressableLEDOutputRefShim(handle, ref status);
    }
    public static void StopAddressableLEDOutput(HalAddressableLEDHandle handle, out HalStatus status)
    {
        status = HalStatus.Ok;
        StopAddressableLEDOutputRefShim(handle, ref status);
    }
    internal static void WriteAddressableLEDData(HalAddressableLEDHandle handle, ReadOnlySpan<AddressableLEDData> data, int length, out HalStatus status)
    {
        status = HalStatus.Ok;
        WriteAddressableLEDDataRefShim(handle, data, length, ref status);
    }
}
