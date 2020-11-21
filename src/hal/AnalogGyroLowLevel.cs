
using Hal.Natives;
using WPIUtil.ILGeneration;

namespace Hal
{

    public static unsafe class AnalogGyroLowLevel
    {
        internal static AnalogGyroLowLevelNative lowLevel = null!;

        internal static void InitializeNatives(IFunctionPointerLoader loader)
        {
            lowLevel = new(loader);
        }

        public static void Calibrate(int handle)
        {
            lowLevel.HAL_CalibrateAnalogGyro(handle);
        }

        public static void Free(int handle)
        {
            lowLevel.HAL_FreeAnalogGyro(handle);
        }

        public static double GetAngle(int handle)
        {
            return lowLevel.HAL_GetAnalogGyroAngle(handle);
        }

        public static int GetCenter(int handle)
        {
            return lowLevel.HAL_GetAnalogGyroCenter(handle);
        }

        public static double GetOffset(int handle)
        {
            return lowLevel.HAL_GetAnalogGyroOffset(handle);
        }

        public static double GetRate(int handle)
        {
            return lowLevel.HAL_GetAnalogGyroRate(handle);
        }

        public static int Initialize(int handle)
        {
            return lowLevel.HAL_InitializeAnalogGyro(handle);
        }

        public static void Reset(int handle)
        {
            lowLevel.HAL_ResetAnalogGyro(handle);
        }

        public static void SetDeadband(int handle, double volts)
        {
            lowLevel.HAL_SetAnalogGyroDeadband(handle, volts);
        }

        public static void SetParameters(int handle, double voltsPerDegreePerSecond, double offset, int center)
        {
            lowLevel.HAL_SetAnalogGyroParameters(handle, voltsPerDegreePerSecond, offset, center);
        }

        public static void SetVoltsPerDegreePerSecond(int handle, double voltsPerDegreePerSecond)
        {
            lowLevel.HAL_SetAnalogGyroVoltsPerDegreePerSecond(handle, voltsPerDegreePerSecond);
        }

        public static void Setup(int handle)
        {
            lowLevel.HAL_SetupAnalogGyro(handle);
        }

    }
}
