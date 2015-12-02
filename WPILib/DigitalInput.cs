using HAL;
using NetworkTables.Tables;
using WPILib.LiveWindows;
using static WPILib.Utility;

namespace WPILib
{
    /// <summary>
    /// Class to read a digital input
    /// </summary>
    public class DigitalInput : DigitalSource, ILiveWindowSendable
    {
        /// <summary>
        /// Create an instance of a Digital Input
        /// </summary>
        /// <param name="channel">The DIO channel for the digital input 0-9 are on-board, 10-25 are on the MXP</param>
        public DigitalInput(int channel)
        {
            InitDigitalPort(channel, true);

            HAL.HAL.Report(ResourceType.kResourceType_DigitalInput, (byte)channel);
            LiveWindow.LiveWindow.AddSensor("DigitalInput", channel, this);
        }

        /// <summary>
        /// Get the value from the Digital Input Channel
        /// </summary>
        /// <returns>The status of the digital input</returns>
        public virtual bool Get()
        {
            int status = 0;
            bool value = HALDigital.GetDIO(m_port, ref status);
            CheckStatus(status);
            return value;
        }

        /// <summary>
        /// Get the channel of the digital input.
        /// </summary>
        public int Channel => m_channel;

        /// <summary>
        /// Is this an analog trigger?
        /// </summary>
        public override bool AnalogTriggerForRouting => false;

        /// <summary>
        /// Initialize a table for this sendable object.
        /// </summary>
        /// <param name="subtable">The table to put the values in.</param>
        public void InitTable(ITable subtable)
        {
            Table = subtable;
            UpdateTable();
        }

        /// <summary>
        /// Returns the table that is currently associated with the sendable
        /// </summary>
        public ITable Table { get; private set; }

        /// <summary>
        /// Returns the string representation of the named data type that will be used by the smart dashboard for this sendable
        /// </summary>
        public string SmartDashboardType => "Digital Input";

        /// <summary>
        /// Update the table for this sendable object with the latest
        /// values.
        /// </summary>
        public void UpdateTable()
        {
            Table?.PutBoolean("Value", Get());
        }

        /// <summary>
        /// Start having this sendable object automatically respond to
        /// value changes reflect the value on the table.
        /// </summary>
        public void StartLiveWindowMode()
        { 
        }

        /// <summary>
        /// Stop having this sendable object automatically respond to value changes.
        /// </summary>
        public void StopLiveWindowMode()
        {
        }
    }
}
