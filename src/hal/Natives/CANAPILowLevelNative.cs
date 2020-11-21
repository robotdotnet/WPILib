using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
using System;

namespace Hal.Natives
{
    public unsafe class CANAPILowLevelNative
    {
        public CANAPILowLevelNative(IFunctionPointerLoader loader)
        {
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader));
            }

            HAL_CleanCANFunc = (delegate* unmanaged[Cdecl] < System.Int32, void >)loader.GetProcAddress("HAL_CleanCAN");
            HAL_InitializeCANFunc = (delegate* unmanaged[Cdecl] < Hal.CANManufacturer, System.Int32, Hal.CANDeviceType, int *, System.Int32 >)loader.GetProcAddress("HAL_InitializeCAN");
            HAL_ReadCANPacketLatestFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, System.Byte *, System.Int32 *, System.UInt64 *, int *, void >)loader.GetProcAddress("HAL_ReadCANPacketLatest");
            HAL_ReadCANPacketNewFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, System.Byte *, System.Int32 *, System.UInt64 *, int *, void >)loader.GetProcAddress("HAL_ReadCANPacketNew");
            HAL_ReadCANPacketTimeoutFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, System.Byte *, System.Int32 *, System.UInt64 *, System.Int32, int *, void >)loader.GetProcAddress("HAL_ReadCANPacketTimeout");
            HAL_StopCANPacketRepeatingFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_StopCANPacketRepeating");
            HAL_WriteCANPacketFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Byte *, System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_WriteCANPacket");
            HAL_WriteCANPacketRepeatingFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Byte *, System.Int32, System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_WriteCANPacketRepeating");
            HAL_WriteCANRTRFrameFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_WriteCANRTRFrame");
        }

        private readonly delegate* unmanaged[Cdecl]<int, void> HAL_CleanCANFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_CleanCAN(int handle)
        {
            HAL_CleanCANFunc(handle);
        }



        private readonly delegate* unmanaged[Cdecl]<CANManufacturer, int, CANDeviceType, int*, int> HAL_InitializeCANFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_InitializeCAN(CANManufacturer manufacturer, int deviceId, CANDeviceType deviceType)
        {
            int status = 0;
            var retVal = HAL_InitializeCANFunc(manufacturer, deviceId, deviceType, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, byte*, int*, ulong*, int*, void> HAL_ReadCANPacketLatestFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_ReadCANPacketLatest(int handle, int apiId, byte* data, int* length, ulong* receivedTimestamp)
        {
            int status = 0;
            HAL_ReadCANPacketLatestFunc(handle, apiId, data, length, receivedTimestamp, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, byte*, int*, ulong*, int*, void> HAL_ReadCANPacketNewFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_ReadCANPacketNew(int handle, int apiId, byte* data, int* length, ulong* receivedTimestamp)
        {
            int status = 0;
            HAL_ReadCANPacketNewFunc(handle, apiId, data, length, receivedTimestamp, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, byte*, int*, ulong*, int, int*, void> HAL_ReadCANPacketTimeoutFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_ReadCANPacketTimeout(int handle, int apiId, byte* data, int* length, ulong* receivedTimestamp, int timeoutMs)
        {
            int status = 0;
            HAL_ReadCANPacketTimeoutFunc(handle, apiId, data, length, receivedTimestamp, timeoutMs, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_StopCANPacketRepeatingFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_StopCANPacketRepeating(int handle, int apiId)
        {
            int status = 0;
            HAL_StopCANPacketRepeatingFunc(handle, apiId, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, byte*, int, int, int*, void> HAL_WriteCANPacketFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_WriteCANPacket(int handle, byte* data, int length, int apiId)
        {
            int status = 0;
            HAL_WriteCANPacketFunc(handle, data, length, apiId, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, byte*, int, int, int, int*, void> HAL_WriteCANPacketRepeatingFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_WriteCANPacketRepeating(int handle, byte* data, int length, int apiId, int repeatMs)
        {
            int status = 0;
            HAL_WriteCANPacketRepeatingFunc(handle, data, length, apiId, repeatMs, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int, int*, void> HAL_WriteCANRTRFrameFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_WriteCANRTRFrame(int handle, int length, int apiId)
        {
            int status = 0;
            HAL_WriteCANRTRFrameFunc(handle, length, apiId, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



    }
}
