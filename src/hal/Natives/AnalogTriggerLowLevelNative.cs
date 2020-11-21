using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
namespace Hal.Natives
{
    public unsafe class AnalogTriggerLowLevelNative : IAnalogTrigger
    {
        [NativeFunctionPointer("HAL_CleanAnalogTrigger")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_CleanAnalogTriggerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_CleanAnalogTrigger(int analogTriggerHandle)
        {
            int status = 0;
            HAL_CleanAnalogTriggerFunc(analogTriggerHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }


        [NativeFunctionPointer("HAL_GetAnalogTriggerFPGAIndex")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetAnalogTriggerFPGAIndexFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetAnalogTriggerFPGAIndex(int analogTriggerHandle)
        {
            int status = 0;
            var retVal = HAL_GetAnalogTriggerFPGAIndexFunc(analogTriggerHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetAnalogTriggerInWindow")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetAnalogTriggerInWindowFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetAnalogTriggerInWindow(int analogTriggerHandle)
        {
            int status = 0;
            var retVal = HAL_GetAnalogTriggerInWindowFunc(analogTriggerHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetAnalogTriggerOutput")]
        private readonly delegate* unmanaged[Cdecl]<int, AnalogTriggerType, int*, int> HAL_GetAnalogTriggerOutputFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetAnalogTriggerOutput(int analogTriggerHandle, AnalogTriggerType type)
        {
            int status = 0;
            var retVal = HAL_GetAnalogTriggerOutputFunc(analogTriggerHandle, type, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetAnalogTriggerTriggerState")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetAnalogTriggerTriggerStateFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetAnalogTriggerTriggerState(int analogTriggerHandle)
        {
            int status = 0;
            var retVal = HAL_GetAnalogTriggerTriggerStateFunc(analogTriggerHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_InitializeAnalogTrigger")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_InitializeAnalogTriggerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_InitializeAnalogTrigger(int portHandle)
        {
            int status = 0;
            var retVal = HAL_InitializeAnalogTriggerFunc(portHandle, &status);
            Hal.StatusHandling.AnalogTriggerStatusCheck(status, portHandle);
            return retVal;
        }


        [NativeFunctionPointer("HAL_InitializeAnalogTriggerDutyCycle")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_InitializeAnalogTriggerDutyCycleFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_InitializeAnalogTriggerDutyCycle(int dutyCycleHandle)
        {
            int status = 0;
            var retVal = HAL_InitializeAnalogTriggerDutyCycleFunc(dutyCycleHandle, &status);
            Hal.StatusHandling.AnalogTriggerDutyCycleStatusCheck(status, dutyCycleHandle);
            return retVal;
        }


        [NativeFunctionPointer("HAL_SetAnalogTriggerAveraged")]
        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetAnalogTriggerAveragedFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetAnalogTriggerAveraged(int analogTriggerHandle, int useAveragedValue)
        {
            int status = 0;
            HAL_SetAnalogTriggerAveragedFunc(analogTriggerHandle, useAveragedValue, &status);
            Hal.StatusHandling.StatusCheck(status);
        }


        [NativeFunctionPointer("HAL_SetAnalogTriggerFiltered")]
        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetAnalogTriggerFilteredFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetAnalogTriggerFiltered(int analogTriggerHandle, int useFilteredValue)
        {
            int status = 0;
            HAL_SetAnalogTriggerFilteredFunc(analogTriggerHandle, useFilteredValue, &status);
            Hal.StatusHandling.StatusCheck(status);
        }


        [NativeFunctionPointer("HAL_SetAnalogTriggerLimitsRaw")]
        private readonly delegate* unmanaged[Cdecl]<int, int, int, int*, void> HAL_SetAnalogTriggerLimitsRawFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetAnalogTriggerLimitsRaw(int analogTriggerHandle, int lower, int upper)
        {
            int status = 0;
            HAL_SetAnalogTriggerLimitsRawFunc(analogTriggerHandle, lower, upper, &status);
            Hal.StatusHandling.StatusCheck(status);
        }


        [NativeFunctionPointer("HAL_SetAnalogTriggerLimitsVoltage")]
        private readonly delegate* unmanaged[Cdecl]<int, double, double, int*, void> HAL_SetAnalogTriggerLimitsVoltageFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetAnalogTriggerLimitsVoltage(int analogTriggerHandle, double lower, double upper)
        {
            int status = 0;
            HAL_SetAnalogTriggerLimitsVoltageFunc(analogTriggerHandle, lower, upper, &status);
            Hal.StatusHandling.StatusCheck(status);
        }


        [NativeFunctionPointer("HAL_SetAnalogTriggerLimitsDutyCycle")]
        private readonly delegate* unmanaged[Cdecl]<int, double, double, int*, void> HAL_SetAnalogTriggerLimitsDutyCycleFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetAnalogTriggerLimitsDutyCycle(int analogTriggerHandle, double lower, double upper)
        {
            int status = 0;
            HAL_SetAnalogTriggerLimitsDutyCycleFunc(analogTriggerHandle, lower, upper, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



    }
}
