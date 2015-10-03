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
    internal class HALPDP
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            HAL_Base.HALPDP.InitializePDP = initializePDP;
            HAL_Base.HALPDP.GetPDPTemperature = getPDPTemperature;
            HAL_Base.HALPDP.GetPDPVoltage = getPDPVoltage;
            HAL_Base.HALPDP.GetPDPChannelCurrent = getPDPChannelCurrent;
            HAL_Base.HALPDP.GetPDPTotalCurrent = getPDPTotalCurrent;
            HAL_Base.HALPDP.GetPDPTotalPower = getPDPTotalPower;
            HAL_Base.HALPDP.GetPDPTotalEnergy = getPDPTotalEnergy;
            HAL_Base.HALPDP.ResetPDPTotalEnergy = resetPDPTotalEnergy;
            HAL_Base.HALPDP.ClearPDPStickyFaults = clearPDPStickyFaults;
        }


        [CalledSimFunction]
        public static void initializePDP(int module)
        {
            InitializeNewPDP(module);
        }

        [CalledSimFunction]
        public static double getPDPTemperature(ref int status, byte module)
        {
            status = 0;
            return halData["pdp"][(int)module]["temperature"];
        }

        [CalledSimFunction]
        public static double getPDPVoltage(ref int status, byte module)
        {
            status = 0;
            return halData["pdp"][(int)module]["voltage"];
        }

        [CalledSimFunction]
        public static double getPDPChannelCurrent(byte channel, ref int status, byte module)
        {
            status = 0;
            return halData["pdp"][(int)module]["current"][(int)channel];
        }

        [CalledSimFunction]
        public static double getPDPTotalCurrent(ref int status, byte module)
        {
            status = 0;
            return halData["pdp"][(int)module]["total_current"];
        }

        [CalledSimFunction]
        public static double getPDPTotalPower(ref int status, byte module)
        {
            status = 0;
            return halData["pdp"][(int)module]["total_power"];
        }

        [CalledSimFunction]
        public static double getPDPTotalEnergy(ref int status, byte module)
        {
            status = 0;
            return halData["pdp"][(int)module]["total_energy"];
        }

        [CalledSimFunction]
        public static void resetPDPTotalEnergy(ref int status, byte module)
        {
            status = 0;
            halData["pdp"][(int)module]["total_energy"] = 0;
        }


        [CalledSimFunction]
        public static void clearPDPStickyFaults(ref int status, byte module)
        {
            status = 0;
        }
    }
}
