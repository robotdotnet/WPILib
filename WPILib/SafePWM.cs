using WPILib.Interfaces;

namespace WPILib
{
    public class SafePWM : PWM, IMotorSafety
    {
        private MotorSafetyHelper m_safetyHelper;

        private void InitSafePWM()
        {
            m_safetyHelper = new MotorSafetyHelper(this);
            m_safetyHelper.Expiration = 0.0;
            m_safetyHelper.SafetyEnabled = false;
        }

        public SafePWM(int channel)
            : base(channel)
        {
            InitSafePWM();
        }

        public double Expiration
        {
            set { m_safetyHelper.Expiration = value; }
            get { return m_safetyHelper.Expiration; }
        }

        public bool Alive => m_safetyHelper.Alive;

        public void StopMotor() => Disable();

        public bool SafetyEnabled
        {
            set { m_safetyHelper.SafetyEnabled = value; }
            get { return m_safetyHelper.SafetyEnabled; }
        }

        public string Description => $"PWM {Channel}";

        public void Feed() => m_safetyHelper.Feed();

        public void Disable() => Raw = PwmDisabled;
    }
}
