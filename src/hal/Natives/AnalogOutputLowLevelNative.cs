using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
namespace Hal.Natives
{
    public unsafe class AnalogOutputLowLevelNative
    {
        [NativeFunctionPointer("HAL_CheckAnalogOutputChannel")]
        private readonly delegate* unmanaged[Cdecl]<int, int> HAL_CheckAnalogOutputChannelFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_CheckAnalogOutputChannel(int channel)
        {
            return HAL_CheckAnalogOutputChannelFunc(channel);
        }


        [NativeFunctionPointer("HAL_FreeAnalogOutputPort")]
        private readonly delegate* unmanaged[Cdecl]<int, void> HAL_FreeAnalogOutputPortFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_FreeAnalogOutputPort(int analogOutputHandle)
        {
            HAL_FreeAnalogOutputPortFunc(analogOutputHandle);
        }


        [NativeFunctionPointer("HAL_GetAnalogOutput")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, double> HAL_GetAnalogOutputFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetAnalogOutput(int analogOutputHandle)
        {
            int status = 0;
            var retVal = HAL_GetAnalogOutputFunc(analogOutputHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_InitializeAnalogOutputPort")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_InitializeAnalogOutputPortFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_InitializeAnalogOutputPort(int portHandle)
        {
            int status = 0;
            var retVal = HAL_InitializeAnalogOutputPortFunc(portHandle, &status);
            Hal.StatusHandling.AnalogOutputStatusCheck(status, portHandle);
            return retVal;
        }


        [NativeFunctionPointer("HAL_SetAnalogOutput")]
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
