//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Linq;
using System.Reflection;

// ReSharper disable CheckNamespace

namespace HAL_Base
{
    public partial class HALPDP
    {
        static HALPDP()
        {
            HAL.Initialize();
        }

        internal static void SetupDelegates()
        {
            string className = MethodBase.GetCurrentMethod().DeclaringType.Name;
            var types = HAL.HALAssembly.GetTypes();
            var q = from t in types where t.IsClass && t.Name == className select t;
            Type type = HAL.HALAssembly.GetType(q.ToList()[0].FullName);
            InitializePDP = (InitializePDPDelegate)Delegate.CreateDelegate(typeof (InitializePDPDelegate), type.GetMethod("initializePDP"));
            GetPDPTemperature = (GetPDPTemperatureDelegate)Delegate.CreateDelegate(typeof(GetPDPTemperatureDelegate), type.GetMethod("getPDPTemperature"));
            GetPDPVoltage = (GetPDPVoltageDelegate)Delegate.CreateDelegate(typeof(GetPDPVoltageDelegate), type.GetMethod("getPDPVoltage"));
            GetPDPChannelCurrent = (GetPDPChannelCurrentDelegate)Delegate.CreateDelegate(typeof(GetPDPChannelCurrentDelegate), type.GetMethod("getPDPChannelCurrent"));
            GetPDPTotalCurrent = (GetPDPTotalCurrentDelegate)Delegate.CreateDelegate(typeof(GetPDPTotalCurrentDelegate), type.GetMethod("getPDPTotalCurrent"));
            GetPDPTotalPower = (GetPDPTotalPowerDelegate)Delegate.CreateDelegate(typeof(GetPDPTotalPowerDelegate), type.GetMethod("getPDPTotalPower"));
            GetPDPTotalEnergy = (GetPDPTotalEnergyDelegate)Delegate.CreateDelegate(typeof(GetPDPTotalEnergyDelegate), type.GetMethod("getPDPTotalEnergy"));
            ResetPDPTotalEnergy = (ResetPDPTotalEnergyDelegate)Delegate.CreateDelegate(typeof(ResetPDPTotalEnergyDelegate), type.GetMethod("resetPDPTotalEnergy"));
            ClearPDPStickyFaults = (ClearPDPStickyFaultsDelegate)Delegate.CreateDelegate(typeof(ClearPDPStickyFaultsDelegate), type.GetMethod("clearPDPStickyFaults"));
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
