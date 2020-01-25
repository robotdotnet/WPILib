using WPIUtil.ILGeneration;

namespace Hal.Natives
{
   public unsafe interface ISPI
    {
         void HAL_CloseSPI(HAL_SPIPort port);

        [StatusCheckLastParameter]  void HAL_ConfigureSPIAutoStall(HAL_SPIPort port, int csToSclkTicks, int stallTicks, int pow2BytesPerRead);

        [StatusCheckLastParameter]  void HAL_ForceSPIAutoRead(HAL_SPIPort port);

        [StatusCheckLastParameter]  void HAL_FreeSPIAuto(HAL_SPIPort port);

        [StatusCheckLastParameter]  int HAL_GetSPIAutoDroppedCount(HAL_SPIPort port);

         int HAL_GetSPIHandle(HAL_SPIPort port);

        [StatusCheckLastParameter]  void HAL_InitSPIAuto(HAL_SPIPort port, int bufferSize);

        [StatusCheckLastParameter]  void HAL_InitializeSPI(HAL_SPIPort port);

         int HAL_ReadSPI(HAL_SPIPort port, byte* buffer, int count);

        [StatusCheckLastParameter]  int HAL_ReadSPIAutoReceivedData(HAL_SPIPort port, uint* buffer, int numToRead, double timeout);

        [StatusCheckLastParameter]  void HAL_SetSPIAutoTransmitData(HAL_SPIPort port,  byte* dataToSend, int dataSize, int zeroSize);

        [StatusCheckLastParameter]  void HAL_SetSPIChipSelectActiveHigh(HAL_SPIPort port);

        [StatusCheckLastParameter]  void HAL_SetSPIChipSelectActiveLow(HAL_SPIPort port);

         void HAL_SetSPIHandle(HAL_SPIPort port, int handle);

         void HAL_SetSPIOpts(HAL_SPIPort port, int msbFirst, int sampleOnTrailing, int clkIdleHigh);

         void HAL_SetSPISpeed(HAL_SPIPort port, int speed);

        [StatusCheckLastParameter]  void HAL_StartSPIAutoRate(HAL_SPIPort port, double period);

        [StatusCheckLastParameter]  void HAL_StartSPIAutoTrigger(HAL_SPIPort port, int digitalSourceHandle, HAL_AnalogTriggerType analogTriggerType, int triggerRising, int triggerFalling);

        [StatusCheckLastParameter]  void HAL_StopSPIAuto(HAL_SPIPort port);

         int HAL_TransactionSPI(HAL_SPIPort port,  byte* dataToSend, byte* dataReceived, int size);

         int HAL_WriteSPI(HAL_SPIPort port,  byte* dataToSend, int sendSize);

    }
}
