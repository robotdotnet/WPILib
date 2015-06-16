using System;
using NetworkTablesDotNet.Tables;
using WPILib.Interfaces;
using WPILib.LiveWindows;

namespace WPILib
{
    /// <summary>
    /// Class for reading analog potentiometers
    /// </summary>
    /// <remarks>Analog potentiometers read in an analog voltage that corresponds 
    /// <para/> to a position.The position is
    /// <para/> in whichever units you choose, by way of the scaling and offset
    /// <para/> constants passed to the constructor. </remarks>
    public class AnalogPotentiometer : IPotentiometer, IDisposable, ILiveWindowSendable
    {
        private double m_fullRange, m_offset;
        private AnalogInput m_analogInput;
        private bool m_initAnalogInput;

        private void InitPot(AnalogInput input, double fullRange, double offset)
        {
            m_fullRange = fullRange;
            m_offset = offset;
            m_analogInput = input;
        }

        public AnalogPotentiometer(int channel, double fullRange, double offset)
        {
            AnalogInput input = new AnalogInput(channel);
            m_initAnalogInput = true;
            InitPot(input, fullRange, offset);
        }

        public AnalogPotentiometer(AnalogInput input, double fullRange, double offset)
        {
            m_initAnalogInput = false;
            InitPot(input, fullRange, offset);
        }

        public AnalogPotentiometer(int channel, double scale)
            : this(channel, scale, 0)
        {
        }

        public AnalogPotentiometer(AnalogInput input, double scale)
            : this(input, scale, 0)
        {
        }

        /// <summary>
        /// AnalogPotentiometer constructor
        /// </summary>
        /// <param name="channel">The analog channel this potentiometer is plugged into.</param>
        public AnalogPotentiometer(int channel)
            : this(channel, 1, 0)
        {
        }

        /// <summary>
        /// AnalogPotentiometer constructor
        /// </summary>
        /// <param name="input">The <see cref="AnalogInput"/> this potentiometer is plugged into.</param>
        public AnalogPotentiometer(AnalogInput input)
            : this(input, 1, 0)
        {
        }

        /// <summary>
        /// Get the current reading of the potentiometer
        /// </summary>
        /// <returns>The current position of the potentiometer</returns>
        public double Get()
        {
            return (m_analogInput.GetVoltage() / ControllerPower.Voltage5V) * m_fullRange + m_offset;
        }

        /// <summary>
        /// Get the result to use in PIDController
        /// </summary>
        /// <returns>The result to use in PIDController</returns>
        public double PidGet() => Get();

        /// <summary>
        /// Frees the potentiometer.
        /// </summary>
        public void Dispose()
        {
            if (m_initAnalogInput)
            {
                m_analogInput.Dispose();
                m_analogInput = null;
                m_initAnalogInput = false;
            }
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
        public string SmartDashboardType => "Analog Input";

        /// <summary>
        /// Update the table for this sendable object with the latest
        /// values.
        /// </summary>
        public void UpdateTable()
        {
            Table?.PutNumber("Value", Get());
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
