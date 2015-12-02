//File automatically generated using robotdotnet-tools. Please do not modify.

// ReSharper disable CheckNamespace

namespace HAL.Base
{
    public partial class HALSerialPort
    {
        static HALSerialPort()
        {
            HAL.Initialize();
        }

        public delegate void SerialInitializePortDelegate(byte port, ref int status);
        public static SerialInitializePortDelegate SerialInitializePort;

        public delegate void SerialSetBaudRateDelegate(byte port, uint baud, ref int status);
        public static SerialSetBaudRateDelegate SerialSetBaudRate;

        public delegate void SerialSetDataBitsDelegate(byte port, byte bits, ref int status);
        public static SerialSetDataBitsDelegate SerialSetDataBits;

        public delegate void SerialSetParityDelegate(byte port, byte parity, ref int status);
        public static SerialSetParityDelegate SerialSetParity;

        public delegate void SerialSetStopBitsDelegate(byte port, byte stopBits, ref int status);
        public static SerialSetStopBitsDelegate SerialSetStopBits;

        public delegate void SerialSetWriteModeDelegate(byte port, byte mode, ref int status);
        public static SerialSetWriteModeDelegate SerialSetWriteMode;

        public delegate void SerialSetFlowControlDelegate(byte port, byte flow, ref int status);
        public static SerialSetFlowControlDelegate SerialSetFlowControl;

        public delegate void SerialSetTimeoutDelegate(byte port, float timeout, ref int status);
        public static SerialSetTimeoutDelegate SerialSetTimeout;

        public delegate void SerialEnableTerminationDelegate(byte port, byte terminator, ref int status);
        public static SerialEnableTerminationDelegate SerialEnableTermination;

        public delegate void SerialDisableTerminationDelegate(byte port, ref int status);
        public static SerialDisableTerminationDelegate SerialDisableTermination;

        public delegate void SerialSetReadBufferSizeDelegate(byte port, uint size, ref int status);
        public static SerialSetReadBufferSizeDelegate SerialSetReadBufferSize;

        public delegate void SerialSetWriteBufferSizeDelegate(byte port, uint size, ref int status);
        public static SerialSetWriteBufferSizeDelegate SerialSetWriteBufferSize;

        public delegate int SerialGetBytesReceivedDelegate(byte port, ref int status);
        public static SerialGetBytesReceivedDelegate SerialGetBytesReceived;

        public delegate uint SerialReadDelegate(byte port, byte[] buffer, int count, ref int status);
        public static SerialReadDelegate SerialRead;

        public delegate uint SerialWriteDelegate(byte port, byte[] buffer, int count, ref int status);
        public static SerialWriteDelegate SerialWrite;

        public delegate void SerialFlushDelegate(byte port, ref int status);
        public static SerialFlushDelegate SerialFlush;

        public delegate void SerialClearDelegate(byte port, ref int status);
        public static SerialClearDelegate SerialClear;

        public delegate void SerialCloseDelegate(byte port, ref int status);
        public static SerialCloseDelegate SerialClose;
    }
}
