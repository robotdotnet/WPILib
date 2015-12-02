using System;
using HAL.Simulator.Inputs;
using HAL.Simulator.Outputs;

namespace HAL.Simulator.Mechanisms
{
    /// <summary>
    /// 
    /// </summary>
    public class ShooterWheelMechanism
    {
        /// <summary>
        /// The m_input
        /// </summary>
        protected ISimSpeedController m_input;
        /// <summary>
        /// The m_output
        /// </summary>
        protected IServoFeedback m_output;
        /// <summary>
        /// The m_model
        /// </summary>
        protected DCMotor m_model;

        /// <summary>
        /// The m_invert
        /// </summary>
        protected bool m_invert;
        /// <summary>
        /// The m_minimum velocity
        /// </summary>
        protected double m_minimumVelocity;

        /// <summary>
        /// Gets or sets the deacceleration constant.
        /// </summary>
        /// <value>
        /// The deacceleration constant.
        /// </value>
        public double DeaccelerationConstant { get; set; }

        /// <summary>
        /// Gets or sets the system inertia.
        /// </summary>
        /// <value>
        /// The system inertia.
        /// </value>
        public double SystemInertia { get; set; }

        /// <summary>
        /// Gets or sets the current radians per second.
        /// </summary>
        /// <value>
        /// The current radians per second.
        /// </value>
        public double CurrentRadiansPerSecond { get; protected set; }

        /// <summary>
        /// Gets or sets the deadzone.
        /// </summary>
        /// <value>
        /// The deadzone.
        /// </value>
        public double Deadzone { get; set; } = 0.001;


        /// <summary>
        /// Initializes a new instance of the <see cref="ShooterWheelMechanism"/> class.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="output">The output.</param>
        /// <param name="model">The model.</param>
        /// <param name="invertInput">if set to <c>true</c> [invert input].</param>
        /// <param name="minimumVelocity">The minimum velocity.</param>
        /// <param name="deaccelConstant">The deaccel constant.</param>
        /// <param name="systemInertia">The system inertia.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Shooter Wheels do not support IsAnalog inputs</exception>
        public ShooterWheelMechanism(ISimSpeedController input, IServoFeedback output, DCMotor model,
            bool invertInput, double minimumVelocity, double deaccelConstant, double systemInertia)
        {
            if (output is SimAnalogInput)
            {
                throw new ArgumentOutOfRangeException(nameof(output), "Shooter Wheels do not support IsAnalog inputs");
            }
            m_input = input;
            m_output = output;
            m_model = model;
            m_invert = invertInput;
            m_minimumVelocity = minimumVelocity;
            DeaccelerationConstant = deaccelConstant;
            SystemInertia = systemInertia;
        }

        /// <summary>
        /// Limits the specified PWM value.
        /// </summary>
        /// <param name="pwmValue">The PWM value.</param>
        /// <returns></returns>
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

            double percentFreeSpeed = CurrentRadiansPerSecond / m_model.MaxSpeed;
            double currentTorque = (1 - percentFreeSpeed) * m_model.MaxTorque;

            double alpha = currentTorque / SystemInertia;
            alpha *= pwmValue;
            alpha -= DeaccelerationConstant;

            double delta = alpha * seconds;

            CurrentRadiansPerSecond += delta;

            if (CurrentRadiansPerSecond > m_model.MaxSpeed)
                CurrentRadiansPerSecond = m_model.MaxSpeed;
            else if (CurrentRadiansPerSecond < m_minimumVelocity)
                CurrentRadiansPerSecond = m_minimumVelocity;

            m_output.SetRate(CurrentRadiansPerSecond);
        }
    }
}
