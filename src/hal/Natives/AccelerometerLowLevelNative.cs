using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
namespace Hal.Natives
{
    public unsafe class AccelerometerLowLevelNative
    {
        [NativeFunctionPointer("HAL_GetAccelerometerX")]
        private readonly delegate* unmanaged[Cdecl]<double> HAL_GetAccelerometerXFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetAccelerometerX()
        {
            return HAL_GetAccelerometerXFunc();
        }


        [NativeFunctionPointer("HAL_GetAccelerometerY")]
        private readonly delegate* unmanaged[Cdecl]<double> HAL_GetAccelerometerYFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetAccelerometerY()
        {
            return HAL_GetAccelerometerYFunc();
        }


        [NativeFunctionPointer("HAL_GetAccelerometerZ")]
        private readonly delegate* unmanaged[Cdecl]<double> HAL_GetAccelerometerZFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetAccelerometerZ()
        {
            return HAL_GetAccelerometerZFunc();
        }


        [NativeFunctionPointer("HAL_SetAccelerometerActive")]
        private readonly delegate* unmanaged[Cdecl]<int, void> HAL_SetAccelerometerActiveFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetAccelerometerActive(int active)
        {
            HAL_SetAccelerometerActiveFunc(active);
        }


        [NativeFunctionPointer("HAL_SetAccelerometerRange")]
        private readonly delegate* unmanaged[Cdecl]<AccelerometerRange, void> HAL_SetAccelerometerRangeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetAccelerometerRange(AccelerometerRange range)
        {
            HAL_SetAccelerometerRangeFunc(range);
        }



    }
}
