using System.Runtime.InteropServices;
using static HAL_Simulator.SimData;

namespace HAL_Simulator
{
    public enum AccelerometerRange
    {
        /// kRange_2G -> 0
        kRange_2G = 0,

        /// kRange_4G -> 1
        kRange_4G = 1,

        /// kRange_8G -> 2
        kRange_8G = 2,
    }

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
