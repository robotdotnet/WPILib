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
        ///param0: boolean
        [CalledSimFunction]
        internal static void setAccelerometerActive(bool param0)
        {
            halData["accelerometer"]["active"] = param0;
        }


        /// Return Type: void
        ///param0: AccelerometerRange
        [CalledSimFunction]
        internal static void setAccelerometerRange(AccelerometerRange param0)
        {
            halData["accelerometer"]["range"] = (int)param0;
        }


        [CalledSimFunction]
        internal static double getAccelerometerX()
        {
            return halData["accelerometer"]["x"];
        }

        [CalledSimFunction]
        internal static double getAccelerometerY()
        {
            return halData["accelerometer"]["y"];
        }

        [CalledSimFunction]
        internal static double getAccelerometerZ()
        {
            return halData["accelerometer"]["z"];
        }
    }
}
