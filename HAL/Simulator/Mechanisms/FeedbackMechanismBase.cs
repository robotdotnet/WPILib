using System;
using HAL.Simulator.Extensions;
using HAL.Simulator.Inputs;
using HAL.Simulator.Outputs;

namespace HAL.Simulator.Mechanisms
{
    /// <summary>
    /// The base class for all feedback mechanisms.
    /// </summary>
    public abstract class FeedbackMechanismBase
    {
        /// <summary>
        /// The motor input for the mechanism.
        /// </summary>
        protected ISimSpeedController m_input;
        /// <summary>
        /// The feedback output for the mechanism.
        /// </summary>
        protected IServoFeedback m_output;
        /// <summary>
        /// The motor and transmission model for the system.
        /// </summary>
        protected DCMotor m_model;
        /// <summary>
        /// True if the motor should be inverted when read.
        /// </summary>
        protected bool m_invert;
        /// <summary>
        /// The maximum radians allowed for the transmission output.
        /// </summary>
        protected double m_maxRadians;
        /// <summary>
        /// The minimum radians allowed for the transmission output.
        /// </summary>
        protected double m_minRadians;
        /// <summary>
        /// The number used to scale motor radians to sensor outputs properly.
        /// </summary>
        protected double m_scaler;

        /// <summary>
        /// Gets the current number of meters traveled using the Feedback Mechanism.
        /// </summary>
        public double CurrentMeters { get; protected set; }

        /// <summary>
        /// Gets the current number of radians the motor has rotated from zero.
        /// </summary>
        public double CurrentRadians { get; protected set; } //current radians

        /// <summary>
        /// Gets the current number of rotations the motor has rotated from zero.
        /// </summary>
        public double CurrentRotations => CurrentRadians / (Math.PI * 2);

        /// <summary>
        /// Gets the current radians per second the mechanism is rotating at.
        /// </summary>
        public double CurrentRadiansPerSecond { get; protected set; } // Current Radians Per Second

        /// <summary>
        /// Gets the current rotations per minute the mechanism is rotating at.
        /// </summary>
        public double CurrentRotationsPerMinute => CurrentRadiansPerSecond.RadiansPerSecondToRpms();

        /// <summary>
        /// Gets or sets the Radians traveled per meter moved.
        /// </summary>
        public double RadiansPerMeter { get; protected set; } = 1.0; //Set to 1.0 so it doesnt divide by 0

        /// <summary>
        /// Gets the rotations traveled per meter moved.
        /// </summary>
        public double RotationsPerMeter => RadiansPerMeter / (Math.PI * 2);

        /// <summary>
        /// Gets or sets the deadzone for the motor to be considered stopped.
        /// </summary>
        public double Deadzone { get; set; } = 0.001;

        /// <summary>
        /// Limits the input value to be between -1 and 1, and sets to 0 if between the <see cref="Deadzone"/>
        /// </summary>
        /// <param name="pwmValue">The PWM value to check</param>
        /// <returns>The properly limited PWM value.</returns>
        public double Limit(double pwmValue)
        {
            if (pwmValue < Deadzone && pwmValue > -Deadzone)
            {
                pwmValue = 0.0;
            }

            if (pwmValue > 1.0)
            {
                pwmValue = 1.0;
            }

            if (pwmValue < -1.0)
            {
                pwmValue = -1.0;
            }

            return pwmValue;
        }


        /// <summary>
        /// Updates the mechanism with the specified delta time
        /// </summary>
        /// <param name="seconds">The delta time in seconds.</param>
        public virtual void Update(double seconds)
        {
            double pwmValue = m_invert ? -m_input.Get() : m_input.Get();
            pwmValue = Limit(pwmValue);

            var radiansPerSecondAtPWMValue = pwmValue * m_model.MaxSpeed;

            var radiansMovedInStep = radiansPerSecondAtPWMValue * seconds;

            CurrentRadians += radiansMovedInStep;

            if (CurrentRadians > m_maxRadians)
            {
                CurrentRadians = m_maxRadians;
            }
            if (CurrentRadians < m_minRadians)
            {
                CurrentRadians = m_minRadians;
            }
            CurrentMeters = CurrentRadians / RadiansPerMeter;
            CurrentRadiansPerSecond = radiansPerSecondAtPWMValue;

            if (m_output == null) return;

            double outputValue = CurrentRadians;

            m_output.SetPosition(outputValue * m_scaler);

            m_output.SetRate(radiansPerSecondAtPWMValue);
        }
    }

}
