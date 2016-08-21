using System;
using System.Runtime.InteropServices;
using HAL.Base;

// ReSharper disable CheckNamespace
namespace HAL.SimulatorHAL
{
    internal class HALSerialPort
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALSerialPort.HAL_InitializeSerialPort = HAL_InitializeSerialPort;
            Base.HALSerialPort.HAL_SetSerialBaudRate = HAL_SetSerialBaudRate;
            Base.HALSerialPort.HAL_SetSerialDataBits = HAL_SetSerialDataBits;
            Base.HALSerialPort.HAL_SetSerialParity = HAL_SetSerialParity;
            Base.HALSerialPort.HAL_SetSerialStopBits = HAL_SetSerialStopBits;
            Base.HALSerialPort.HAL_SetSerialWriteMode = HAL_SetSerialWriteMode;
            Base.HALSerialPort.HAL_SetSerialFlowControl = HAL_SetSerialFlowControl;
            Base.HALSerialPort.HAL_SetSerialTimeout = HAL_SetSerialTimeout;
            Base.HALSerialPort.HAL_EnableSerialTermination = HAL_EnableSerialTermination;
            Base.HALSerialPort.HAL_DisableSerialTermination = HAL_DisableSerialTermination;
            Base.HALSerialPort.HAL_SetSerialReadBufferSize = HAL_SetSerialReadBufferSize;
            Base.HALSerialPort.HAL_SetSerialWriteBufferSize = HAL_SetSerialWriteBufferSize;
            Base.HALSerialPort.HAL_GetSerialBytesReceived = HAL_GetSerialBytesReceived;
            Base.HALSerialPort.HAL_ReadSerial = HAL_ReadSerial;
            Base.HALSerialPort.HAL_WriteSerial = HAL_WriteSerial;
            Base.HALSerialPort.HAL_FlushSerial = HAL_FlushSerial;
            Base.HALSerialPort.HAL_ClearSerial = HAL_ClearSerial;
            Base.HALSerialPort.HAL_CloseSerial = HAL_CloseSerial;
        }

        public static void HAL_InitializeSerialPort(int port, ref int status)
        {
        }

        public static void HAL_SetSerialBaudRate(int port, int baud, ref int status)
        {
        }

        public static void HAL_SetSerialDataBits(int port, int bits, ref int status)
        {
        }

        public static void HAL_SetSerialParity(int port, int parity, ref int status)
        {
        }

        public static void HAL_SetSerialStopBits(int port, int stopBits, ref int status)
        {
        }

        public static void HAL_SetSerialWriteMode(int port, int mode, ref int status)
        {
        }

        public static void HAL_SetSerialFlowControl(int port, int flow, ref int status)
        {
        }

        public static void HAL_SetSerialTimeout(int port, double timeout, ref int status)
        {
        }

        public static void HAL_EnableSerialTermination(int port, byte terminator, ref int status)
        {
        }

        public static void HAL_DisableSerialTermination(int port, ref int status)
        {
        }

        public static void HAL_SetSerialReadBufferSize(int port, int size, ref int status)
        {
        }

        public static void HAL_SetSerialWriteBufferSize(int port, int size, ref int status)
        {
        }

        public static int HAL_GetSerialBytesReceived(int port, ref int status)
        {
            throw new NotImplementedException();
        }

        public static int HAL_ReadSerial(int port, byte buffer, int count, ref int status)
        {
            throw new NotImplementedException();
        }

        public static int HAL_WriteSerial(int port, byte buffer, int count, ref int status)
        {
            throw new NotImplementedException();
        }

        public static void HAL_FlushSerial(int port, ref int status)
        {
        }

        public static void HAL_ClearSerial(int port, ref int status)
        {
        }

        public static void HAL_CloseSerial(int port, ref int status)
        {
        }
    }
}

