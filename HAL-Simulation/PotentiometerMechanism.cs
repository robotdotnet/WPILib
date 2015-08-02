using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAL_Simulator.Inputs;
using HAL_Simulator.Outputs;


namespace HAL_Simulator
{
    public class PotentiometerMechanism
    {
        protected ISimSpeedController m_input;
        protected SimAnalogInput m_output;
        protected double m_voltage_per_tick;
        protected DCMotor m_model;
        protected int m_pdp_channel;
        protected double m_load;
        protected double m_start_voltage;
        protected bool m_inverted;

        public PotentiometerMechanism(ISimSpeedController input, SimAnalogInput output,
            int pdp_channel, double voltage_per_tick, DCMotor model,
            double load, double start_voltage, bool invert)
        {
            m_input = input;
            m_output = output;
            m_pdp_channel = pdp_channel;
            m_voltage_per_tick = voltage_per_tick;
            m_model = model;
            m_load = load;
            m_start_voltage = start_voltage;
            m_inverted = invert;
        }

        public void SetLoad(double load)
        {
            m_load = load;
        }

        public void Reset(double voltage)
        {
            m_model.Reset(0,0,0);
            m_start_voltage = voltage;
            m_output.Set(0);

        }

        public void Step(double battery_voltage, double acceleration, double timestep)
        {
            double command = -m_input.Get()*battery_voltage;
            m_model.Step(command, m_load, acceleration, timestep);
            m_output.Set((m_model.GetPosition()/m_voltage_per_tick) + m_start_voltage);
        }

        public double GetVoltage()
        {
            return m_output.GetVoltage();
        }
    }
}
