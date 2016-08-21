using System;
using System.Runtime.InteropServices;
// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALAnalogGyro
    {
        static HALAnalogGyro()
        {
            HAL.Initialize();
        }

        public delegate int HAL_InitializeAnalogGyroDelegate(int handle, ref int status);
        public static HAL_InitializeAnalogGyroDelegate HAL_InitializeAnalogGyro;

        public delegate void HAL_SetupAnalogGyroDelegate(int handle, ref int status);
        public static HAL_SetupAnalogGyroDelegate HAL_SetupAnalogGyro;

        public delegate void HAL_FreeAnalogGyroDelegate(int handle);
        public static HAL_FreeAnalogGyroDelegate HAL_FreeAnalogGyro;

        public delegate void HAL_SetAnalogGyroParametersDelegate(int handle, double voltsPerDegreePerSecond, double offset, int center, ref int status);
        public static HAL_SetAnalogGyroParametersDelegate HAL_SetAnalogGyroParameters;

        public delegate void HAL_SetAnalogGyroVoltsPerDegreePerSecondDelegate(int handle, double voltsPerDegreePerSecond, ref int status);
        public static HAL_SetAnalogGyroVoltsPerDegreePerSecondDelegate HAL_SetAnalogGyroVoltsPerDegreePerSecond;

        public delegate void HAL_ResetAnalogGyroDelegate(int handle, ref int status);
        public static HAL_ResetAnalogGyroDelegate HAL_ResetAnalogGyro;

        public delegate void HAL_CalibrateAnalogGyroDelegate(int handle, ref int status);
        public static HAL_CalibrateAnalogGyroDelegate HAL_CalibrateAnalogGyro;

        public delegate void HAL_SetAnalogGyroDeadbandDelegate(int handle, double volts, ref int status);
        public static HAL_SetAnalogGyroDeadbandDelegate HAL_SetAnalogGyroDeadband;

        public delegate double HAL_GetAnalogGyroAngleDelegate(int handle, ref int status);
        public static HAL_GetAnalogGyroAngleDelegate HAL_GetAnalogGyroAngle;

        public delegate double HAL_GetAnalogGyroRateDelegate(int handle, ref int status);
        public static HAL_GetAnalogGyroRateDelegate HAL_GetAnalogGyroRate;

        public delegate double HAL_GetAnalogGyroOffsetDelegate(int handle, ref int status);
        public static HAL_GetAnalogGyroOffsetDelegate HAL_GetAnalogGyroOffset;

        public delegate int HAL_GetAnalogGyroCenterDelegate(int handle, ref int status);
        public static HAL_GetAnalogGyroCenterDelegate HAL_GetAnalogGyroCenter;
    }
}

