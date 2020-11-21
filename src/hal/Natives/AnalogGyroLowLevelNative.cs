using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
namespace Hal.Natives
{
    public unsafe class AnalogGyroLowLevelNative : IAnalogGyro
    {
        [NativeFunctionPointer("HAL_CalibrateAnalogGyro")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_CalibrateAnalogGyroFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_CalibrateAnalogGyro(int handle)
        {
            int status = 0;
            HAL_CalibrateAnalogGyroFunc(handle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }


        [NativeFunctionPointer("HAL_FreeAnalogGyro")]
        private readonly delegate* unmanaged[Cdecl]<int, void> HAL_FreeAnalogGyroFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_FreeAnalogGyro(int handle)
        {
            HAL_FreeAnalogGyroFunc(handle);
        }


        [NativeFunctionPointer("HAL_GetAnalogGyroAngle")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, double> HAL_GetAnalogGyroAngleFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetAnalogGyroAngle(int handle)
        {
            int status = 0;
            var retVal = HAL_GetAnalogGyroAngleFunc(handle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetAnalogGyroCenter")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetAnalogGyroCenterFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetAnalogGyroCenter(int handle)
        {
            int status = 0;
            var retVal = HAL_GetAnalogGyroCenterFunc(handle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetAnalogGyroOffset")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, double> HAL_GetAnalogGyroOffsetFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetAnalogGyroOffset(int handle)
        {
            int status = 0;
            var retVal = HAL_GetAnalogGyroOffsetFunc(handle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetAnalogGyroRate")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, double> HAL_GetAnalogGyroRateFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetAnalogGyroRate(int handle)
        {
            int status = 0;
            var retVal = HAL_GetAnalogGyroRateFunc(handle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_InitializeAnalogGyro")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_InitializeAnalogGyroFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_InitializeAnalogGyro(int analogHandle)
        {
            int status = 0;
            var retVal = HAL_InitializeAnalogGyroFunc(analogHandle, &status);
            Hal.StatusHandling.AnalogGyroStatusCheck(status, analogHandle);
            return retVal;
        }


        [NativeFunctionPointer("HAL_ResetAnalogGyro")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_ResetAnalogGyroFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_ResetAnalogGyro(int handle)
        {
            int status = 0;
            HAL_ResetAnalogGyroFunc(handle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }


        [NativeFunctionPointer("HAL_SetAnalogGyroDeadband")]
        private readonly delegate* unmanaged[Cdecl]<int, double, int*, void> HAL_SetAnalogGyroDeadbandFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetAnalogGyroDeadband(int handle, double volts)
        {
            int status = 0;
            HAL_SetAnalogGyroDeadbandFunc(handle, volts, &status);
            Hal.StatusHandling.StatusCheck(status);
        }


        [NativeFunctionPointer("HAL_SetAnalogGyroParameters")]
        private readonly delegate* unmanaged[Cdecl]<int, double, double, int, int*, void> HAL_SetAnalogGyroParametersFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetAnalogGyroParameters(int handle, double voltsPerDegreePerSecond, double offset, int center)
        {
            int status = 0;
            HAL_SetAnalogGyroParametersFunc(handle, voltsPerDegreePerSecond, offset, center, &status);
            Hal.StatusHandling.StatusCheck(status);
        }


        [NativeFunctionPointer("HAL_SetAnalogGyroVoltsPerDegreePerSecond")]
        private readonly delegate* unmanaged[Cdecl]<int, double, int*, void> HAL_SetAnalogGyroVoltsPerDegreePerSecondFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetAnalogGyroVoltsPerDegreePerSecond(int handle, double voltsPerDegreePerSecond)
        {
            int status = 0;
            HAL_SetAnalogGyroVoltsPerDegreePerSecondFunc(handle, voltsPerDegreePerSecond, &status);
            Hal.StatusHandling.StatusCheck(status);
        }


        [NativeFunctionPointer("HAL_SetupAnalogGyro")]
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
