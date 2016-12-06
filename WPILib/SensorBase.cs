using System;
using System.Text;
using HAL.Base;

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
        public static readonly int SystemClockTicksPerMicrosecond = HALConstants.HAL_GetSystemClockTicksPerMicrosecond();

        /// The number of Digital Channels found on the RoboRIO
        public static readonly int DigitalChannels = HALPorts.HAL_GetNumDigitalChannels();
        /// The number of Analog Inputs found on the RoboRIO
        public static readonly int AnalogInputChannels = HALPorts.HAL_GetNumAnalogInputs();
        /// The number of Analog Outputs found on the RoboRIO
        public static readonly int AnalogOutputChannels = HALPorts.HAL_GetNumAnalogOutputs();
        /// The number of Solenoid Channels found on each PCM
        public static readonly int SolenoidChannels = HALPorts.HAL_GetNumSolenoidChannels();
        /// The number of PCM Modules allowed on the CAN Bus
        public static readonly int PCMModules = HALPorts.HAL_GetNumPCMModules();
        /// Then number of PWM Channels found the RoboRIO
        public static readonly int PwmChannels = HALPorts.HAL_GetNumPWMChannels();
        /// The number of Relay Channels found on the RoboRIO
        public static readonly int RelayChannels = HALPorts.HAL_GetNumRelayHeaders();
        /// The number of Channels found on each PDP
        public static readonly int PDPChannels = HALPorts.HAL_GetNumPDPChannels();
        /// The number of PDP Modules allowed on the CAN Bus
        public static readonly int PDPModules = HALPorts.HAL_GetNumPDPModules();

        private static int s_defaultSolenoidModule = 0;

        /// <summary>
        /// Verify that the solenoid module is correct
        /// </summary>
        /// <param name="moduleNumber">The module number to check.</param>
        protected static void CheckSolenoidModule(int moduleNumber)
        {
            if (HALSolenoid.HAL_CheckSolenoidModule(moduleNumber))
            {
                StringBuilder buf = new StringBuilder();
                buf.Append("Requested solenoid module is out of range. Minimumm: 0, Maximum: ");
                buf.Append(PCMModules);
                buf.Append(", Requested: ");
                buf.Append(moduleNumber);
                throw new ArgumentOutOfRangeException(nameof(moduleNumber), buf.ToString());
            }
        }

        /// <summary>
        /// Check that the digital channel number is valid.
        /// </summary>
        /// <remarks>Channel numbers are 0 based on the RoboRIO.</remarks>
        /// <param name="channel">The channel number to check.</param>
        protected static void CheckDigitalChannel(int channel)
        {
            if (!HALDIO.HAL_CheckDIOChannel(channel))
            {
                StringBuilder buf = new StringBuilder();
                buf.Append("Requested DIO channel is out of range. Minimumm: 0, Maximum: ");
                buf.Append(DigitalChannels);
                buf.Append(", Requested: ");
                buf.Append(channel);
                throw new ArgumentOutOfRangeException(nameof(channel), "Requested digital channel number is out of range.");
            }
        }

        /// <summary>
        /// Check that the relay channel number is valid.
        /// </summary>
        /// <remarks>Channel numbers are 0 based on the RoboRIO.</remarks>
        /// <param name="channel">The channel number to check.</param>
        protected static void CheckRelayChannel(int channel)
        {
            if (!HALRelay.HAL_CheckRelayChannel(channel))
            {
                StringBuilder buf = new StringBuilder();
                buf.Append("Requested relay channel is out of range. Minimumm: 0, Maximum: ");
                buf.Append(RelayChannels);
                buf.Append(", Requested: ");
                buf.Append(channel);
                throw new ArgumentOutOfRangeException(nameof(channel), "Requested relay channel number is out of range.");
            }
        }

        /// <summary>
        /// Check that the PWM channel number is valid.
        /// </summary>
        /// <remarks>Channel numbers are 0 based on the RoboRIO.</remarks>
        /// <param name="channel">The channel number to check.</param>
        protected static void CheckPwmChannel(int channel)
        {
            if (!HALPWM.HAL_CheckPWMChannel(channel))
            {
                StringBuilder buf = new StringBuilder();
                buf.Append("Requested PWM channel is out of range. Minimumm: 0, Maximum: ");
                buf.Append(PwmChannels);
                buf.Append(", Requested: ");
                buf.Append(channel);
                throw new ArgumentOutOfRangeException(nameof(channel), "Requested PWM channel number is out of range.");
            }
        }

        /// <summary>
        /// Check that the analog channel number is valid.
        /// </summary>
        /// <remarks>Channel numbers are 0 based on the RoboRIO.</remarks>
        /// <param name="channel">The channel number to check.</param>
        protected static void CheckAnalogInputChannel(int channel)
        {
            if (!HALAnalogInput.HAL_CheckAnalogInputChannel(channel))
            {
                StringBuilder buf = new StringBuilder();
                buf.Append("Requested analog input channel is out of range. Minimumm: 0, Maximum: ");
                buf.Append(AnalogInputChannels);
                buf.Append(", Requested: ");
                buf.Append(channel);
                throw new ArgumentOutOfRangeException(nameof(channel), "Requested analog input channel number is out of range.");
            }
        }

        /// <summary>
        /// Check that the analog output channel number is valid.
        /// </summary>
        /// <remarks>Channel numbers are 0 based on the RoboRIO.</remarks>
        /// <param name="channel">The channel number to check.</param>
        protected static void CheckAnalogOutputChannel(int channel)
        {
            if (!HALAnalogOutput.HAL_CheckAnalogOutputChannel(channel))
            {
                StringBuilder buf = new StringBuilder();
                buf.Append("Requested analog output channel is out of range. Minimumm: 0, Maximum: ");
                buf.Append(AnalogOutputChannels);
                buf.Append(", Requested: ");
                buf.Append(channel);
                throw new ArgumentOutOfRangeException(nameof(channel), "Requested analog output channel number is out of range.");
            }
        }

        /// <summary>
        /// Check that the solenoid channel number is valid.
        /// </summary>
        /// <remarks>Channel numbers are 0 based on the PCM.</remarks>
        /// <param name="channel">The channel number to check.</param>
        protected static void CheckSolenoidChannel(int channel)
        {
            if (!HALSolenoid.HAL_CheckSolenoidChannel(channel))
            {
                StringBuilder buf = new StringBuilder();
                buf.Append("Requested solenoid channel is out of range. Minimumm: 0, Maximum: ");
                buf.Append(SolenoidChannels);
                buf.Append(", Requested: ");
                buf.Append(channel);
                throw new ArgumentOutOfRangeException(nameof(channel), "Requested solenoid channel number is out of range.");
            }
        }

        /// <summary>
        /// Check that the power distribution channel number is valid.
        /// </summary>
        /// <remarks>Channel numbers are 0 based on the PDP.</remarks>
        /// <param name="channel">The channel number to check.</param>
        protected static void CheckPDPChannel(int channel)
        {
            if (!HALPDP.HAL_CheckPDPChannel(channel))
            {
                StringBuilder buf = new StringBuilder();
                buf.Append("Requested PDP channel is out of range. Minimumm: 0, Maximum: ");
                buf.Append(PDPChannels);
                buf.Append(", Requested: ");
                buf.Append(channel);
                throw new ArgumentOutOfRangeException(nameof(channel), "Requested PDP channel number is out of range.");
            }
        }

        /// <summary>
        /// Check that the PDP CAN ID is in valid.
        /// </summary>
        /// <remarks>The default is 0, and this should be used by most teams.</remarks>
        /// <param name="module">The CAN ID to check</param>
        protected static void CheckPDPModule(int module)
        {
            if (!HALPDP.HAL_CheckPDPModule(module))
            {
                StringBuilder buf = new StringBuilder();
                buf.Append("Requested PDP module is out of range. Minimumm: 0, Maximum: ");
                buf.Append(PDPModules);
                buf.Append(", Requested: ");
                buf.Append(module);
                throw new ArgumentOutOfRangeException(nameof(module), "Requested PDP module number is out of range.");
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
