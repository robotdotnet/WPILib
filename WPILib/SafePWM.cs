using WPILib.Interfaces;

namespace WPILib
{
    /// <summary>
    /// This class is used to create PWM devices that implement <see cref="IMotorSafety"/>.
    /// This will cause a watchdog to be created for the object, and if it is
    /// not updated within a certain period, it will be stopped.
    /// </summary>
    public class SafePWM : PWM, IMotorSafety
    {
        private MotorSafetyHelper m_safetyHelper;

        private void InitSafePWM()
        {
            m_safetyHelper = new MotorSafetyHelper(this)
            {
                Expiration = 0.0,
                SafetyEnabled = false
            };
        }

        /// <summary>
        /// Creates a new SafePWM object.
        /// </summary>
        /// <param name="channel">The PWM Channel that the Object is attached to. 0-9 are on-board, 10-19 are on the MXP port</param>
        public SafePWM(int channel)
            : base(channel)
        {
            InitSafePWM();
        }
        ///<inheritdoc/>
        public double Expiration
        {
            set { m_safetyHelper.Expiration = value; }
            get { return m_safetyHelper.Expiration; }
        }
        ///<inheritdoc/>
        public bool Alive => m_safetyHelper.Alive;
        ///<inheritdoc/>
        public void StopMotor() => Disable();
        ///<inheritdoc/>
        public bool SafetyEnabled
        {
            set { m_safetyHelper.SafetyEnabled = value; }
            get { return m_safetyHelper.SafetyEnabled; }
        }

        ///<inheritdoc/>
        public string Description => $"PWM {Channel}";

        /// <summary>
        /// Feeds the Motor Safety Timer
        /// </summary>
        /// <remarks>This method should be called by the derived class whenever its
        /// setpoint gets updated, in order to feed the watchdog.</remarks>
        public void Feed() => m_safetyHelper.Feed();

        /// <summary>
        /// Disables the PWM output.
        /// </summary>
        public void Disable() => SetRaw(PwmDisabled);
    }
}
