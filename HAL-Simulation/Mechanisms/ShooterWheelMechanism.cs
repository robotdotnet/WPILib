using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAL_Simulator.Inputs;
using HAL_Simulator.Outputs;

namespace HAL_Simulator.Mechanisms
{
    public class ShooterWheelMechanism
    {
        protected ISimSpeedController m_input;
        protected IServoFeedback m_output;
        protected DCMotor m_model;

        protected bool m_invert;
        protected double m_minimumVelocity;

        public double DeaccelerationConstant { get; set; }

        public double SystemInertia { get; set; }

        public double CurrentRadiansPerSecond { get; protected set; }

        public double Deadzone { get; set; } = 0.001;


        public ShooterWheelMechanism(ISimSpeedController input, IServoFeedback output, DCMotor model,
            bool invertInput, double minimumVelocity, double deaccelConstant, double systemInertia)
        {
            if (output is SimAnalogInput)
            {
                throw new ArgumentOutOfRangeException(nameof(output), "Shooter Wheels do not support analog inputs");
            }
            m_input = input;
            m_output = output;
            m_model = model;
            m_invert = invertInput;
            m_minimumVelocity = minimumVelocity;
            DeaccelerationConstant = deaccelConstant;
            SystemInertia = systemInertia;
        }

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

            double output;
            if (CurrentRadiansPerSecond == 0)
                output = double.NaN;
            else
                output = 1 / CurrentRadiansPerSecond;
            m_output.SetPeriod(output);
        }
    }
}
