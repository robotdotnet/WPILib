using System;
using HAL.Simulator.Inputs;
using HAL.Simulator.Outputs;

namespace HAL.Simulator.Mechanisms
{
    /// <summary>
    /// A simulator mechanism that can be used to run a linear system using an encoder.
    /// </summary>
    /// <seealso cref="FeedbackMechanismBase" />
    public class LinearEncoderMechanism : FeedbackMechanismBase
    {
        private double m_offset;
        /// <summary>
        /// Initializes a new instance of the <see cref="LinearEncoderMechanism"/> class.
        /// </summary>
        /// <param name="input">The motor driving the system.</param>
        /// <param name="output">The encoder giving feedback to the system.</param>
        /// <param name="encoderCpr">The CPR of the encoder. If not a 1:1 ratio on the axle, scale this beforehand.</param>
        /// <param name="model">The transmission model to use.</param>
        /// <param name="spoolRadius">The radius of your spool in Meters. (Use the radius of the up spool if using a cascade elevator).</param>
        /// <param name="startHeight">The start height of your elevator relative to the reset sensor in meters. If no reset sensor then use 0.</param>
        /// <param name="invertInput">if set to <c>true</c> [invert input].</param>
        public LinearEncoderMechanism(ISimSpeedController input, SimEncoder output, double encoderCpr, DCMotor model,
            double spoolRadius, double startHeight, bool invertInput)
        {
            m_input = input;
            m_output = output;
            m_model = model;
            m_invert = invertInput;

            RadiansPerMeter = 1 / spoolRadius;

            m_scaler = encoderCpr / (Math.PI * 2);

            m_offset = startHeight;

            CurrentMeters = m_offset;

            m_maxRadians = double.MaxValue;
            m_minRadians = double.MinValue;

        }

        /// <inheritdoc/>
        public override void Update(double seconds)
        {
            base.Update(seconds);
            CurrentMeters += m_offset;
            m_checkHome?.Invoke();
        }

        private Action m_checkHome = null;

        /// <summary>
        /// Sets the homing sensor location.
        /// </summary>
        /// <param name="homeInput">The digital input to use for the homing sensor</param>
        /// <param name="rising">True if homing on the rising edge, otherwise false.</param>
        /// <param name="meters">The distance relative to your lowest elevator point the sensor is located at (in meters).</param>
        /// <param name="threshold">The threshold for which your encoder would trigger (in meters).</param>
        public void SetHomeLocation(SimDigitalInput homeInput, bool rising, double meters, double threshold)
        {
            homeInput.Set(!rising);

            m_checkHome = () =>
            {
                if (CurrentMeters < meters + threshold && CurrentMeters > meters - threshold)
                {
                    homeInput.Set(rising);
                }
                else
                {
                    homeInput.Set(!rising);
                }
            };

            Action<dynamic, dynamic> handler = null;

            handler = (k, v) =>
            {
                m_offset = CurrentMeters;
                CurrentRadians = 0;
                ((SimEncoder)m_output).EncoderData.Cancel(k, handler);
                ((SimEncoder)m_output).EncoderData.Reset = false;
            };
            ((SimEncoder)m_output).EncoderData.Register("Reset", handler);




        }
    }
}
