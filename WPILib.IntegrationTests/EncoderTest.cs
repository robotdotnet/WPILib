using NUnit.Framework;
using WPILib.IntegrationTests.Fixtures;
using WPILib.IntegrationTests.Test;
using WPILib.Interfaces;

namespace WPILib.IntegrationTests
{
    [TestFixture(TestBench.DioCrossConnectA1, TestBench.DioCrossConnectA2, TestBench.DioCrossConnectB2, TestBench.DioCrossConnectB1, EncodingType.K4X)]
    [TestFixture(TestBench.DioCrossConnectA1, TestBench.DioCrossConnectA2, TestBench.DioCrossConnectB1, TestBench.DioCrossConnectB2, EncodingType.K4X)]
    [TestFixture(TestBench.DioCrossConnectA2, TestBench.DioCrossConnectA1, TestBench.DioCrossConnectB2, TestBench.DioCrossConnectB1, EncodingType.K4X)]
    [TestFixture(TestBench.DioCrossConnectA2, TestBench.DioCrossConnectA1, TestBench.DioCrossConnectB1, TestBench.DioCrossConnectB2, EncodingType.K4X)]
    [TestFixture(TestBench.DioCrossConnectB1, TestBench.DioCrossConnectB2, TestBench.DioCrossConnectA2, TestBench.DioCrossConnectA1, EncodingType.K4X)]
    [TestFixture(TestBench.DioCrossConnectB1, TestBench.DioCrossConnectB2, TestBench.DioCrossConnectA1, TestBench.DioCrossConnectA2, EncodingType.K4X)]
    [TestFixture(TestBench.DioCrossConnectB2, TestBench.DioCrossConnectB1, TestBench.DioCrossConnectA2, TestBench.DioCrossConnectA1, EncodingType.K4X)]
    [TestFixture(TestBench.DioCrossConnectB2, TestBench.DioCrossConnectB1, TestBench.DioCrossConnectA1, TestBench.DioCrossConnectA2, EncodingType.K4X)]
    [TestFixture(TestBench.DioCrossConnectA1, TestBench.DioCrossConnectA2, TestBench.DioCrossConnectB2, TestBench.DioCrossConnectB1, EncodingType.K2X)]
    [TestFixture(TestBench.DioCrossConnectA1, TestBench.DioCrossConnectA2, TestBench.DioCrossConnectB1, TestBench.DioCrossConnectB2, EncodingType.K2X)]
    [TestFixture(TestBench.DioCrossConnectA2, TestBench.DioCrossConnectA1, TestBench.DioCrossConnectB2, TestBench.DioCrossConnectB1, EncodingType.K2X)]
    [TestFixture(TestBench.DioCrossConnectA2, TestBench.DioCrossConnectA1, TestBench.DioCrossConnectB1, TestBench.DioCrossConnectB2, EncodingType.K2X)]
    [TestFixture(TestBench.DioCrossConnectB1, TestBench.DioCrossConnectB2, TestBench.DioCrossConnectA2, TestBench.DioCrossConnectA1, EncodingType.K2X)]
    [TestFixture(TestBench.DioCrossConnectB1, TestBench.DioCrossConnectB2, TestBench.DioCrossConnectA1, TestBench.DioCrossConnectA2, EncodingType.K2X)]
    [TestFixture(TestBench.DioCrossConnectB2, TestBench.DioCrossConnectB1, TestBench.DioCrossConnectA2, TestBench.DioCrossConnectA1, EncodingType.K2X)]
    [TestFixture(TestBench.DioCrossConnectB2, TestBench.DioCrossConnectB1, TestBench.DioCrossConnectA1, TestBench.DioCrossConnectA2, EncodingType.K2X)]
    [TestFixture(TestBench.DioCrossConnectA1, TestBench.DioCrossConnectA2, TestBench.DioCrossConnectB2, TestBench.DioCrossConnectB1, EncodingType.K1X)]
    [TestFixture(TestBench.DioCrossConnectA1, TestBench.DioCrossConnectA2, TestBench.DioCrossConnectB1, TestBench.DioCrossConnectB2, EncodingType.K1X)]
    [TestFixture(TestBench.DioCrossConnectA2, TestBench.DioCrossConnectA1, TestBench.DioCrossConnectB2, TestBench.DioCrossConnectB1, EncodingType.K1X)]
    [TestFixture(TestBench.DioCrossConnectA2, TestBench.DioCrossConnectA1, TestBench.DioCrossConnectB1, TestBench.DioCrossConnectB2, EncodingType.K1X)]
    [TestFixture(TestBench.DioCrossConnectB1, TestBench.DioCrossConnectB2, TestBench.DioCrossConnectA2, TestBench.DioCrossConnectA1, EncodingType.K1X)]
    [TestFixture(TestBench.DioCrossConnectB1, TestBench.DioCrossConnectB2, TestBench.DioCrossConnectA1, TestBench.DioCrossConnectA2, EncodingType.K1X)]
    [TestFixture(TestBench.DioCrossConnectB2, TestBench.DioCrossConnectB1, TestBench.DioCrossConnectA2, TestBench.DioCrossConnectA1, EncodingType.K1X)]
    [TestFixture(TestBench.DioCrossConnectB2, TestBench.DioCrossConnectB1, TestBench.DioCrossConnectA1, TestBench.DioCrossConnectA2, EncodingType.K1X)]
    public class EncoderTest
    {
        private static FakeEncoderFixture s_encoder;

