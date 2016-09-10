using HAL.NativeLoader;

// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALSerialPort
    {
        static HALSerialPort()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALSerialPort>(LibraryLoaderHolder.NativeLoader);
        }

        /// <summary>
        /// Use this to force load the definitions in the file
        /// </summary>
        public static void Ping()
        {
        }

        public delegate void HAL_InitializeSerialPortDelegate(int port, ref int status);
        [NativeDelegate] public static HAL_InitializeSerialPortDelegate HAL_InitializeSerialPort;

        public delegate void HAL_SetSerialBaudRateDelegate(int port, int baud, ref int status);
        [NativeDelegate] public static HAL_SetSerialBaudRateDelegate HAL_SetSerialBaudRate;

        public delegate void HAL_SetSerialDataBitsDelegate(int port, int bits, ref int status);
        [NativeDelegate] public static HAL_SetSerialDataBitsDelegate HAL_SetSerialDataBits;

        public delegate void HAL_SetSerialParityDelegate(int port, int parity, ref int status);
        [NativeDelegate] public static HAL_SetSerialParityDelegate HAL_SetSerialParity;

        public delegate void HAL_SetSerialStopBitsDelegate(int port, int stopBits, ref int status);
        [NativeDelegate] public static HAL_SetSerialStopBitsDelegate HAL_SetSerialStopBits;

        public delegate void HAL_SetSerialWriteModeDelegate(int port, int mode, ref int status);
        [NativeDelegate] public static HAL_SetSerialWriteModeDelegate HAL_SetSerialWriteMode;

        public delegate void HAL_SetSerialFlowControlDelegate(int port, int flow, ref int status);
        [NativeDelegate] public static HAL_SetSerialFlowControlDelegate HAL_SetSerialFlowControl;

        public delegate void HAL_SetSerialTimeoutDelegate(int port, double timeout, ref int status);
        [NativeDelegate] public static HAL_SetSerialTimeoutDelegate HAL_SetSerialTimeout;

        public delegate void HAL_EnableSerialTerminationDelegate(int port, byte terminator, ref int status);
        [NativeDelegate] public static HAL_EnableSerialTerminationDelegate HAL_EnableSerialTermination;

        public delegate void HAL_DisableSerialTerminationDelegate(int port, ref int status);
        [NativeDelegate] public static HAL_DisableSerialTerminationDelegate HAL_DisableSerialTermination;

        public delegate void HAL_SetSerialReadBufferSizeDelegate(int port, int size, ref int status);
        [NativeDelegate] public static HAL_SetSerialReadBufferSizeDelegate HAL_SetSerialReadBufferSize;

        public delegate void HAL_SetSerialWriteBufferSizeDelegate(int port, int size, ref int status);
        [NativeDelegate] public static HAL_SetSerialWriteBufferSizeDelegate HAL_SetSerialWriteBufferSize;

        public delegate int HAL_GetSerialBytesReceivedDelegate(int port, ref int status);
        [NativeDelegate] public static HAL_GetSerialBytesReceivedDelegate HAL_GetSerialBytesReceived;

        public delegate int HAL_ReadSerialDelegate(int port, byte[] buffer, int count, ref int status);
        [NativeDelegate] public static HAL_ReadSerialDelegate HAL_ReadSerial;

        public delegate int HAL_WriteSerialDelegate(int port, byte[] buffer, int count, ref int status);
        [NativeDelegate] public static HAL_WriteSerialDelegate HAL_WriteSerial;

        public delegate void HAL_FlushSerialDelegate(int port, ref int status);
        [NativeDelegate] public static HAL_FlushSerialDelegate HAL_FlushSerial;

        public delegate void HAL_ClearSerialDelegate(int port, ref int status);
        [NativeDelegate] public static HAL_ClearSerialDelegate HAL_ClearSerial;

        public delegate void HAL_CloseSerialDelegate(int port, ref int status);
        [NativeDelegate] public static HAL_CloseSerialDelegate HAL_CloseSerial;
    }
}

