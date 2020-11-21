using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
using System;

namespace Hal.Natives
{
    public unsafe class SPILowLevelNative
    {
        public SPILowLevelNative(IFunctionPointerLoader loader)
        {
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader));
            }

            HAL_CloseSPIFunc = (delegate* unmanaged[Cdecl] < Hal.SPIPort, void >)loader.GetProcAddress("HAL_CloseSPI");
            HAL_ConfigureSPIAutoStallFunc = (delegate* unmanaged[Cdecl] < Hal.SPIPort, System.Int32, System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_ConfigureSPIAutoStall");
            HAL_ForceSPIAutoReadFunc = (delegate* unmanaged[Cdecl] < Hal.SPIPort, int *, void >)loader.GetProcAddress("HAL_ForceSPIAutoRead");
            HAL_FreeSPIAutoFunc = (delegate* unmanaged[Cdecl] < Hal.SPIPort, int *, void >)loader.GetProcAddress("HAL_FreeSPIAuto");
            HAL_GetSPIAutoDroppedCountFunc = (delegate* unmanaged[Cdecl] < Hal.SPIPort, int *, System.Int32 >)loader.GetProcAddress("HAL_GetSPIAutoDroppedCount");
            HAL_GetSPIHandleFunc = (delegate* unmanaged[Cdecl] < Hal.SPIPort, System.Int32 >)loader.GetProcAddress("HAL_GetSPIHandle");
            HAL_InitSPIAutoFunc = (delegate* unmanaged[Cdecl] < Hal.SPIPort, System.Int32, int *, void >)loader.GetProcAddress("HAL_InitSPIAuto");
            HAL_InitializeSPIFunc = (delegate* unmanaged[Cdecl] < Hal.SPIPort, int *, void >)loader.GetProcAddress("HAL_InitializeSPI");
            HAL_ReadSPIFunc = (delegate* unmanaged[Cdecl] < Hal.SPIPort, System.Byte *, System.Int32, System.Int32 >)loader.GetProcAddress("HAL_ReadSPI");
            HAL_ReadSPIAutoReceivedDataFunc = (delegate* unmanaged[Cdecl] < Hal.SPIPort, System.UInt32 *, System.Int32, System.Double, int *, System.Int32 >)loader.GetProcAddress("HAL_ReadSPIAutoReceivedData");
            HAL_SetSPIAutoTransmitDataFunc = (delegate* unmanaged[Cdecl] < Hal.SPIPort, System.Byte *, System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetSPIAutoTransmitData");
            HAL_SetSPIChipSelectActiveHighFunc = (delegate* unmanaged[Cdecl] < Hal.SPIPort, int *, void >)loader.GetProcAddress("HAL_SetSPIChipSelectActiveHigh");
            HAL_SetSPIChipSelectActiveLowFunc = (delegate* unmanaged[Cdecl] < Hal.SPIPort, int *, void >)loader.GetProcAddress("HAL_SetSPIChipSelectActiveLow");
            HAL_SetSPIHandleFunc = (delegate* unmanaged[Cdecl] < Hal.SPIPort, System.Int32, void >)loader.GetProcAddress("HAL_SetSPIHandle");
            HAL_SetSPIOptsFunc = (delegate* unmanaged[Cdecl] < Hal.SPIPort, System.Int32, System.Int32, System.Int32, void >)loader.GetProcAddress("HAL_SetSPIOpts");
            HAL_SetSPISpeedFunc = (delegate* unmanaged[Cdecl] < Hal.SPIPort, System.Int32, void >)loader.GetProcAddress("HAL_SetSPISpeed");
            HAL_StartSPIAutoRateFunc = (delegate* unmanaged[Cdecl] < Hal.SPIPort, System.Double, int *, void >)loader.GetProcAddress("HAL_StartSPIAutoRate");
            HAL_StartSPIAutoTriggerFunc = (delegate* unmanaged[Cdecl] < Hal.SPIPort, System.Int32, Hal.AnalogTriggerType, System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_StartSPIAutoTrigger");
            HAL_StopSPIAutoFunc = (delegate* unmanaged[Cdecl] < Hal.SPIPort, int *, void >)loader.GetProcAddress("HAL_StopSPIAuto");
            HAL_TransactionSPIFunc = (delegate* unmanaged[Cdecl] < Hal.SPIPort, System.Byte *, System.Byte *, System.Int32, System.Int32 >)loader.GetProcAddress("HAL_TransactionSPI");
            HAL_WriteSPIFunc = (delegate* unmanaged[Cdecl] < Hal.SPIPort, System.Byte *, System.Int32, System.Int32 >)loader.GetProcAddress("HAL_WriteSPI");
        }

        private readonly delegate* unmanaged[Cdecl]<SPIPort, void> HAL_CloseSPIFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_CloseSPI(SPIPort port)
        {
            HAL_CloseSPIFunc(port);
        }



        private readonly delegate* unmanaged[Cdecl]<SPIPort, int, int, int, int*, void> HAL_ConfigureSPIAutoStallFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_ConfigureSPIAutoStall(SPIPort port, int csToSclkTicks, int stallTicks, int pow2BytesPerRead)
        {
            int status = 0;
            HAL_ConfigureSPIAutoStallFunc(port, csToSclkTicks, stallTicks, pow2BytesPerRead, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<SPIPort, int*, void> HAL_ForceSPIAutoReadFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_ForceSPIAutoRead(SPIPort port)
        {
            int status = 0;
            HAL_ForceSPIAutoReadFunc(port, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<SPIPort, int*, void> HAL_FreeSPIAutoFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_FreeSPIAuto(SPIPort port)
        {
            int status = 0;
            HAL_FreeSPIAutoFunc(port, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<SPIPort, int*, int> HAL_GetSPIAutoDroppedCountFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetSPIAutoDroppedCount(SPIPort port)
        {
            int status = 0;
            var retVal = HAL_GetSPIAutoDroppedCountFunc(port, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<SPIPort, int> HAL_GetSPIHandleFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetSPIHandle(SPIPort port)
        {
            return HAL_GetSPIHandleFunc(port);
        }



        private readonly delegate* unmanaged[Cdecl]<SPIPort, int, int*, void> HAL_InitSPIAutoFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_InitSPIAuto(SPIPort port, int bufferSize)
        {
            int status = 0;
            HAL_InitSPIAutoFunc(port, bufferSize, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<SPIPort, int*, void> HAL_InitializeSPIFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_InitializeSPI(SPIPort port)
        {
            int status = 0;
            HAL_InitializeSPIFunc(port, &status);
            Hal.StatusHandling.SPIStatusCheck(status, port);
        }



        private readonly delegate* unmanaged[Cdecl]<SPIPort, byte*, int, int> HAL_ReadSPIFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_ReadSPI(SPIPort port, byte* buffer, int count)
        {
            return HAL_ReadSPIFunc(port, buffer, count);
        }



        private readonly delegate* unmanaged[Cdecl]<SPIPort, uint*, int, double, int*, int> HAL_ReadSPIAutoReceivedDataFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_ReadSPIAutoReceivedData(SPIPort port, uint* buffer, int numToRead, double timeout)
        {
            int status = 0;
            var retVal = HAL_ReadSPIAutoReceivedDataFunc(port, buffer, numToRead, timeout, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<SPIPort, byte*, int, int, int*, void> HAL_SetSPIAutoTransmitDataFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetSPIAutoTransmitData(SPIPort port, byte* dataToSend, int dataSize, int zeroSize)
        {
            int status = 0;
            HAL_SetSPIAutoTransmitDataFunc(port, dataToSend, dataSize, zeroSize, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<SPIPort, int*, void> HAL_SetSPIChipSelectActiveHighFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetSPIChipSelectActiveHigh(SPIPort port)
        {
            int status = 0;
            HAL_SetSPIChipSelectActiveHighFunc(port, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<SPIPort, int*, void> HAL_SetSPIChipSelectActiveLowFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetSPIChipSelectActiveLow(SPIPort port)
        {
            int status = 0;
            HAL_SetSPIChipSelectActiveLowFunc(port, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<SPIPort, int, void> HAL_SetSPIHandleFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetSPIHandle(SPIPort port, int handle)
        {
            HAL_SetSPIHandleFunc(port, handle);
        }



        private readonly delegate* unmanaged[Cdecl]<SPIPort, int, int, int, void> HAL_SetSPIOptsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetSPIOpts(SPIPort port, int msbFirst, int sampleOnTrailing, int clkIdleHigh)
        {
            HAL_SetSPIOptsFunc(port, msbFirst, sampleOnTrailing, clkIdleHigh);
        }



        private readonly delegate* unmanaged[Cdecl]<SPIPort, int, void> HAL_SetSPISpeedFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetSPISpeed(SPIPort port, int speed)
        {
            HAL_SetSPISpeedFunc(port, speed);
        }



        private readonly delegate* unmanaged[Cdecl]<SPIPort, double, int*, void> HAL_StartSPIAutoRateFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_StartSPIAutoRate(SPIPort port, double period)
        {
            int status = 0;
            HAL_StartSPIAutoRateFunc(port, period, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<SPIPort, int, AnalogTriggerType, int, int, int*, void> HAL_StartSPIAutoTriggerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_StartSPIAutoTrigger(SPIPort port, int digitalSourceHandle, AnalogTriggerType analogTriggerType, int triggerRising, int triggerFalling)
        {
            int status = 0;
            HAL_StartSPIAutoTriggerFunc(port, digitalSourceHandle, analogTriggerType, triggerRising, triggerFalling, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<SPIPort, int*, void> HAL_StopSPIAutoFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_StopSPIAuto(SPIPort port)
        {
            int status = 0;
            HAL_StopSPIAutoFunc(port, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<SPIPort, byte*, byte*, int, int> HAL_TransactionSPIFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_TransactionSPI(SPIPort port, byte* dataToSend, byte* dataReceived, int size)
        {
            return HAL_TransactionSPIFunc(port, dataToSend, dataReceived, size);
        }



        private readonly delegate* unmanaged[Cdecl]<SPIPort, byte*, int, int> HAL_WriteSPIFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_WriteSPI(SPIPort port, byte* dataToSend, int sendSize)
        {
            return HAL_WriteSPIFunc(port, dataToSend, sendSize);
        }



    }
}
