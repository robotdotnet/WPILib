using WPILib.IntegrationTests.MockHardware;

namespace WPILib.IntegrationTests.Fixtures
{
    public class FakeCounterFixture : ITestFixture
    {
        private readonly DIOCrossConnectFixture dio;
        private bool m_allocated;
        private readonly FakeCounterSource source;
        private readonly Counter counter;

        public FakeCounterFixture(DIOCrossConnectFixture dio)
        {
            this.dio = dio;
            m_allocated = false;
            source = new FakeCounterSource(dio.GetOutput());
            counter = new Counter(dio.GetInput());
        }

        public FakeCounterFixture(int input, int output)
        {
            this.dio = new DIOCrossConnectFixture(input, output);
            m_allocated = true;
            source = new FakeCounterSource(dio.GetOutput());
            counter = new Counter(dio.GetInput());
        }

        public FakeCounterSource GetFakeCounterSource()
        {
            return source;
        }

        public Counter GetCounter()
        {
            return counter;
        }


        public bool Setup()
        {
            return true;
        }

        public bool Reset()
        {
            counter.Reset();
            return true;
        }

        public bool Teardown()
        {
            counter.Dispose();
            source.Dispose();
            if (m_allocated)
            {
                dio.Teardown();
                m_allocated = false;
            }
            return true;

        }
    }
}
