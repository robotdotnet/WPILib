using HAL;
using WPILib.LiveWindows;

namespace WPILib
{
    /// <summary>
    /// VEX Robotics Victor 888 Speed Controller
    /// </summary>
    /// <remarks>
    /// The Vex Robotics Victor 884 Speed Controller can also be used with this
    /// class but may need to be calibrated per the Victor 884 user manual.
    /// </remarks>
    public class Victor : PWMSpeedController
    {
        /// <summary>
        /// Common initialization code called by all constructors.
        /// </summary><remarks>
        /// Note that the Victor uses the following bounds for PWM values.  These values were determined
        /// empirically and optimized for the Victor 888. These values should work reasonably well for
        /// Victor 884 controllers also but if users experience issues such as asymmetric behaviour around
        /// the deadband or inability to saturate the controller in either direction, calibration is recommended.
        /// The calibration procedure can be found in the Victor 884 User Manual available from VEX Robotics:
        /// http://content.vexrobotics.com/docs/ifi-v884-users-manual-9-25-06.pdf
        /// <para> </para>
        /// <para />  - 2.027ms = full "forward"
        /// <para />  - 1.525ms = the "high end" of the deadband range
        /// <para />  - 1.507ms = center of the deadband range (off)
        /// <para />  - 1.49ms = the "low end" of the deadband range
        /// <para />  - 1.026ms = full "reverse"
        /// </remarks>
        protected void InitVictor()
        {
            SetBounds(2.027, 1.525, 1.507, 1.49, 1.026);
            PeriodMultiplier = PeriodMultiplier.K2X;
            SetRaw(CenterPwm);
            SetZeroLatch();

            LiveWindow.LiveWindow.AddActuator(nameof(Victor), Channel, this);
            HAL.HAL.Report(ResourceType.kResourceType_Victor, (byte)Channel);
        }

        /// <summary>
        /// Creates a new Victor 888 Motor Controller.
        /// </summary>
        /// <remarks>Please see <see cref="VictorSP"/> for PWM control of VictorSP motor controllers.</remarks>
        /// <param name="channel">The PWM Channel that the Victor is attached to. 0-9 are on-board, 10-19 are on the MXP port</param>
        public Victor(int channel)
            : base(channel)
        {
            InitVictor();
        }
    }
}
