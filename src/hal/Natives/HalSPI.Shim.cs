using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using WPIHal;
using WPIHal.Handles;

namespace WPIHal.Natives;

public static unsafe partial class HalSPI
{
    public static void ConfigureSPIAutoStall(SPIPort port, int csToSclkTicks, int stallTicks, int pow2BytesPerRead, out HalStatus status)
    {
        status = HalStatus.Ok;
        ConfigureSPIAutoStallRefShim(port, csToSclkTicks, stallTicks, pow2BytesPerRead, ref status);
    }
    public static void ForceSPIAutoRead(SPIPort port, out HalStatus status)
    {
        status = HalStatus.Ok;
        ForceSPIAutoReadRefShim(port, ref status);
    }
    public static void FreeSPIAuto(SPIPort port, out HalStatus status)
    {
        status = HalStatus.Ok;
        FreeSPIAutoRefShim(port, ref status);
    }
    public static int GetSPIAutoDroppedCount(SPIPort port, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetSPIAutoDroppedCountRefShim(port, ref status);
    }
    public static void InitSPIAuto(SPIPort port, int bufferSize, out HalStatus status)
    {
        status = HalStatus.Ok;
        InitSPIAutoRefShim(port, bufferSize, ref status);
    }
    public static void InitializeSPI(SPIPort port, out HalStatus status)
    {
        status = HalStatus.Ok;
        InitializeSPIRefShim(port, ref status);
    }
    public static int ReadSPIAutoReceivedData(SPIPort port, Span<uint> buffer, int numToRead, double timeout, out HalStatus status)
    {
        status = HalStatus.Ok;
        return ReadSPIAutoReceivedDataRefShim(port, buffer, numToRead, timeout, ref status);
    }
    public static void SetSPIAutoTransmitData(SPIPort port, ReadOnlySpan<byte> dataToSend, int dataSize, int zeroSize, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetSPIAutoTransmitDataRefShim(port, dataToSend, dataSize, zeroSize, ref status);
    }
    public static void SetSPIChipSelectActiveHigh(SPIPort port, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetSPIChipSelectActiveHighRefShim(port, ref status);
    }
    public static void SetSPIChipSelectActiveLow(SPIPort port, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetSPIChipSelectActiveLowRefShim(port, ref status);
    }
    public static void StartSPIAutoRate(SPIPort port, double period, out HalStatus status)
    {
        status = HalStatus.Ok;
        StartSPIAutoRateRefShim(port, period, ref status);
    }
    public static void StartSPIAutoTrigger(SPIPort port, HalAnalogTriggerHandle digitalSourceHandle, AnalogTriggerType analogTriggerType, int triggerRising, int triggerFalling, out HalStatus status)
    {
        status = HalStatus.Ok;
        StartSPIAutoTriggerRefShim(port, digitalSourceHandle, analogTriggerType, triggerRising, triggerFalling, ref status);
    }
    public static void StopSPIAuto(SPIPort port, out HalStatus status)
    {
        status = HalStatus.Ok;
        StopSPIAutoRefShim(port, ref status);
    }
}
