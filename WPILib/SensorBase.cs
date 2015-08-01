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
        /// The number of clock ticks per microsecond from the FPGA
        public const int SystemClockTicksPerMicrosecond = 40;
        /// The number of Digital Channels found on the RoboRIO
        public const int DigitalChannels = 26;
        /// The number of Analog Inputs found on the RoboRIO
        public const int AnalogInputChannels = 8;
        /// The number of Analog Outputs found on the RoboRIO
        public const int AnalogOutputChannels = 2;
        /// The number of Solenoid Channels found on each PCM
        public const int SolenoidChannels = 8;
        /// The number of PCM Modules allowed on the CAN Bus
        public const int SolenoidModules = 63;
        /// Then number of PWM Channels found the RoboRIO
        public const int PwmChannels = 20;
        /// The number of Relay Channels found on the RoboRIO
        public const int RelayChannels = 4;
        /// The number of Channels found on each PDP
        public const int PDPChannels = 16;
        /// The number of PDP Modules allowed on the CAN Bus
        public const int PDPModules = 63;

        private static int s_defaultSolenoidModule = 0;

        /// <summary>
        /// Verify that the solenoid module is correct
        /// </summary>
        /// <param name="moduleNumber">The module number to check.</param>
        protected static void CheckSolenoidModule(int moduleNumber)
        {
            if (moduleNumber < 0 || moduleNumber >= SolenoidModules)
            {
                throw new ArgumentOutOfRangeException(nameof(moduleNumber), "Requested Solenoid Module is out of range");
            }
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
        protected static void CheckPDPChannel(int channel)
        {
            if (channel < 0 || channel >= PDPChannels)
            {
                throw new IndexOutOfRangeException("Requested PDP channel number is out of range.");
            }
        }

        /// <summary>
        /// Check that the PDP CAN ID is in valid.
        /// </summary>
        /// <remarks>The default is 0, and this should be used by most teams.</remarks>
        /// <param name="module">The CAN ID to check</param>
        protected static void CheckPDPModule(int module)
        {
            if (module < 0 || module > PDPModules)
            {
                throw new IndexOutOfRangeException("Requested PDP module number is out of range.");
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
