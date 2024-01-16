using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal.Handles;

namespace WPIHal.Natives;

public static partial class HalSerialPort
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_ClearSerial")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void ClearSerialRefShim(HalSerialPortHandle handle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_CloseSerial")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void CloseSerialRefShim(HalSerialPortHandle handle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_DisableSerialTermination")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void DisableSerialTerminationRefShim(HalSerialPortHandle handle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_EnableSerialTermination")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void EnableSerialTerminationRefShim(HalSerialPortHandle handle, byte terminator, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_FlushSerial")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void FlushSerialRefShim(HalSerialPortHandle handle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetSerialBytesReceived")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetSerialBytesReceivedRefShim(HalSerialPortHandle handle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetSerialFD")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetSerialFDRefShim(HalSerialPortHandle handle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_InitializeSerialPort")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial HalSerialPortHandle InitializeSerialPortRefShim(RioSerialPort port, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_InitializeSerialPortDirect", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial HalSerialPortHandle InitializeSerialPortDirectRefShim(RioSerialPort port, string portName, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_ReadSerial")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int ReadSerialRefShim(HalSerialPortHandle handle, Span<byte> buffer, int count, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetSerialBaudRate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetSerialBaudRateRefShim(HalSerialPortHandle handle, int baud, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetSerialDataBits")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetSerialDataBitsRefShim(HalSerialPortHandle handle, int bits, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetSerialFlowControl")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetSerialFlowControlRefShim(HalSerialPortHandle handle, int flow, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetSerialParity")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetSerialParityRefShim(HalSerialPortHandle handle, int parity, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetSerialReadBufferSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetSerialReadBufferSizeRefShim(HalSerialPortHandle handle, int size, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetSerialStopBits")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetSerialStopBitsRefShim(HalSerialPortHandle handle, int stopBits, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetSerialTimeout")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetSerialTimeoutRefShim(HalSerialPortHandle handle, double timeout, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetSerialWriteBufferSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetSerialWriteBufferSizeRefShim(HalSerialPortHandle handle, int size, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetSerialWriteMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetSerialWriteModeRefShim(HalSerialPortHandle handle, int mode, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_WriteSerial")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int WriteSerialRefShim(HalSerialPortHandle handle, ReadOnlySpan<byte> buffer, int count, ref HalStatus status);


}
