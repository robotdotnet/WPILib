using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
namespace Hal.Natives
{
    public unsafe class PWMLowLevelNative
    {
        [NativeFunctionPointer("HAL_CheckPWMChannel")]
        private readonly delegate* unmanaged[Cdecl]<int, int> HAL_CheckPWMChannelFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_CheckPWMChannel(int channel)
        {
            return HAL_CheckPWMChannelFunc(channel);
        }


        [NativeFunctionPointer("HAL_FreePWMPort")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_FreePWMPortFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_FreePWMPort(int pwmPortHandle)
        {
            int status = 0;
            HAL_FreePWMPortFunc(pwmPortHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }


        [NativeFunctionPointer("HAL_GetPWMCycleStartTime")]
        private readonly delegate* unmanaged[Cdecl]<int*, ulong> HAL_GetPWMCycleStartTimeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong HAL_GetPWMCycleStartTime()
        {
            int status = 0;
            var retVal = HAL_GetPWMCycleStartTimeFunc(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetPWMEliminateDeadband")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetPWMEliminateDeadbandFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetPWMEliminateDeadband(int pwmPortHandle)
        {
            int status = 0;
            var retVal = HAL_GetPWMEliminateDeadbandFunc(pwmPortHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetPWMLoopTiming")]
        private readonly delegate* unmanaged[Cdecl]<int*, int> HAL_GetPWMLoopTimingFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetPWMLoopTiming()
        {
            int status = 0;
            var retVal = HAL_GetPWMLoopTimingFunc(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetPWMPosition")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, double> HAL_GetPWMPositionFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetPWMPosition(int pwmPortHandle)
        {
            int status = 0;
            var retVal = HAL_GetPWMPositionFunc(pwmPortHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetPWMRaw")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetPWMRawFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetPWMRaw(int pwmPortHandle)
        {
            int status = 0;
            var retVal = HAL_GetPWMRawFunc(pwmPortHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetPWMSpeed")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, double> HAL_GetPWMSpeedFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetPWMSpeed(int pwmPortHandle)
        {
            int status = 0;
            var retVal = HAL_GetPWMSpeedFunc(pwmPortHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_InitializePWMPort")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_InitializePWMPortFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_InitializePWMPort(int portHandle)
        {
            int status = 0;
            var retVal = HAL_InitializePWMPortFunc(portHandle, &status);
            Hal.StatusHandling.PWMStatusCheck(status, portHandle);
            return retVal;
        }


        [NativeFunctionPointer("HAL_LatchPWMZero")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_LatchPWMZeroFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_LatchPWMZero(int pwmPortHandle)
        {
            int status = 0;
            HAL_LatchPWMZeroFunc(pwmPortHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }


        [NativeFunctionPointer("HAL_SetPWMConfig")]
        private readonly delegate* unmanaged[Cdecl]<int, double, double, double, double, double, int*, void> HAL_SetPWMConfigFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetPWMConfig(int pwmPortHandle, double maxPwm, double deadbandMaxPwm, double centerPwm, double deadbandMinPwm, double minPwm)
        {
            int status = 0;
            HAL_SetPWMConfigFunc(pwmPortHandle, maxPwm, deadbandMaxPwm, centerPwm, deadbandMinPwm, minPwm, &status);
            Hal.StatusHandling.StatusCheck(status);
        }


        [NativeFunctionPointer("HAL_SetPWMConfigRaw")]
        private readonly delegate* unmanaged[Cdecl]<int, int, int, int, int, int, int*, void> HAL_SetPWMConfigRawFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetPWMConfigRaw(int pwmPortHandle, int maxPwm, int deadbandMaxPwm, int centerPwm, int deadbandMinPwm, int minPwm)
        {
            int status = 0;
            HAL_SetPWMConfigRawFunc(pwmPortHandle, maxPwm, deadbandMaxPwm, centerPwm, deadbandMinPwm, minPwm, &status);
            Hal.StatusHandling.StatusCheck(status);
        }


        [NativeFunctionPointer("HAL_SetPWMDisabled")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_SetPWMDisabledFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetPWMDisabled(int pwmPortHandle)
        {
            int status = 0;
            HAL_SetPWMDisabledFunc(pwmPortHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }


        [NativeFunctionPointer("HAL_SetPWMEliminateDeadband")]
        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetPWMEliminateDeadbandFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetPWMEliminateDeadband(int pwmPortHandle, int eliminateDeadband)
        {
            int status = 0;
            HAL_SetPWMEliminateDeadbandFunc(pwmPortHandle, eliminateDeadband, &status);
            Hal.StatusHandling.StatusCheck(status);
        }


        [NativeFunctionPointer("HAL_SetPWMPeriodScale")]
        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetPWMPeriodScaleFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetPWMPeriodScale(int pwmPortHandle, int squelchMask)
        {
            int status = 0;
            HAL_SetPWMPeriodScaleFunc(pwmPortHandle, squelchMask, &status);
            Hal.StatusHandling.StatusCheck(status);
        }


        [NativeFunctionPointer("HAL_SetPWMPosition")]
        private readonly delegate* unmanaged[Cdecl]<int, double, int*, void> HAL_SetPWMPositionFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetPWMPosition(int pwmPortHandle, double position)
        {
            int status = 0;
            HAL_SetPWMPositionFunc(pwmPortHandle, position, &status);
            Hal.StatusHandling.StatusCheck(status);
        }


        [NativeFunctionPointer("HAL_SetPWMRaw")]
        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetPWMRawFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetPWMRaw(int pwmPortHandle, int value)
        {
            int status = 0;
            HAL_SetPWMRawFunc(pwmPortHandle, value, &status);
            Hal.StatusHandling.StatusCheck(status);
        }


        [NativeFunctionPointer("HAL_SetPWMSpeed")]
        private readonly delegate* unmanaged[Cdecl]<int, double, int*, void> HAL_SetPWMSpeedFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetPWMSpeed(int pwmPortHandle, double speed)
        {
            int status = 0;
            HAL_SetPWMSpeedFunc(pwmPortHandle, speed, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



    }
}
