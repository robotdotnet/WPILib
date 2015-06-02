using WPILib.Interfaces;

namespace WPILib
{
    public class SafePWM : PWM, MotorSafety
    {
        private MotorSafetyHelper m_safetyHelper;

        private void InitSafePWM()
        {
            m_safetyHelper = new MotorSafetyHelper(this);
            m_safetyHelper.SetExpiration(0.0);
            m_safetyHelper.SetSafetyEnabled(false);
        }

        public SafePWM(int channel)
            : base(channel)
        {
            InitSafePWM();
        }

        public void SetExpiration(double timeout)
        {
            m_safetyHelper.SetExpiration(timeout);
        }

        public double GetExpiration()
        {
            return m_safetyHelper.GetExpiration();
        }

        public bool IsAlive()
        {
            return m_safetyHelper.IsAlive();
        }

        public void StopMotor()
        {
            Disable();
        }

        public void SetSafetyEnabled(bool enabled)
        {
            m_safetyHelper.SetSafetyEnabled(enabled);
        }

        public bool IsSafetyEnabled()
        {
            return m_safetyHelper.IsSafetyEnabled();
        }

        public string GetDescription()
        {
            return "PWM " + GetChannel();
        }

        public void Feed()
        {
            m_safetyHelper.Feed();
        }

        public void Disable()
        {
            SetRaw(PwmDisabled);
        }
    }
}
