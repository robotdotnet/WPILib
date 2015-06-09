

using System;
using HAL_Base;

namespace WPILib
{
    public class DigitalOutput : DigitalSource
    {
        private IntPtr m_pwmGenerator = IntPtr.Zero;

        public DigitalOutput(int channel)
        {
            InitDigitalPort(channel, false);

            HAL.Report(ResourceType.kResourceType_DigitalOutput, (byte)channel);
        }

        public override void Dispose()
        {
            if (m_pwmGenerator != IntPtr.Zero)
            {
                DisablePWM();
            }

            base.Dispose();
        }

        public bool Value
        {
            set
            {
                int status = 0;
                HALDigital.SetDIO(m_port, (short) (value ? 0 : 1), ref status);
            }
        }

        public int Channel => m_channel;

        public void Pulse(int channel, float pulseLength)
        {
            int status = 0;
            HALDigital.Pulse(m_port, pulseLength, ref status);
        }

        public bool Pulsing
        {
            get
            {
                int status = 0;
                bool value = HALDigital.IsPulsing(m_port, ref status);
                return value;
            }
        }

        public double PWMRate
        {
            set
            {
                int status = 0;
                HALDigital.SetPWMRate(value, ref status);
            }
        }

        public void EnablePWM(double initialDutyCycle)
        {
            if (m_pwmGenerator != IntPtr.Zero)
                return;
            int status = 0;
            m_pwmGenerator = HALDigital.AllocatePWM(ref status);
            HALDigital.SetPWMDutyCycle(m_pwmGenerator, initialDutyCycle, ref status);
            HALDigital.SetPWMOutputChannel(m_pwmGenerator, (uint)m_channel, ref status);
        }

        public void DisablePWM()
        {
            if (m_pwmGenerator == IntPtr.Zero)
                return;
            int status = 0;
            HALDigital.SetPWMOutputChannel(m_pwmGenerator, (uint)DigitalChannels, ref status);
            HALDigital.FreePWM(m_pwmGenerator, ref status);
            m_pwmGenerator = IntPtr.Zero;
        }

        public double DutyCycle
        {
            set
            {
                if (m_pwmGenerator == IntPtr.Zero)
                    return;
                int status = 0;
                HALDigital.SetPWMDutyCycle(m_pwmGenerator, value, ref status);
            }
        }
    }
}
