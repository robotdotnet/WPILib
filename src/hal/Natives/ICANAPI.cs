using WPIUtil.ILGeneration;

namespace Hal.Natives
{
   public unsafe interface ICANAPI
    {
        void HAL_CleanCAN(int handle);

        [StatusCheckLastParameter]  int HAL_InitializeCAN(CANManufacturer manufacturer, int deviceId, CANDeviceType deviceType);

        [StatusCheckLastParameter]  void HAL_ReadCANPacketLatest(int handle, int apiId, byte* data, int* length, ulong* receivedTimestamp);

        [StatusCheckLastParameter]  void HAL_ReadCANPacketNew(int handle, int apiId, byte* data, int* length, ulong* receivedTimestamp);

        [StatusCheckLastParameter]  void HAL_ReadCANPacketTimeout(int handle, int apiId, byte* data, int* length, ulong* receivedTimestamp, int timeoutMs);

        [StatusCheckLastParameter]  void HAL_StopCANPacketRepeating(int handle, int apiId);

        [StatusCheckLastParameter]  void HAL_WriteCANPacket(int handle,  byte* data, int length, int apiId);

        [StatusCheckLastParameter]  void HAL_WriteCANPacketRepeating(int handle,  byte* data, int length, int apiId, int repeatMs);

        [StatusCheckLastParameter]  void HAL_WriteCANRTRFrame(int handle, int length, int apiId);

    }
}
