using System;
using NUnit.Framework;

namespace WPILib.IntegrationTests.Fixtures
{
    public class DIOCrossConnectFixture : ITestFixture
    {
        private readonly DigitalInput input;
        private readonly DigitalOutput output;
        private bool m_allocated;

        public DIOCrossConnectFixture(DigitalInput input, DigitalOutput output)
        {
            Assert.NotNull(input);
            Assert.NotNull(output);
            this.input = input;
            this.output = output;
            m_allocated = false;
        }

        public DIOCrossConnectFixture(int input, int output)
        {
            Assert.AreNotEqual(input, output);
            this.input = new DigitalInput(input);
            this.output = new DigitalOutput(output);
            m_allocated = true;
        }

        public DigitalInput GetInput()
        {
            return input;
        }

        public DigitalOutput GetOutput()
        {
            return output;
        }

        public bool Setup()
        {
            return true;
        }

        public bool Reset()
        {
            try
            {
                input.CancelInterrupts();
            }
            catch (InvalidOperationException)
            {

            }
            output.Set(false);
            return true;
        }

        public bool Teardown()
        {
            if (m_allocated)
            {
                input.Dispose();
                output.Dispose();
                m_allocated = false;
            }
            return true;
        }
    }
}
