using System;

namespace WPILib.IntegrationTests.Fixtures
{
    public abstract class RelayCrossConnectFixture : ITestFixture
    {
        private DigitalInput m_inputOne;
        private DigitalInput m_inputTwo;
        private Relay m_relay;

        private bool m_initialized;
        private bool m_freed;



        protected abstract Relay GiveRelay();

        protected abstract DigitalInput GiveInputOne();

        protected abstract DigitalInput GiveInputTwo();

        private void Initialize()
        {
            lock (this)
            {
                if (!m_initialized)
                {
                    m_relay = GiveRelay();
                    m_inputOne = GiveInputOne();
                    m_inputTwo = GiveInputTwo();
                    m_initialized = true;
                }
            }
        }

        public Relay GetRelay()
        {
            Initialize();
            return m_relay;
        }

        public DigitalInput GetInputOne()
        {
            Initialize();
            return m_inputOne;
        }

        public DigitalInput GetInputTwo()
        {
            Initialize();
            return m_inputTwo;
        }

        public bool Setup()
        {
            Initialize();
            return true;
        }

        public bool Reset()
        {
            Initialize();
            return true;
        }

        public bool Teardown()
        {
            if (!m_freed)
            {
                m_relay.Dispose();
                m_inputOne.Dispose();
                m_inputTwo.Dispose();
                m_freed = true;
            }
            else
            {
                throw new SystemException($"You attempted to free the {nameof(RelayCrossConnectFixture)} multiple times");
            }
            return true;
        }
    }
}
