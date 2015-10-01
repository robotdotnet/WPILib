using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAL_Simulator.Inputs;
using HAL_Simulator.Outputs;

namespace HAL_Simulator.Mechanisms
{
    public class LinearEncoderMechanism : AbstractFeedbackMechanism
    {
        private double m_offset;
        public LinearEncoderMechanism(ISimSpeedController input, SimEncoder output, double encoderCPR, DCMotor model,
            double spoolRadius, double startHeight, bool invertInput)//, double topLimit = double.MaxValue / 500)
        {
            m_input = input;
            m_output = output;
            m_model = model;
            m_invert = invertInput;

            RadiansPerMeter = 1 / spoolRadius;

            m_scaler = encoderCPR / (Math.PI * 2);

            m_offset = startHeight;

            CurrentMeters = m_offset;

            m_maxRadians = double.MaxValue;
            m_minRadians = double.MinValue;

        }

        public override void Update(double seconds)
        {
            base.Update(seconds);
            CurrentMeters += m_offset;
            m_checkHome?.Invoke();
        }

        private Action m_checkHome = null;

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
