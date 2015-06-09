﻿using HAL_Base;

namespace WPILib
{
    public class PowerDistributionPanel : SensorBase
    {
        public double Voltage
        {
            get
            {
                int status = 0;
                double value = HALPDP.GetPDPVoltage(ref status);
                return value;
            }
        }

        public double Temperature
        {
            get
            {
                int status = 0;
                double value = HALPDP.GetPDPTemperature(ref status);
                return value;
            }
        }

        public double GetChannel(int channel)
        {
            int status = 0;
            double value = HALPDP.GetPDPChannelCurrent((byte)channel, ref status);
            return value;
        }

        public double TotalCurrent
        {
            get
            {
                int status = 0;
                double value = HALPDP.GetPDPTotalCurrent(ref status);
                return value;
            }
        }

        public double TotalPower
        {
            get
            {
                int status = 0;
                double value = HALPDP.GetPDPTotalPower(ref status);
                return value;
            }
        }

        public double TotalEnergy
        {
            get
            {
                int status = 0;
                double value = HALPDP.GetPDPTotalEnergy(ref status);
                return value;
            }
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
