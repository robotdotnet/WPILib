using NUnit.Framework;
using WPILib.IntegrationTests.Fixtures;
using WPILib.IntegrationTests.Test;
using WPILib.Interfaces;

namespace WPILib.IntegrationTests
{
    [TestFixture(TestBench.DIOCrossConnectA1, TestBench.DIOCrossConnectA2, TestBench.DIOCrossConnectB2, TestBench.DIOCrossConnectB1, EncodingType.K4X)]
    [TestFixture(TestBench.DIOCrossConnectA1, TestBench.DIOCrossConnectA2, TestBench.DIOCrossConnectB1, TestBench.DIOCrossConnectB2, EncodingType.K4X)]
    [TestFixture(TestBench.DIOCrossConnectA2, TestBench.DIOCrossConnectA1, TestBench.DIOCrossConnectB2, TestBench.DIOCrossConnectB1, EncodingType.K4X)]
    [TestFixture(TestBench.DIOCrossConnectA2, TestBench.DIOCrossConnectA1, TestBench.DIOCrossConnectB1, TestBench.DIOCrossConnectB2, EncodingType.K4X)]
    [TestFixture(TestBench.DIOCrossConnectB1, TestBench.DIOCrossConnectB2, TestBench.DIOCrossConnectA2, TestBench.DIOCrossConnectA1, EncodingType.K4X)]
    [TestFixture(TestBench.DIOCrossConnectB1, TestBench.DIOCrossConnectB2, TestBench.DIOCrossConnectA1, TestBench.DIOCrossConnectA2, EncodingType.K4X)]
    [TestFixture(TestBench.DIOCrossConnectB2, TestBench.DIOCrossConnectB1, TestBench.DIOCrossConnectA2, TestBench.DIOCrossConnectA1, EncodingType.K4X)]
    [TestFixture(TestBench.DIOCrossConnectB2, TestBench.DIOCrossConnectB1, TestBench.DIOCrossConnectA1, TestBench.DIOCrossConnectA2, EncodingType.K4X)]
    [TestFixture(TestBench.DIOCrossConnectA1, TestBench.DIOCrossConnectA2, TestBench.DIOCrossConnectB2, TestBench.DIOCrossConnectB1, EncodingType.K2X)]
    [TestFixture(TestBench.DIOCrossConnectA1, TestBench.DIOCrossConnectA2, TestBench.DIOCrossConnectB1, TestBench.DIOCrossConnectB2, EncodingType.K2X)]
    [TestFixture(TestBench.DIOCrossConnectA2, TestBench.DIOCrossConnectA1, TestBench.DIOCrossConnectB2, TestBench.DIOCrossConnectB1, EncodingType.K2X)]
    [TestFixture(TestBench.DIOCrossConnectA2, TestBench.DIOCrossConnectA1, TestBench.DIOCrossConnectB1, TestBench.DIOCrossConnectB2, EncodingType.K2X)]
    [TestFixture(TestBench.DIOCrossConnectB1, TestBench.DIOCrossConnectB2, TestBench.DIOCrossConnectA2, TestBench.DIOCrossConnectA1, EncodingType.K2X)]
    [TestFixture(TestBench.DIOCrossConnectB1, TestBench.DIOCrossConnectB2, TestBench.DIOCrossConnectA1, TestBench.DIOCrossConnectA2, EncodingType.K2X)]
    [TestFixture(TestBench.DIOCrossConnectB2, TestBench.DIOCrossConnectB1, TestBench.DIOCrossConnectA2, TestBench.DIOCrossConnectA1, EncodingType.K2X)]
    [TestFixture(TestBench.DIOCrossConnectB2, TestBench.DIOCrossConnectB1, TestBench.DIOCrossConnectA1, TestBench.DIOCrossConnectA2, EncodingType.K2X)]
    [TestFixture(TestBench.DIOCrossConnectA1, TestBench.DIOCrossConnectA2, TestBench.DIOCrossConnectB2, TestBench.DIOCrossConnectB1, EncodingType.K1X)]
    [TestFixture(TestBench.DIOCrossConnectA1, TestBench.DIOCrossConnectA2, TestBench.DIOCrossConnectB1, TestBench.DIOCrossConnectB2, EncodingType.K1X)]
    [TestFixture(TestBench.DIOCrossConnectA2, TestBench.DIOCrossConnectA1, TestBench.DIOCrossConnectB2, TestBench.DIOCrossConnectB1, EncodingType.K1X)]
    [TestFixture(TestBench.DIOCrossConnectA2, TestBench.DIOCrossConnectA1, TestBench.DIOCrossConnectB1, TestBench.DIOCrossConnectB2, EncodingType.K1X)]
    [TestFixture(TestBench.DIOCrossConnectB1, TestBench.DIOCrossConnectB2, TestBench.DIOCrossConnectA2, TestBench.DIOCrossConnectA1, EncodingType.K1X)]
    [TestFixture(TestBench.DIOCrossConnectB1, TestBench.DIOCrossConnectB2, TestBench.DIOCrossConnectA1, TestBench.DIOCrossConnectA2, EncodingType.K1X)]
    [TestFixture(TestBench.DIOCrossConnectB2, TestBench.DIOCrossConnectB1, TestBench.DIOCrossConnectA2, TestBench.DIOCrossConnectA1, EncodingType.K1X)]
    [TestFixture(TestBench.DIOCrossConnectB2, TestBench.DIOCrossConnectB1, TestBench.DIOCrossConnectA1, TestBench.DIOCrossConnectA2, EncodingType.K1X)]
    public class EncoderTest
    {
        private static FakeEncoderFixture encoder = null;

