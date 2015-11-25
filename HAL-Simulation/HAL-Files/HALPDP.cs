using System;
using System.Linq;
using HAL_Base;
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
