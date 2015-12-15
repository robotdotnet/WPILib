using System;
using NetworkTables.Tables;
using WPILib.Interfaces;
using WPILib.LiveWindow;

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
        private PIDSourceType m_pidSource = PIDSourceType.Displacement;

        private void InitPot(AnalogInput input, double fullRange, double offset)
        {
            m_fullRange = fullRange;
            m_offset = offset;
            m_analogInput = input;
        }

        /// <summary>
        /// Creates a new Analog Potentiometer on the specified channel. [0..3] on RIO, [4..7] on MXP.
        /// </summary>
        /// <remarks>
        /// Use the fullRange and offset values so that the output produces meaningful
        /// values. I.E: you have a 270 degree potentiometer and you want the output to
        /// be degrees with the halfway point at 0 degrees. The fullRange value is 270.0(degrees)
        /// and the offset is -135.0 since the halfway point after scaling is 135 degrees.
        /// <para/>
        /// This will calculate the result from the fullRange times the fraction of the
        /// supply voltage, plus the offset.
        /// </remarks>
        /// <param name="channel">The channel this potentiometer is plugged into.</param>
        /// <param name="fullRange">The scaling to multiply the fraction by to get a 
        /// meaningful unit.</param>
        /// <param name="offset">The offset to add to the scaled value for controlling the
        /// zero value.</param>
        public AnalogPotentiometer(int channel, double fullRange = 1, double offset = 0)
        {
            AnalogInput input = new AnalogInput(channel);
            m_initAnalogInput = true;
            InitPot(input, fullRange, offset);
        }

        /// <summary>
        /// Creates a new Analog Potentiometer with the precreated input.
        /// </summary>
        /// <remarks>
        /// Use the fullRange and offset values so that the output produces meaningful
        /// values. I.E: you have a 270 degree potentiometer and you want the output to
        /// be degrees with the halfway point at 0 degrees. The fullRange value is 270.0(degrees)
        /// and the offset is -135.0 since the halfway point after scaling is 135 degrees.
        /// <para/>
        /// This will calculate the result from the fullRange times the fraction of the
        /// supply voltage, plus the offset.
        /// </remarks>
        /// <param name="input">The <see cref="AnalogInput"/> this potentiometer is plugged into.</param>
        /// <param name="fullRange">The scaling to multiply the fraction by to get a 
        /// meaningful unit.</param>
        /// <param name="offset">The offset to add to the scaled value for controlling the
        /// zero value.</param>
        public AnalogPotentiometer(AnalogInput input, double fullRange = 1, double offset = 0)
        {
            m_initAnalogInput = false;
            InitPot(input, fullRange, offset);
        }

        /// <summary>
        /// Get the current reading of the potentiometer
        /// </summary>
        /// <returns>The current position of the potentiometer</returns>
        public virtual double Get()
        {
            return (m_analogInput.GetVoltage() / ControllerPower.GetVoltage5V()) * m_fullRange + m_offset;
        }

        /// <summary>
        /// Get the result to use in PIDController
        /// </summary>
        /// <returns>The result to use in PIDController</returns>
        public virtual double PidGet() => Get();

        ///<inheritdoc/>
        public PIDSourceType PIDSourceType {
            get
            {
                return m_pidSource;
            }
            set
            {
                if (value != PIDSourceType.Displacement)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Only displacement PID is allowed for potentiometers");
                }
                m_pidSource = value;
            }
        }

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
