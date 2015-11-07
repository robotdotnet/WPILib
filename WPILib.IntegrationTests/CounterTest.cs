using NUnit.Framework;
using WPILib.IntegrationTests.Fixtures;
using WPILib.IntegrationTests.Test;

namespace WPILib.IntegrationTests
{
    [TestFixture(TestBench.DioCrossConnectA1, TestBench.DioCrossConnectA2)]
    [TestFixture(TestBench.DioCrossConnectA2, TestBench.DioCrossConnectA1)]
    [TestFixture(TestBench.DioCrossConnectB1, TestBench.DioCrossConnectB2)]
    [TestFixture(TestBench.DioCrossConnectB2, TestBench.DioCrossConnectB1)]
    public class CounterTest : AbstractComsSetup
    {
        private static FakeCounterFixture s_counter;

        private readonly int m_input;
        private readonly int m_output;

        public CounterTest(int input, int output)
        {
            m_input = input;
            m_output = output;
            s_counter?.Teardown();
            s_counter = new FakeCounterFixture(input, output);
        }

        [TestFixtureTearDown]
        public static void TearDownAfterClass()
        {
            s_counter.Teardown();
            s_counter = null;
        }

        [SetUp]
        public void Setup()
        {
            s_counter.Setup();
            s_counter.Reset();
        }

        [Test]
        public void TestDefault()
        {
            Assert.AreEqual(0, s_counter.GetCounter().Get(), "Counter did not reset to 0");
        }

        [Test, Timeout(5000)]
        public void TestCount()
        {
            int goal = 100;
            s_counter.GetFakeCounterSource().SetCount(goal);
            s_counter.GetFakeCounterSource().Execute();

            int count = s_counter.GetCounter().Get();

            Assert.AreEqual(goal, count, $"Fake Counter, Input: {m_input}, Output: {m_output}, did not return {goal} instead got: {count}");
        }
    }
}
