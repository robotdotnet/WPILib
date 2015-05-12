using System;
using System.Collections.Generic;
using System.Text;
using HAL_FRC;

namespace WPILib
{
    public class DigitalOutput : DigitalSource
    {
        private IntPtr _pwmGenerator = IntPtr.Zero;

        public DigitalOutput(int channel)
        {
            InitDigitalPort(channel, false);

            HAL.Report(ResourceType.kResourceType_DigitalOutput, (byte)channel);
        }

        public override void Free()
        {
            if (_pwmGenerator != IntPtr.Zero)
            {
                DisablePWM();
            }

            base.Free();
        }

        public void Set(bool value)
        {
            int status = 0;
            HALDigital.setDIO(_port, (short)(value ? 0 : 1), ref status);
        }

        public int GetChannel()
        {
            return _channel;
        }

        public void Pulse(int channel, float pulseLength)
        {
            int status = 0;
            HALDigital.pulse(_port, pulseLength, ref status);
        }

        public bool IsPulsing()
        {
            int status = 0;
            bool value = HALDigital.isPulsing(_port, ref status);
            return value;
        }

        public void SetPWMRate(double rate)
        {
            int status = 0;
            HALDigital.setPWMRate(rate, ref status);
        }

        public void EnablePWM(double initialDutyCycle)
        {
            if (_pwmGenerator != IntPtr.Zero)
                return;
            int status = 0;
            _pwmGenerator = HALDigital.allocatePWM(ref status);
            HALDigital.setPWMDutyCycle(_pwmGenerator, initialDutyCycle, ref status);
            HALDigital.setPWMOutputChannel(_pwmGenerator, (uint)_channel, ref status);
        }

        public void DisablePWM()
        {
            if (_pwmGenerator == IntPtr.Zero)
                return;
            int status = 0;
            HALDigital.setPWMOutputChannel(_pwmGenerator, (uint)DigitalChannels, ref status);
            HALDigital.freePWM(_pwmGenerator, ref status);
            _pwmGenerator = IntPtr.Zero;
        }

        public void UpdateDutyCycle(double dutyCycle)
        {
            if (_pwmGenerator == IntPtr.Zero)
                return;
            int status = 0;
            HALDigital.setPWMDutyCycle(_pwmGenerator, dutyCycle, ref status);

        }
    }
}
