using System;
using HAL_Simulator.Inputs;
using HAL_Simulator.Outputs;

namespace HAL_Simulator.Mechanisms
{
    public abstract class ServoMechanism
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

        public double RadiansPerMeter { get; protected set; }

        public double Deadzone { get; set; } = 0.001;


        public static double InchesToMeters(double inches)
        {
            return inches * 0.0254;
        }

        public static double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
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

            double outputValue = CurrentRadians;// / (m_maxRadians - m_minRadians);
            m_output.Set(outputValue * m_scaler);
            
        }

        /*
        protected ISimSpeedController m_input;
        protected SimEncoder m_output;
        protected double m_encoder_distance_per_tick;
        protected DCMotor m_model;
        protected double m_start_position;
        protected int m_pdp_channel;
        protected double m_load;
        protected Limits m_limits;

        public class Limits
        {
            public Limits(double min, double max)
            {
                min_position = min;
                max_position = max;
            }

            public double min_position = -1E9;
            public double max_position = 1E9;
        }

        public ServoMechanism(ISimSpeedController input, SimEncoder output,
            int pdp_channel, double encoder_distance_per_tick, DCMotor model, 
            double load, Limits limits)
        {
            m_input = input;
            m_output = output;
            m_pdp_channel = pdp_channel;
            m_encoder_distance_per_tick = encoder_distance_per_tick;
            m_model = model;
            m_load = load;
            m_limits = limits;
            m_start_position = 0;
        }

        public void SetLoad(double load)
        {
            m_model.Load = load;
        }
        */
    }

}
