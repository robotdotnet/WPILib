namespace WPILib.IntegrationTests.Fixtures
{
    public abstract class AnalogCrossConnectFixture : ITestFixture
    {
        private bool m_initialized;

        private AnalogInput m_input;
        private AnalogOutput m_output;

        internal abstract AnalogInput GiveAnalogInput();

        internal abstract AnalogOutput GiveAnalogOutput();

        private void Initialize()
        {
            lock (this)
            {
                if (!m_initialized)
                {
                    m_input = GiveAnalogInput();
                    m_output = GiveAnalogOutput();
                    m_initialized = true;
                }
            }
        }


        public bool Setup()
        {
            Initialize();
            m_output.SetVoltage(0);
            return true;
        }

        public bool Reset()
        {
            Initialize();
            return true;
        }

        public bool Teardown()
        {
            m_input.Dispose();
            m_output.Dispose();
            return true;
        }

        public AnalogOutput GetOutput()
        {
            Initialize();
            return m_output;
        }

        public AnalogInput GetInput()
        {
            Initialize();
            return m_input;
        }
    }
}
