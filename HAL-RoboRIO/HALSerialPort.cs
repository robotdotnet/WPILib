//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL_Base;

namespace HAL_RoboRIO
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALSerialPort
    {
        internal static void Initialize(IntPtr library, IDllLoader loader)
        {
            HAL_Base.HALSerialPort.SerialInitializePort = (HAL_Base.HALSerialPort.SerialInitializePortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialInitializePort"), typeof(HAL_Base.HALSerialPort.SerialInitializePortDelegate));

            HAL_Base.HALSerialPort.SerialSetBaudRate = (HAL_Base.HALSerialPort.SerialSetBaudRateDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialSetBaudRate"), typeof(HAL_Base.HALSerialPort.SerialSetBaudRateDelegate));

            HAL_Base.HALSerialPort.SerialSetDataBits = (HAL_Base.HALSerialPort.SerialSetDataBitsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialSetDataBits"), typeof(HAL_Base.HALSerialPort.SerialSetDataBitsDelegate));

            HAL_Base.HALSerialPort.SerialSetParity = (HAL_Base.HALSerialPort.SerialSetParityDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialSetParity"), typeof(HAL_Base.HALSerialPort.SerialSetParityDelegate));

            HAL_Base.HALSerialPort.SerialSetStopBits = (HAL_Base.HALSerialPort.SerialSetStopBitsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialSetStopBits"), typeof(HAL_Base.HALSerialPort.SerialSetStopBitsDelegate));

            HAL_Base.HALSerialPort.SerialSetWriteMode = (HAL_Base.HALSerialPort.SerialSetWriteModeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialSetWriteMode"), typeof(HAL_Base.HALSerialPort.SerialSetWriteModeDelegate));

            HAL_Base.HALSerialPort.SerialSetFlowControl = (HAL_Base.HALSerialPort.SerialSetFlowControlDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialSetFlowControl"), typeof(HAL_Base.HALSerialPort.SerialSetFlowControlDelegate));

            HAL_Base.HALSerialPort.SerialSetTimeout = (HAL_Base.HALSerialPort.SerialSetTimeoutDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialSetTimeout"), typeof(HAL_Base.HALSerialPort.SerialSetTimeoutDelegate));

            HAL_Base.HALSerialPort.SerialEnableTermination = (HAL_Base.HALSerialPort.SerialEnableTerminationDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialEnableTermination"), typeof(HAL_Base.HALSerialPort.SerialEnableTerminationDelegate));

            HAL_Base.HALSerialPort.SerialDisableTermination = (HAL_Base.HALSerialPort.SerialDisableTerminationDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialDisableTermination"), typeof(HAL_Base.HALSerialPort.SerialDisableTerminationDelegate));

            HAL_Base.HALSerialPort.SerialSetReadBufferSize = (HAL_Base.HALSerialPort.SerialSetReadBufferSizeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialSetReadBufferSize"), typeof(HAL_Base.HALSerialPort.SerialSetReadBufferSizeDelegate));

            HAL_Base.HALSerialPort.SerialSetWriteBufferSize = (HAL_Base.HALSerialPort.SerialSetWriteBufferSizeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialSetWriteBufferSize"), typeof(HAL_Base.HALSerialPort.SerialSetWriteBufferSizeDelegate));

            HAL_Base.HALSerialPort.SerialGetBytesReceived = (HAL_Base.HALSerialPort.SerialGetBytesReceivedDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialGetBytesReceived"), typeof(HAL_Base.HALSerialPort.SerialGetBytesReceivedDelegate));

            HAL_Base.HALSerialPort.SerialRead = (HAL_Base.HALSerialPort.SerialReadDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialRead"), typeof(HAL_Base.HALSerialPort.SerialReadDelegate));

            HAL_Base.HALSerialPort.SerialWrite = (HAL_Base.HALSerialPort.SerialWriteDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialWrite"), typeof(HAL_Base.HALSerialPort.SerialWriteDelegate));

            HAL_Base.HALSerialPort.SerialFlush = (HAL_Base.HALSerialPort.SerialFlushDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialFlush"), typeof(HAL_Base.HALSerialPort.SerialFlushDelegate));

            HAL_Base.HALSerialPort.SerialClear = (HAL_Base.HALSerialPort.SerialClearDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialClear"), typeof(HAL_Base.HALSerialPort.SerialClearDelegate));

            HAL_Base.HALSerialPort.SerialClose = (HAL_Base.HALSerialPort.SerialCloseDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialClose"), typeof(HAL_Base.HALSerialPort.SerialCloseDelegate));

        }

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "serialInitializePort")]
        public static extern void serialInitializePort(byte port, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "serialSetBaudRate")]
        public static extern void serialSetBaudRate(byte port, uint baud, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "serialSetDataBits")]
        public static extern void serialSetDataBits(byte port, byte bits, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "serialSetParity")]
        public static extern void serialSetParity(byte port, byte parity, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "serialSetStopBits")]
        public static extern void serialSetStopBits(byte port, byte stopBits, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "serialSetWriteMode")]
        public static extern void serialSetWriteMode(byte port, byte mode, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "serialSetFlowControl")]
        public static extern void serialSetFlowControl(byte port, byte flow, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "serialSetTimeout")]
        public static extern void serialSetTimeout(byte port, float timeout, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "serialEnableTermination")]
        public static extern void serialEnableTermination(byte port, byte terminator, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "serialDisableTermination")]
        public static extern void serialDisableTermination(byte port, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "serialSetReadBufferSize")]
        public static extern void serialSetReadBufferSize(byte port, uint size, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "serialSetWriteBufferSize")]
        public static extern void serialSetWriteBufferSize(byte port, uint size, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "serialGetBytesReceived")]
        public static extern int serialGetBytesReceived(byte port, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "serialRead")]
        public static extern uint serialRead(byte port, byte[] buffer, int count, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "serialWrite")]
        public static extern uint serialWrite(byte port, byte[] buffer, int count, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "serialFlush")]
        public static extern void serialFlush(byte port, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "serialClear")]
        public static extern void serialClear(byte port, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "serialClose")]
        public static extern void serialClose(byte port, ref int status);
    }
}
