using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
namespace Hal.Natives
{
    public unsafe class SolenoidLowLevelNative
    {
        [NativeFunctionPointer("HAL_CheckSolenoidChannel")]
        private readonly delegate* unmanaged[Cdecl]<int, int> HAL_CheckSolenoidChannelFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_CheckSolenoidChannel(int channel)
        {
            return HAL_CheckSolenoidChannelFunc(channel);
        }


        [NativeFunctionPointer("HAL_CheckSolenoidModule")]
        private readonly delegate* unmanaged[Cdecl]<int, int> HAL_CheckSolenoidModuleFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_CheckSolenoidModule(int module)
        {
            return HAL_CheckSolenoidModuleFunc(module);
        }


        [NativeFunctionPointer("HAL_ClearAllPCMStickyFaults")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_ClearAllPCMStickyFaultsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_ClearAllPCMStickyFaults(int module)
        {
            int status = 0;
            HAL_ClearAllPCMStickyFaultsFunc(module, &status);
            Hal.StatusHandling.StatusCheck(status);
        }


        [NativeFunctionPointer("HAL_FireOneShot")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_FireOneShotFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_FireOneShot(int solenoidPortHandle)
        {
            int status = 0;
            HAL_FireOneShotFunc(solenoidPortHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }


        [NativeFunctionPointer("HAL_FreeSolenoidPort")]
        private readonly delegate* unmanaged[Cdecl]<int, void> HAL_FreeSolenoidPortFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_FreeSolenoidPort(int solenoidPortHandle)
        {
            HAL_FreeSolenoidPortFunc(solenoidPortHandle);
        }


        [NativeFunctionPointer("HAL_GetAllSolenoids")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetAllSolenoidsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetAllSolenoids(int module)
        {
            int status = 0;
            var retVal = HAL_GetAllSolenoidsFunc(module, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetPCMSolenoidBlackList")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetPCMSolenoidBlackListFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetPCMSolenoidBlackList(int module)
        {
            int status = 0;
            var retVal = HAL_GetPCMSolenoidBlackListFunc(module, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetPCMSolenoidVoltageFault")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetPCMSolenoidVoltageFaultFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetPCMSolenoidVoltageFault(int module)
        {
            int status = 0;
            var retVal = HAL_GetPCMSolenoidVoltageFaultFunc(module, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetPCMSolenoidVoltageStickyFault")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetPCMSolenoidVoltageStickyFaultFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetPCMSolenoidVoltageStickyFault(int module)
        {
            int status = 0;
            var retVal = HAL_GetPCMSolenoidVoltageStickyFaultFunc(module, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetSolenoid")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetSolenoidFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetSolenoid(int solenoidPortHandle)
        {
            int status = 0;
            var retVal = HAL_GetSolenoidFunc(solenoidPortHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_InitializeSolenoidPort")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_InitializeSolenoidPortFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_InitializeSolenoidPort(int portHandle)
        {
            int status = 0;
            var retVal = HAL_InitializeSolenoidPortFunc(portHandle, &status);
            Hal.StatusHandling.SolenoidStatusCheck(status, portHandle);
            return retVal;
        }


        [NativeFunctionPointer("HAL_SetAllSolenoids")]
        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetAllSolenoidsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetAllSolenoids(int module, int state)
        {
            int status = 0;
            HAL_SetAllSolenoidsFunc(module, state, &status);
            Hal.StatusHandling.StatusCheck(status);
        }


        [NativeFunctionPointer("HAL_SetOneShotDuration")]
        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetOneShotDurationFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetOneShotDuration(int solenoidPortHandle, int durMS)
        {
            int status = 0;
            HAL_SetOneShotDurationFunc(solenoidPortHandle, durMS, &status);
            Hal.StatusHandling.StatusCheck(status);
        }


        [NativeFunctionPointer("HAL_SetSolenoid")]
        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetSolenoidFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetSolenoid(int solenoidPortHandle, int value)
        {
            int status = 0;
            HAL_SetSolenoidFunc(solenoidPortHandle, value, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



    }
}
