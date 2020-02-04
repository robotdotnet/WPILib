using WPIUtil.ILGeneration;

namespace Hal.Natives
{
    [StatusCheckedBy(typeof(StatusHandling))]
    public unsafe interface IAnalogInput
    {
        int HAL_CheckAnalogInputChannel(int channel);

        int HAL_CheckAnalogModule(int module);

        void HAL_FreeAnalogInputPort(int analogPortHandle);

        [StatusCheckLastParameter] int HAL_GetAnalogAverageBits(int analogPortHandle);

        [StatusCheckLastParameter] int HAL_GetAnalogAverageValue(int analogPortHandle);

        [StatusCheckLastParameter] double HAL_GetAnalogAverageVoltage(int analogPortHandle);

        [StatusCheckLastParameter] int HAL_GetAnalogLSBWeight(int analogPortHandle);

        [StatusCheckLastParameter] int HAL_GetAnalogOffset(int analogPortHandle);

        [StatusCheckLastParameter] int HAL_GetAnalogOversampleBits(int analogPortHandle);

        [StatusCheckLastParameter] double HAL_GetAnalogSampleRate();

        [StatusCheckLastParameter] int HAL_GetAnalogValue(int analogPortHandle);

        [StatusCheckLastParameter] double HAL_GetAnalogValueToVolts(int analogPortHandle, int rawValue);

        [StatusCheckLastParameter] double HAL_GetAnalogVoltage(int analogPortHandle);

        [StatusCheckLastParameter] int HAL_GetAnalogVoltsToValue(int analogPortHandle, double voltage);

        [StatusCheckRange(0, typeof(StatusHandling), "AnalogInputStatusCheck")] int HAL_InitializeAnalogInputPort(int portHandle);

        [StatusCheckLastParameter] void HAL_SetAnalogAverageBits(int analogPortHandle, int bits);

        void HAL_SetAnalogInputSimDevice(int handle, int device);

        [StatusCheckLastParameter] void HAL_SetAnalogOversampleBits(int analogPortHandle, int bits);

        [StatusCheckLastParameter] void HAL_SetAnalogSampleRate(double samplesPerSecond);

    }
}
