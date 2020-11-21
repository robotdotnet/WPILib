using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
using System;

namespace Hal.Natives
{
    public unsafe class AnalogInputLowLevelNative
    {
        public AnalogInputLowLevelNative(IFunctionPointerLoader loader)
        {
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader));
            }

            HAL_CheckAnalogInputChannelFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32 >)loader.GetProcAddress("HAL_CheckAnalogInputChannel");
            HAL_CheckAnalogModuleFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32 >)loader.GetProcAddress("HAL_CheckAnalogModule");
            HAL_FreeAnalogInputPortFunc = (delegate* unmanaged[Cdecl] < System.Int32, void >)loader.GetProcAddress("HAL_FreeAnalogInputPort");
            HAL_GetAnalogAverageBitsFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_GetAnalogAverageBits");
            HAL_GetAnalogAverageValueFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_GetAnalogAverageValue");
            HAL_GetAnalogAverageVoltageFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Double >)loader.GetProcAddress("HAL_GetAnalogAverageVoltage");
            HAL_GetAnalogLSBWeightFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_GetAnalogLSBWeight");
            HAL_GetAnalogOffsetFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_GetAnalogOffset");
            HAL_GetAnalogOversampleBitsFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_GetAnalogOversampleBits");
            HAL_GetAnalogSampleRateFunc = (delegate* unmanaged[Cdecl] < int *, System.Double >)loader.GetProcAddress("HAL_GetAnalogSampleRate");
            HAL_GetAnalogValueFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_GetAnalogValue");
            HAL_GetAnalogValueToVoltsFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, System.Double >)loader.GetProcAddress("HAL_GetAnalogValueToVolts");
            HAL_GetAnalogVoltageFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Double >)loader.GetProcAddress("HAL_GetAnalogVoltage");
            HAL_GetAnalogVoltsToValueFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Double, int *, System.Int32 >)loader.GetProcAddress("HAL_GetAnalogVoltsToValue");
            HAL_InitializeAnalogInputPortFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_InitializeAnalogInputPort");
            HAL_SetAnalogAverageBitsFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetAnalogAverageBits");
            HAL_SetAnalogInputSimDeviceFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, void >)loader.GetProcAddress("HAL_SetAnalogInputSimDevice");
            HAL_SetAnalogOversampleBitsFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetAnalogOversampleBits");
            HAL_SetAnalogSampleRateFunc = (delegate* unmanaged[Cdecl] < System.Double, int *, void >)loader.GetProcAddress("HAL_SetAnalogSampleRate");
        }

        private readonly delegate* unmanaged[Cdecl]<int, int> HAL_CheckAnalogInputChannelFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_CheckAnalogInputChannel(int channel)
        {
            return HAL_CheckAnalogInputChannelFunc(channel);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int> HAL_CheckAnalogModuleFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_CheckAnalogModule(int module)
        {
            return HAL_CheckAnalogModuleFunc(module);
        }



        private readonly delegate* unmanaged[Cdecl]<int, void> HAL_FreeAnalogInputPortFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_FreeAnalogInputPort(int analogPortHandle)
        {
            HAL_FreeAnalogInputPortFunc(analogPortHandle);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetAnalogAverageBitsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetAnalogAverageBits(int analogPortHandle)
        {
            int status = 0;
            var retVal = HAL_GetAnalogAverageBitsFunc(analogPortHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetAnalogAverageValueFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetAnalogAverageValue(int analogPortHandle)
        {
            int status = 0;
            var retVal = HAL_GetAnalogAverageValueFunc(analogPortHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, double> HAL_GetAnalogAverageVoltageFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetAnalogAverageVoltage(int analogPortHandle)
        {
            int status = 0;
            var retVal = HAL_GetAnalogAverageVoltageFunc(analogPortHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetAnalogLSBWeightFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetAnalogLSBWeight(int analogPortHandle)
        {
            int status = 0;
            var retVal = HAL_GetAnalogLSBWeightFunc(analogPortHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetAnalogOffsetFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetAnalogOffset(int analogPortHandle)
        {
            int status = 0;
            var retVal = HAL_GetAnalogOffsetFunc(analogPortHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetAnalogOversampleBitsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetAnalogOversampleBits(int analogPortHandle)
        {
            int status = 0;
            var retVal = HAL_GetAnalogOversampleBitsFunc(analogPortHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int*, double> HAL_GetAnalogSampleRateFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetAnalogSampleRate()
        {
            int status = 0;
            var retVal = HAL_GetAnalogSampleRateFunc(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetAnalogValueFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetAnalogValue(int analogPortHandle)
        {
            int status = 0;
            var retVal = HAL_GetAnalogValueFunc(analogPortHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, double> HAL_GetAnalogValueToVoltsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetAnalogValueToVolts(int analogPortHandle, int rawValue)
        {
            int status = 0;
            var retVal = HAL_GetAnalogValueToVoltsFunc(analogPortHandle, rawValue, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, double> HAL_GetAnalogVoltageFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetAnalogVoltage(int analogPortHandle)
        {
            int status = 0;
            var retVal = HAL_GetAnalogVoltageFunc(analogPortHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, double, int*, int> HAL_GetAnalogVoltsToValueFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetAnalogVoltsToValue(int analogPortHandle, double voltage)
        {
            int status = 0;
            var retVal = HAL_GetAnalogVoltsToValueFunc(analogPortHandle, voltage, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_InitializeAnalogInputPortFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_InitializeAnalogInputPort(int portHandle)
        {
            int status = 0;
            var retVal = HAL_InitializeAnalogInputPortFunc(portHandle, &status);
            Hal.StatusHandling.AnalogInputStatusCheck(status, portHandle);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetAnalogAverageBitsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetAnalogAverageBits(int analogPortHandle, int bits)
        {
            int status = 0;
            HAL_SetAnalogAverageBitsFunc(analogPortHandle, bits, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, void> HAL_SetAnalogInputSimDeviceFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetAnalogInputSimDevice(int handle, int device)
        {
            HAL_SetAnalogInputSimDeviceFunc(handle, device);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetAnalogOversampleBitsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetAnalogOversampleBits(int analogPortHandle, int bits)
        {
            int status = 0;
            HAL_SetAnalogOversampleBitsFunc(analogPortHandle, bits, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<double, int*, void> HAL_SetAnalogSampleRateFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetAnalogSampleRate(double samplesPerSecond)
        {
            int status = 0;
            HAL_SetAnalogSampleRateFunc(samplesPerSecond, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



    }
}
