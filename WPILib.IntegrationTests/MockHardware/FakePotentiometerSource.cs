using System;

namespace WPILib.IntegrationTests.MockHardware
{
    public class FakePotentiometerSource : IDisposable
    {
        private AnalogOutput output;
        private bool m_init_output;
        private double potMaxAngle;
        private double potMaxVoltage = 5.0;
        private readonly double defaultPotMaxAngle;

        public FakePotentiometerSource(AnalogOutput output, double defaultPotMaxAngle)
        {
            this.defaultPotMaxAngle = defaultPotMaxAngle;
            potMaxAngle = defaultPotMaxAngle;
            this.output = output;
            m_init_output = false;
        }

        public FakePotentiometerSource(int port, double defaultPotMaxAngle)
            : this(new AnalogOutput(port), defaultPotMaxAngle)
        {
            m_init_output = true;
        }

        public void SetMaxVoltage(double voltage)
        {
            potMaxVoltage = voltage;
        }

        public void SetRange(double range)
        {
            potMaxAngle = range;
        }

        public void Reset()
        {
            potMaxAngle = defaultPotMaxAngle;
            output.SetVoltage(0.0);
        }

        public void SetAngle(double angle)
        {
            output.SetVoltage((potMaxVoltage / potMaxAngle) * angle);
        }

        public void SetVoltage(double voltage)
        {
            output.SetVoltage(voltage);
        }

        public double GetVoltage()
        {
            return output.GetVoltage();
        }

        public double GetAngle()
        {
            double voltage = output.GetVoltage();
            if (voltage == 0)
            {
                return 0;
            }
            return voltage * (potMaxAngle / potMaxVoltage);
        }

        public void Dispose()
        {
            if (m_init_output)
            {
                output.Dispose();
                output = null;
                m_init_output = false;
            }
        }
    }
}
