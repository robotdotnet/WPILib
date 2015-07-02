//File automatically generated using robotdotnet-tools. Please do not modify.

using System.Runtime.InteropServices;

namespace HAL_RoboRIO
{
    public class HALSerialPort
    {

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
