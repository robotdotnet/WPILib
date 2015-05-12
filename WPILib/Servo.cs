using System;
using System.Collections.Generic;
using System.Text;
using HAL_RoboRIO;

namespace WPILib
{
    public class Servo : PWM
    {

        private static double kMaxServoAngle = 180.0;
        private static double kMinServoAngle = 0.0;

        protected static double kDefaultMaxServoPWM = 2.4;
        protected static double kDefaultMinServoPWM = 0.6;

        private void InitServo()
        {
            SetBounds(kDefaultMaxServoPWM, 0, 0, 0, kDefaultMinServoPWM);
            SetPeriodMultiplier(PeriodMultiplier.k4x_val);

            HAL.Report(ResourceType.kResourceType_Servo, (byte)GetChannel());

        }

        public Servo (int channel) : base(channel)
        {
            InitServo();
        }

        public void Set (double value)
        {
            SetPosition(value);
        }

        public double Get()
        {
            return GetPosition();
        }

        public void SetAngle(double degrees)
        {
            if (degrees < kMinServoAngle)
                degrees = kMinServoAngle;
            else if (degrees > kMaxServoAngle)
                degrees = kMaxServoAngle;
            SetPosition(((degrees - kMinServoAngle)) / GetServoAngleRange());
        }

        public double GetAngle()
        {
            return GetPosition() * GetServoAngleRange() + kMinServoAngle;
        }

        private double GetServoAngleRange()
        {
            return kMaxServoAngle - kMinServoAngle;
        }


    }
}
