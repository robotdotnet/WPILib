
using Hal.Natives;
using System;
using WPIUtil.NativeUtilities;

namespace Hal
{
    [NativeInterface(typeof(ISerialPort))]
    public unsafe static class SerialPort
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        private static ISerialPort lowLevel;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // Field is never assigned to
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        public static void ClearSerial(int handle)
        {
            lowLevel.HAL_ClearSerial(handle);
        }

        public static void CloseSerial(int handle)
        {
            lowLevel.HAL_CloseSerial(handle);
        }

        public static void DisableSerialTermination(int handle)
        {
            lowLevel.HAL_DisableSerialTermination(handle);
        }

        public static void EnableSerialTermination(int handle, byte terminator)
        {
            lowLevel.HAL_EnableSerialTermination(handle, terminator);
        }

        public static void FlushSerial(int handle)
        {
            lowLevel.HAL_FlushSerial(handle);
        }

        public static int GetSerialBytesReceived(int handle)
        {
            return lowLevel.HAL_GetSerialBytesReceived(handle);
        }

        public static int GetSerialFD(int handle)
        {
            return lowLevel.HAL_GetSerialFD(handle);
        }

        public static int Initialize(SerialPortLocation port)
        {
            return lowLevel.HAL_InitializeSerialPort(port);
        }

        public static int InitializeDirect(SerialPortLocation port, byte* portName)
        {
            return lowLevel.HAL_InitializeSerialPortDirect(port, portName);
        }

        public static int ReadSerial(int handle, byte* buffer, int count)
        {
            return lowLevel.HAL_ReadSerial(handle, buffer, count);
        }

        public static void SetSerialBaudRate(int handle, int baud)
        {
            lowLevel.HAL_SetSerialBaudRate(handle, baud);
        }

        public static void SetSerialDataBits(int handle, int bits)
        {
            lowLevel.HAL_SetSerialDataBits(handle, bits);
        }

        public static void SetSerialFlowControl(int handle, int flow)
        {
            lowLevel.HAL_SetSerialFlowControl(handle, flow);
        }

        public static void SetSerialParity(int handle, int parity)
        {
            lowLevel.HAL_SetSerialParity(handle, parity);
        }

        public static void SetSerialReadBufferSize(int handle, int size)
        {
            lowLevel.HAL_SetSerialReadBufferSize(handle, size);
        }

        public static void SetSerialStopBits(int handle, int stopBits)
        {
            lowLevel.HAL_SetSerialStopBits(handle, stopBits);
        }

        public static void SetSerialTimeout(int handle, double timeout)
        {
            lowLevel.HAL_SetSerialTimeout(handle, timeout);
        }

        public static void SetSerialWriteBufferSize(int handle, int size)
        {
            lowLevel.HAL_SetSerialWriteBufferSize(handle, size);
        }

        public static void SetSerialWriteMode(int handle, int mode)
        {
            lowLevel.HAL_SetSerialWriteMode(handle, mode);
        }

        public static int WriteSerial(int handle, byte* buffer, int count)
        {
            return lowLevel.HAL_WriteSerial(handle, buffer, count);
        }

    }
}
