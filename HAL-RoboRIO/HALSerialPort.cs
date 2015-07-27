//File automatically generated using robotdotnet-tools. Please do not modify.

using System.Runtime.InteropServices;

namespace HAL_RoboRIO
{
    internal class HALSerialPort
    {

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "serialInitializePort")]
        internal static extern void serialInitializePort(byte port, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "serialSetBaudRate")]
        internal static extern void serialSetBaudRate(byte port, uint baud, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "serialSetDataBits")]
        internal static extern void serialSetDataBits(byte port, byte bits, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "serialSetParity")]
        internal static extern void serialSetParity(byte port, byte parity, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "serialSetStopBits")]
        internal static extern void serialSetStopBits(byte port, byte stopBits, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "serialSetWriteMode")]
        internal static extern void serialSetWriteMode(byte port, byte mode, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "serialSetFlowControl")]
        internal static extern void serialSetFlowControl(byte port, byte flow, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "serialSetTimeout")]
        internal static extern void serialSetTimeout(byte port, float timeout, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "serialEnableTermination")]
        internal static extern void serialEnableTermination(byte port, byte terminator, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "serialDisableTermination")]
        internal static extern void serialDisableTermination(byte port, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "serialSetReadBufferSize")]
        internal static extern void serialSetReadBufferSize(byte port, uint size, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "serialSetWriteBufferSize")]
        internal static extern void serialSetWriteBufferSize(byte port, uint size, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "serialGetBytesReceived")]
        internal static extern int serialGetBytesReceived(byte port, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "serialRead")]
        internal static extern uint serialRead(byte port, byte[] buffer, int count, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "serialWrite")]
        internal static extern uint serialWrite(byte port, byte[] buffer, int count, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "serialFlush")]
        internal static extern void serialFlush(byte port, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "serialClear")]
        internal static extern void serialClear(byte port, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "serialClose")]
        internal static extern void serialClose(byte port, ref int status);
    }
}
