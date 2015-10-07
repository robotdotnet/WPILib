using System;
using HAL_Base;
using static HAL_Simulator.SimData;

// ReSharper disable RedundantAssignment
// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace
#pragma warning disable 1591

namespace HAL_Simulator
{
    ///<inheritdoc cref="HAL"/>
    internal class HALAccelerometer
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            HAL_Base.HALAccelerometer.SetAccelerometerActive = setAccelerometerActive;
            HAL_Base.HALAccelerometer.SetAccelerometerRange = setAccelerometerRange;
            HAL_Base.HALAccelerometer.GetAccelerometerX = getAccelerometerX;
            HAL_Base.HALAccelerometer.GetAccelerometerY = getAccelerometerY;
            HAL_Base.HALAccelerometer.GetAccelerometerZ = getAccelerometerZ;
        }

        /// Return Type: void
        ///range: boolean
        [CalledSimFunction]
        public static void setAccelerometerActive(bool active)
        {
            Accelerometer.Active = active;
        }


        /// Return Type: void
        ///range: HALAccelerometerRange
        [CalledSimFunction]
        public static void setAccelerometerRange(HALAccelerometerRange range)
        {
            Accelerometer.Range = range;
        }


        [CalledSimFunction]
        public static double getAccelerometerX()
        {
            return Accelerometer.X;
        }

        [CalledSimFunction]
        public static double getAccelerometerY()
        {
            return Accelerometer.Y;
        }

        [CalledSimFunction]
        public static double getAccelerometerZ()
        {
            return Accelerometer.Z;
        }
    }
}
