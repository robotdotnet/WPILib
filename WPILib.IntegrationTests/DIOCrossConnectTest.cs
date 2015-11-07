using NUnit.Framework;
using WPILib.IntegrationTests.Fixtures;
using WPILib.IntegrationTests.Test;

namespace WPILib.IntegrationTests
{
    [TestFixture(TestBench.DioCrossConnectA1, TestBench.DioCrossConnectA2)]
    [TestFixture(TestBench.DioCrossConnectA2, TestBench.DioCrossConnectA1)]
    [TestFixture(TestBench.DioCrossConnectB1, TestBench.DioCrossConnectB2)]
    [TestFixture(TestBench.DioCrossConnectB2, TestBench.DioCrossConnectB1)]
    public class DioCrossConnectTest : AbstractInterruptTest
    {
        private static DioCrossConnectFixture s_dio;


        public DioCrossConnectTest(int input, int output)
        {
            s_dio?.Teardown();
            s_dio = new DioCrossConnectFixture(input, output);
        }

        [TestFixtureTearDown]
        public static void TearDownAfterClass()
        {
            s_dio.Teardown();
            s_dio = null;
        }

        [TearDown]
        public void TearDown()
        {
            s_dio.Reset();
        }

        [Test]
        public void TestSetHigh()
        {
            s_dio.GetOutput().Set(true);
            Assert.IsTrue(s_dio.GetInput().Get(), "DIO Not High after no delay");
            Timer.Delay(0.02);
            Assert.IsTrue(s_dio.GetInput().Get(), "DIO Not High after 0.05s delay");
        }

        [Test]
        public void TestSetLow()
        {
            s_dio.GetOutput().Set(false);
            Assert.IsFalse(s_dio.GetInput().Get(), "DIO Not Low after no delay");
            Timer.Delay(0.02);
            Assert.IsFalse(s_dio.GetInput().Get(), "DIO Not Low after 0.05s delay");
        }

        internal override InterruptableSensorBase GiveInterruptableSensorBase()
        {
            return s_dio.GetInput();
        }

        internal override void FreeInterruptableSensorBase()
        {

        }

        internal override void SetInterruptHigh()
        {
            s_dio.GetOutput().Set(true);
        }

        internal override void SetInterruptLow()
        {
            s_dio.GetOutput().Set(false);
        }
    }
}
