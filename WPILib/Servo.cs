using HAL_Base;

namespace WPILib
{
    public class Servo : PWM
    {
        private static double s_MaxServoAngle = 180.0;
        private static double s_MinServoAngle = 0.0;

        protected static double s_defaultMaxServoPWM = 2.4;
        protected static double s_defaultMinServoPWM = 0.6;

        private void InitServo()
        {
            SetBounds(s_defaultMaxServoPWM, 0, 0, 0, s_defaultMinServoPWM);
            SetPeriodMultiplier(PeriodMultiplier.K4X);

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
            if (degrees < s_MinServoAngle)
                degrees = s_MinServoAngle;
            else if (degrees > s_MaxServoAngle)
                degrees = s_MaxServoAngle;
            SetPosition(((degrees - s_MinServoAngle)) / GetServoAngleRange());
        }

        public double GetAngle()
        {
            return GetPosition() * GetServoAngleRange() + s_MinServoAngle;
        }

        private double GetServoAngleRange()
        {
            return s_MaxServoAngle - s_MinServoAngle;
        }
    }
}
