using WPIUtil.ILGeneration;

namespace Hal.Natives
{
    [StatusCheckedBy(typeof(StatusHandling))]
    public unsafe interface IPWM
    {
        int HAL_CheckPWMChannel(int channel);

        [StatusCheckLastParameter] void HAL_FreePWMPort(int pwmPortHandle);

        [StatusCheckLastParameter] ulong HAL_GetPWMCycleStartTime();

        [StatusCheckLastParameter] int HAL_GetPWMEliminateDeadband(int pwmPortHandle);

        [StatusCheckLastParameter] int HAL_GetPWMLoopTiming();

        [StatusCheckLastParameter] double HAL_GetPWMPosition(int pwmPortHandle);

        [StatusCheckLastParameter] int HAL_GetPWMRaw(int pwmPortHandle);

        [StatusCheckLastParameter] double HAL_GetPWMSpeed(int pwmPortHandle);

        [StatusCheckRange(0, typeof(StatusHandling), "PWMStatusCheck")] int HAL_InitializePWMPort(int portHandle);

        [StatusCheckLastParameter] void HAL_LatchPWMZero(int pwmPortHandle);

        [StatusCheckLastParameter] void HAL_SetPWMConfig(int pwmPortHandle, double maxPwm, double deadbandMaxPwm, double centerPwm, double deadbandMinPwm, double minPwm);

        [StatusCheckLastParameter] void HAL_SetPWMConfigRaw(int pwmPortHandle, int maxPwm, int deadbandMaxPwm, int centerPwm, int deadbandMinPwm, int minPwm);

        [StatusCheckLastParameter] void HAL_SetPWMDisabled(int pwmPortHandle);

        [StatusCheckLastParameter] void HAL_SetPWMEliminateDeadband(int pwmPortHandle, int eliminateDeadband);

        [StatusCheckLastParameter] void HAL_SetPWMPeriodScale(int pwmPortHandle, int squelchMask);

        [StatusCheckLastParameter] void HAL_SetPWMPosition(int pwmPortHandle, double position);

        [StatusCheckLastParameter] void HAL_SetPWMRaw(int pwmPortHandle, int value);

        [StatusCheckLastParameter] void HAL_SetPWMSpeed(int pwmPortHandle, double speed);

    }
}
