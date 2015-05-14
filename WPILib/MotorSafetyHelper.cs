

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WPILib.Interfaces;

namespace WPILib
{
    public class MotorSafetyHelper
    {
        public const double DEFAULT_SAFETY_EXPIRATION = 0.1;

        private double _expiration;
        private bool _enabled;
        private double _stopTime;
        private MotorSafety _safeObject;
        private MotorSafetyHelper _nextHelper;
        private static MotorSafetyHelper s_headHelper = null;

        public MotorSafetyHelper(MotorSafety safeObject)
        {
            _safeObject = safeObject;
            _enabled = true;
            _expiration = DEFAULT_SAFETY_EXPIRATION;
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
            if (!_enabled)// || RobotState.isDisabled() || RobotState.isTest())
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
