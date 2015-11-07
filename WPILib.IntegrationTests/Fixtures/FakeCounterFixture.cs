using WPILib.IntegrationTests.MockHardware;

namespace WPILib.IntegrationTests.Fixtures
{
    public class FakeCounterFixture : ITestFixture
    {
        private readonly DioCrossConnectFixture m_dio;
        private bool m_allocated;
        private readonly FakeCounterSource m_source;
        private readonly Counter m_counter;

        public FakeCounterFixture(DioCrossConnectFixture dio)
        {
            m_dio = dio;
            m_allocated = false;
            m_source = new FakeCounterSource(dio.GetOutput());
            m_counter = new Counter(dio.GetInput());
        }

        public FakeCounterFixture(int input, int output)
        {
            m_dio = new DioCrossConnectFixture(input, output);
            m_allocated = true;
            m_source = new FakeCounterSource(m_dio.GetOutput());
            m_counter = new Counter(m_dio.GetInput());
        }

        public FakeCounterSource GetFakeCounterSource()
        {
            return m_source;
        }

        public Counter GetCounter()
        {
            return m_counter;
        }


        public bool Setup()
        {
            return true;
        }

        public bool Reset()
        {
            m_counter.Reset();
            return true;
        }

        public bool Teardown()
        {
            m_counter.Dispose();
            m_source.Dispose();
            if (m_allocated)
            {
                m_dio.Teardown();
                m_allocated = false;
            }
            return true;

        }
    }
}
