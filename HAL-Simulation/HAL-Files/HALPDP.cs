using System.Linq;
using System.Runtime.InteropServices;
using HAL_Simulator.Data;
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
        [CalledSimFunction]
        public static void initializePDP(int module)
        {
            InitializePDP(module);
        }

        [CalledSimFunction]
        public static double getPDPTemperature(ref int status, byte module)
        {
            status = 0;
            return SimData.GetPDP(module).Temperature;
        }

        [CalledSimFunction]
        public static double getPDPVoltage(ref int status, byte module)
        {
            status = 0;
            return SimData.GetPDP(module).Voltage;
        }

        [CalledSimFunction]
        public static double getPDPChannelCurrent(byte channel, ref int status, byte module)
        {
            status = 0;
            return SimData.GetPDP(module).Current[channel];
        }

        [CalledSimFunction]
        public static double getPDPTotalCurrent(ref int status, byte module)
        {
            status = 0;
            PDPData data = SimData.GetPDP(module);
            return data.Current.Sum();
        }

        [CalledSimFunction]
        public static double getPDPTotalPower(ref int status, byte module)
        {
            status = 0;
            return getPDPTotalCurrent(ref status, module) * getPDPVoltage(ref status, module);
        }

        [CalledSimFunction]
        public static double getPDPTotalEnergy(ref int status, byte module)
        {
            status = 0;
            return SimData.GetPDP(module).TotalEnergy;
        }

        [CalledSimFunction]
        public static void resetPDPTotalEnergy(ref int status, byte module)
        {
            status = 0;
            SimData.GetPDP(module).TotalEnergy = 0;
        }


        [CalledSimFunction]
        public static void clearPDPStickyFaults(ref int status, byte module)
        {
            status = 0;
        }
    }
}
