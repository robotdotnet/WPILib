using System;
using HAL;
using HAL.Base;
using NetworkTables.Tables;
using WPILib.Exceptions;
using WPILib.LiveWindow;
using static WPILib.Utility;
using HALAnalog = HAL.Base.HALAnalog;

namespace WPILib
{
    /// <summary>
    /// Analog Output class
    /// </summary>
    public class AnalogOutput : SensorBase, ILiveWindowSendable
    {
        private static readonly Resource s_channels = new Resource(AnalogOutputChannels);
        private IntPtr m_port;
        private int m_channel;

        /// <summary>
        /// Construct an analog output on a specified MXP channel.
        /// </summary>
        /// <param name="channel">The channel number to represent. [0..1] on MXP.</param>
        public AnalogOutput(int channel)
        {
            m_channel = channel;

            if (!HALAnalog.CheckAnalogOutputChannel((uint)channel))
            {
                throw new AllocationException("Analog output channel " + m_channel
                        + " cannot be allocated. Channel is not present.");
            }

            s_channels.Allocate(channel, "Analog output channel " + m_channel +" is already allocated");

            IntPtr portPointer = HAL.Base.HAL.GetPort((byte) channel);

            int status = 0;
            m_port = HALAnalog.InitializeAnalogOutputPort(portPointer, ref status);
            CheckStatus(status);
            LiveWindow.LiveWindow.AddSensor("AnalogOutput", channel, this);
            HAL.Base.HAL.Report(ResourceType.kResourceType_AnalogOutput, (byte) channel, 1);
        }

        /// <summary>
        /// Channel Destructor.
        /// </summary>
        public override void Dispose()
        {
            HALAnalog.FreeAnalogOutputPort(m_port);
            m_port = IntPtr.Zero;
            s_channels.Deallocate(m_channel);
            m_channel = 0;
        }

        /// <summary>
        /// Set the voltage being output.
        /// </summary>
        /// <param name="voltage">The voltage to output</param>
        public virtual void SetVoltage(double voltage)
        {
            int status = 0;
            HALAnalog.SetAnalogOutput(m_port, voltage, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Get the voltage being output
        /// </summary>
        /// <returns>The voltage being output</returns>
        public virtual double GetVoltage()
        {
            int status = 0;

            double voltage = HALAnalog.GetAnalogOutput(m_port, ref status);
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
