using System;
using HAL.Base;
using static HAL.Simulator.SimData;

// ReSharper disable RedundantAssignment
// ReSharper disable InconsistentNaming
#pragma warning disable 1591

namespace HAL.SimulatorHAL
{
    ///<inheritdoc cref="HAL"/>
    internal class HALAccelerometer
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALAccelerometer.SetAccelerometerActive = setAccelerometerActive;
            Base.HALAccelerometer.SetAccelerometerRange = setAccelerometerRange;
            Base.HALAccelerometer.GetAccelerometerX = getAccelerometerX;
            Base.HALAccelerometer.GetAccelerometerY = getAccelerometerY;
            Base.HALAccelerometer.GetAccelerometerZ = getAccelerometerZ;
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
