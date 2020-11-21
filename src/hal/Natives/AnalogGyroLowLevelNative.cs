using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
using System;

namespace Hal.Natives
{
    public unsafe class AnalogGyroLowLevelNative
    {
        public AnalogGyroLowLevelNative(IFunctionPointerLoader loader)
        {
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader));
            }

            HAL_CalibrateAnalogGyroFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, void >)loader.GetProcAddress("HAL_CalibrateAnalogGyro");
            HAL_FreeAnalogGyroFunc = (delegate* unmanaged[Cdecl] < System.Int32, void >)loader.GetProcAddress("HAL_FreeAnalogGyro");
            HAL_GetAnalogGyroAngleFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Double >)loader.GetProcAddress("HAL_GetAnalogGyroAngle");
            HAL_GetAnalogGyroCenterFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_GetAnalogGyroCenter");
            HAL_GetAnalogGyroOffsetFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Double >)loader.GetProcAddress("HAL_GetAnalogGyroOffset");
            HAL_GetAnalogGyroRateFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Double >)loader.GetProcAddress("HAL_GetAnalogGyroRate");
            HAL_InitializeAnalogGyroFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_InitializeAnalogGyro");
            HAL_ResetAnalogGyroFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, void >)loader.GetProcAddress("HAL_ResetAnalogGyro");
            HAL_SetAnalogGyroDeadbandFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Double, int *, void >)loader.GetProcAddress("HAL_SetAnalogGyroDeadband");
            HAL_SetAnalogGyroParametersFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Double, System.Double, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetAnalogGyroParameters");
            HAL_SetAnalogGyroVoltsPerDegreePerSecondFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Double, int *, void >)loader.GetProcAddress("HAL_SetAnalogGyroVoltsPerDegreePerSecond");
            HAL_SetupAnalogGyroFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, void >)loader.GetProcAddress("HAL_SetupAnalogGyro");
        }

        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_CalibrateAnalogGyroFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_CalibrateAnalogGyro(int handle)
        {
            int status = 0;
            HAL_CalibrateAnalogGyroFunc(handle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, void> HAL_FreeAnalogGyroFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_FreeAnalogGyro(int handle)
        {
            HAL_FreeAnalogGyroFunc(handle);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, double> HAL_GetAnalogGyroAngleFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetAnalogGyroAngle(int handle)
        {
            int status = 0;
            var retVal = HAL_GetAnalogGyroAngleFunc(handle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetAnalogGyroCenterFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetAnalogGyroCenter(int handle)
        {
            int status = 0;
            var retVal = HAL_GetAnalogGyroCenterFunc(handle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, double> HAL_GetAnalogGyroOffsetFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetAnalogGyroOffset(int handle)
        {
            int status = 0;
            var retVal = HAL_GetAnalogGyroOffsetFunc(handle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, double> HAL_GetAnalogGyroRateFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetAnalogGyroRate(int handle)
        {
            int status = 0;
            var retVal = HAL_GetAnalogGyroRateFunc(handle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_InitializeAnalogGyroFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_InitializeAnalogGyro(int analogHandle)
        {
            int status = 0;
            var retVal = HAL_InitializeAnalogGyroFunc(analogHandle, &status);
            Hal.StatusHandling.AnalogGyroStatusCheck(status, analogHandle);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_ResetAnalogGyroFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_ResetAnalogGyro(int handle)
        {
            int status = 0;
            HAL_ResetAnalogGyroFunc(handle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, double, int*, void> HAL_SetAnalogGyroDeadbandFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetAnalogGyroDeadband(int handle, double volts)
        {
            int status = 0;
            HAL_SetAnalogGyroDeadbandFunc(handle, volts, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, double, double, int, int*, void> HAL_SetAnalogGyroParametersFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetAnalogGyroParameters(int handle, double voltsPerDegreePerSecond, double offset, int center)
        {
            int status = 0;
            HAL_SetAnalogGyroParametersFunc(handle, voltsPerDegreePerSecond, offset, center, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, double, int*, void> HAL_SetAnalogGyroVoltsPerDegreePerSecondFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetAnalogGyroVoltsPerDegreePerSecond(int handle, double voltsPerDegreePerSecond)
        {
            int status = 0;
            HAL_SetAnalogGyroVoltsPerDegreePerSecondFunc(handle, voltsPerDegreePerSecond, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_SetupAnalogGyroFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetupAnalogGyro(int handle)
        {
            int status = 0;
            HAL_SetupAnalogGyroFunc(handle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



    }
}
