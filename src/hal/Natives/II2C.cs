using WPIUtil.ILGeneration;

namespace Hal.Natives
{
   public unsafe interface II2C
    {
         void HAL_CloseI2C(I2CPort port);

        [StatusCheckLastParameter]  void HAL_InitializeI2C(I2CPort port);

         int HAL_ReadI2C(I2CPort port, int deviceAddress, byte* buffer, int count);

         int HAL_TransactionI2C(I2CPort port, int deviceAddress,  byte* dataToSend, int sendSize, byte* dataReceived, int receiveSize);

         int HAL_WriteI2C(I2CPort port, int deviceAddress,  byte* dataToSend, int sendSize);

    }
}
