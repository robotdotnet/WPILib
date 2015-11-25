using System;
using HAL_Simulator.Inputs;
using HAL_Simulator.Outputs;

namespace HAL_Simulator.Mechanisms
{
    /// <summary>
    /// A simulator mechanism that can be used to run a linear system using an encoder.
    /// </summary>
    /// <seealso cref="HAL_Simulator.Mechanisms.FeedbackMechanismBase" />
    public class LinearEncoderMechanism : FeedbackMechanismBase
    {
        private double m_offset;
        /// <summary>
        /// Initializes a new instance of the <see cref="LinearEncoderMechanism"/> class.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="output">The output.</param>
        /// <param name="encoderCpr">The encoder CPR.</param>
        /// <param name="model">The model.</param>
        /// <param name="spoolRadius">The spool radius.</param>
        /// <param name="startHeight">The start height.</param>
        /// <param name="invertInput">if set to <c>true</c> [invert input].</param>
        public LinearEncoderMechanism(ISimSpeedController input, SimEncoder output, double encoderCpr, DCMotor model,
            double spoolRadius, double startHeight, bool invertInput)//, double topLimit = double.MaxValue / 500)
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
        /// Sets the home location.
        /// </summary>
        /// <param name="homeInput">The home input.</param>
        /// <param name="rising">if set to <c>true</c> [rising].</param>
        /// <param name="meters">The meters.</param>
        /// <param name="threshold">The threshold.</param>
        public void SetHomeLocation(SimDigitalInput homeInput, bool rising, double meters, double threshold)
        {
            homeInput.Set(!rising);

            m_checkHome = () =>
            {
                if (CurrentMeters < (meters + threshold) && CurrentMeters > (meters - threshold))
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
