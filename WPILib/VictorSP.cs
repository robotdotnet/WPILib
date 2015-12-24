using HAL.Base;

namespace WPILib
{
    /// <summary>
    /// VEX Robotics Victor SP Speed Controller
    /// </summary>
    public class VictorSP : PWMSpeedController
    {
        /// <summary>
        /// Common initialization code called by all constructors.
        /// </summary><remarks>
        /// Note that the VictorSP uses the following bounds for PWM values. These values should work reasonably well for
        /// most controllers, but if users experience issues such as asymmetric behavior around
        /// the deadband or inability to saturate the controller in either direction, calibration is recommended.
        /// The calibration procedure can be found in the VictorSP User Manual available from CTRE.
        /// <para> </para>
        /// <para />  - 2.004ms = full "forward"
        /// <para />  - 1.52ms = the "high end" of the deadband range
        /// <para />  - 1.50ms = center of the deadband range (off)
        /// <para />  - 1.48ms = the "low end" of the deadband range
        /// <para />  - .997ms = full "reverse"
        /// </remarks>
        protected void InitVictorSP()
        {
            SetBounds(2.004, 1.52, 1.50, 1.48, .997);
            PeriodMultiplier = PeriodMultiplier.K1X;
            SetRaw(CenterPwm);
            SetZeroLatch();

            LiveWindow.LiveWindow.AddActuator(nameof(VictorSP), Channel, this);
            HAL.Base.HAL.Report(ResourceType.kResourceType_VictorSP, (byte)Channel);
        }

        /// <summary>
        /// Creates a new VictorSP Motor Controller.
        /// </summary>
        /// <param name="channel">The PWM Channel that the VictorSP is attached to. 0-9 are on-board, 10-19 are on the MXP port</param>
        public VictorSP(int channel)
            : base(channel)
        {
            InitVictorSP();
        }
    }
}
