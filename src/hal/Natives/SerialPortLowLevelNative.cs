using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
using System;

namespace Hal.Natives
{
    public unsafe class SerialPortLowLevelNative
    {
        public SerialPortLowLevelNative(IFunctionPointerLoader loader)
        {
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader));
            }

            HAL_ClearSerialFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, void >)loader.GetProcAddress("HAL_ClearSerial");
            HAL_CloseSerialFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, void >)loader.GetProcAddress("HAL_CloseSerial");
            HAL_DisableSerialTerminationFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, void >)loader.GetProcAddress("HAL_DisableSerialTermination");
            HAL_EnableSerialTerminationFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Byte, int *, void >)loader.GetProcAddress("HAL_EnableSerialTermination");
            HAL_FlushSerialFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, void >)loader.GetProcAddress("HAL_FlushSerial");
            HAL_GetSerialBytesReceivedFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_GetSerialBytesReceived");
            HAL_GetSerialFDFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_GetSerialFD");
            HAL_InitializeSerialPortFunc = (delegate* unmanaged[Cdecl] < Hal.SerialPortLocation, int *, System.Int32 >)loader.GetProcAddress("HAL_InitializeSerialPort");
            HAL_InitializeSerialPortDirectFunc = (delegate* unmanaged[Cdecl] < Hal.SerialPortLocation, System.Byte *, int *, System.Int32 >)loader.GetProcAddress("HAL_InitializeSerialPortDirect");
            HAL_ReadSerialFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Byte *, System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_ReadSerial");
            HAL_SetSerialBaudRateFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetSerialBaudRate");
            HAL_SetSerialDataBitsFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetSerialDataBits");
            HAL_SetSerialFlowControlFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetSerialFlowControl");
            HAL_SetSerialParityFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetSerialParity");
            HAL_SetSerialReadBufferSizeFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetSerialReadBufferSize");
            HAL_SetSerialStopBitsFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetSerialStopBits");
            HAL_SetSerialTimeoutFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Double, int *, void >)loader.GetProcAddress("HAL_SetSerialTimeout");
            HAL_SetSerialWriteBufferSizeFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetSerialWriteBufferSize");
            HAL_SetSerialWriteModeFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetSerialWriteMode");
            HAL_WriteSerialFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Byte *, System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_WriteSerial");
        }

        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_ClearSerialFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_ClearSerial(int handle)
        {
            int status = 0;
            HAL_ClearSerialFunc(handle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_CloseSerialFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_CloseSerial(int handle)
        {
            int status = 0;
            HAL_CloseSerialFunc(handle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_DisableSerialTerminationFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_DisableSerialTermination(int handle)
        {
            int status = 0;
            HAL_DisableSerialTerminationFunc(handle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, byte, int*, void> HAL_EnableSerialTerminationFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_EnableSerialTermination(int handle, byte terminator)
        {
            int status = 0;
            HAL_EnableSerialTerminationFunc(handle, terminator, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_FlushSerialFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_FlushSerial(int handle)
        {
            int status = 0;
            HAL_FlushSerialFunc(handle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetSerialBytesReceivedFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetSerialBytesReceived(int handle)
        {
            int status = 0;
            var retVal = HAL_GetSerialBytesReceivedFunc(handle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetSerialFDFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetSerialFD(int handle)
        {
            int status = 0;
            var retVal = HAL_GetSerialFDFunc(handle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<SerialPortLocation, int*, int> HAL_InitializeSerialPortFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_InitializeSerialPort(SerialPortLocation port)
        {
            int status = 0;
            var retVal = HAL_InitializeSerialPortFunc(port, &status);
            Hal.StatusHandling.SerialPortStatusCheck(status, port);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<SerialPortLocation, byte*, int*, int> HAL_InitializeSerialPortDirectFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_InitializeSerialPortDirect(SerialPortLocation port, byte* portName)
        {
            int status = 0;
            var retVal = HAL_InitializeSerialPortDirectFunc(port, portName, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, byte*, int, int*, int> HAL_ReadSerialFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_ReadSerial(int handle, byte* buffer, int count)
        {
            int status = 0;
            var retVal = HAL_ReadSerialFunc(handle, buffer, count, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetSerialBaudRateFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetSerialBaudRate(int handle, int baud)
        {
            int status = 0;
            HAL_SetSerialBaudRateFunc(handle, baud, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetSerialDataBitsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetSerialDataBits(int handle, int bits)
        {
            int status = 0;
            HAL_SetSerialDataBitsFunc(handle, bits, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetSerialFlowControlFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetSerialFlowControl(int handle, int flow)
        {
            int status = 0;
            HAL_SetSerialFlowControlFunc(handle, flow, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetSerialParityFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetSerialParity(int handle, int parity)
        {
            int status = 0;
            HAL_SetSerialParityFunc(handle, parity, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetSerialReadBufferSizeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetSerialReadBufferSize(int handle, int size)
        {
            int status = 0;
            HAL_SetSerialReadBufferSizeFunc(handle, size, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetSerialStopBitsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetSerialStopBits(int handle, int stopBits)
        {
            int status = 0;
            HAL_SetSerialStopBitsFunc(handle, stopBits, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, double, int*, void> HAL_SetSerialTimeoutFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetSerialTimeout(int handle, double timeout)
        {
            int status = 0;
            HAL_SetSerialTimeoutFunc(handle, timeout, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetSerialWriteBufferSizeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetSerialWriteBufferSize(int handle, int size)
        {
            int status = 0;
            HAL_SetSerialWriteBufferSizeFunc(handle, size, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetSerialWriteModeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetSerialWriteMode(int handle, int mode)
        {
            int status = 0;
            HAL_SetSerialWriteModeFunc(handle, mode, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, byte*, int, int*, int> HAL_WriteSerialFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_WriteSerial(int handle, byte* buffer, int count)
        {
            int status = 0;
            var retVal = HAL_WriteSerialFunc(handle, buffer, count, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



    }
}
