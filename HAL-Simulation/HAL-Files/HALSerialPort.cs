using System.Runtime.InteropServices;

// ReSharper disable RedundantAssignment
// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace
#pragma warning disable 1591

namespace HAL_Simulator
{
    ///<inheritdoc cref="HAL"/>
    internal class HALSerialPort
    {
        /// Return Type: void
        ///port: byte
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "serialInitializePort")]
        [CalledSimFunction]
        internal static extern void serialInitializePort(byte port, ref int status);


        /// Return Type: void
        ///port: byte
        ///baud: unsigned int
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "serialSetBaudRate")]
        [CalledSimFunction]
        internal static extern void serialSetBaudRate(byte port, uint baud, ref int status);


        /// Return Type: void
        ///port: byte
        ///bits: byte
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "serialSetDataBits")]
        [CalledSimFunction]
        internal static extern void serialSetDataBits(byte port, byte bits, ref int status);


        /// Return Type: void
        ///port: byte
        ///parity: byte
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "serialSetParity")]
        [CalledSimFunction]
        internal static extern void serialSetParity(byte port, byte parity, ref int status);


        /// Return Type: void
        ///port: byte
        ///stopBits: byte
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "serialSetStopBits")]
        [CalledSimFunction]
        internal static extern void serialSetStopBits(byte port, byte stopBits, ref int status);


        /// Return Type: void
        ///port: byte
        ///mode: byte
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "serialSetWriteMode")]
        [CalledSimFunction]
        internal static extern void serialSetWriteMode(byte port, byte mode, ref int status);


        /// Return Type: void
        ///port: byte
        ///flow: byte
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "serialSetFlowControl")]
        [CalledSimFunction]
        internal static extern void serialSetFlowControl(byte port, byte flow, ref int status);


        /// Return Type: void
        ///port: byte
        ///timeout: float
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "serialSetTimeout")]
        [CalledSimFunction]
        internal static extern void serialSetTimeout(byte port, float timeout, ref int status);


        /// Return Type: void
        ///port: byte
        ///terminator: char
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "serialEnableTermination")]
        [CalledSimFunction]
        internal static extern void serialEnableTermination(byte port, byte terminator, ref int status);


        /// Return Type: void
        ///port: byte
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "serialDisableTermination")]
        [CalledSimFunction]
        internal static extern void serialDisableTermination(byte port, ref int status);


        /// Return Type: void
        ///port: byte
        ///size: unsigned int
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "serialSetReadBufferSize")]
        [CalledSimFunction]
        internal static extern void serialSetReadBufferSize(byte port, uint size, ref int status);


        /// Return Type: void
        ///port: byte
        ///size: unsigned int
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "serialSetWriteBufferSize")]
        [CalledSimFunction]
        internal static extern void serialSetWriteBufferSize(byte port, uint size, ref int status);


        /// Return Type: int
        ///port: byte
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "serialGetBytesReceived")]
        [CalledSimFunction]
        internal static extern int serialGetBytesReceived(byte port, ref int status);


        /// Return Type: unsigned int
        ///port: byte
        ///buffer: char*
        ///count: int
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "serialRead")]
        [CalledSimFunction]
        internal static extern uint serialRead(byte port, byte[] buffer, int count, ref int status);


        /// Return Type: unsigned int
        ///port: byte
        ///buffer: char*
        ///count: int
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "serialWrite")]
        [CalledSimFunction]
        internal static extern uint serialWrite(byte port, byte[] buffer, int count, ref int status);


        /// Return Type: void
        ///port: byte
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "serialFlush")]
        [CalledSimFunction]
        internal static extern void serialFlush(byte port, ref int status);


        /// Return Type: void
        ///port: byte
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "serialClear")]
        [CalledSimFunction]
        internal static extern void serialClear(byte port, ref int status);


        /// Return Type: void
        ///port: byte
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "serialClose")]
        [CalledSimFunction]
        internal static extern void serialClose(byte port, ref int status);
    }
}
