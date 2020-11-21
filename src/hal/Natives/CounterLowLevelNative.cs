using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
using System;

namespace Hal.Natives
{
    public unsafe class CounterLowLevelNative
    {
        public CounterLowLevelNative(IFunctionPointerLoader loader)
        {
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader));
            }

            HAL_GetCounterFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_GetCounter");
            HAL_ClearCounterDownSourceFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, void >)loader.GetProcAddress("HAL_ClearCounterDownSource");
            HAL_ClearCounterUpSourceFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, void >)loader.GetProcAddress("HAL_ClearCounterUpSource");
            HAL_FreeCounterFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, void >)loader.GetProcAddress("HAL_FreeCounter");
            HAL_GetCounterSamplesToAverageFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_GetCounterSamplesToAverage");
            HAL_GetCounterDirectionFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_GetCounterDirection");
            HAL_GetCounterPeriodFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Double >)loader.GetProcAddress("HAL_GetCounterPeriod");
            HAL_InitializeCounterFunc = (delegate* unmanaged[Cdecl] < Hal.CounterMode, System.Int32 *, int *, System.Int32 >)loader.GetProcAddress("HAL_InitializeCounter");
            HAL_ResetCounterFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, void >)loader.GetProcAddress("HAL_ResetCounter");
            HAL_SetCounterAverageSizeFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetCounterAverageSize");
            HAL_SetCounterDownSourceFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, Hal.AnalogTriggerType, int *, void >)loader.GetProcAddress("HAL_SetCounterDownSource");
            HAL_SetCounterDownSourceEdgeFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetCounterDownSourceEdge");
            HAL_SetCounterExternalDirectionModeFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, void >)loader.GetProcAddress("HAL_SetCounterExternalDirectionMode");
            HAL_SetCounterMaxPeriodFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Double, int *, void >)loader.GetProcAddress("HAL_SetCounterMaxPeriod");
            HAL_SetCounterPulseLengthModeFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Double, int *, void >)loader.GetProcAddress("HAL_SetCounterPulseLengthMode");
            HAL_SetCounterReverseDirectionFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetCounterReverseDirection");
            HAL_SetCounterSamplesToAverageFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetCounterSamplesToAverage");
            HAL_SetCounterSemiPeriodModeFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetCounterSemiPeriodMode");
            HAL_SetCounterUpDownModeFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, void >)loader.GetProcAddress("HAL_SetCounterUpDownMode");
            HAL_SetCounterUpSourceFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, Hal.AnalogTriggerType, int *, void >)loader.GetProcAddress("HAL_SetCounterUpSource");
            HAL_SetCounterUpSourceEdgeFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetCounterUpSourceEdge");
            HAL_SetCounterUpdateWhenEmptyFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetCounterUpdateWhenEmpty");
            HAL_GetCounterStoppedFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_GetCounterStopped");
        }

        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetCounterFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetCounter(int counterHandle)
        {
            int status = 0;
            var retVal = HAL_GetCounterFunc(counterHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_ClearCounterDownSourceFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_ClearCounterDownSource(int counterHandle)
        {
            int status = 0;
            HAL_ClearCounterDownSourceFunc(counterHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_ClearCounterUpSourceFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_ClearCounterUpSource(int counterHandle)
        {
            int status = 0;
            HAL_ClearCounterUpSourceFunc(counterHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_FreeCounterFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_FreeCounter(int counterHandle)
        {
            int status = 0;
            HAL_FreeCounterFunc(counterHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetCounterSamplesToAverageFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetCounterSamplesToAverage(int counterHandle)
        {
            int status = 0;
            var retVal = HAL_GetCounterSamplesToAverageFunc(counterHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetCounterDirectionFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetCounterDirection(int counterHandle)
        {
            int status = 0;
            var retVal = HAL_GetCounterDirectionFunc(counterHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, double> HAL_GetCounterPeriodFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetCounterPeriod(int counterHandle)
        {
            int status = 0;
            var retVal = HAL_GetCounterPeriodFunc(counterHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<CounterMode, int*, int*, int> HAL_InitializeCounterFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_InitializeCounter(CounterMode mode, int* index)
        {
            int status = 0;
            var retVal = HAL_InitializeCounterFunc(mode, index, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_ResetCounterFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_ResetCounter(int counterHandle)
        {
            int status = 0;
            HAL_ResetCounterFunc(counterHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetCounterAverageSizeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetCounterAverageSize(int counterHandle, int size)
        {
            int status = 0;
            HAL_SetCounterAverageSizeFunc(counterHandle, size, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, AnalogTriggerType, int*, void> HAL_SetCounterDownSourceFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetCounterDownSource(int counterHandle, int digitalSourceHandle, AnalogTriggerType analogTriggerType)
        {
            int status = 0;
            HAL_SetCounterDownSourceFunc(counterHandle, digitalSourceHandle, analogTriggerType, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int, int*, void> HAL_SetCounterDownSourceEdgeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetCounterDownSourceEdge(int counterHandle, int risingEdge, int fallingEdge)
        {
            int status = 0;
            HAL_SetCounterDownSourceEdgeFunc(counterHandle, risingEdge, fallingEdge, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_SetCounterExternalDirectionModeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetCounterExternalDirectionMode(int counterHandle)
        {
            int status = 0;
            HAL_SetCounterExternalDirectionModeFunc(counterHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, double, int*, void> HAL_SetCounterMaxPeriodFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetCounterMaxPeriod(int counterHandle, double maxPeriod)
        {
            int status = 0;
            HAL_SetCounterMaxPeriodFunc(counterHandle, maxPeriod, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, double, int*, void> HAL_SetCounterPulseLengthModeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetCounterPulseLengthMode(int counterHandle, double threshold)
        {
            int status = 0;
            HAL_SetCounterPulseLengthModeFunc(counterHandle, threshold, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetCounterReverseDirectionFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetCounterReverseDirection(int counterHandle, int reverseDirection)
        {
            int status = 0;
            HAL_SetCounterReverseDirectionFunc(counterHandle, reverseDirection, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetCounterSamplesToAverageFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetCounterSamplesToAverage(int counterHandle, int samplesToAverage)
        {
            int status = 0;
            HAL_SetCounterSamplesToAverageFunc(counterHandle, samplesToAverage, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetCounterSemiPeriodModeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetCounterSemiPeriodMode(int counterHandle, int highSemiPeriod)
        {
            int status = 0;
            HAL_SetCounterSemiPeriodModeFunc(counterHandle, highSemiPeriod, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_SetCounterUpDownModeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetCounterUpDownMode(int counterHandle)
        {
            int status = 0;
            HAL_SetCounterUpDownModeFunc(counterHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, AnalogTriggerType, int*, void> HAL_SetCounterUpSourceFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetCounterUpSource(int counterHandle, int digitalSourceHandle, AnalogTriggerType analogTriggerType)
        {
            int status = 0;
            HAL_SetCounterUpSourceFunc(counterHandle, digitalSourceHandle, analogTriggerType, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int, int*, void> HAL_SetCounterUpSourceEdgeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetCounterUpSourceEdge(int counterHandle, int risingEdge, int fallingEdge)
        {
            int status = 0;
            HAL_SetCounterUpSourceEdgeFunc(counterHandle, risingEdge, fallingEdge, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetCounterUpdateWhenEmptyFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetCounterUpdateWhenEmpty(int counterHandle, int enabled)
        {
            int status = 0;
            HAL_SetCounterUpdateWhenEmptyFunc(counterHandle, enabled, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetCounterStoppedFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetCounterStopped(int counterHandle)
        {
            int status = 0;
            var retVal = HAL_GetCounterStoppedFunc(counterHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



    }
}
