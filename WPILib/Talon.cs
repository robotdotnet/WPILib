using HAL;
using HAL.Base;
using WPILib.LiveWindows;

namespace WPILib
{
    /// <summary>
    /// Cross the Road Electronics (CTRE) Talon and Talon SR Speed Controller
    /// </summary>
    public class Talon : PWMSpeedController
    {
        /// <summary>
        /// Common initialization code called by all constructors.
        /// </summary><remarks>
        /// <para> </para>
        /// Note that the Talon uses the following bounds for PWM values. These values should work reasonably well for
        /// most controllers, but if users experience issues such as asymmetric behavior around
        /// the deadband or inability to saturate the controller in either direction, calibration is recommended.
        /// The calibration procedure can be found in the Talon User Manual available from CTRE.
        /// <para> </para>
        /// <para />  - 2.037ms = full "forward"
        /// <para />  - 1.539ms = the "high end" of the deadband range
        /// <para />  - 1.513ms = center of the deadband range (off)
        /// <para />  - 1.487ms = the "low end" of the deadband range
        /// <para />  - .989ms = full "reverse"
        /// </remarks>
        protected void InitTalon()
        {
            SetBounds(2.037, 1.539, 1.513, 1.487, 0.989);
            PeriodMultiplier = PeriodMultiplier.K1X;
            SetRaw(CenterPwm);
            SetZeroLatch();

            LiveWindow.LiveWindow.AddActuator(nameof(Talon), Channel, this);
            HAL.Base.HAL.Report(ResourceType.kResourceType_Talon, (byte)Channel);
        }

        /// <summary>
        /// Creates a new Talon or Talon SR Motor Controller.
        /// </summary>
        /// <remarks>Please see <see cref="TalonSRX"/> for PWM control of TalonSRX motor controllers.</remarks>
        /// <param name="channel">The PWM Channel that the Talon is attached to. 0-9 are on-board, 10-19 are on the MXP port</param>
        public Talon(int channel)
            : base(channel)
        {
            InitTalon();
        }
    }
}
