using WPIUtil.ILGeneration;

namespace Hal.Natives
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
    [StatusCheckedBy(typeof(StatusHandling))]
    public unsafe interface IAnalogOutput
    {
        int HAL_CheckAnalogOutputChannel(int channel);

        void HAL_FreeAnalogOutputPort(int analogOutputHandle);

        [StatusCheckLastParameter] double HAL_GetAnalogOutput(int analogOutputHandle);

        [StatusCheckRange(0, typeof(StatusHandling), "AnalogOutputStatusCheck")] int HAL_InitializeAnalogOutputPort(int portHandle);

        [StatusCheckLastParameter] void HAL_SetAnalogOutput(int analogOutputHandle, double voltage);

    }
}
