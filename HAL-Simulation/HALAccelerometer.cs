using System.Diagnostics.CodeAnalysis;
using HAL_Base;
using static HAL_Simulator.SimData;
// ReSharper disable RedundantAssignment
// ReSharper disable InconsistentNaming

namespace HAL_Simulator
{
    public class HALAccelerometer
    {
        /// Return Type: void
        ///param0: boolean
        public static void setAccelerometerActive(bool param0)
        {
            halData["accelerometer"]["active"] = param0;
        }


        /// Return Type: void
        ///param0: AccelerometerRange
        public static void setAccelerometerRange(AccelerometerRange param0)
        {
            halData["accelerometer"]["range"] = (int) param0;
        }


        public static double getAccelerometerX()
        {
            return halData["accelerometer"]["x"];
        }

        public static double getAccelerometerY()
        {
            return halData["accelerometer"]["y"];
        }

        public static double getAccelerometerZ()
        {
            return halData["accelerometer"]["z"];
        }
    }
}
