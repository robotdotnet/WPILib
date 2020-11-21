using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
namespace Hal.Natives
{
    public unsafe class SerialPortLowLevelNative
    {
        
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
