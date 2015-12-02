using System;
using HAL;
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
            global::HAL.HALAccelerometer.SetAccelerometerActive = setAccelerometerActive;
            global::HAL.HALAccelerometer.SetAccelerometerRange = setAccelerometerRange;
            global::HAL.HALAccelerometer.GetAccelerometerX = getAccelerometerX;
            global::HAL.HALAccelerometer.GetAccelerometerY = getAccelerometerY;
            global::HAL.HALAccelerometer.GetAccelerometerZ = getAccelerometerZ;
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
