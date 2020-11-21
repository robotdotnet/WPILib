using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
namespace Hal.Natives
{
    public unsafe class DutyCycleLowLevelNative
    {
        [NativeFunctionPointer("HAL_FreeDutyCycle")]
        private readonly delegate* unmanaged[Cdecl]<int, void> HAL_FreeDutyCycleFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_FreeDutyCycle(int dutyCycleHandle)
        {
            HAL_FreeDutyCycleFunc(dutyCycleHandle);
        }


        [NativeFunctionPointer("HAL_GetDutyCycleFPGAIndex")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetDutyCycleFPGAIndexFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetDutyCycleFPGAIndex(int dutyCycleHandle)
        {
            int status = 0;
            var retVal = HAL_GetDutyCycleFPGAIndexFunc(dutyCycleHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetDutyCycleFrequency")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetDutyCycleFrequencyFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetDutyCycleFrequency(int dutyCycleHandle)
        {
            int status = 0;
            var retVal = HAL_GetDutyCycleFrequencyFunc(dutyCycleHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetDutyCycleOutput")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, double> HAL_GetDutyCycleOutputFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetDutyCycleOutput(int dutyCycleHandle)
        {
            int status = 0;
            var retVal = HAL_GetDutyCycleOutputFunc(dutyCycleHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetDutyCycleOutputRaw")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetDutyCycleOutputRawFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetDutyCycleOutputRaw(int dutyCycleHandle)
        {
            int status = 0;
            var retVal = HAL_GetDutyCycleOutputRawFunc(dutyCycleHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetDutyCycleOutputScaleFactor")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetDutyCycleOutputScaleFactorFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetDutyCycleOutputScaleFactor(int dutyCycleHandle)
        {
            int status = 0;
            var retVal = HAL_GetDutyCycleOutputScaleFactorFunc(dutyCycleHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_InitializeDutyCycle")]
        private readonly delegate* unmanaged[Cdecl]<int, AnalogTriggerType, int*, int> HAL_InitializeDutyCycleFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_InitializeDutyCycle(int digitalSourceHandle, AnalogTriggerType triggerType)
        {
            int status = 0;
            var retVal = HAL_InitializeDutyCycleFunc(digitalSourceHandle, triggerType, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_SetDutyCycleSimDevice")]
        private readonly delegate* unmanaged[Cdecl]<int, int, void> HAL_SetDutyCycleSimDeviceFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetDutyCycleSimDevice(int handle, int device)
        {
            HAL_SetDutyCycleSimDeviceFunc(handle, device);
        }



    }
}
