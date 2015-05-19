//File automatically generated using robotdotnet-tools. Please do not modify.
using System;
using System.Linq;
using System.Reflection;

namespace HAL_Base
{
    public partial class HALSerialPort
    {
        internal static void SetupDelegates()
        {
            string className = MethodBase.GetCurrentMethod().DeclaringType.Name;
            var types = HAL.HALAssembly.GetTypes();
            var q = from t in types where t.IsClass && t.Name == className select t;
            Type type = HAL.HALAssembly.GetType(q.ToList()[0].FullName);
            SerialInitializePort = (SerialInitializePortDelegate)Delegate.CreateDelegate(typeof(SerialInitializePortDelegate), type.GetMethod("serialInitializePort"));
            SerialSetBaudRate = (SerialSetBaudRateDelegate)Delegate.CreateDelegate(typeof(SerialSetBaudRateDelegate), type.GetMethod("serialSetBaudRate"));
            SerialSetDataBits = (SerialSetDataBitsDelegate)Delegate.CreateDelegate(typeof(SerialSetDataBitsDelegate), type.GetMethod("serialSetDataBits"));
            SerialSetParity = (SerialSetParityDelegate)Delegate.CreateDelegate(typeof(SerialSetParityDelegate), type.GetMethod("serialSetParity"));
            SerialSetStopBits = (SerialSetStopBitsDelegate)Delegate.CreateDelegate(typeof(SerialSetStopBitsDelegate), type.GetMethod("serialSetStopBits"));
            SerialSetWriteMode = (SerialSetWriteModeDelegate)Delegate.CreateDelegate(typeof(SerialSetWriteModeDelegate), type.GetMethod("serialSetWriteMode"));
            SerialSetFlowControl = (SerialSetFlowControlDelegate)Delegate.CreateDelegate(typeof(SerialSetFlowControlDelegate), type.GetMethod("serialSetFlowControl"));
            SerialSetTimeout = (SerialSetTimeoutDelegate)Delegate.CreateDelegate(typeof(SerialSetTimeoutDelegate), type.GetMethod("serialSetTimeout"));
            SerialEnableTermination = (SerialEnableTerminationDelegate)Delegate.CreateDelegate(typeof(SerialEnableTerminationDelegate), type.GetMethod("serialEnableTermination"));
            SerialDisableTermination = (SerialDisableTerminationDelegate)Delegate.CreateDelegate(typeof(SerialDisableTerminationDelegate), type.GetMethod("serialDisableTermination"));
            SerialSetReadBufferSize = (SerialSetReadBufferSizeDelegate)Delegate.CreateDelegate(typeof(SerialSetReadBufferSizeDelegate), type.GetMethod("serialSetReadBufferSize"));
            SerialSetWriteBufferSize = (SerialSetWriteBufferSizeDelegate)Delegate.CreateDelegate(typeof(SerialSetWriteBufferSizeDelegate), type.GetMethod("serialSetWriteBufferSize"));
            SerialGetBytesReceived = (SerialGetBytesReceivedDelegate)Delegate.CreateDelegate(typeof(SerialGetBytesReceivedDelegate), type.GetMethod("serialGetBytesReceived"));
            SerialRead = (SerialReadDelegate)Delegate.CreateDelegate(typeof(SerialReadDelegate), type.GetMethod("serialRead"));
            SerialWrite = (SerialWriteDelegate)Delegate.CreateDelegate(typeof(SerialWriteDelegate), type.GetMethod("serialWrite"));
            SerialFlush = (SerialFlushDelegate)Delegate.CreateDelegate(typeof(SerialFlushDelegate), type.GetMethod("serialFlush"));
            SerialClear = (SerialClearDelegate)Delegate.CreateDelegate(typeof(SerialClearDelegate), type.GetMethod("serialClear"));
            SerialClose = (SerialCloseDelegate)Delegate.CreateDelegate(typeof(SerialCloseDelegate), type.GetMethod("serialClose"));
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

        public delegate uint SerialReadDelegate(byte port, string buffer, int count, ref int status);
        public static SerialReadDelegate SerialRead;

        public delegate uint SerialWriteDelegate(byte port, string buffer, int count, ref int status);
        public static SerialWriteDelegate SerialWrite;

        public delegate void SerialFlushDelegate(byte port, ref int status);
        public static SerialFlushDelegate SerialFlush;

        public delegate void SerialClearDelegate(byte port, ref int status);
        public static SerialClearDelegate SerialClear;

        public delegate void SerialCloseDelegate(byte port, ref int status);
        public static SerialCloseDelegate SerialClose;
    }
}