        private readonly bool flip = false; //Does this test need to flip the inputs
        private readonly int inputA;
        private readonly int inputB;
        private readonly int outputA;
        private readonly int outputB;

        public EncoderTest(int inputA, int outputA, int inputB, int outputB, EncodingType type)
        {
            this.inputA = inputA;
            this.inputB = inputB;
            this.outputA = outputA;
            this.outputB = outputB;

            //If the encoder from a previous test is allocated then we must free its members
            encoder?.Teardown();
            //this.flip = flip == 0;
            encoder = new FakeEncoderFixture(inputA, outputA, inputB, outputB, type);
        }

        [TestFixtureTearDown]
        public static void TearDownAfterClass()
        {
            encoder.Teardown();
            encoder = null;
        }

        [SetUp]
        public void Setup()
        {
            encoder.Setup();
            TestDefaultState();
        }

        [TearDown]
        public void TearDown()
        {
            encoder.Reset();
        }
        //TODO: Fix
        [Test]
        public void TestDefaultState()
        {
            int value = encoder.GetEncoder().Get();
            Assert.AreEqual(0, value, ErrorMessage(0, value));
        }

        [Test]
        public void TestCountUp()
        {
            int goal = 100;
            encoder.GetFakeEncoderSource().SetCount(goal);
            encoder.GetFakeEncoderSource().SetForward(flip);
            encoder.GetFakeEncoderSource().Execute();
            int value = encoder.GetEncoder().Get();
            Assert.AreEqual(goal, value, ErrorMessage(goal, value));

        }

        [Test]
        public void TestCountDown()
        {
            int goal = -100;
            encoder.GetFakeEncoderSource().SetCount(goal);
            encoder.GetFakeEncoderSource().SetForward(!flip);
            encoder.GetFakeEncoderSource().Execute();
            int value = encoder.GetEncoder().Get();
            Assert.AreEqual(goal, value, ErrorMessage(goal, value));

        }

        private string ErrorMessage(int goal, int trueValue)
        {
            return "Encoder ({In,Out}): {" + inputA + ", " + outputA + "},{" + inputB + ", " + outputB + "} Returned: " + trueValue + ", Wanted: " + goal;

        }
    }
}