        private readonly bool m_flip = false; //Does this test need to flip the inputs
        private readonly int m_inputA;
        private readonly int m_inputB;
        private readonly int m_outputA;
        private readonly int m_outputB;

        public EncoderTest(int inputA, int outputA, int inputB, int outputB, EncodingType type)
        {
            m_inputA = inputA;
            m_inputB = inputB;
            m_outputA = outputA;
            m_outputB = outputB;

            //If the encoder from a previous test is allocated then we must free its members
            s_encoder?.Teardown();
            //this.flip = flip == 0;
            s_encoder = new FakeEncoderFixture(inputA, outputA, inputB, outputB, type);
        }

        [TestFixtureSetUp]
        public static void SetUpBeforeClass()
        {
            //TODO: Get mock encoders working in sim
            if (RobotBase.IsSimulation)
            {
                Assert.Ignore();
            }
        }

        [TestFixtureTearDown]
        public static void TearDownAfterClass()
        {
            s_encoder.Teardown();
            s_encoder = null;
        }

        [SetUp]
        public void Setup()
        {
            s_encoder.Setup();
            TestDefaultState();
        }

        [TearDown]
        public void TearDown()
        {
            s_encoder.Reset();
        }
        //TODO: Fix
        [Test]
        public void TestDefaultState()
        {
            int value = s_encoder.GetEncoder().Get();
            Assert.AreEqual(0, value, ErrorMessage(0, value));
        }

        [Test]
        public void TestCountUp()
        {
            int goal = 100;
            s_encoder.GetFakeEncoderSource().SetCount(goal);
            s_encoder.GetFakeEncoderSource().SetForward(m_flip);
            s_encoder.GetFakeEncoderSource().Execute();
            int value = s_encoder.GetEncoder().Get();
            Assert.AreEqual(goal, value, ErrorMessage(goal, value));

        }

        [Test]
        public void TestCountDown()
        {
            int goal = -100;
            s_encoder.GetFakeEncoderSource().SetCount(goal);
            s_encoder.GetFakeEncoderSource().SetForward(!m_flip);
            s_encoder.GetFakeEncoderSource().Execute();
            int value = s_encoder.GetEncoder().Get();
            Assert.AreEqual(goal, value, ErrorMessage(goal, value));

        }

        private string ErrorMessage(int goal, int trueValue)
        {
            return "Encoder ({In,Out}): {" + m_inputA + ", " + m_outputA + "},{" + m_inputB + ", " + m_outputB + "} Returned: " + trueValue + ", Wanted: " + goal;

        }
    }
}
