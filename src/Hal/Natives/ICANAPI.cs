using System;
using System.Collections.Generic;
using System.Text;
using WPIUtil.ILGeneration;

namespace Hal.Natives
{
    public unsafe interface ICANAPI
    {
        [StatusCheckLastParameter]
        int HAL_InitializeCAN(CANManufacturer manufacturer, int deviceId, CANDeviceType deviceType);

        void HAL_CleanCAN(int handle);

        [StatusCheckLastParameter]
        void HAL_WriteCANPacket(int handle, byte* data, int length, int apiId);

        [StatusCheckLastParameter]
        void HAL_WriteCANPacketRepeating(int handle, byte* data, int length, int apiId, int repeatMs);

        [StatusCheckLastParameter]
        void HAL_WriteCANRTRFrame(int handle, int length, int apiId);

        [StatusCheckLastParameter]
        void HAL_StopCANPacketRepeating(int handle, int apiId);

        [StatusCheckLastParameter]
        void HAL_ReadCANPacketNew(int handle, int apiId, byte* data, int* length, ulong* receivedTimestamp);

        [StatusCheckLastParameter]
        void HAL_ReadCANPacketLatest(int handle, int apiId, byte* data, int* length, ulong* receivedTimestamp);

        [StatusCheckLastParameter]
        void HAL_ReadCANPacketTimeout(int handle, int apiId, byte* data, int* length, ulong* receivedTimestamp, int timeoutMs);

    }
}
