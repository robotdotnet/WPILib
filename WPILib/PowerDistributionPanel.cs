using NetworkTables.Tables;
using WPILib.LiveWindows;
using static HAL.HALPDP;

namespace WPILib
{
    /// <summary>
    /// Class for getting voltage, current, temperature, and energy from the CAN PDP.
    /// </summary>
    public class PowerDistributionPanel : SensorBase, ILiveWindowSendable
    {
        private readonly int m_module = 0;

        public PowerDistributionPanel(int module)
        {
            m_module = module;
            CheckPDPModule(m_module);
            InitializePDP(m_module);
        }

        public PowerDistributionPanel() : this(0)
        { }

        public double GetVoltage()
        {
            int status = 0;
            double value = GetPDPVoltage((byte)m_module, ref status);
            return value;
        }

        public double GetTemperature()
        {
            int status = 0;
            double value = GetPDPTemperature((byte)m_module, ref status);
            return value;
        }

        public double GetCurrent(int channel)
        {
            int status = 0;
            double value = GetPDPChannelCurrent((byte)channel, (byte)m_module, ref status);
            return value;
        }

        public double GetTotalCurrent()
        {
            int status = 0;
            double value = GetPDPTotalCurrent((byte)m_module, ref status);
            return value;
        }

        public double GetTotalPower()
        {
            int status = 0;
            double value = GetPDPTotalPower((byte)m_module, ref status);
            return value;
        }

        public double GetTotalEnergy()
        {
            int status = 0;
            double value = GetPDPTotalEnergy((byte)m_module, ref status);
            return value;
        }

        public void ResetTotalEnergy()
        {
            int status = 0;
            ResetPDPTotalEnergy((byte)m_module, ref status);
        }

        public void ClearStickyFaults()
        {
            int status = 0;
            ClearPDPStickyFaults((byte)m_module, ref status);
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
