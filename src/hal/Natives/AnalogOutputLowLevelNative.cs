using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
using System;

namespace Hal.Natives
{
    public unsafe class AnalogOutputLowLevelNative
    {
        public AnalogOutputLowLevelNative(IFunctionPointerLoader loader)
        {
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader));
            }

            HAL_CheckAnalogOutputChannelFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32 >)loader.GetProcAddress("HAL_CheckAnalogOutputChannel");
            HAL_FreeAnalogOutputPortFunc = (delegate* unmanaged[Cdecl] < System.Int32, void >)loader.GetProcAddress("HAL_FreeAnalogOutputPort");
            HAL_GetAnalogOutputFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Double >)loader.GetProcAddress("HAL_GetAnalogOutput");
            HAL_InitializeAnalogOutputPortFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_InitializeAnalogOutputPort");
            HAL_SetAnalogOutputFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Double, int *, void >)loader.GetProcAddress("HAL_SetAnalogOutput");
        }

        private readonly delegate* unmanaged[Cdecl]<int, int> HAL_CheckAnalogOutputChannelFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_CheckAnalogOutputChannel(int channel)
        {
            return HAL_CheckAnalogOutputChannelFunc(channel);
        }



        private readonly delegate* unmanaged[Cdecl]<int, void> HAL_FreeAnalogOutputPortFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_FreeAnalogOutputPort(int analogOutputHandle)
        {
            HAL_FreeAnalogOutputPortFunc(analogOutputHandle);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, double> HAL_GetAnalogOutputFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetAnalogOutput(int analogOutputHandle)
        {
            int status = 0;
            var retVal = HAL_GetAnalogOutputFunc(analogOutputHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_InitializeAnalogOutputPortFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_InitializeAnalogOutputPort(int portHandle)
        {
            int status = 0;
            var retVal = HAL_InitializeAnalogOutputPortFunc(portHandle, &status);
            Hal.StatusHandling.AnalogOutputStatusCheck(status, portHandle);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, double, int*, void> HAL_SetAnalogOutputFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetAnalogOutput(int analogOutputHandle, double voltage)
        {
            int status = 0;
            HAL_SetAnalogOutputFunc(analogOutputHandle, voltage, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



    }
}
