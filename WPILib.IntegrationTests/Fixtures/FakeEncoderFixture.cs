using NUnit.Framework;
using WPILib.IntegrationTests.MockHardware;
using WPILib.Interfaces;

namespace WPILib.IntegrationTests.Fixtures
{
    class FakeEncoderFixture : ITestFixture
    {
        private readonly DioCrossConnectFixture m_dio1;
        private readonly DioCrossConnectFixture m_dio2;
        private bool m_allocated;

        private readonly FakeEncoderSource m_source;

        private readonly int[] m_sourcePort = new int[2];
        private readonly Encoder m_encoder;
        private readonly int[] m_encoderPort = new int[2];


        public FakeEncoderFixture(DioCrossConnectFixture dio1, DioCrossConnectFixture dio2)
        {
            Assert.NotNull(dio1);
            Assert.NotNull(dio2);
            m_dio1 = dio1;
            m_dio2 = dio2;
            m_allocated = false;
            m_source = new FakeEncoderSource(dio1.GetOutput(), dio2.GetOutput());
            m_encoder = new Encoder(dio1.GetInput(), dio2.GetInput());
        }

        public FakeEncoderFixture(int inputA, int outputA, int inputB, int outputB, EncodingType type)
        {
            Assert.AreNotEqual(outputA, outputB);
            Assert.AreNotEqual(outputA, inputA);
            Assert.AreNotEqual(outputA, inputB);
            Assert.AreNotEqual(outputB, inputA);
            Assert.AreNotEqual(outputB, inputB);
            Assert.AreNotEqual(inputA, inputB);
            m_dio1 = new DioCrossConnectFixture(inputA, outputA);
            m_dio2 = new DioCrossConnectFixture(inputB, outputB);
            m_allocated = true;
            m_sourcePort[0] = outputA;
            m_sourcePort[1] = outputB;
            m_encoderPort[0] = inputA;
            m_encoderPort[1] = inputB;
            m_source = new FakeEncoderSource(m_dio1.GetOutput(), m_dio2.GetOutput());
            m_encoder = new Encoder(m_dio1.GetInput(), m_dio2.GetInput(), false, type);
        }

        public FakeEncoderSource GetFakeEncoderSource()
        {
            return m_source;
        }

        public Encoder GetEncoder()
        {
            return m_encoder;
        }


        public bool Setup()
        {
            return true;
        }

        public bool Reset()
        {
            m_dio1.Reset();
            m_dio2.Reset();
            m_encoder.Reset();
            return true;
        }

        public bool Teardown()
        {
            m_source.Dispose();
            m_encoder.Dispose();
            if (m_allocated)
            {
                m_dio1.Teardown();
                m_dio2.Teardown();
                m_allocated = false;
            }
            return true;

        }
    }
}
