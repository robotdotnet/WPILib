using System;

namespace WPILib
{
    /// <summary>
    /// This class is the base for all sensors.
    /// </summary>
    /// <remarks>This scores status information for sensors, as well as containing
    /// utility functions for checking channels and error processing.</remarks>
    public abstract class SensorBase : IDisposable
    {
        public const int SystemClockTicksPerMicrosecond = 40;

        public const int DigitalChannels = 26;

        public const int AnalogInputChannels = 8;

        public const int AnalogOutputChannels = 2;

        public const int SolenoidChannels = 2;

        public const int PwmChannels = 20;

        public const int RelayChannels = 4;

        public const int PDPChannels = 16;

        private static int s_defaultSolenoidModule = 0;

        /// <summary>
        /// Verify that the solenoid module is correct
        /// </summary>
        /// <param name="moduleNumber">The module number to check.</param>
        protected static void CheckSolenoidModule(int moduleNumber)
        {
        }

        /// <summary>
        /// Check that the digital channel number is valid.
        /// </summary>
        /// <remarks>Channel numbers are 0 based on the RoboRIO.</remarks>
        /// <param name="channel">The channel number to check.</param>
        protected static void CheckDigitalChannel(int channel)
        {
            if (channel < 0 || channel >= DigitalChannels)
            {
                throw new IndexOutOfRangeException("Requested digital channel number is out of range.");
            }
        }

        /// <summary>
        /// Check that the relay channel number is valid.
        /// </summary>
        /// <remarks>Channel numbers are 0 based on the RoboRIO.</remarks>
        /// <param name="channel">The channel number to check.</param>
        protected static void CheckRelayChannel(int channel)
        {
            if (channel < 0 || channel >= RelayChannels)
            {
                throw new IndexOutOfRangeException("Requested relay channel number is out of range.");
            }
        }

        /// <summary>
        /// Check that the PWM channel number is valid.
        /// </summary>
        /// <remarks>Channel numbers are 0 based on the RoboRIO.</remarks>
        /// <param name="channel">The channel number to check.</param>
        protected static void CheckPwmChannel(int channel)
        {
            if (channel < 0 || channel >= PwmChannels)
            {
                throw new IndexOutOfRangeException("Requested PWM channel number is out of range.");
            }
        }

        /// <summary>
        /// Check that the analog channel number is valid.
        /// </summary>
        /// <remarks>Channel numbers are 0 based on the RoboRIO.</remarks>
        /// <param name="channel">The channel number to check.</param>
        protected static void CheckAnalogInputChannel(int channel)
        {
            if (channel < 0 || channel >= AnalogInputChannels)
            {
                throw new IndexOutOfRangeException("Requested analog input channel number is out of range.");
            }
        }

        /// <summary>
        /// Check that the analog output channel number is valid.
        /// </summary>
        /// <remarks>Channel numbers are 0 based on the RoboRIO.</remarks>
        /// <param name="channel">The channel number to check.</param>
        protected static void CheckAnalogOutputChannel(int channel)
        {
            if (channel < 0 || channel >= AnalogOutputChannels)
            {
                throw new IndexOutOfRangeException("Requested analog output channel number is out of range.");
            }
        }

        /// <summary>
        /// Check that the solenoid channel number is valid.
        /// </summary>
        /// <remarks>Channel numbers are 0 based on the PCM.</remarks>
        /// <param name="channel">The channel number to check.</param>
        protected static void CheckSolenoidChannel(int channel)
        {
            if (channel < 0 || channel >= SolenoidChannels)
            {
                throw new IndexOutOfRangeException("Requested solenoid channel number is out of range.");
            }
        }

        /// <summary>
        /// Check that the power distribution channel number is valid.
        /// </summary>
        /// <remarks>Channel numbers are 0 based on the PDP.</remarks>
        /// <param name="channel">The channel number to check.</param>
        protected static void CheckPdpChannel(int channel)
        {
            if (channel < 0 || channel >= PDPChannels)
            {
                throw new IndexOutOfRangeException("Requested PDP channel number is out of range.");
            }
        }

        /// <summary>
        /// Gets or Sets the default solenoid module number;
        /// </summary>
        public static int DefaultSolenoidModule
        {
            get { return s_defaultSolenoidModule; }
            set
            {
                CheckSolenoidModule(value);
                s_defaultSolenoidModule = value;
            }
        }

        ///<inheritdoc/>
        public virtual void Dispose()
        {
        }
    }
}
