using System;
using System.IO;

namespace HAL.Base
{
    public partial class HAL
    {
        public const int HALInvalidHandle = 0;

        ///This contains constants used for the DriverStation.
        public class DriverStationConstants
        {
            ///The number of joystick ports
            public const int JoystickPorts = 6;
            ///The number of joystick axes
            public const int MaxJoystickAxes = 12;
            ///The number of joystick POVs
            public const int MaxJoystickPOVs = 12;
        }

        /// <summary>
        /// HAL Initialization. Must be called before any other HAL functions.
        /// </summary>
        /// <param name="mode">Initialization Mode</param>
        public static void Initialize(int mode = 0)
        {
            var rv = HAL_Initialize(mode);
            if (rv != 1)
            {
                throw new Exception($"HAL Initialize Failed with return code {rv}");
            }
        }


        public static long Report(ResourceType resource, Instances instanceNumber, int context = 0, string feature = null)
        {
            return HAL_Report((int)resource, (int)instanceNumber, context, feature);
        }

        public static long Report(ResourceType resource, int instanceNumber, int context = 0, string feature = null)
        {
            return HAL_Report((byte)resource, instanceNumber, context, feature);
        }

        public static long Report(int resource, Instances instanceNumber, int context = 0, string feature = null)
        {
            return HAL_Report(resource, (int)instanceNumber, context, feature);
        }

        public static long Report(int resource, int instanceNumber, int context = 0, string feature = null)
        {
            return HAL_Report(resource, instanceNumber, context, feature);
        }
    }
}
