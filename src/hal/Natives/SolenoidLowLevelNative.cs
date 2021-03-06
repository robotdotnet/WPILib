﻿using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
using System;

namespace Hal.Natives
{
    public unsafe class SolenoidLowLevelNative
    {
        public SolenoidLowLevelNative(IFunctionPointerLoader loader)
        {
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader));
            }

            HAL_CheckSolenoidChannelFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32 >)loader.GetProcAddress("HAL_CheckSolenoidChannel");
            HAL_CheckSolenoidModuleFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32 >)loader.GetProcAddress("HAL_CheckSolenoidModule");
            HAL_ClearAllPCMStickyFaultsFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, void >)loader.GetProcAddress("HAL_ClearAllPCMStickyFaults");
            HAL_FireOneShotFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, void >)loader.GetProcAddress("HAL_FireOneShot");
            HAL_FreeSolenoidPortFunc = (delegate* unmanaged[Cdecl] < System.Int32, void >)loader.GetProcAddress("HAL_FreeSolenoidPort");
            HAL_GetAllSolenoidsFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_GetAllSolenoids");
            HAL_GetPCMSolenoidBlackListFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_GetPCMSolenoidBlackList");
            HAL_GetPCMSolenoidVoltageFaultFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_GetPCMSolenoidVoltageFault");
            HAL_GetPCMSolenoidVoltageStickyFaultFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_GetPCMSolenoidVoltageStickyFault");
            HAL_GetSolenoidFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_GetSolenoid");
            HAL_InitializeSolenoidPortFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_InitializeSolenoidPort");
            HAL_SetAllSolenoidsFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetAllSolenoids");
            HAL_SetOneShotDurationFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetOneShotDuration");
            HAL_SetSolenoidFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetSolenoid");
        }

        private readonly delegate* unmanaged[Cdecl]<int, int> HAL_CheckSolenoidChannelFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_CheckSolenoidChannel(int channel)
        {
            return HAL_CheckSolenoidChannelFunc(channel);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int> HAL_CheckSolenoidModuleFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_CheckSolenoidModule(int module)
        {
            return HAL_CheckSolenoidModuleFunc(module);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_ClearAllPCMStickyFaultsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_ClearAllPCMStickyFaults(int module)
        {
            int status = 0;
            HAL_ClearAllPCMStickyFaultsFunc(module, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_FireOneShotFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_FireOneShot(int solenoidPortHandle)
        {
            int status = 0;
            HAL_FireOneShotFunc(solenoidPortHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, void> HAL_FreeSolenoidPortFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_FreeSolenoidPort(int solenoidPortHandle)
        {
            HAL_FreeSolenoidPortFunc(solenoidPortHandle);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetAllSolenoidsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetAllSolenoids(int module)
        {
            int status = 0;
            var retVal = HAL_GetAllSolenoidsFunc(module, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetPCMSolenoidBlackListFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetPCMSolenoidBlackList(int module)
        {
            int status = 0;
            var retVal = HAL_GetPCMSolenoidBlackListFunc(module, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetPCMSolenoidVoltageFaultFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetPCMSolenoidVoltageFault(int module)
        {
            int status = 0;
            var retVal = HAL_GetPCMSolenoidVoltageFaultFunc(module, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetPCMSolenoidVoltageStickyFaultFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetPCMSolenoidVoltageStickyFault(int module)
        {
            int status = 0;
            var retVal = HAL_GetPCMSolenoidVoltageStickyFaultFunc(module, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetSolenoidFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetSolenoid(int solenoidPortHandle)
        {
            int status = 0;
            var retVal = HAL_GetSolenoidFunc(solenoidPortHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_InitializeSolenoidPortFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_InitializeSolenoidPort(int portHandle)
        {
            int status = 0;
            var retVal = HAL_InitializeSolenoidPortFunc(portHandle, &status);
            Hal.StatusHandling.SolenoidStatusCheck(status, portHandle);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetAllSolenoidsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetAllSolenoids(int module, int state)
        {
            int status = 0;
            HAL_SetAllSolenoidsFunc(module, state, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetOneShotDurationFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetOneShotDuration(int solenoidPortHandle, int durMS)
        {
            int status = 0;
            HAL_SetOneShotDurationFunc(solenoidPortHandle, durMS, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



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
