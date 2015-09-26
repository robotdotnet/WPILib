using System;
using System.Runtime.InteropServices;
using HAL_Base;
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
            HAL_Base.HALPower.GetVinVoltage = getVinVoltage;
            HAL_Base.HALPower.GetVinCurrent = getVinCurrent;
            HAL_Base.HALPower.GetUserVoltage6V = getUserVoltage6V;
            HAL_Base.HALPower.GetUserCurrent6V = getUserCurrent6V;
            HAL_Base.HALPower.GetUserActive6V = getUserActive6V;
            HAL_Base.HALPower.GetUserCurrentFaults6V = getUserCurrentFaults6V;
            HAL_Base.HALPower.GetUserVoltage5V = getUserVoltage5V;
            HAL_Base.HALPower.GetUserCurrent5V = getUserCurrent5V;
            HAL_Base.HALPower.GetUserActive5V = getUserActive5V;
            HAL_Base.HALPower.GetUserCurrentFaults5V = getUserCurrentFaults5V;
            HAL_Base.HALPower.GetUserVoltage3V3 = getUserVoltage3V3;
            HAL_Base.HALPower.GetUserCurrent3V3 = getUserCurrent3V3;
            HAL_Base.HALPower.GetUserActive3V3 = getUserActive3V3;
            HAL_Base.HALPower.GetUserCurrentFaults3V3 = getUserCurrentFaults3V3;
        }


        [CalledSimFunction]
        public static float getVinVoltage(ref int status)
        {
            status = 0;
            return (float)halData["power"]["vin_voltage"];
        }

        [CalledSimFunction]
        public static float getVinCurrent(ref int status)
        {
            status = 0;
            return (float)halData["power"]["vin_current"];
        }

        [CalledSimFunction]
        public static float getUserVoltage6V(ref int status)
        {
            status = 0;
            return (float)halData["power"]["user_voltage_6v"];
        }

        [CalledSimFunction]
        public static float getUserCurrent6V(ref int status)
        {
            status = 0;
            return (float)halData["power"]["user_current_6v"];
        }

        [CalledSimFunction]
        public static bool getUserActive6V(ref int status)
        {
            status = 0;
            return halData["power"]["user_active_6v"];
        }

        [CalledSimFunction]
        public static int getUserCurrentFaults6V(ref int status)
        {
            status = 0;
            return halData["power"]["user_faults_6v"];
        }

        [CalledSimFunction]
        public static float getUserVoltage5V(ref int status)
        {
            status = 0;
            return (float)halData["power"]["user_voltage_5v"];
        }

        [CalledSimFunction]
        public static float getUserCurrent5V(ref int status)
        {
            status = 0;
            return (float)halData["power"]["user_current_5v"];
        }

        [CalledSimFunction]
        public static bool getUserActive5V(ref int status)
        {
            status = 0;
            return halData["power"]["user_active_5v"];
        }

        [CalledSimFunction]
        public static int getUserCurrentFaults5V(ref int status)
        {
            status = 0;
            return halData["power"]["user_faults_5v"];
        }

        [CalledSimFunction]
        public static float getUserVoltage3V3(ref int status)
        {
            status = 0;
            return (float)halData["power"]["user_voltage_3v3"];
        }

        [CalledSimFunction]
        public static float getUserCurrent3V3(ref int status)
        {
            status = 0;
            return (float)halData["power"]["user_current_3v3"];
        }

        [CalledSimFunction]
        public static bool getUserActive3V3(ref int status)
        {
            status = 0;
            return halData["power"]["user_active_3v3"];
        }

        [CalledSimFunction]
        public static int getUserCurrentFaults3V3(ref int status)
        {
            status = 0;
            return halData["power"]["user_faults_3v3"];
        }
    }
}
