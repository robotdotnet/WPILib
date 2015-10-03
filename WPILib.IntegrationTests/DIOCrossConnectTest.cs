using NUnit.Framework;
using WPILib.IntegrationTests.Fixtures;
using WPILib.IntegrationTests.Test;

namespace WPILib.IntegrationTests
{
    [TestFixture(TestBench.DIOCrossConnectA1, TestBench.DIOCrossConnectA2)]
    [TestFixture(TestBench.DIOCrossConnectA2, TestBench.DIOCrossConnectA1)]
    [TestFixture(TestBench.DIOCrossConnectB1, TestBench.DIOCrossConnectB2)]
    [TestFixture(TestBench.DIOCrossConnectB2, TestBench.DIOCrossConnectB1)]
    public class DIOCrossConnectTest : AbstractInterruptTest
    {
        private static DIOCrossConnectFixture dio = null;


        public DIOCrossConnectTest(int input, int output)
        {
            dio?.Teardown();
            dio = new DIOCrossConnectFixture(input, output);
        }

        [TestFixtureTearDown]
        public static void TearDownAfterClass()
        {
            dio.Teardown();
            dio = null;
        }

        [TearDown]
        public void TearDown()
        {
            dio.Reset();
        }

        [Test]
        public void TestSetHigh()
        {
            dio.GetOutput().Set(true);
            Assert.IsTrue(dio.GetInput().Get(), "DIO Not High after no delay");
            Timer.Delay(0.02);
            Assert.IsTrue(dio.GetInput().Get(), "DIO Not High after 0.05s delay");
        }

        [Test]
        public void TestSetLow()
        {
            dio.GetOutput().Set(false);
            Assert.IsFalse(dio.GetInput().Get(), "DIO Not Low after no delay");
            Timer.Delay(0.02);
            Assert.IsFalse(dio.GetInput().Get(), "DIO Not Low after 0.05s delay");
        }

        internal override InterruptableSensorBase GiveInterruptableSensorBase()
        {
            return dio.GetInput();
        }

        internal override void FreeInterruptableSensorBase()
        {

        }

        internal override void SetInterruptHigh()
        {
            dio.GetOutput().Set(true);
        }

        internal override void SetInterruptLow()
        {
            dio.GetOutput().Set(false);
        }
    }
}
