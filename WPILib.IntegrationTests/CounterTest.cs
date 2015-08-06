using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WPILib.IntegrationTests.Fixtures;
using WPILib.IntegrationTests.Test;

namespace WPILib.IntegrationTests
{
    [TestFixture(TestBench.DIOCrossConnectA1, TestBench.DIOCrossConnectA2)]
    [TestFixture(TestBench.DIOCrossConnectA2, TestBench.DIOCrossConnectA1)]
    [TestFixture(TestBench.DIOCrossConnectB1, TestBench.DIOCrossConnectB2)]
    [TestFixture(TestBench.DIOCrossConnectB2, TestBench.DIOCrossConnectB1)]
    public class CounterTest : AbstractComsSetup
    {
        private static FakeCounterFixture counter = null;

        private int input;
        private int output;

        public CounterTest(int input, int output)
        {
            this.input = input;
            this.output = output;
            counter?.Teardown();
            counter = new FakeCounterFixture(input, output);
        }

        [TestFixtureTearDown]
        public static void TearDownAfterClass()
        {
            counter.Teardown();
            counter = null;
        }

        [SetUp]
        public void Setup()
        {
            counter.Setup();
            counter.Reset();
        }

        [Test]
        public void TestDefault()
        {
            Assert.AreEqual(0, counter.GetCounter().Get(), "Counter did not reset to 0");
        }

        [Test, Timeout(5000)]
        public void TestCount()
        {
            int goal = 100;
            counter.GetFakeCounterSource().SetCount(goal);
            counter.GetFakeCounterSource().Execute();

            int count = counter.GetCounter().Get();

            Assert.AreEqual(goal, count, $"Fake Counter, Input: {input}, Output: {output}, did not return {goal} instead got: {count}");
        }
    }
}
