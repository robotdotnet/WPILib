using System;

namespace WPILib.IntegrationTests.MockHardware
{
    public class FakePotentiometerSource : IDisposable
    {
        private AnalogOutput m_output;
        private bool m_initOutput;
        private double m_potMaxAngle;
        private double m_potMaxVoltage = 5.0;
        private readonly double m_defaultPotMaxAngle;

        public FakePotentiometerSource(AnalogOutput output, double defaultPotMaxAngle)
        {
            m_defaultPotMaxAngle = defaultPotMaxAngle;
            m_potMaxAngle = defaultPotMaxAngle;
            m_output = output;
            m_initOutput = false;
        }

        public FakePotentiometerSource(int port, double defaultPotMaxAngle)
            : this(new AnalogOutput(port), defaultPotMaxAngle)
        {
            m_initOutput = true;
        }

        public void SetMaxVoltage(double voltage)
        {
            m_potMaxVoltage = voltage;
        }

        public void SetRange(double range)
        {
            m_potMaxAngle = range;
        }

        public void Reset()
        {
            m_potMaxAngle = m_defaultPotMaxAngle;
            m_output.SetVoltage(0.0);
        }

        public void SetAngle(double angle)
        {
            m_output.SetVoltage(m_potMaxVoltage / m_potMaxAngle * angle);
        }

        public void SetVoltage(double voltage)
        {
            m_output.SetVoltage(voltage);
        }

        public double GetVoltage()
        {
            return m_output.GetVoltage();
        }

        public double GetAngle()
        {
            double voltage = m_output.GetVoltage();
            if (voltage == 0)
            {
                return 0;
            }
            return voltage * (m_potMaxAngle / m_potMaxVoltage);
        }

        public void Dispose()
        {
            if (m_initOutput)
            {
                m_output.Dispose();
                m_output = null;
                m_initOutput = false;
            }
        }
    }
}
