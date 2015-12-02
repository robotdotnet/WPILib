using System;
using HAL;
using HAL.Base;
using NetworkTables;
using NetworkTables.Tables;
using WPILib.LiveWindows;
using static HAL.Base.HALDigital;
using static WPILib.Utility;
using HALDigital = HAL.Base.HALDigital;

namespace WPILib
{
    /// <summary>
    /// Class to write digital outputs.
    /// </summary>
    public class DigitalOutput : DigitalSource, ILiveWindowSendable, ITableListener
    {
        private IntPtr m_pwmGenerator = IntPtr.Zero;

        /// <summary>
        /// Create an instance of a digital output.
        /// </summary>
        /// <param name="channel">The DIO channel to use for the digital output. 0-9 are on-board, 10-25 are on the MXP</param>
        public DigitalOutput(int channel)
        {
            InitDigitalPort(channel, false);

            HAL.Base.HAL.Report(ResourceType.kResourceType_DigitalOutput, (byte)channel);
        }

        /// <summary>
        /// Free the resources associated with a digital output
        /// </summary>
        public override void Dispose()
        {
            if (m_pwmGenerator != IntPtr.Zero)
            {
                DisablePWM();
            }

            base.Dispose();
        }

        /// <summary>
        /// Set the value of the digital output.
        /// </summary>
        /// <param name="value">True if on, false if off.</param>
        public virtual void Set(bool value)
        {
            int status = 0;
            SetDIO(m_port, (short)(value ? 1 : 0), ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Get the GPIO channel number that this object represents.
        /// </summary>
        public int Channel => m_channel;

        /// <summary>
        /// Write a pulse to the digital output.
        /// There can only be a single pulse going at any time.
        /// </summary>
        /// <param name="pulseLength">The length of the pulse</param>
        public void Pulse(float pulseLength)
        {
            int status = 0;
            HALDigital.Pulse(m_port, pulseLength, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Write a pulse to the digital output.
        /// There can only be a single pulse going at any time.
        /// </summary>
        /// <param name="pulseLength">The length of the pulse</param>
        [Obsolete("Use the float version for better portability")]
        public void Pulse(int pulseLength)
        {
            int status = 0;
            float convertedPulse = (float)(pulseLength / 1.0e9 * (GetLoopTiming(ref status) * 25));
            HALDigital.Pulse(m_port, convertedPulse, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Returns if the pulse is still going.
        /// </summary>
        public bool Pulsing
        {
            get
            {
                int status = 0;
                bool value = IsPulsing(m_port, ref status);
                CheckStatus(status);
                return value;
            }
        }

        /// <summary>
        /// Set the PWM frequency of the PWM output. The valid range is from 0.6Hz to 19kHz.
        /// <para/>There is only 1 frequency for all channels.
        /// </summary>
        public double PWMRate
        {
            set
            {
                int status = 0;
                SetPWMRate(value, ref status);
                CheckStatus(status);
            }
        }

        /// <summary>
        /// Enable a PWM output on the line.
        /// </summary>
        /// <remarks>Allocate one of the 6 DO PWM generator resources.
        /// <para/> Supply the initial duty-cycle to output so as to avoid a glitch when
        /// first starting.
        /// <para/> The resolution of the duty cycle is 8-bit for low frequencies(1kHz or
        /// less) but is reduced the higher the frequency of the PWM signal is.</remarks>
        /// <param name="initialDutyCycle">The duty cycle to start generating. [0..1]</param>
        public void EnablePWM(double initialDutyCycle)
        {
            if (m_pwmGenerator != IntPtr.Zero)
                return;
            int status = 0;
            m_pwmGenerator = AllocatePWM(ref status);
            CheckStatus(status);
            SetPWMDutyCycle(m_pwmGenerator, initialDutyCycle, ref status);
            CheckStatus(status);
            SetPWMOutputChannel(m_pwmGenerator, (uint)m_channel, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Change this line from a PWM output back to a static Digital Output line.
        /// </summary>
        public void DisablePWM()
        {
            if (m_pwmGenerator == IntPtr.Zero)
                return;
            int status = 0;
            SetPWMOutputChannel(m_pwmGenerator, (uint)DigitalChannels, ref status);
            CheckStatus(status);
            FreePWM(m_pwmGenerator, ref status);
            CheckStatus(status);
            m_pwmGenerator = IntPtr.Zero;
        }

        /// <summary>
        /// Change the duty cycle that is being generated on the line.
        /// The resolution of the duty cycle is 8-bit for low frequencies,
        /// but is reduced the higher the frequency of the PWM signal.
        /// </summary>
        /// <param name="value">The duty-cycle to change to. [0..1]</param>
        public void UpdateDutyCycle(double value)
        {
            if (m_pwmGenerator == IntPtr.Zero)
                return;
            int status = 0;
            SetPWMDutyCycle(m_pwmGenerator, value, ref status);
            CheckStatus(status);
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
        public string SmartDashboardType => "Digital Output";

        /// <summary>
        /// Update the table for this sendable object with the latest
        /// values.
        /// </summary>
        public void UpdateTable()
        {
            //TODO: Put value
        }

        /// <summary>
        /// Start having this sendable object automatically respond to
        /// value changes reflect the value on the table.
        /// </summary>
        public void StartLiveWindowMode() => Table.AddTableListener("Value", this, true);

        /// <summary>
        /// Stop having this sendable object automatically respond to value changes.
        /// </summary>
        public void StopLiveWindowMode() => Table.RemoveTableListener(this);

        /// <inheritdoc/>
        public void ValueChanged(ITable source, string key, object value, NotifyFlags flags)
        {
            Set((bool)value);
        }
    }
}
