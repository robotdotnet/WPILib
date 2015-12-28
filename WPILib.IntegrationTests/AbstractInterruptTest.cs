using System.Threading;
using NUnit.Framework;
using WPILib.IntegrationTests.Test;
using static WPILib.Timer;

namespace WPILib.IntegrationTests
{
    public abstract class AbstractInterruptTest : AbstractComsSetup
    {
        private InterruptableSensorBase m_interrupable;

        private InterruptableSensorBase GetInterruptable()
        {
            return m_interrupable ?? (m_interrupable = GiveInterruptableSensorBase());
        }

        [TearDown]
        public void InterruptTeardown()
        {
            if (m_interrupable != null)
            {
                FreeInterruptableSensorBase();
                m_interrupable = null;
            }
        }

        internal abstract InterruptableSensorBase GiveInterruptableSensorBase();

        internal abstract void FreeInterruptableSensorBase();

        internal abstract void SetInterruptHigh();

        internal abstract void SetInterruptLow();

        internal class InterruptCounter
        {
            private int m_count;

            internal void Increment()
            {
                Interlocked.Increment(ref m_count);
            }

            internal int GetCount()
            {
                return m_count;
            }
        }

        internal class TestHandlerFunction
        {
            internal long InterruptFireTime;

            internal int InterruptComplete;

            private readonly InterruptCounter m_counter;

            internal TestHandlerFunction(InterruptCounter counter)
            {
                m_counter = counter;
            }

            public void Fired(uint u, object o)
            {
                Interlocked.Exchange(ref InterruptFireTime, Utility.GetFPGATime());
                m_counter.Increment();
                Assert.AreSame(m_counter, o);
                Interlocked.Exchange(ref InterruptComplete, 1);
                //Console.WriteLine("Fired!!!!");
            }
        }

        [Test, Timeout(1500)]
        public void TestSingleInterruptsTriggering()
        {
            InterruptCounter counter = new InterruptCounter();



            TestHandlerFunction function = new TestHandlerFunction(counter);

            GetInterruptable().RequestInterrupts(function.Fired, counter);
            GetInterruptable().EnableInterrupts();

            SetInterruptLow();
            Delay(0.01);

            long interruptTriggerTime = Utility.GetFPGATime();

            SetInterruptHigh();

            while (function.InterruptComplete == 0)
            {
                Delay(0.005);
            }


            Assert.AreEqual(1, counter.GetCount(), "The interrupt did not fire the expected number of times");


            long range = 10000;

            string error = "The interrupt did not fire within the expected time period (values in milliseconds)";
            Assert.Greater(function.InterruptFireTime, interruptTriggerTime - range, error);
            Assert.Less(function.InterruptFireTime, interruptTriggerTime + range, error);

            error = "The ReadRisingTimestamp() did not return the correct value (values in seconds";
            Assert.Greater(GetInterruptable().ReadRisingTimestanp(), (interruptTriggerTime - range) / 1e6, error);
            Assert.Less(GetInterruptable().ReadRisingTimestanp(), (interruptTriggerTime + range) / 1e6, error);

        }

        [Test, Timeout(2100)]
        public void TestMultipleInterruptsTriggering()
        {
            InterruptCounter counter = new InterruptCounter();
            TestHandlerFunction function = new TestHandlerFunction(counter);

            GetInterruptable().RequestInterrupts(function.Fired, counter);
            GetInterruptable().EnableInterrupts();

            int fireCount = 50;
            for (int i = 0; i < fireCount; i++)
            {
                SetInterruptLow();
                SetInterruptHigh();
                while (function.InterruptComplete == 0)
                {
                    Delay(0.005);
                }
                function.InterruptComplete = 0;
            }
            Assert.AreEqual(fireCount, counter.GetCount(), "The interrupt did not fire the expected number of times");

        }

        private const int SynchronousTimeout = 5;
        [Test, Timeout((int)(SynchronousTimeout * 1e3))]
        public void TestSynchronousInterruptsTriggering()
        {
            GetInterruptable().RequestInterrupts();

            double synchronousDelay = SynchronousTimeout / 2.0;

            Thread t = new Thread(() =>
            {
                Delay(synchronousDelay);
                SetInterruptLow();
                SetInterruptHigh();
            });
            t.Start();
            long startTimeStamp = Utility.GetFPGATime();
            
            GetInterruptable().WaitForInterrupt(SynchronousTimeout * 2);
            long stopTimeStamp = Utility.GetFPGATime();

            double interruptRunTime = (stopTimeStamp - startTimeStamp) * 1e-6;

            Assert.AreEqual(synchronousDelay, interruptRunTime, 0.1, "The interrupt did not run for the expected ammount of time (units in seconds)");
        }

        [Test, Timeout(4000)]
        public void TestDisableStopsInterruptFiring()
        {
            InterruptCounter counter = new InterruptCounter();
            TestHandlerFunction function = new TestHandlerFunction(counter);

            GetInterruptable().RequestInterrupts(function.Fired, counter);
            GetInterruptable().EnableInterrupts();

            int fireCount = 50;
            for (int i = 0; i < fireCount; i++)
            {
                SetInterruptLow();
                SetInterruptHigh();
                while (function.InterruptComplete == 0)
                {
                    Delay(0.005);
                }
                function.InterruptComplete = 0;
            }

            GetInterruptable().DisableInterrupts();

            for (int i = 0; i < fireCount; i++)
            {
                SetInterruptLow();
                SetInterruptHigh();
                Delay(0.005);
            }

            Assert.AreEqual(fireCount, counter.GetCount(), "The interrupt did not fire the expected number of times");
        }
    }
}
