

using System;
using System.Collections.Generic;
using System.Text;
using HAL_FRC;

namespace WPILib
{
    public class Servo : PWM
    {
        private static double s_kMaxServoAngle = 180.0;
        private static double s_kMinServoAngle = 0.0;

        protected static double kDefaultMaxServoPWM = 2.4;
        protected static double kDefaultMinServoPWM = 0.6;

        private void InitServo()
        {
            SetBounds(kDefaultMaxServoPWM, 0, 0, 0, kDefaultMinServoPWM);
            SetPeriodMultiplier(PeriodMultiplier.k4x_val);

            HAL.Report(ResourceType.kResourceType_Servo, (byte)GetChannel());
        }

        public Servo(int channel) : base(channel)
        {
            InitServo();
        }

        public void Set(double value)
        {
            SetPosition(value);
        }

        public double Get()
        {
            return GetPosition();
        }

        public void SetAngle(double degrees)
        {
            if (degrees < s_kMinServoAngle)
                degrees = s_kMinServoAngle;
            else if (degrees > s_kMaxServoAngle)
                degrees = s_kMaxServoAngle;
            SetPosition(((degrees - s_kMinServoAngle)) / GetServoAngleRange());
        }

        public double GetAngle()
        {
            return GetPosition() * GetServoAngleRange() + s_kMinServoAngle;
        }

        private double GetServoAngleRange()
        {
            return s_kMaxServoAngle - s_kMinServoAngle;
        }
    }
}
