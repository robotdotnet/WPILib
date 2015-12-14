using System;
using HAL.Simulator.Inputs;
using HAL.Simulator.Outputs;

namespace HAL.Simulator.Mechanisms
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="FeedbackMechanismBase" />
    public class LinearPotentiometerMechanism : FeedbackMechanismBase
    {
        //String travel and Spool Radius in meters
        /// <summary>
        /// Initializes a new instance of the <see cref="LinearPotentiometerMechanism"/> class.
        /// </summary>
        /// <param name="input">The motor driving the system.</param>
        /// <param name="output">The potentiometer giving feedback to the system.</param>
        /// <param name="model">The motor model with transmission to use.</param>
        /// <param name="startPercentage">The starting percentages of the potentiometer from 0.</param>
        /// <param name="stringTravel">The potentiometer travel scaled to be linear (in meters).</param>
        /// <param name="spoolRadius">The radius of your spool in Meters. (Use the radius of the up spool if using a cascade elevator).</param>
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
