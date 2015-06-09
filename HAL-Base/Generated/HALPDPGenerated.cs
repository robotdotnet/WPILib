//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Linq;
using System.Reflection;

namespace HAL_Base
{
    public partial class HALPDP
    {
        internal static void SetupDelegates()
        {
            string className = MethodBase.GetCurrentMethod().DeclaringType.Name;
            var types = HAL.HALAssembly.GetTypes();
            var q = from t in types where t.IsClass && t.Name == className select t;
            Type type = HAL.HALAssembly.GetType(q.ToList()[0].FullName);
            GetPDPTemperature = (GetPDPTemperatureDelegate)Delegate.CreateDelegate(typeof(GetPDPTemperatureDelegate), type.GetMethod("getPDPTemperature"));
            GetPDPVoltage = (GetPDPVoltageDelegate)Delegate.CreateDelegate(typeof(GetPDPVoltageDelegate), type.GetMethod("getPDPVoltage"));
            GetPDPChannelCurrent = (GetPDPChannelCurrentDelegate)Delegate.CreateDelegate(typeof(GetPDPChannelCurrentDelegate), type.GetMethod("getPDPChannelCurrent"));
            GetPDPTotalCurrent = (GetPDPTotalCurrentDelegate)Delegate.CreateDelegate(typeof(GetPDPTotalCurrentDelegate), type.GetMethod("getPDPTotalCurrent"));
            GetPDPTotalPower = (GetPDPTotalPowerDelegate)Delegate.CreateDelegate(typeof(GetPDPTotalPowerDelegate), type.GetMethod("getPDPTotalPower"));
            GetPDPTotalEnergy = (GetPDPTotalEnergyDelegate)Delegate.CreateDelegate(typeof(GetPDPTotalEnergyDelegate), type.GetMethod("getPDPTotalEnergy"));
            ResetPDPTotalEnergy = (ResetPDPTotalEnergyDelegate)Delegate.CreateDelegate(typeof(ResetPDPTotalEnergyDelegate), type.GetMethod("resetPDPTotalEnergy"));
            ClearPDPStickyFaults = (ClearPDPStickyFaultsDelegate)Delegate.CreateDelegate(typeof(ClearPDPStickyFaultsDelegate), type.GetMethod("clearPDPStickyFaults"));
        }

        public delegate double GetPDPTemperatureDelegate(ref int status);
        public static GetPDPTemperatureDelegate GetPDPTemperature;

        public delegate double GetPDPVoltageDelegate(ref int status);
        public static GetPDPVoltageDelegate GetPDPVoltage;

        public delegate double GetPDPChannelCurrentDelegate(byte channel, ref int status);
        public static GetPDPChannelCurrentDelegate GetPDPChannelCurrent;

        public delegate double GetPDPTotalCurrentDelegate(ref int status);
        public static GetPDPTotalCurrentDelegate GetPDPTotalCurrent;

        public delegate double GetPDPTotalPowerDelegate(ref int status);
        public static GetPDPTotalPowerDelegate GetPDPTotalPower;

        public delegate double GetPDPTotalEnergyDelegate(ref int status);
        public static GetPDPTotalEnergyDelegate GetPDPTotalEnergy;

        public delegate void ResetPDPTotalEnergyDelegate(ref int status);
        public static ResetPDPTotalEnergyDelegate ResetPDPTotalEnergy;

        public delegate void ClearPDPStickyFaultsDelegate(ref int status);
        public static ClearPDPStickyFaultsDelegate ClearPDPStickyFaults;
    }
}
