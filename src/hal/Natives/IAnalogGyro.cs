using WPIUtil.ILGeneration;

namespace Hal.Natives
{
    [StatusCheckedBy(typeof(StatusHandling))]
    public unsafe interface IAnalogGyro
    {
        [StatusCheckLastParameter] void HAL_CalibrateAnalogGyro(int handle);

        void HAL_FreeAnalogGyro(int handle);

        [StatusCheckLastParameter] double HAL_GetAnalogGyroAngle(int handle);

        [StatusCheckLastParameter] int HAL_GetAnalogGyroCenter(int handle);

        [StatusCheckLastParameter] double HAL_GetAnalogGyroOffset(int handle);

        [StatusCheckLastParameter] double HAL_GetAnalogGyroRate(int handle);

        [StatusCheckRange(0, typeof(StatusHandling), "AnalogGyroStatusCheck")] int HAL_InitializeAnalogGyro(int analogHandle);

        [StatusCheckLastParameter] void HAL_ResetAnalogGyro(int handle);

        [StatusCheckLastParameter] void HAL_SetAnalogGyroDeadband(int handle, double volts);

        [StatusCheckLastParameter] void HAL_SetAnalogGyroParameters(int handle, double voltsPerDegreePerSecond, double offset, int center);

        [StatusCheckLastParameter] void HAL_SetAnalogGyroVoltsPerDegreePerSecond(int handle, double voltsPerDegreePerSecond);

        [StatusCheckLastParameter] void HAL_SetupAnalogGyro(int handle);

    }
}
