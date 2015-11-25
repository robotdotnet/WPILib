using HAL_Simulator.Inputs;
using HAL_Simulator.Outputs;

namespace HAL_Simulator.Mechanisms
{
    public abstract class FeedbackMechanismBase
    {
        protected ISimSpeedController m_input;
        protected IServoFeedback m_output;
        protected DCMotor m_model;
        protected bool m_invert;
        protected double m_maxRadians;
        protected double m_minRadians;
        protected double m_scaler;

        public double CurrentMeters { get; protected set; }

        public double CurrentRadians { get; protected set; } //current radians

        public double CurrentRadiansPerSecond { get; protected set; } // Current Radians Per Second

        public double RadiansPerMeter { get; protected set; } = 1.0; ///Set to 1.0 so it doesnt divide by 0

        public double Deadzone { get; set; } = 0.001;

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

            double output;
            if (radiansPerSecondAtPWMValue == 0)
                output = double.NaN;
            else
                output = 1 / radiansPerSecondAtPWMValue;

            m_output.SetPeriod(output);
        }
    }

}
