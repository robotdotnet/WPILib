using System;
using NUnit.Framework;

namespace WPILib.IntegrationTests.Fixtures
{
    public class DioCrossConnectFixture : ITestFixture
    {
        private readonly DigitalInput m_input;
        private readonly DigitalOutput m_output;
        private bool m_allocated;

        public DioCrossConnectFixture(DigitalInput input, DigitalOutput output)
        {
            Assert.NotNull(input);
            Assert.NotNull(output);
            m_input = input;
            m_output = output;
            m_allocated = false;
        }

        public DioCrossConnectFixture(int input, int output)
        {
            Assert.AreNotEqual(input, output);
            m_input = new DigitalInput(input);
            m_output = new DigitalOutput(output);
            m_allocated = true;
        }

        public DigitalInput GetInput()
        {
            return m_input;
        }

        public DigitalOutput GetOutput()
        {
            return m_output;
        }

        public bool Setup()
        {
            return true;
        }

        public bool Reset()
        {
            try
            {
                m_input.CancelInterrupts();
            }
            catch (InvalidOperationException)
            {

            }
            m_output.Set(false);
            return true;
        }

        public bool Teardown()
        {
            if (m_allocated)
            {
                m_input.Dispose();
                m_output.Dispose();
                m_allocated = false;
            }
            return true;
        }
    }
}
