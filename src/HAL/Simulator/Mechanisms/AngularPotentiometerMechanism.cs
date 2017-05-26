using System;
using HAL.Simulator.Inputs;
using HAL.Simulator.Outputs;

namespace HAL.Simulator.Mechanisms
{
    /// <summary>
    /// This class is used to create a simulated arm that is driven by a potentiometer.
    /// </summary>
    /// <seealso cref="FeedbackMechanismBase" />
    public class AngularPotentiometerMechanism : FeedbackMechanismBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AngularPotentiometerMechanism"/> class.
        /// </summary>
        /// <param name="input">The motor driving the system</param>
        /// <param name="output">The Analog Input giving feedback to the system..</param>
        /// <param name="model">The motor model.</param>
        /// <param name="startPercentage">The starting percentage of the potentiometer from 0.</param>
        /// <param name="potentiometerRotations">The number of rotations the potentiometer has.</param>
        /// <param name="invertInput">if set to <c>true</c> [invert input].</param>
        public AngularPotentiometerMechanism(ISimSpeedController input, SimAnalogInput output, DCMotor model, 
            double startPercentage, double potentiometerRotations, bool invertInput)
        {
            m_input = input;
            m_output = output;
            m_model = model;
            m_maxRadians = potentiometerRotations * (Math.PI * 2);
            m_minRadians = 0;
            CurrentRadians = (m_maxRadians - m_minRadians) *startPercentage;
            m_invert = invertInput;
            m_scaler = 5 / (m_maxRadians - m_minRadians);
        }
    }
}
