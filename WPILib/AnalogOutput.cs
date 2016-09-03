using System;
using HAL.Base;
using NetworkTables.Tables;
using WPILib.Exceptions;
using WPILib.LiveWindow;
using static WPILib.Utility;
using static HAL.Base.HALAnalogOutput;
using static HAL.Base.HAL;
using static HAL.Base.HALPorts;

namespace WPILib
{
    /// <summary>
    /// Analog Output class
    /// </summary>
    public class AnalogOutput : SensorBase, ILiveWindowSendable
    {
        private int m_halHandle;

        /// <summary>
        /// Construct an analog output on a specified MXP channel.
        /// </summary>
        /// <param name="channel">The channel number to represent. [0..1] on MXP.</param>
        public AnalogOutput(int channel)
        {

            CheckAnalogOutputChannel(channel);

            int status = 0;
            m_halHandle = HAL_InitializeAnalogOutputPort(HAL_GetPort(channel), ref status);
            if (status != 0)
            {
                CheckStatusRange(status, 0, HAL_GetNumAnalogOutputs(), channel);
                m_halHandle = HALInvalidHandle;
                return;
            }
            LiveWindow.LiveWindow.AddSensor("AnalogOutput", channel, this);
            Report(ResourceType.kResourceType_AnalogOutput, (byte) channel, 1);
        }

        /// <summary>
        /// Channel Destructor.
        /// </summary>
        public override void Dispose()
        {
            HAL_FreeAnalogOutputPort(m_halHandle);
            m_halHandle = HALInvalidHandle;
        }

        /// <summary>
        /// Set the voltage being output.
        /// </summary>
        /// <param name="voltage">The voltage to output</param>
        public virtual void SetVoltage(double voltage)
        {
            int status = 0;
            HAL_SetAnalogOutput(m_halHandle, voltage, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Get the voltage being output
        /// </summary>
        /// <returns>The voltage being output</returns>
        public virtual double GetVoltage()
        {
            int status = 0;

            double voltage = HAL_GetAnalogOutput(m_halHandle, ref status);
            CheckStatus(status);
            return voltage;
        }

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
        public string SmartDashboardType => "Analog Output";

        /// <summary>
        /// Update the table for this sendable object with the latest
        /// values.
        /// </summary>
        public void UpdateTable()
        {
            Table?.PutNumber("Value", GetVoltage());
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
