using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
namespace Hal.Natives
{
    public unsafe class AccelerometerLowLevelNative
    {
        
        private readonly delegate* unmanaged[Cdecl]<double> HAL_GetAccelerometerXFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetAccelerometerX()
        {
            return HAL_GetAccelerometerXFunc();
        }


        
        private readonly delegate* unmanaged[Cdecl]<double> HAL_GetAccelerometerYFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetAccelerometerY()
        {
            return HAL_GetAccelerometerYFunc();
        }


        
        private readonly delegate* unmanaged[Cdecl]<double> HAL_GetAccelerometerZFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetAccelerometerZ()
        {
            return HAL_GetAccelerometerZFunc();
        }


        
        private readonly delegate* unmanaged[Cdecl]<int, void> HAL_SetAccelerometerActiveFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetAccelerometerActive(int active)
        {
            HAL_SetAccelerometerActiveFunc(active);
        }


        
        private readonly delegate* unmanaged[Cdecl]<AccelerometerRange, void> HAL_SetAccelerometerRangeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetAccelerometerRange(AccelerometerRange range)
        {
            HAL_SetAccelerometerRangeFunc(range);
        }



    }
}
