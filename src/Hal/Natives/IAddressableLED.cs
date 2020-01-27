using WPIUtil.ILGeneration;

namespace Hal.Natives
{
   public unsafe interface IAddressableLED
    {
         void HAL_FreeAddressableLED(int handle);

        [StatusCheckLastParameter]  int HAL_InitializeAddressableLED( int outputPort);

        [StatusCheckLastParameter]  void HAL_SetAddressableLEDBitTiming(int handle, int lowTime0NanoSeconds, int highTime0NanoSeconds, int lowTime1NanoSeconds, int highTime1NanoSeconds);

        [StatusCheckLastParameter]  void HAL_SetAddressableLEDLength(int handle, int length);

        [StatusCheckLastParameter]  void HAL_SetAddressableLEDOutputPort(int handle, int outputPort);

        [StatusCheckLastParameter]  void HAL_SetAddressableLEDSyncTime(int handle, int syncTimeMicroSeconds);

        [StatusCheckLastParameter]  void HAL_StartAddressableLEDOutput(int handle);

        [StatusCheckLastParameter]  void HAL_StopAddressableLEDOutput(int handle);

        [StatusCheckLastParameter]  void HAL_WriteAddressableLEDData(int handle, AddressableLEDData* data, int length);

    }
}
