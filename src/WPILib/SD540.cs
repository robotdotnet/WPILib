using HAL.Base;

namespace WPILib
{
    /// <summary>
    /// MindSensors SD540 Speed Controller.
    /// </summary>
    public class SD540 : PWMSpeedController
    {
        /// <summary>
        /// Common initialization code called by all constructors.
        /// </summary>
        /// <remarks>
        /// Note that the SD540 uses the following bounds for PWM values.
        /// These values should work reasonably well for most controllers, but
        /// if users experience issues such as asymmetric behavior around the
        /// deadband or inability to saturate the controller in either direction,
        /// callibration is recommended. The callibration procedure can be found
        /// int the SD540 User Manual available from Mindsensors.
        /// </remarks>
        protected void InitSD540()
        {
            SetBounds(2.05, 1.55, 1.50, 1.44, .94);
            PeriodMultiplier = PeriodMultiplier.K1X;
            SetDisabled();
            SetZeroLatch();

            LiveWindow.LiveWindow.AddActuator(nameof(SD540), Channel, this);
            HAL.Base.HAL.Report(ResourceType.kResourceType_MindsensorsSD540, (byte)Channel);
        }

        /// <summary>
        /// Creates a new <see cref="SD540"/>.
        /// </summary>
        /// <param name="channel">The PWM Channel that the SD540 is attached to. 0-9 are on-board, 10-19 are on the MXP port</param>
        public SD540(int channel)
            : base(channel)
        {
            InitSD540();
        }
    }
}
