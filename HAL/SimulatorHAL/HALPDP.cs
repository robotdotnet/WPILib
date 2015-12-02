using System;
using System.Linq;
using HAL.Base;
using HAL.Simulator.Data;
using static HAL.Simulator.SimData;

// ReSharper disable RedundantAssignment
// ReSharper disable InconsistentNaming

#pragma warning disable 1591

namespace HAL.SimulatorHAL
{
    ///<inheritdoc cref="HAL"/>
    internal class HALPDP
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALPDP.InitializePDP = initializePDP;
            Base.HALPDP.GetPDPTemperature = getPDPTemperature;
            Base.HALPDP.GetPDPVoltage = getPDPVoltage;
            Base.HALPDP.GetPDPChannelCurrent = getPDPChannelCurrent;
            Base.HALPDP.GetPDPTotalCurrent = getPDPTotalCurrent;
            Base.HALPDP.GetPDPTotalPower = getPDPTotalPower;
            Base.HALPDP.GetPDPTotalEnergy = getPDPTotalEnergy;
            Base.HALPDP.ResetPDPTotalEnergy = resetPDPTotalEnergy;
            Base.HALPDP.ClearPDPStickyFaults = clearPDPStickyFaults;
        }


        [CalledSimFunction]
        public static void initializePDP(int module)
        {
            InitializePDP(module);
        }

        [CalledSimFunction]
        public static double getPDPTemperature(byte module, ref int status)
        {
            status = 0;
            return GetPDP(module).Temperature;
        }

        [CalledSimFunction]
        public static double getPDPVoltage(byte module, ref int status)
        {
            status = 0;
            return GetPDP(module).Voltage;
        }

        [CalledSimFunction]
        public static double getPDPChannelCurrent(byte channel, byte module, ref int status)
        {
            status = 0;
            return GetPDP(module).Current[channel];
        }

        [CalledSimFunction]
        public static double getPDPTotalCurrent(byte module, ref int status)
        {
            status = 0;
            PDPData data = GetPDP(module);
            return data.Current.Sum();
        }

        [CalledSimFunction]
        public static double getPDPTotalPower(byte module, ref int status)
        {
            status = 0;
            return getPDPTotalCurrent(module, ref status) * getPDPVoltage(module, ref status);
        }

        [CalledSimFunction]
        public static double getPDPTotalEnergy(byte module, ref int status)
        {
            status = 0;
            return GetPDP(module).TotalEnergy;
        }

        [CalledSimFunction]
        public static void resetPDPTotalEnergy(byte module, ref int status)
        {
            status = 0;
            GetPDP(module).TotalEnergy = 0;
        }


        [CalledSimFunction]
        public static void clearPDPStickyFaults(byte module, ref int status)
        {
            status = 0;
        }
    }
}
