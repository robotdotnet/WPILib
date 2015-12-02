using HAL;
using NUnit.Framework;
using WPILib.Exceptions;
using WPILib.IntegrationTests.Fixtures;
using WPILib.IntegrationTests.Test;

namespace WPILib.IntegrationTests
{
    [TestFixture]
    public class AnalogCrossConnectTest : AbstractInterruptTest
    {
        private static AnalogCrossConnectFixture s_analogIo;

        private const double DelayTime = 0.01;

        [TestFixtureSetUp]
        public static void SetUpBeforeClass()
        {
            s_analogIo = TestBench.GetAnalogCrossConnectFixture();
        }

        [TestFixtureTearDown]
        public static void TearDownAfterClass()
        {
            s_analogIo.Teardown();
            s_analogIo = null;
        }

        [SetUp]
        public void SetUp()
        {
            s_analogIo.Setup();
        }

        [Test]
        public void TestAnalogOutput()
        {
            for (int i = 0; i < 50; i++)
            {
                s_analogIo.GetOutput().SetVoltage(i / 10.0f);
                Timer.Delay(DelayTime);
                Assert.AreEqual(s_analogIo.GetOutput().GetVoltage(), s_analogIo.GetInput().GetVoltage(), 0.01, $"Failed on {i}");
            }
        }

        [Test]
        public void TestAnalogTriggerBelowWindow()
        {
            AnalogTrigger trigger = new AnalogTrigger(s_analogIo.GetInput());
            trigger.SetLimitsVoltage(2.0f, 3.0f);

            s_analogIo.GetOutput().SetVoltage(1.0f);
            Timer.Delay(DelayTime);

            Assert.IsFalse(trigger.InWindow, "Analog Trigger is in the window (2V, 3V)");
            Assert.IsFalse(trigger.TriggerState, "Analog trigger is on");

            trigger.Dispose();
        }

        [Test]
        public void TestAnalogTriggerInWindow()
        {
            AnalogTrigger trigger = new AnalogTrigger(s_analogIo.GetInput());
            trigger.SetLimitsVoltage(2.0f, 3.0f);

            s_analogIo.GetOutput().SetVoltage(2.5f);
            Timer.Delay(DelayTime);

            Assert.IsTrue(trigger.InWindow, "Analog Trigger is not in the window (2V, 3V)");
            //Not checking for state, because state will be whichever state it was in last.

            trigger.Dispose();
        }

        [Test]
        public void TestAnalogTriggerAboveWindow()
        {
            AnalogTrigger trigger = new AnalogTrigger(s_analogIo.GetInput());
            trigger.SetLimitsVoltage(2.0f, 3.0f);

            s_analogIo.GetOutput().SetVoltage(4.0f);
            Timer.Delay(DelayTime);

            Assert.IsFalse(trigger.InWindow, "Analog Trigger is in the window (2V, 3V)");
            Assert.IsTrue(trigger.TriggerState, "Analog trigger is not on");

            trigger.Dispose();
        }

        [Test]
        public void TestAnalogTriggerCounter()
        {
            AnalogTrigger trigger = new AnalogTrigger(s_analogIo.GetInput());
            trigger.SetLimitsVoltage(2.0f, 3.0f);
            Counter counter = new Counter(trigger);

            for (int i = 0; i < 50; i++)
            {
                s_analogIo.GetOutput().SetVoltage(1.0);
                Timer.Delay(DelayTime);
                s_analogIo.GetOutput().SetVoltage(4.0);
                Timer.Delay(DelayTime);
            }

            Assert.AreEqual(50, counter.Get(), "Analog trigger counter did not count 50 ticks");

            counter.Dispose();
            trigger.Dispose();
        }

        [Test]
        public void TestExceptionOnInvalidAccumulatorPort()
        {
            Assert.Throws<UncleanStatusException>(() => s_analogIo.GetInput().GetAccumulatorCount());
        }

        private AnalogTrigger m_interruptTrigger;
        private AnalogTriggerOutput m_interruptTriggerOutput;


        internal override InterruptableSensorBase GiveInterruptableSensorBase()
        {
            m_interruptTrigger = new AnalogTrigger(s_analogIo.GetInput());
            m_interruptTrigger.SetLimitsVoltage(2.0f, 3.0f);
            m_interruptTriggerOutput = new AnalogTriggerOutput(m_interruptTrigger, AnalogTriggerType.State);
            return m_interruptTriggerOutput;
        }

        internal override void FreeInterruptableSensorBase()
        {
            m_interruptTriggerOutput.CancelInterrupts();
            m_interruptTriggerOutput.Dispose();
            m_interruptTriggerOutput = null;
            m_interruptTrigger.Dispose();
            m_interruptTrigger = null;
        }

        internal override void SetInterruptHigh()
        {
            s_analogIo.GetOutput().SetVoltage(4.0);
            Timer.Delay(0.005);
        }

        internal override void SetInterruptLow()
        {
            s_analogIo.GetOutput().SetVoltage(1.0);
            Timer.Delay(0.005);
        }
    }
}
