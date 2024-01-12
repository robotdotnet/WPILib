using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using WPIHal;
using WPIHal.Handles;

namespace WPIHal.Natives;

public static unsafe partial class HalSerialPort
{
    public static void ClearSerial(HalSerialPortHandle handle, out HalStatus status)
    {
        status = HalStatus.Ok;
        ClearSerialRefShim(handle, ref status);
    }
    public static void CloseSerial(HalSerialPortHandle handle, out HalStatus status)
    {
        status = HalStatus.Ok;
        CloseSerialRefShim(handle, ref status);
    }
    public static void DisableSerialTermination(HalSerialPortHandle handle, out HalStatus status)
    {
        status = HalStatus.Ok;
        DisableSerialTerminationRefShim(handle, ref status);
    }
    public static void EnableSerialTermination(HalSerialPortHandle handle, byte terminator, out HalStatus status)
    {
        status = HalStatus.Ok;
        EnableSerialTerminationRefShim(handle, terminator, ref status);
    }
    public static void FlushSerial(HalSerialPortHandle handle, out HalStatus status)
    {
        status = HalStatus.Ok;
        FlushSerialRefShim(handle, ref status);
    }
    public static int GetSerialBytesReceived(HalSerialPortHandle handle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetSerialBytesReceivedRefShim(handle, ref status);
    }
    public static int GetSerialFD(HalSerialPortHandle handle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetSerialFDRefShim(handle, ref status);
    }
    public static HalSerialPortHandle InitializeSerialPort(RioSerialPort port, out HalStatus status)
    {
        status = HalStatus.Ok;
        return InitializeSerialPortRefShim(port, ref status);
    }
    public static HalSerialPortHandle InitializeSerialPortDirect(RioSerialPort port, string portName, out HalStatus status)
    {
        status = HalStatus.Ok;
        return InitializeSerialPortDirectRefShim(port, portName, ref status);
    }
    public static int ReadSerial(HalSerialPortHandle handle, Span<byte> buffer, int count, out HalStatus status)
    {
        status = HalStatus.Ok;
        return ReadSerialRefShim(handle, buffer, count, ref status);
    }
    public static void SetSerialBaudRate(HalSerialPortHandle handle, int baud, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetSerialBaudRateRefShim(handle, baud, ref status);
    }
    public static void SetSerialDataBits(HalSerialPortHandle handle, int bits, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetSerialDataBitsRefShim(handle, bits, ref status);
    }
    public static void SetSerialFlowControl(HalSerialPortHandle handle, int flow, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetSerialFlowControlRefShim(handle, flow, ref status);
    }
    public static void SetSerialParity(HalSerialPortHandle handle, int parity, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetSerialParityRefShim(handle, parity, ref status);
    }
    public static void SetSerialReadBufferSize(HalSerialPortHandle handle, int size, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetSerialReadBufferSizeRefShim(handle, size, ref status);
    }
    public static void SetSerialStopBits(HalSerialPortHandle handle, int stopBits, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetSerialStopBitsRefShim(handle, stopBits, ref status);
    }
    public static void SetSerialTimeout(HalSerialPortHandle handle, double timeout, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetSerialTimeoutRefShim(handle, timeout, ref status);
    }
    public static void SetSerialWriteBufferSize(HalSerialPortHandle handle, int size, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetSerialWriteBufferSizeRefShim(handle, size, ref status);
    }
    public static void SetSerialWriteMode(HalSerialPortHandle handle, int mode, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetSerialWriteModeRefShim(handle, mode, ref status);
    }
    public static int WriteSerial(HalSerialPortHandle handle, ReadOnlySpan<byte> buffer, int count, out HalStatus status)
    {
        status = HalStatus.Ok;
        return WriteSerialRefShim(handle, buffer, count, ref status);
    }
}
