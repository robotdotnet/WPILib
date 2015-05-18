using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPILib.Interfaces;
using HAL_Base;

namespace WPILib
{
    public class PowerDistributionPanel : SensorBase
    {
        public PowerDistributionPanel()
        {
            
        }

        public double GetVoltage()
        {
            int status = 0;
            double value = HALPDP.GetPDPVoltage(ref status);
            return value;
        }

        public double GetTemperature()
        {
            int status = 0;
            double value = HALPDP.GetPDPTemperature(ref status);
            return value;
        }

        public double GetChannel(int channel)
        {
            int status = 0;
            double value = HALPDP.GetPDPChannelCurrent((byte)channel, ref status);
            return value;
        }

        public double GetTotalCurrent()
        {
            int status = 0;
            double value = HALPDP.GetPDPTotalCurrent(ref status);
            return value;
        }

        public double GetTotalPower()
        {
            int status = 0;
            double value = HALPDP.GetPDPTotalPower(ref status);
            return value;
        }

        public double GetTotalEnergy()
        {
            int status = 0;
            double value = HALPDP.GetPDPTotalEnergy(ref status);
            return value;
        }

        public void ResetEnergyTotal()
        {
            int status = 0;
            HALPDP.ResetPDPTotalEnergy(ref status);
        }

        public void ClearStickyFaults()
        {
            int status = 0;
            HALPDP.ClearPDPStickyFaults(ref status);
        }

        
    }
}
