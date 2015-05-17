

using System;
using System.IO;
using WPILib.Interfaces;

namespace WPILib
{
    public class MotorSafetyHelper
    {
        public const double DefaultSafetyExpiration = 0.1;

        private double _expiration;
        private bool _enabled;
        private double _stopTime;
        private MotorSafety _safeObject;
        private MotorSafetyHelper _nextHelper;
        private static MotorSafetyHelper s_headHelper = null;
        private static DriverStation s_ds;

        public MotorSafetyHelper(MotorSafety safeObject)
        {
            _safeObject = safeObject;
            s_ds = DriverStation.GetInstance();
            _enabled = false;
            _expiration = DefaultSafetyExpiration;
            _stopTime = Timer.GetFPGATimestamp();
            _nextHelper = s_headHelper;
            s_headHelper = this;
        }

        public void Feed()
        {
            _stopTime = Timer.GetFPGATimestamp() + _expiration;
        }

        public void SetExpiration(double expirationTime)
        {
            _expiration = expirationTime;
        }

        public double GetExpiration()
        {
            return _expiration;
        }

        public bool IsAlive()
        {
            return !_enabled || _stopTime > Timer.GetFPGATimestamp();
        }

        public void Check()
        {
            if (!_enabled || s_ds.IsDisabled() || s_ds.IsTest())
                return;
            if (_stopTime < Timer.GetFPGATimestamp())
            {
                TextWriter errorWriter = Console.Error;
                errorWriter.WriteLine(_safeObject.GetDescription() + "... Output not updated often enough.");
                errorWriter.Close();

                _safeObject.StopMotor();
            }
        }

        public void SetSafetyEnabled(bool enabled)
        {
            _enabled = enabled;
        }

        public bool IsSafetyEnabled()
        {
            return _enabled;
        }

        public static void CheckMotors()
        {
            for (MotorSafetyHelper msh = s_headHelper; msh != null; msh = msh._nextHelper)
            {
                msh.Check();
            }
        }
    }
}
