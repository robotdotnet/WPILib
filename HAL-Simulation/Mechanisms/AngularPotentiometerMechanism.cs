using HAL_Simulator.Inputs;
using HAL_Simulator.Outputs;

namespace HAL_Simulator.Mechanisms
{
    public class AngularPotentiometerMechanism : AbstractFeedbackMechanism
    {
        public AngularPotentiometerMechanism(ISimSpeedController input, SimAnalogInput output, DCMotor model, 
            double startPercentage, double potentiometerRadians, bool invertInput)
        {
            m_input = input;
            m_output = output;
            m_model = model;
            m_maxRadians = potentiometerRadians;
            m_minRadians = 0;
            CurrentRadians = (m_maxRadians - m_minRadians) *startPercentage;
            m_invert = invertInput;
            m_scaler = 5 / (m_maxRadians - m_minRadians);
        }
    }
}
