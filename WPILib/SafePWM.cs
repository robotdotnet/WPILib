namespace WPILib
{
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

        public string Description => $"PWM {Channel}";

        public void Feed() => m_safetyHelper.Feed();

        public void Disable() => SetRaw(PwmDisabled);
    }
}
