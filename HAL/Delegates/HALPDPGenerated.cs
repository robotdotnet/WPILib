//File automatically generated using robotdotnet-tools. Please do not modify.

// ReSharper disable CheckNamespace

namespace HAL.Base
{
    public partial class HALPDP
    {
        static HALPDP()
        {
            HAL.Initialize();
        }

        public delegate void InitializePDPDelegate(int module);
        public static InitializePDPDelegate InitializePDP;

        public delegate double GetPDPTemperatureDelegate(byte module, ref int status);
        public static GetPDPTemperatureDelegate GetPDPTemperature;

        public delegate double GetPDPVoltageDelegate(byte module, ref int status);
        public static GetPDPVoltageDelegate GetPDPVoltage;

        public delegate double GetPDPChannelCurrentDelegate(byte module, byte channel, ref int status);
        public static GetPDPChannelCurrentDelegate GetPDPChannelCurrent;

        public delegate double GetPDPTotalCurrentDelegate(byte module, ref int status);
        public static GetPDPTotalCurrentDelegate GetPDPTotalCurrent;

        public delegate double GetPDPTotalPowerDelegate(byte module, ref int status);
        public static GetPDPTotalPowerDelegate GetPDPTotalPower;

        public delegate double GetPDPTotalEnergyDelegate(byte module, ref int status);
        public static GetPDPTotalEnergyDelegate GetPDPTotalEnergy;

        public delegate void ResetPDPTotalEnergyDelegate(byte module, ref int status);
        public static ResetPDPTotalEnergyDelegate ResetPDPTotalEnergy;

        public delegate void ClearPDPStickyFaultsDelegate(byte module, ref int status);
        public static ClearPDPStickyFaultsDelegate ClearPDPStickyFaults;
    }
}
