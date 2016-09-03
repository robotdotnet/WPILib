using HAL.Base;
using NetworkTables.Tables;
using WPILib.LiveWindow;
using static WPILib.Utility;
using static HAL.Base.HAL;
using static HAL.Base.HALPorts;
using static HAL.Base.HALDIO;

namespace WPILib
{
    /// <summary>
    /// Class to read a digital input
    /// </summary>
    public class DigitalInput : DigitalSource, ILiveWindowSendable
    {
        private int m_halHandle = HALInvalidHandle;

        /// <summary>
        /// Create an instance of a Digital Input
        /// </summary>
        /// <param name="channel">The DIO channel for the digital input 0-9 are on-board, 10-25 are on the MXP</param>
        public DigitalInput(int channel)
        {
            CheckDigitalChannel(channel);

            Channel = channel;

            int status = 0;
            m_halHandle = HAL_InitializeDIOPort(HAL_GetPort(channel), true, ref status);
            CheckStatusRange(status, 0, HAL_GetNumDigitalPins(), channel);

            Report(ResourceType.kResourceType_DigitalInput, (byte)channel);
            LiveWindow.LiveWindow.AddSensor("DigitalInput", channel, this);
        }

        /// <inheritdoc/>
        public override int PortHandleForRouting => m_halHandle;
        /// <inheritdoc/>
        public override AnalogTriggerType AnalogTriggerTypeForRouting => 0;

        /// <inheritdoc/>
        public override void Dispose()
        {
            base.Dispose();

            HAL_FreeDIOPort(m_halHandle);
            m_halHandle = 0;
        }

        /// <summary>
        /// Get the value from the Digital Input Channel
        /// </summary>
        /// <returns>The status of the digital input</returns>
        public virtual bool Get()
        {
            int status = 0;
            bool value = HAL_GetDIO(m_halHandle, ref status);
            CheckStatus(status);
            return value;
        }

        /// <inheritdoc/>
        public override bool AnalogTrigger => false;

        /// <summary>
        /// Get the channel of the digital input.
        /// </summary>
        public override int Channel { get; } = 0;

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
