using System;
using System.Collections.Generic;
using System.Text;
using WPILib.Util;
using WPILib.Interfaces;

namespace WPILib
{
    public class SafePWM : PWM, MotorSafety
    {
        private MotorSafetyHelper _safetyHelper;

        void InitSafePWM()
        {
            _safetyHelper = new MotorSafetyHelper(this);
            _safetyHelper.SetExpiration(0.0);
            _safetyHelper.SetSafetyEnabled(false);
        }

        public SafePWM(int channel)
            : base(channel)
        {
            InitSafePWM();
        }

        public void SetExpiration(double timeout)
        {
            _safetyHelper.SetExpiration(timeout);
        }

        public double GetExpiration()
        {
            return _safetyHelper.GetExpiration();
        }

        public bool IsAlive()
        {
            return _safetyHelper.IsAlive();
        }

        public void StopMotor()
        {
            Disable();
        }

        public void SetSafetyEnabled(bool enabled)
        {
            _safetyHelper.SetSafetyEnabled(enabled);
        }

        public bool IsSafetyEnabled()
        {
            return _safetyHelper.IsSafetyEnabled();
        }

        public string GetDescription()
        {
            return "PWM " + GetChannel();
        }

        public void Feed()
        {
            _safetyHelper.Feed();
        }

        public void Disable()
        {
            SetRaw(PwmDisabled);
        }
    }
}
