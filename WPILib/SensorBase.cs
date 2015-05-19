

using System;

namespace WPILib
{
    public abstract class SensorBase
    {
        public static readonly int SystemClockTicksPerMicrosecond = 40;

        public static readonly int DigitalChannels = 26;

        public static readonly int AnalogInputChannels = 8;

        public static readonly int AnalogOutputChannels = 2;

        public static readonly int SolenoidChannels = 2;

        public static readonly int PwmChannels = 20;

        public static readonly int RelayChannels = 4;

        public static readonly int PDPChannels = 16;

        private static int s_defaultSolenoidModule = 0;

        public static void SetDefaultSolenoidModule(int moduleNumber)
        {
            CheckSolenoidModule(moduleNumber);
            s_defaultSolenoidModule = moduleNumber;
        }

        protected static void CheckSolenoidModule(int moduleNumber)
        {
        }

        /**
     * Check that the digital channel number is valid.
     * Verify that the channel number is one of the legal channel numbers. Channel numbers are
     * 1-based.
     *
     * @param channel The channel number to check.
     */
        protected static void CheckDigitalChannel(int channel)
        {
            if (channel < 0 || channel >= DigitalChannels)
            {
                throw new IndexOutOfRangeException("Requested digital channel number is out of range.");
            }
        }

        /**
         * Check that the digital channel number is valid.
         * Verify that the channel number is one of the legal channel numbers. Channel numbers are
         * 1-based.
         *
         * @param channel The channel number to check.
         */
        protected static void CheckRelayChannel(int channel)
        {
            if (channel < 0 || channel >= RelayChannels)
            {
                throw new IndexOutOfRangeException("Requested relay channel number is out of range.");
            }
        }

        /**
         * Check that the digital channel number is valid.
         * Verify that the channel number is one of the legal channel numbers. Channel numbers are
         * 1-based.
         *
         * @param channel The channel number to check.
         */
        protected static void CheckPwmChannel(int channel)
        {
            if (channel < 0 || channel >= PwmChannels)
            {
                throw new IndexOutOfRangeException("Requested PWM channel number is out of range.");
            }
        }

        /**
         * Check that the analog input number is value.
         * Verify that the analog input number is one of the legal channel numbers. Channel numbers
         * are 0-based.
         *
         * @param channel The channel number to check.
         */
        protected static void CheckAnalogInputChannel(int channel)
        {
            if (channel < 0 || channel >= AnalogInputChannels)
            {
                throw new IndexOutOfRangeException("Requested analog input channel number is out of range.");
            }
        }

        /**
         * Check that the analog input number is value.
         * Verify that the analog input number is one of the legal channel numbers. Channel numbers
         * are 0-based.
         *
         * @param channel The channel number to check.
         */
        protected static void CheckAnalogOutputChannel(int channel)
        {
            if (channel < 0 || channel >= AnalogOutputChannels)
            {
                throw new IndexOutOfRangeException("Requested analog output channel number is out of range.");
            }
        }

        /**
         * Verify that the solenoid channel number is within limits.  Channel numbers
         * are 1-based.
         *
         * @param channel The channel number to check.
         */
        protected static void CheckSolenoidChannel(int channel)
        {
            if (channel < 0 || channel >= SolenoidChannels)
            {
                throw new IndexOutOfRangeException("Requested solenoid channel number is out of range.");
            }
        }

        /**
         * Verify that the power distribution channel number is within limits.
         * Channel numbers are 1-based.
         *
         * @param channel The channel number to check.
         */
        protected static void CheckPdpChannel(int channel)
        {
            if (channel < 0 || channel >= PDPChannels)
            {
                throw new IndexOutOfRangeException("Requested PDP channel number is out of range.");
            }
        }

        public static int GetDefaultSolenoidModule()
        {
            return s_defaultSolenoidModule;
        }

        public virtual void Free()
        {
        }
    }
}
