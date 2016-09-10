using NetworkTables.Tables;
using WPILib.LiveWindow;
using static HAL.Base.HALPDP;

namespace WPILib
{
    /// <summary>
    /// Class for getting voltage, current, temperature, and energy from the CAN PDP.
    /// </summary>
    public class PowerDistributionPanel : SensorBase, ILiveWindowSendable
    {
        private readonly int m_module = 0;

        /// <summary>
        /// Creates a new <see cref="PowerDistributionPanel"/> class using the specified module.
        /// </summary>
        /// <param name="module">The module number of the PDP.</param>
        public PowerDistributionPanel(int module)
        {
            m_module = module;
            CheckPDPModule(m_module);
            int status = 0;
            HAL_InitializePDP(m_module, ref status);
        }

        /// <summary>
        /// Creates a new <see cref="PowerDistributionPanel"/> with the default module of 0.
        /// </summary>
        public PowerDistributionPanel() : this(0)
        { }

        /// <summary>
        /// Query the input voltage of the PDP
        /// </summary>
        /// <returns>The voltage of the PDP in volts.</returns>
        public double GetVoltage()
        {
            int status = 0;
            double value = HAL_GetPDPVoltage((byte)m_module, ref status);
            return value;
        }
        
        /// <summary>
        /// Query the temperature of the PDP.
        /// </summary>
        /// <returns>The temperature of the PDP in degrees Celsius.</returns>
        public double GetTemperature()
        {
            int status = 0;
            double value = HAL_GetPDPTemperature((byte)m_module, ref status);
            return value;
        }

        /// <summary>
        /// Query the current of a single channel of the PDP.
        /// </summary>
        /// <param name="channel">The channel to read from. [0..15]</param>
        /// <returns>The current of the PDP channel in Amperes.</returns>
        public double GetCurrent(int channel)
        {
            int status = 0;
            CheckPDPChannel(channel);
            double value = HAL_GetPDPChannelCurrent((byte)m_module, (byte)channel, ref status);
            return value;
        }

        /// <summary>
        /// Query the current of all monitored PDP channels (0-15).
        /// </summary>
        /// <returns>The current of all channels in Amperes.</returns>
        public double GetTotalCurrent()
        {
            int status = 0;
            double value = HAL_GetPDPTotalCurrent((byte)m_module, ref status);
            return value;
        }

        /// <summary>
        /// Query the total power drawn from the monitored PDP channels.
        /// </summary>
        /// <returns>The total power in Watts.</returns>
        public double GetTotalPower()
        {
            int status = 0;
            double value = HAL_GetPDPTotalPower((byte)m_module, ref status);
            return value;
        }

        /// <summary>
        /// Query the total energy drawn from the monitored PDP channels.
        /// </summary>
        /// <returns>The total enegery in Joules.</returns>
        public double GetTotalEnergy()
        {
            int status = 0;
            double value = HAL_GetPDPTotalEnergy((byte)m_module, ref status);
            return value;
        }

        /// <summary>
        /// Resets the total energy to 0.
        /// </summary>
        public void ResetTotalEnergy()
        {
            int status = 0;
            HAL_ResetPDPTotalEnergy((byte)m_module, ref status);
        }


        /// <summary>
        /// Clears all PDP Sticky Faults.
        /// </summary>
        public void ClearStickyFaults()
        {
            int status = 0;
            HAL_ClearPDPStickyFaults((byte)m_module, ref status);
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
