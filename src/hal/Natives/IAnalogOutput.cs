using WPIUtil.ILGeneration;

namespace Hal.Natives
{
    public unsafe interface IAnalogOutput
    {
        int HAL_CheckAnalogOutputChannel(int channel);

        void HAL_FreeAnalogOutputPort(int analogOutputHandle);

        [StatusCheckLastParameter] double HAL_GetAnalogOutput(int analogOutputHandle);

        [StatusCheckLastParameter] int HAL_InitializeAnalogOutputPort(int portHandle);

        [StatusCheckLastParameter] void HAL_SetAnalogOutput(int analogOutputHandle, double voltage);

    }
}
