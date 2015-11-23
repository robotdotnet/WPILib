using System;
using HAL_Simulator.Inputs;
using HAL_Simulator.Outputs;

namespace HAL_Simulator.Mechanisms
{
    public class LinearPotentiometerMechanism : FeedbackMechanismBase
    {
        //String travel and Spool Radius in meters
        public LinearPotentiometerMechanism(ISimSpeedController input, SimAnalogInput output, DCMotor model,
            double startPercentage, double stringTravel, double spoolRadius, bool invertInput)
        {
            m_input = input;
            m_output = output;
            m_model = model;

            double metersPerRotation = Math.PI * (spoolRadius * 2);
            double totalRotations = stringTravel / metersPerRotation;
            double totalRadians = totalRotations * Math.PI * 2;
            RadiansPerMeter = totalRadians / stringTravel;
            m_maxRadians = totalRadians;
            m_minRadians = 0;

            CurrentRadians = (m_maxRadians - m_minRadians) * startPercentage;
            CurrentMeters = CurrentRadians / RadiansPerMeter;
            m_invert = invertInput;
            m_scaler = 5/ (m_maxRadians - m_minRadians);
        }
    }
}
