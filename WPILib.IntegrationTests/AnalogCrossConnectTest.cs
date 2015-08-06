using HAL_Base;
using NUnit.Framework;
using WPILib.Exceptions;
using WPILib.IntegrationTests.Fixtures;
using WPILib.IntegrationTests.Test;

namespace WPILib.IntegrationTests
{
    [TestFixture]
    public class AnalogCrossConnectTest : AbstractInterruptTest
    {
        private static AnalogCrossConnectFixture analogIO;

        private const double DelayTime = 0.01;

        [TestFixtureSetUp]
        public static void SetUpBeforeClass()
        {
            analogIO = TestBench.GetAnalogCrossConnectFixture();
        }

        [TestFixtureTearDown]
        public static void TearDownAfterClass()
        {
            analogIO.Teardown();
            analogIO = null;
        }

        [SetUp]
        public void SetUp()
        {
            analogIO.Setup();
        }

        [Test]
        public void TestAnalogOutput()
        {
            for (int i = 0; i < 50; i++)
            {
                analogIO.GetOutput().SetVoltage(i / 10.0f);
                Timer.Delay(DelayTime);
                Assert.AreEqual(analogIO.GetOutput().GetVoltage(), analogIO.GetInput().GetVoltage(), 0.01, $"Failed on {i}");
            }
        }

        [Test]
        public void TestAnalogTriggerBelowWindow()
        {
            AnalogTrigger trigger = new AnalogTrigger(analogIO.GetInput());
            trigger.SetLimitsVoltage(2.0f, 3.0f);

            analogIO.GetOutput().SetVoltage(1.0f);
            Timer.Delay(DelayTime);

            Assert.IsFalse(trigger.InWindow, "Analog Trigger is in the window (2V, 3V)");
            Assert.IsFalse(trigger.TriggerState, "Analog trigger is on");

            trigger.Dispose();
        }

        [Test]
        public void TestAnalogTriggerInWindow()
        {
            AnalogTrigger trigger = new AnalogTrigger(analogIO.GetInput());
            trigger.SetLimitsVoltage(2.0f, 3.0f);

            analogIO.GetOutput().SetVoltage(2.5f);
            Timer.Delay(DelayTime);

            Assert.IsTrue(trigger.InWindow, "Analog Trigger is not in the window (2V, 3V)");
            Assert.IsFalse(trigger.TriggerState, "Analog trigger is on");

            trigger.Dispose();
        }

        [Test]
        public void TestAnalogTriggerAboveWindow()
        {
            AnalogTrigger trigger = new AnalogTrigger(analogIO.GetInput());
            trigger.SetLimitsVoltage(2.0f, 3.0f);

            analogIO.GetOutput().SetVoltage(4.0f);
            Timer.Delay(DelayTime);

            Assert.IsFalse(trigger.InWindow, "Analog Trigger is in the window (2V, 3V)");
            Assert.IsTrue(trigger.TriggerState, "Analog trigger is not on");

            trigger.Dispose();
        }

        [Test]
        public void TestAnalogTriggerCounter()
        {
            AnalogTrigger trigger = new AnalogTrigger(analogIO.GetInput());
            trigger.SetLimitsVoltage(2.0f, 3.0f);
            Counter counter = new Counter(trigger);

            for (int i = 0; i < 50; i++)
            {
                analogIO.GetOutput().SetVoltage(1.0);
                Timer.Delay(DelayTime);
                analogIO.GetOutput().SetVoltage(4.0);
                Timer.Delay(DelayTime);
            }

            Assert.AreEqual(50, counter.Get(), "Analog trigger counter did not count 50 ticks");

            counter.Dispose();
            trigger.Dispose();
        }

        [Test]
        public void TestExceptionOnInvalidAccumulatorPort()
        {
            Assert.Throws<UncleanStatusException>(() => analogIO.GetInput().GetAccumulatorCount());
        }

        private AnalogTrigger interruptTrigger;
        private AnalogTriggerOutput interruptTriggerOutput;


        internal override InterruptableSensorBase GiveInterruptableSensorBase()
        {
            interruptTrigger = new AnalogTrigger(analogIO.GetInput());
            interruptTrigger.SetLimitsVoltage(2.0f, 3.0f);
            interruptTriggerOutput = new AnalogTriggerOutput(interruptTrigger, AnalogTriggerType.State);
            return interruptTriggerOutput;
        }

        internal override void FreeInterruptableSensorBase()
        {
            interruptTriggerOutput.CancelInterrupts();
            interruptTriggerOutput.Dispose();
            interruptTriggerOutput = null;
            interruptTrigger.Dispose();
            interruptTrigger = null;
        }

        internal override void SetInterruptHigh()
        {
            analogIO.GetOutput().SetVoltage(4.0);
            Timer.Delay(0.005);
        }

        internal override void SetInterruptLow()
        {
            analogIO.GetOutput().SetVoltage(1.0);
            Timer.Delay(0.005);
        }
    }
}
