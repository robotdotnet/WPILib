using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPILib.Interfaces;
using HAL_FRC;

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
            double value = HALPDP.getPDPVoltage(ref status);
            return value;
        }

        public double GetTemperature()
        {
            int status = 0;
            double value = HALPDP.getPDPTemperature(ref status);
            return value;
        }

        public double GetChannel(int channel)
        {
            int status = 0;
            double value = HALPDP.getPDPChannelCurrent((byte)channel, ref status);
            return value;
        }

        public double GetTotalCurrent()
        {
            int status = 0;
            double value = HALPDP.getPDPTotalCurrent(ref status);
            return value;
        }

        public double GetTotalPower()
        {
            int status = 0;
            double value = HALPDP.getPDPTotalPower(ref status);
            return value;
        }

        public double GetTotalEnergy()
        {
            int status = 0;
            double value = HALPDP.getPDPTotalEnergy(ref status);
            return value;
        }

        public void ResetEnergyTotal()
        {
            int status = 0;
            HALPDP.resetPDPTotalEnergy(ref status);
        }

        public void ClearStickyFaults()
        {
            int status = 0;
            HALPDP.clearPDPStickyFaults(ref status);
        }

        
    }
}
