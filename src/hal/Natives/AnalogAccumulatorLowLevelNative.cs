using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
using System;

namespace Hal.Natives
{
    public unsafe class AnalogAccumulatorLowLevelNative
    {
        public AnalogAccumulatorLowLevelNative(IFunctionPointerLoader loader)
        {
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader));
            }

            HAL_GetAccumulatorCountFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int64 >)loader.GetProcAddress("HAL_GetAccumulatorCount");
            HAL_GetAccumulatorOutputFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int64 *, System.Int64 *, int *, void >)loader.GetProcAddress("HAL_GetAccumulatorOutput");
            HAL_GetAccumulatorValueFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int64 >)loader.GetProcAddress("HAL_GetAccumulatorValue");
            HAL_InitAccumulatorFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, void >)loader.GetProcAddress("HAL_InitAccumulator");
            HAL_IsAccumulatorChannelFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_IsAccumulatorChannel");
            HAL_ResetAccumulatorFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, void >)loader.GetProcAddress("HAL_ResetAccumulator");
            HAL_SetAccumulatorCenterFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetAccumulatorCenter");
            HAL_SetAccumulatorDeadbandFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetAccumulatorDeadband");
        }

        private readonly delegate* unmanaged[Cdecl]<int, int*, long> HAL_GetAccumulatorCountFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long HAL_GetAccumulatorCount(int analogPortHandle)
        {
            int status = 0;
            var retVal = HAL_GetAccumulatorCountFunc(analogPortHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, long*, long*, int*, void> HAL_GetAccumulatorOutputFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_GetAccumulatorOutput(int analogPortHandle, long* value, long* count)
        {
            int status = 0;
            HAL_GetAccumulatorOutputFunc(analogPortHandle, value, count, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, long> HAL_GetAccumulatorValueFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long HAL_GetAccumulatorValue(int analogPortHandle)
        {
            int status = 0;
            var retVal = HAL_GetAccumulatorValueFunc(analogPortHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_InitAccumulatorFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_InitAccumulator(int analogPortHandle)
        {
            int status = 0;
            HAL_InitAccumulatorFunc(analogPortHandle, &status);
            Hal.StatusHandling.AccumulatorStatusCheck(status, analogPortHandle);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_IsAccumulatorChannelFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_IsAccumulatorChannel(int analogPortHandle)
        {
            int status = 0;
            var retVal = HAL_IsAccumulatorChannelFunc(analogPortHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_ResetAccumulatorFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_ResetAccumulator(int analogPortHandle)
        {
            int status = 0;
            HAL_ResetAccumulatorFunc(analogPortHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetAccumulatorCenterFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetAccumulatorCenter(int analogPortHandle, int center)
        {
            int status = 0;
            HAL_SetAccumulatorCenterFunc(analogPortHandle, center, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetAccumulatorDeadbandFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetAccumulatorDeadband(int analogPortHandle, int deadband)
        {
            int status = 0;
            HAL_SetAccumulatorDeadbandFunc(analogPortHandle, deadband, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



    }
}
