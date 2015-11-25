using System;
using HAL_Simulator.Inputs;
using HAL_Simulator.Outputs;

namespace HAL_Simulator.Mechanisms
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="HAL_Simulator.Mechanisms.FeedbackMechanismBase" />
    public class LinearPotentiometerMechanism : FeedbackMechanismBase
    {
        //String travel and Spool Radius in meters
        /// <summary>
        /// Initializes a new instance of the <see cref="LinearPotentiometerMechanism"/> class.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="output">The output.</param>
        /// <param name="model">The model.</param>
        /// <param name="startPercentage">The start percentage.</param>
        /// <param name="stringTravel">The string travel.</param>
        /// <param name="spoolRadius">The spool radius.</param>
        /// <param name="invertInput">if set to <c>true</c> [invert input].</param>
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
