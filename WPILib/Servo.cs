using HAL.Base;
using NetworkTables;
using NetworkTables.Tables;
using static HAL.Base.HAL;

namespace WPILib
{
    /// <summary>
    /// This class is used for interfacing with 
    /// </summary>
    public class Servo : PWM
    {
        private const double MaxServoAngle = 180.0;
        private const double MinServoAngle = 0.0;

        /// <summary>
        /// The default max value for the servo.
        /// </summary>
        protected const double DefaultMaxServoPWM = 2.4;
        /// <summary>
        /// The default min value for the servo.
        /// </summary>
        protected const double DefaultMinServoPWM = 0.6;

        private void InitServo()
        {
            SetBounds(DefaultMaxServoPWM, 0, 0, 0, DefaultMinServoPWM);
            PeriodMultiplier = PeriodMultiplier.K4X;

            Report(ResourceType.kResourceType_Servo, (byte)Channel);
            LiveWindow.LiveWindow.AddActuator("Servo", Channel, this);
        }

        /// <summary>
        /// Creates a new Servo.
        /// </summary>
        /// <param name="channel">The PWM Channel that the Servo is attached to. 0-9 are on-board, 10-19 are on the MXP port</param>
        public Servo(int channel) : base(channel)
        {
            InitServo();
        }

        /// <summary>
        /// Sets the absolute value of the servo.
        /// </summary>
        /// <param name="value">The absolute setpoint between 0.0 and 1.0</param>
        public virtual void Set(double value)
        {
            SetPosition(value);
        }

        /// <summary>
        /// Gets the latest absolute value of the servo.
        /// </summary>
        /// <returns>The latest absolute setpoint between 0.0 and 1.0</returns>
        public virtual double Get()
        {
            return GetPosition();
        }

        /// <summary>
        /// Sets the servo angle.
        /// </summary>
        /// <param name="degrees">The angle in degrees between 0 and 180</param>
        public virtual void SetAngle(double degrees)
        {
            if (degrees < MinServoAngle)
                degrees = MinServoAngle;
            else if (degrees > MaxServoAngle)
                degrees = MaxServoAngle;
            SetPosition(((degrees - MinServoAngle))/ServoAngleRange);
        }

        /// <summary>
        /// Gets the latest servo angle.
        /// </summary>
        /// <returns>The latest angle in degrees between 0 and 180</returns>
        public virtual double GetAngle()
        {
            return GetPosition()*ServoAngleRange + MinServoAngle;
        }

        /// <summary>
        /// Gets the range of the servo.
        /// </summary>
        private static double ServoAngleRange => MaxServoAngle - MinServoAngle;

        ///<inheritdoc/>
        public override string SmartDashboardType => "Servo";

        ///<inheritdoc/>
        public override void InitTable(ITable subtable)
        {
            Table = subtable;
            UpdateTable();
        }

        ///<inheritdoc/>
        public override void UpdateTable()
        {
            Table?.PutNumber("Value", Get());
        }

        ///<inheritdoc/>
        public override void StartLiveWindowMode()
        {
            Table?.AddTableListener("Value", this, true);
        }

        ///<inheritdoc/>
        public override void StopLiveWindowMode()
        {
            Table?.RemoveTableListener(this);
        }

        ///<inheritdoc/>
        public override void ValueChanged(ITable table, string key, object value, NotifyFlags flags)
        {
            Set((double)value);
        }
    }
}
