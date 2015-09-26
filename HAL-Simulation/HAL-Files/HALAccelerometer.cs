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
        /// Return Type: void
        ///range: boolean
        [CalledSimFunction]
        public static void setAccelerometerActive(bool active)
        {
            SimData.Accelerometer.Active = active;
        }


        /// Return Type: void
        ///range: AccelerometerRange
        [CalledSimFunction]
        public static void setAccelerometerRange(AccelerometerRange range)
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
