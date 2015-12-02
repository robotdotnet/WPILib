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
    internal class HALPower
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            global::HAL.HALPower.GetVinVoltage = getVinVoltage;
            global::HAL.HALPower.GetVinCurrent = getVinCurrent;
            global::HAL.HALPower.GetUserVoltage6V = getUserVoltage6V;
            global::HAL.HALPower.GetUserCurrent6V = getUserCurrent6V;
            global::HAL.HALPower.GetUserActive6V = getUserActive6V;
            global::HAL.HALPower.GetUserCurrentFaults6V = getUserCurrentFaults6V;
            global::HAL.HALPower.GetUserVoltage5V = getUserVoltage5V;
            global::HAL.HALPower.GetUserCurrent5V = getUserCurrent5V;
            global::HAL.HALPower.GetUserActive5V = getUserActive5V;
            global::HAL.HALPower.GetUserCurrentFaults5V = getUserCurrentFaults5V;
            global::HAL.HALPower.GetUserVoltage3V3 = getUserVoltage3V3;
            global::HAL.HALPower.GetUserCurrent3V3 = getUserCurrent3V3;
            global::HAL.HALPower.GetUserActive3V3 = getUserActive3V3;
            global::HAL.HALPower.GetUserCurrentFaults3V3 = getUserCurrentFaults3V3;
        }


        [CalledSimFunction]
        public static float getVinVoltage(ref int status)
        {
            status = 0;
            return RoboRioData.VInVoltage;
        }

        [CalledSimFunction]
        public static float getVinCurrent(ref int status)
        {
            status = 0;
            return RoboRioData.VInCurrent;
        }

        [CalledSimFunction]
        public static float getUserVoltage6V(ref int status)
        {
            status = 0;
            return RoboRioData.UserVoltage6V;
        }

        [CalledSimFunction]
        public static float getUserCurrent6V(ref int status)
        {
            status = 0;
            return RoboRioData.UserCurrent6V;
        }

        [CalledSimFunction]
        public static bool getUserActive6V(ref int status)
        {
            status = 0;
            return RoboRioData.UserActive6V;
        }

        [CalledSimFunction]
        public static int getUserCurrentFaults6V(ref int status)
        {
            status = 0;
            return RoboRioData.UserFaults6V;
        }

        [CalledSimFunction]
        public static float getUserVoltage5V(ref int status)
        {
            status = 0;
            return RoboRioData.UserVoltage5V;
        }

        [CalledSimFunction]
        public static float getUserCurrent5V(ref int status)
        {
            status = 0;
            return RoboRioData.UserCurrent5V;
        }

        [CalledSimFunction]
        public static bool getUserActive5V(ref int status)
        {
            status = 0;
            return RoboRioData.UserActive5V;
        }

        [CalledSimFunction]
        public static int getUserCurrentFaults5V(ref int status)
        {
            status = 0;
            return RoboRioData.UserFaults5V;
        }

        [CalledSimFunction]
        public static float getUserVoltage3V3(ref int status)
        {
            status = 0;
            return RoboRioData.UserVoltage3V3;
        }

        [CalledSimFunction]
        public static float getUserCurrent3V3(ref int status)
        {
            status = 0;
            return RoboRioData.UserCurrent3V3;
        }

        [CalledSimFunction]
        public static bool getUserActive3V3(ref int status)
        {
            status = 0;
            return RoboRioData.UserActive3V3;
        }

        [CalledSimFunction]
        public static int getUserCurrentFaults3V3(ref int status)
        {
            status = 0;
            return RoboRioData.UserFaults3V3;
        }
    }
}
