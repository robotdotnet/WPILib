using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
namespace Hal.Natives
{
    public unsafe class CANAPINative : ICANAPI
    {
        [NativeFunctionPointer("HAL_CleanCAN")]
        private readonly delegate* unmanaged[Cdecl]<int, void> HAL_CleanCANFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_CleanCAN(int handle)
        {
            HAL_CleanCANFunc(handle);
        }


        [NativeFunctionPointer("HAL_InitializeCAN")]
        private readonly delegate* unmanaged[Cdecl]<CANManufacturer, int, CANDeviceType, int*, int> HAL_InitializeCANFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_InitializeCAN(CANManufacturer manufacturer, int deviceId, CANDeviceType deviceType)
        {
            int status = 0;
            var retVal = HAL_InitializeCANFunc(manufacturer, deviceId, deviceType, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_ReadCANPacketLatest")]
        private readonly delegate* unmanaged[Cdecl]<int, int, byte*, int*, ulong*, int*, void> HAL_ReadCANPacketLatestFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_ReadCANPacketLatest(int handle, int apiId, byte* data, int* length, ulong* receivedTimestamp)
        {
            int status = 0;
            HAL_ReadCANPacketLatestFunc(handle, apiId, data, length, receivedTimestamp, &status);
            Hal.StatusHandling.StatusCheck(status);
        }


        [NativeFunctionPointer("HAL_ReadCANPacketNew")]
        private readonly delegate* unmanaged[Cdecl]<int, int, byte*, int*, ulong*, int*, void> HAL_ReadCANPacketNewFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_ReadCANPacketNew(int handle, int apiId, byte* data, int* length, ulong* receivedTimestamp)
        {
            int status = 0;
            HAL_ReadCANPacketNewFunc(handle, apiId, data, length, receivedTimestamp, &status);
            Hal.StatusHandling.StatusCheck(status);
        }


        [NativeFunctionPointer("HAL_ReadCANPacketTimeout")]
        private readonly delegate* unmanaged[Cdecl]<int, int, byte*, int*, ulong*, int, int*, void> HAL_ReadCANPacketTimeoutFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_ReadCANPacketTimeout(int handle, int apiId, byte* data, int* length, ulong* receivedTimestamp, int timeoutMs)
        {
            int status = 0;
            HAL_ReadCANPacketTimeoutFunc(handle, apiId, data, length, receivedTimestamp, timeoutMs, &status);
            Hal.StatusHandling.StatusCheck(status);
        }


        [NativeFunctionPointer("HAL_StopCANPacketRepeating")]
        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_StopCANPacketRepeatingFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_StopCANPacketRepeating(int handle, int apiId)
        {
            int status = 0;
            HAL_StopCANPacketRepeatingFunc(handle, apiId, &status);
            Hal.StatusHandling.StatusCheck(status);
        }


        [NativeFunctionPointer("HAL_WriteCANPacket")]
        private readonly delegate* unmanaged[Cdecl]<int, byte*, int, int, int*, void> HAL_WriteCANPacketFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_WriteCANPacket(int handle, byte* data, int length, int apiId)
        {
            int status = 0;
            HAL_WriteCANPacketFunc(handle, data, length, apiId, &status);
            Hal.StatusHandling.StatusCheck(status);
        }


        [NativeFunctionPointer("HAL_WriteCANPacketRepeating")]
        private readonly delegate* unmanaged[Cdecl]<int, byte*, int, int, int, int*, void> HAL_WriteCANPacketRepeatingFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_WriteCANPacketRepeating(int handle, byte* data, int length, int apiId, int repeatMs)
        {
            int status = 0;
            HAL_WriteCANPacketRepeatingFunc(handle, data, length, apiId, repeatMs, &status);
            Hal.StatusHandling.StatusCheck(status);
        }


        [NativeFunctionPointer("HAL_WriteCANRTRFrame")]
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
