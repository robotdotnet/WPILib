using System;
using System.Collections.Generic;
using System.Text;
using WPIUtil.ILGeneration;

namespace Hal.Natives
{
    public interface IPWM
    {
        [StatusCheckLastParameter]
        int HAL_InitializePWMPort(int portHandle);

        [StatusCheckLastParameter]
        void HAL_FreePWMPort(int pwmPortHandle);

        int HAL_CheckPWMChannel(int channel);

        [StatusCheckLastParameter]
        void HAL_SetPWMConfig(int pwmPortHandle, double maxPwm, double deadbandMaxPwm, double centerPwm, double deadbandMinPwm, double minPwm);

        [StatusCheckLastParameter]
        void HAL_SetPWMConfigRaw(int pwmPortHandle, int maxPwm, int deadbandMaxPwm, int centerPwm, int deadbandMinPwm, int minPwm);

        [StatusCheckLastParameter]
        unsafe void HAL_GetPWMConfigRaw(int pwmPortHandle, int* maxPwm, int* deadbandMaxPwm, int* centerPwm, int* deadbandMinPwm, int* minPwm);

        [StatusCheckLastParameter]
        void HAL_SetPWMEliminateDeadband(int pwmPortHandle, int eliminateDeadband);

        [StatusCheckLastParameter]
        int HAL_GetPWMEliminateDeadband(int pwmPortHandle);

        [StatusCheckLastParameter]
        void HAL_SetPWMRaw(int pwmPortHandle, int value);

        [StatusCheckLastParameter]
        void HAL_SetPWMSpeed(int pwmPortHandle, double speed);

        [StatusCheckLastParameter]
        void HAL_SetPWMPosition(int pwmPortHandle, double position);

        [StatusCheckLastParameter]
        void HAL_SetPWMDisabled(int pwmPortHandle);


        [StatusCheckLastParameter]
        int HAL_GetPWMRaw(int pwmPortHandle);

        [StatusCheckLastParameter]
        double HAL_GetPWMSpeed(int pwmPortHandle);

        [StatusCheckLastParameter]
        double HAL_GetPWMPosition(int pwmPortHandle);

        [StatusCheckLastParameter]
        void HAL_LatchPWMZero(int pwmPortHandle);

        [StatusCheckLastParameter]
        void HAL_SetPWMPeriodScale(int pwmPortHandle, int squelchMask);

        [StatusCheckLastParameter]
        int HAL_GetPWMLoopTiming();

        [StatusCheckLastParameter]
        ulong HAL_GetPWMCycleStartTime();
    }
}
