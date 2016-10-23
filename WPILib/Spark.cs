using HAL.Base;

namespace WPILib
{
    /// <summary>
    /// REV Robotics SPARK Speed Controller
    /// </summary>
    public class Spark : PWMSpeedController
    {
        /// <summary>
        /// Common initialization code called by all constructors.
        /// </summary>
        /// <remarks>
        /// Note that the SPARK uses the following bounds for PWM values.
        /// These values should work reasonably well for most controllers, but
        /// if users experience issues such as asymmetric behavior around the
        /// deadband or inability to saturate the controller in either direction,
        /// callibration is recommended. The callibration procedure can be found
        /// int the Spark User Manual available from REV Robotics.
        /// </remarks>
        protected void InitSpark()
        {
            SetBounds(2.003, 1.55, 1.50, 1.46, .999);
            PeriodMultiplier = PeriodMultiplier.K1X;
            SetDisabled();
            SetZeroLatch();

            LiveWindow.LiveWindow.AddActuator(nameof(Spark), Channel, this);
            HAL.Base.HAL.Report(ResourceType.kResourceType_RevSPARK, (byte)Channel);
        }

        /// <summary>
        /// Creates a new <see cref="Spark"/>
        /// </summary>
        /// <param name="channel">The PWM Channel that the Spark is attached to. 0-9 are on-board, 10-19 are on the MXP port</param>
        public Spark(int channel)
            : base(channel)
        {
            InitSpark();
        }
    }
}
