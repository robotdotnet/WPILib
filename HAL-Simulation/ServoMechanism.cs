using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAL_Simulator.Inputs;
using HAL_Simulator.Outputs;

namespace HAL_Simulator
{
    public class ServoMechanism
    {
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
            m_load = load;
        }

        public void Reset(double position)
        {
            m_model.Reset(position,0,0);
            m_start_position = position;
            m_output.SetCount(0);
            m_output.SetPeriod(float.MaxValue);
            //Ignoring current
        }

        public void Step(double battery_voltage, double acceleration, double timestep)
        {
            double command = m_input.Get()*battery_voltage;
            m_model.Step(command, m_load, acceleration, timestep);
            m_output.SetCount((m_model.GetPosition() - m_start_position) / m_encoder_distance_per_tick);
        }

        
    }
}
