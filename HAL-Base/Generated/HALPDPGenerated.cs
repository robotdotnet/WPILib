//File automatically generated using robotdotnet-tools. Please do not modify.

// ReSharper disable CheckNamespace

namespace HAL_Base
{
    public partial class HALPDP
    {
        static HALPDP()
        {
            HAL.Initialize();
        }

        public delegate void InitializePDPDelegate(int module);
        public static InitializePDPDelegate InitializePDP;

        public delegate double GetPDPTemperatureDelegate(ref int status, byte module);
        public static GetPDPTemperatureDelegate GetPDPTemperature;

        public delegate double GetPDPVoltageDelegate(ref int status, byte module);
        public static GetPDPVoltageDelegate GetPDPVoltage;

        public delegate double GetPDPChannelCurrentDelegate(byte channel, ref int status, byte module);
        public static GetPDPChannelCurrentDelegate GetPDPChannelCurrent;

        public delegate double GetPDPTotalCurrentDelegate(ref int status, byte module);
        public static GetPDPTotalCurrentDelegate GetPDPTotalCurrent;

        public delegate double GetPDPTotalPowerDelegate(ref int status, byte module);
        public static GetPDPTotalPowerDelegate GetPDPTotalPower;

        public delegate double GetPDPTotalEnergyDelegate(ref int status, byte module);
        public static GetPDPTotalEnergyDelegate GetPDPTotalEnergy;

        public delegate void ResetPDPTotalEnergyDelegate(ref int status, byte module);
        public static ResetPDPTotalEnergyDelegate ResetPDPTotalEnergy;

        public delegate void ClearPDPStickyFaultsDelegate(ref int status, byte module);
        public static ClearPDPStickyFaultsDelegate ClearPDPStickyFaults;
    }
}
