using WPIUtil.ILGeneration;

namespace Hal.Natives
{
   public unsafe interface IDIO
    {
        [StatusCheckLastParameter]  int HAL_AllocateDigitalPWM();

         int HAL_CheckDIOChannel(int channel);

         void HAL_FreeDIOPort(int dioPortHandle);

        [StatusCheckLastParameter] void HAL_SetDIO(int dioPortHandle, int value);

        [StatusCheckLastParameter]  void HAL_FreeDigitalPWM(int pwmGenerator);

        [StatusCheckLastParameter]  int HAL_GetDIO(int dioPortHandle);

        [StatusCheckLastParameter]  int HAL_GetDIODirection(int dioPortHandle);

        [StatusCheckLastParameter]  long HAL_GetFilterPeriod(int filterIndex);

        [StatusCheckLastParameter]  int HAL_GetFilterSelect(int dioPortHandle);

        [StatusCheckLastParameter]  int HAL_InitializeDIOPort(int portHandle, int input);

        [StatusCheckLastParameter]  int HAL_IsAnyPulsing();

        [StatusCheckLastParameter]  int HAL_IsPulsing(int dioPortHandle);

        [StatusCheckLastParameter]  void HAL_Pulse(int dioPortHandle, double pulseLength);

         void HAL_SetDIOSimDevice(int handle, int device);

        [StatusCheckLastParameter]  void HAL_SetDIODirection(int dioPortHandle, int input);

        [StatusCheckLastParameter]  void HAL_SetDigitalPWMDutyCycle(int pwmGenerator, double dutyCycle);

        [StatusCheckLastParameter]  void HAL_SetDigitalPWMOutputChannel(int pwmGenerator, int channel);

        [StatusCheckLastParameter]  void HAL_SetDigitalPWMRate(double rate);

        [StatusCheckLastParameter]  void HAL_SetFilterPeriod(int filterIndex, long value);

        [StatusCheckLastParameter]  void HAL_SetFilterSelect(int dioPortHandle, int filterIndex);

    }
}
