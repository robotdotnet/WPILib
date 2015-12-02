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
        /// <param name="input">The input.</param>
        /// <param name="output">The output.</param>
        /// <param name="model">The model.</param>
        /// <param name="startPercentage">The start percentage.</param>
        /// <param name="potentiometerRadians">The potentiometer radians.</param>
        /// <param name="invertInput">if set to <c>true</c> [invert input].</param>
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
