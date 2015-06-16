using HAL_Base;
using WPILib.LiveWindows;
using static HAL_Base.HAL;

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
            PeriodMultiplier = PeriodMultiplier.K4X;

            Report(ResourceType.kResourceType_Servo, (byte)Channel);
            LiveWindow.AddActuator("Servo", Channel, this);
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

        public double Angle
        {
            get { return GetPosition()*ServoAngleRange + s_MinServoAngle; }
            set
            {
                if (value < s_MinServoAngle)
                    value = s_MinServoAngle;
                else if (value > s_MaxServoAngle)
                    value = s_MaxServoAngle;
                SetPosition(((value - s_MinServoAngle))/ServoAngleRange);
            }
        }

        private double ServoAngleRange => s_MaxServoAngle - s_MinServoAngle;

        public new string SmartDashboardType => "Servo";
    }
}
