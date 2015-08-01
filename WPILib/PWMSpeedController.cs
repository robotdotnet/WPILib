using System;
using WPILib.Interfaces;

namespace WPILib
{
    /// <summary>
    /// This is the base class for all PWM Speed Controllers.
    /// </summary>
    /// <remarks>
    /// The Java and C++ versions this explicitly implemented in every speed controller.
    /// It is easier to have this as the base class, and only have to run and init function.
    /// This saves time from copy pasting code every time a new motor controller is added.
    /// </remarks>
    public abstract class PWMSpeedController : SafePWM, ISpeedController
    {
        /// <summary>
        /// Constructor for a PWM Speed Controller.
        /// </summary>
        /// <param name="channel">The PWM Channel that the Speed Controller is attached to. 0-9 are on-board, 10-19 are on the MXP port</param>
        protected PWMSpeedController(int channel)
            : base(channel)
        {
        }

        /// <inheritdoc/>
        public virtual void PidWrite(double value)
        {
            Set(value);
        }

        /// <inheritdoc/>
        public virtual void Set(double value)
        {
            SetSpeed(Inverted ? -value : value);
            Feed();
        }

        /// <inheritdoc/>
        public virtual double Get()
        {
            return GetSpeed();
        }

        /// <inheritdoc/>
        [Obsolete("For compatibility with CAN Jaguar. Please use Set(double) instead.")]
        public virtual void Set(double value, byte syncGroup)
        {
            SetSpeed(Inverted ? -value : value);
            Feed();
        }

        /// <inheritdoc/>
        public bool Inverted { get; set; }
    }
}
