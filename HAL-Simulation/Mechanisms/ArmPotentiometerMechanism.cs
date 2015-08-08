using System;
using HAL_Simulator.Inputs;
using HAL_Simulator.Outputs;

namespace HAL_Simulator.Mechanisms
{
    public class ArmPotentiometerMechanism : ServoMechanism
    {
        public ArmPotentiometerMechanism(ISimSpeedController input, SimAnalogInput output, DCMotor model, 
            double startPercentage, double potentiometerDegrees, bool invertInput)
        {
            m_input = input;
            m_output = output;
            m_model = model;
            m_maxRadians = DegreesToRadians(potentiometerDegrees);
            m_minRadians = 0;
            CurrentRadians = (m_maxRadians - m_minRadians) *startPercentage;
            m_invert = invertInput;
            m_scaler = 5 / (m_maxRadians - m_minRadians);
        }
    }
}
