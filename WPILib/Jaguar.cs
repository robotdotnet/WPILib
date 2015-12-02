using HAL;
using WPILib.LiveWindows;

namespace WPILib
{
    /// <summary>
    /// Texas Instruments / Vex Robotics Jaguar Speed Controller as a PWM Device.
    /// </summary>
    /// <seealso cref="CANJaguar"> CANJaguar for CAN Control</seealso>
    public class Jaguar : PWMSpeedController
    {
        /// <summary>
        /// Common initialization code called by all constructors.
        /// </summary>
        protected void InitJaguar()
        {
            /*
             * Input profile defined by Luminary Micro.
             *
             * Full reverse ranges from 0.671325ms to 0.6972211ms
             * Proportional reverse ranges from 0.6972211ms to 1.4482078ms
             * Neutral ranges from 1.4482078ms to 1.5517922ms
             * Proportional forward ranges from 1.5517922ms to 2.3027789ms
             * Full forward ranges from 2.3027789ms to 2.328675ms
             */
            SetBounds(2.31, 1.55, 1.507, 1.454, .697);
            PeriodMultiplier = PeriodMultiplier.K1X;
            SetRaw(CenterPwm);
            SetZeroLatch();

            HAL.HAL.Report(ResourceType.kResourceType_Jaguar, (byte)Channel);
            LiveWindow.LiveWindow.AddActuator("Jaguar", Channel, this);
        }

        /// <summary>
        /// Creates a new Jaguar Motor Controller in PWM Mode.
        /// </summary>
        /// <remarks>See <see cref="CANJaguar"/> for using a Jaguar in CAN mode.</remarks>
        /// <param name="channel">The PWM channel that the Jaguar is attached to. 0-9 are on-board, 10-19 are on the MXP port</param>
        public Jaguar(int channel)
            : base(channel)
        {
            InitJaguar();
        }
    }
}
