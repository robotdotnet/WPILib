using HAL_Base;
using NetworkTablesDotNet.Tables;
using WPILib.LiveWindows;

namespace WPILib
{
    public class PowerDistributionPanel : SensorBase, ILiveWindowSendable
    {
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

        public double GetCurrent(int channel)
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

        ///<inheritdoc />
        public void InitTable(ITable subtable)
        {
            Table = subtable;
            UpdateTable();
        }
        ///<inheritdoc />
        public ITable Table { get; private set; }
        ///<inheritdoc />
        public string SmartDashboardType => "PowerDistributionPanel";
        ///<inheritdoc />
        public void UpdateTable()
        {
            if (Table != null)
            {
                Table.PutNumber("Chan0", GetCurrent(0));
                Table.PutNumber("Chan1", GetCurrent(1));
                Table.PutNumber("Chan2", GetCurrent(2));
                Table.PutNumber("Chan3", GetCurrent(3));
                Table.PutNumber("Chan4", GetCurrent(4));
                Table.PutNumber("Chan5", GetCurrent(5));
                Table.PutNumber("Chan6", GetCurrent(6));
                Table.PutNumber("Chan7", GetCurrent(7));
                Table.PutNumber("Chan8", GetCurrent(8));
                Table.PutNumber("Chan9", GetCurrent(9));
                Table.PutNumber("Chan10", GetCurrent(10));
                Table.PutNumber("Chan11", GetCurrent(11));
                Table.PutNumber("Chan12", GetCurrent(12));
                Table.PutNumber("Chan13", GetCurrent(13));
                Table.PutNumber("Chan14", GetCurrent(14));
                Table.PutNumber("Chan15", GetCurrent(15));
                Table.PutNumber("Voltage", GetVoltage());
                Table.PutNumber("TotalCurrent", GetTotalCurrent());
            }
        }
        ///<inheritdoc />
        public void StartLiveWindowMode()
        {
        }
        ///<inheritdoc />
        public void StopLiveWindowMode()
        {
        }
    }
}
