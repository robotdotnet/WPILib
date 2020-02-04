using WPIUtil.ILGeneration;

namespace Hal.Natives
{
    [StatusCheckedBy(typeof(StatusHandling))]
    public unsafe interface ISPI
    {
        void HAL_CloseSPI(SPIPort port);

        [StatusCheckLastParameter] void HAL_ConfigureSPIAutoStall(SPIPort port, int csToSclkTicks, int stallTicks, int pow2BytesPerRead);

        [StatusCheckLastParameter] void HAL_ForceSPIAutoRead(SPIPort port);

        [StatusCheckLastParameter] void HAL_FreeSPIAuto(SPIPort port);

        [StatusCheckLastParameter] int HAL_GetSPIAutoDroppedCount(SPIPort port);

        int HAL_GetSPIHandle(SPIPort port);

        [StatusCheckLastParameter] void HAL_InitSPIAuto(SPIPort port, int bufferSize);

        [StatusCheckRange(0, typeof(StatusHandling), "SPIStatusCheck")] void HAL_InitializeSPI(SPIPort port);

        int HAL_ReadSPI(SPIPort port, byte* buffer, int count);

        [StatusCheckLastParameter] int HAL_ReadSPIAutoReceivedData(SPIPort port, uint* buffer, int numToRead, double timeout);

        [StatusCheckLastParameter] void HAL_SetSPIAutoTransmitData(SPIPort port, byte* dataToSend, int dataSize, int zeroSize);

        [StatusCheckLastParameter] void HAL_SetSPIChipSelectActiveHigh(SPIPort port);

        [StatusCheckLastParameter] void HAL_SetSPIChipSelectActiveLow(SPIPort port);

        void HAL_SetSPIHandle(SPIPort port, int handle);

        void HAL_SetSPIOpts(SPIPort port, int msbFirst, int sampleOnTrailing, int clkIdleHigh);

        void HAL_SetSPISpeed(SPIPort port, int speed);

        [StatusCheckLastParameter] void HAL_StartSPIAutoRate(SPIPort port, double period);

        [StatusCheckLastParameter] void HAL_StartSPIAutoTrigger(SPIPort port, int digitalSourceHandle, AnalogTriggerType analogTriggerType, int triggerRising, int triggerFalling);

        [StatusCheckLastParameter] void HAL_StopSPIAuto(SPIPort port);

        int HAL_TransactionSPI(SPIPort port, byte* dataToSend, byte* dataReceived, int size);

        int HAL_WriteSPI(SPIPort port, byte* dataToSend, int sendSize);

    }
}
