using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
using System;

namespace Hal.Natives
{
    public unsafe class AccelerometerLowLevelNative
    {
        public AccelerometerLowLevelNative(IFunctionPointerLoader loader)
        {
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader));
            }

            HAL_GetAccelerometerXFunc = (delegate* unmanaged[Cdecl] < System.Double >)loader.GetProcAddress("HAL_GetAccelerometerX");
            HAL_GetAccelerometerYFunc = (delegate* unmanaged[Cdecl] < System.Double >)loader.GetProcAddress("HAL_GetAccelerometerY");
            HAL_GetAccelerometerZFunc = (delegate* unmanaged[Cdecl] < System.Double >)loader.GetProcAddress("HAL_GetAccelerometerZ");
            HAL_SetAccelerometerActiveFunc = (delegate* unmanaged[Cdecl] < System.Int32, void >)loader.GetProcAddress("HAL_SetAccelerometerActive");
            HAL_SetAccelerometerRangeFunc = (delegate* unmanaged[Cdecl] < Hal.AccelerometerRange, void >)loader.GetProcAddress("HAL_SetAccelerometerRange");
        }

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
