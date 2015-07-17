using HAL_Base;
using static HAL_Simulator.SimData;

// ReSharper disable RedundantAssignment
// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace
#pragma warning disable 1591

namespace HAL_Simulator
{
    ///<inheritdoc cref="HAL"/>
    public class HALAccelerometer
    {
        /// Return Type: void
        ///param0: boolean
        [CalledSimFunction]
        public static void setAccelerometerActive(bool param0)
        {
            halData["accelerometer"]["active"] = param0;
        }


        /// Return Type: void
        ///param0: AccelerometerRange
        [CalledSimFunction]
        public static void setAccelerometerRange(AccelerometerRange param0)
        {
            halData["accelerometer"]["range"] = (int)param0;
        }


        [CalledSimFunction]
        public static double getAccelerometerX()
        {
            return halData["accelerometer"]["x"];
        }

        [CalledSimFunction]
        public static double getAccelerometerY()
        {
            return halData["accelerometer"]["y"];
        }

        [CalledSimFunction]
        public static double getAccelerometerZ()
        {
            return halData["accelerometer"]["z"];
        }
    }
}
