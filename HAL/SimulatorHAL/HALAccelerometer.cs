using System;
using HAL.Base;
using static HAL.Simulator.SimData;

// ReSharper disable CheckNamespace
namespace HAL.SimulatorHAL
{
    internal class HALAccelerometer
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALAccelerometer.HAL_SetAccelerometerActive = HAL_SetAccelerometerActive;
            Base.HALAccelerometer.HAL_SetAccelerometerRange = HAL_SetAccelerometerRange;
            Base.HALAccelerometer.HAL_GetAccelerometerX = HAL_GetAccelerometerX;
            Base.HALAccelerometer.HAL_GetAccelerometerY = HAL_GetAccelerometerY;
            Base.HALAccelerometer.HAL_GetAccelerometerZ = HAL_GetAccelerometerZ;
        }

        public static void HAL_SetAccelerometerActive(bool active)
        {
            Accelerometer.Active = active;
        }

        public static void HAL_SetAccelerometerRange(HALAccelerometerRange range)
        {
            Accelerometer.Range = range;
        }

        public static double HAL_GetAccelerometerX()
        {
            return Accelerometer.X;
        }

        public static double HAL_GetAccelerometerY()
        {
            return Accelerometer.Y;
        }

        public static double HAL_GetAccelerometerZ()
        {
            return Accelerometer.Z;
        }
    }
}

