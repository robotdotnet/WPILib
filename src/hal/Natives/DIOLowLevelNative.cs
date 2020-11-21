using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
using System;

namespace Hal.Natives
{
    public unsafe class DIOLowLevelNative
    {
        public DIOLowLevelNative(IFunctionPointerLoader loader)
        {
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader));
            }

            HAL_AllocateDigitalPWMFunc = (delegate* unmanaged[Cdecl] < int *, System.Int32 >)loader.GetProcAddress("HAL_AllocateDigitalPWM");
            HAL_CheckDIOChannelFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32 >)loader.GetProcAddress("HAL_CheckDIOChannel");
            HAL_FreeDIOPortFunc = (delegate* unmanaged[Cdecl] < System.Int32, void >)loader.GetProcAddress("HAL_FreeDIOPort");
            HAL_SetDIOFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetDIO");
            HAL_FreeDigitalPWMFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, void >)loader.GetProcAddress("HAL_FreeDigitalPWM");
            HAL_GetDIOFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_GetDIO");
            HAL_GetDIODirectionFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_GetDIODirection");
            HAL_GetFilterPeriodFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int64 >)loader.GetProcAddress("HAL_GetFilterPeriod");
            HAL_GetFilterSelectFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_GetFilterSelect");
            HAL_InitializeDIOPortFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_InitializeDIOPort");
            HAL_IsAnyPulsingFunc = (delegate* unmanaged[Cdecl] < int *, System.Int32 >)loader.GetProcAddress("HAL_IsAnyPulsing");
            HAL_IsPulsingFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_IsPulsing");
            HAL_PulseFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Double, int *, void >)loader.GetProcAddress("HAL_Pulse");
            HAL_SetDIOSimDeviceFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, void >)loader.GetProcAddress("HAL_SetDIOSimDevice");
            HAL_SetDIODirectionFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetDIODirection");
            HAL_SetDigitalPWMDutyCycleFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Double, int *, void >)loader.GetProcAddress("HAL_SetDigitalPWMDutyCycle");
            HAL_SetDigitalPWMOutputChannelFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetDigitalPWMOutputChannel");
            HAL_SetDigitalPWMRateFunc = (delegate* unmanaged[Cdecl] < System.Double, int *, void >)loader.GetProcAddress("HAL_SetDigitalPWMRate");
            HAL_SetFilterPeriodFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int64, int *, void >)loader.GetProcAddress("HAL_SetFilterPeriod");
            HAL_SetFilterSelectFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetFilterSelect");
        }

        private readonly delegate* unmanaged[Cdecl]<int*, int> HAL_AllocateDigitalPWMFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_AllocateDigitalPWM()
        {
            int status = 0;
            var retVal = HAL_AllocateDigitalPWMFunc(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int> HAL_CheckDIOChannelFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_CheckDIOChannel(int channel)
        {
            return HAL_CheckDIOChannelFunc(channel);
        }



        private readonly delegate* unmanaged[Cdecl]<int, void> HAL_FreeDIOPortFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_FreeDIOPort(int dioPortHandle)
        {
            HAL_FreeDIOPortFunc(dioPortHandle);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetDIOFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetDIO(int dioPortHandle, int value)
        {
            int status = 0;
            HAL_SetDIOFunc(dioPortHandle, value, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_FreeDigitalPWMFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_FreeDigitalPWM(int pwmGenerator)
        {
            int status = 0;
            HAL_FreeDigitalPWMFunc(pwmGenerator, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetDIOFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetDIO(int dioPortHandle)
        {
            int status = 0;
            var retVal = HAL_GetDIOFunc(dioPortHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetDIODirectionFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetDIODirection(int dioPortHandle)
        {
            int status = 0;
            var retVal = HAL_GetDIODirectionFunc(dioPortHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, long> HAL_GetFilterPeriodFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long HAL_GetFilterPeriod(int filterIndex)
        {
            int status = 0;
            var retVal = HAL_GetFilterPeriodFunc(filterIndex, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetFilterSelectFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetFilterSelect(int dioPortHandle)
        {
            int status = 0;
            var retVal = HAL_GetFilterSelectFunc(dioPortHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, int> HAL_InitializeDIOPortFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_InitializeDIOPort(int portHandle, int input)
        {
            int status = 0;
            var retVal = HAL_InitializeDIOPortFunc(portHandle, input, &status);
            Hal.StatusHandling.DIOStatusCheck(status, portHandle);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int*, int> HAL_IsAnyPulsingFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_IsAnyPulsing()
        {
            int status = 0;
            var retVal = HAL_IsAnyPulsingFunc(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_IsPulsingFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_IsPulsing(int dioPortHandle)
        {
            int status = 0;
            var retVal = HAL_IsPulsingFunc(dioPortHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, double, int*, void> HAL_PulseFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_Pulse(int dioPortHandle, double pulseLength)
        {
            int status = 0;
            HAL_PulseFunc(dioPortHandle, pulseLength, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, void> HAL_SetDIOSimDeviceFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetDIOSimDevice(int handle, int device)
        {
            HAL_SetDIOSimDeviceFunc(handle, device);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetDIODirectionFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetDIODirection(int dioPortHandle, int input)
        {
            int status = 0;
            HAL_SetDIODirectionFunc(dioPortHandle, input, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, double, int*, void> HAL_SetDigitalPWMDutyCycleFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetDigitalPWMDutyCycle(int pwmGenerator, double dutyCycle)
        {
            int status = 0;
            HAL_SetDigitalPWMDutyCycleFunc(pwmGenerator, dutyCycle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetDigitalPWMOutputChannelFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetDigitalPWMOutputChannel(int pwmGenerator, int channel)
        {
            int status = 0;
            HAL_SetDigitalPWMOutputChannelFunc(pwmGenerator, channel, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<double, int*, void> HAL_SetDigitalPWMRateFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetDigitalPWMRate(double rate)
        {
            int status = 0;
            HAL_SetDigitalPWMRateFunc(rate, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, long, int*, void> HAL_SetFilterPeriodFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetFilterPeriod(int filterIndex, long value)
        {
            int status = 0;
            HAL_SetFilterPeriodFunc(filterIndex, value, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetFilterSelectFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetFilterSelect(int dioPortHandle, int filterIndex)
        {
            int status = 0;
            HAL_SetFilterSelectFunc(dioPortHandle, filterIndex, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



    }
}
