using System;
using System.Collections.Generic;
using System.Threading;
using HAL_Simulator;
using NUnit.Framework;
using WPILib.Exceptions;

namespace WPILib.Tests
{
    [TestFixture]
    public class TestInterrupt : TestBase
    { 

        public DigitalInput NewInput()
        {
            return new DigitalInput(0);
        }

        [Test]
        public void TestCreateAllInterrupts()
        {
            List<DigitalInput> inputs = new List<DigitalInput>();
            for (int i = 0; i < NumInterrupts; i++)
            {
                inputs.Add(new DigitalInput(i));
            }

            foreach (var input in inputs)
            {
                input.RequestInterrupts();
            }

            foreach (var input in inputs)
            {
                input.Dispose();
            }
        }

        [Test]
        public void TestCreateLimits()
        {
            List<DigitalInput> inputs = new List<DigitalInput>();
            for (int i = 0; i < NumInterrupts; i++)
            {
                inputs.Add(new DigitalInput(i));
            }

            foreach (var input in inputs)
            {
                input.RequestInterrupts();
            }
            DigitalInput in9 = new DigitalInput(NumInterrupts);
            Assert.Throws<AllocationException>(() => in9.RequestInterrupts());
            in9.Dispose();

            foreach (var input in inputs)
            {
                input.Dispose();
            }
        }

        [Test]
        public void TestCreateDispose()
        {
            DigitalInput d1 = new DigitalInput(0);
            DigitalInput d2 = new DigitalInput(1);

            d1.RequestInterrupts();
            Assert.AreEqual(0, (int)d1.InterruptIndex);

            d1.CancelInterrupts();

            d2.RequestInterrupts();
            Assert.AreEqual(0, (int)d2.InterruptIndex);

            d1.Dispose();
            d2.Dispose();
        }

        [Test]
        public void TestSynchronousTimeout()
        {
            using (DigitalInput d = NewInput())
            {
                d.RequestInterrupts();
                var retVal = d.WaitForInterrupt(0.5);

                Assert.AreEqual(WaitResult.Timeout, retVal);
            }
        }

        [Test]
        public void TestSynchronousRisingEdgeChecked()
        {
            using (DigitalInput d = NewInput())
            {
                d.RequestInterrupts();

                Thread t = new Thread(() =>
                {
                    Thread.Sleep(100);
                    SimData.DIO[0].Value = true;
                });

                t.Start();

                var retVal = d.WaitForInterrupt(0.5);

                Assert.AreEqual(WaitResult.RisingEdge, retVal);
            }
        }

        [Test]
        public void TestSynchronousRisingEdgeNotChecked()
        {
            using (DigitalInput d = NewInput())
            {
                d.RequestInterrupts();
                d.SetUpSourceEdge(false, false);

                Thread t = new Thread(() =>
                {
                    Thread.Sleep(100);
                    SimData.DIO[0].Value = true;
                });

                t.Start();

                var retVal = d.WaitForInterrupt(0.5);

                Assert.AreEqual(WaitResult.Timeout, retVal);
            }
        }

        [Test]
        public void TestSynchronousFallingEdgeChecked()
        {
            using (DigitalInput d = NewInput())
            {
                d.RequestInterrupts();
                d.SetUpSourceEdge(false, true);
                SimData.DIO[0].Value = true;

                Thread t = new Thread(() =>
                {
                    Thread.Sleep(100);
                    SimData.DIO[0].Value = false;
                });

                t.Start();

                var retVal = d.WaitForInterrupt(0.5);

                Assert.AreEqual(WaitResult.FallingEdge, retVal);
            }
        }

        [Test]
        public void TestSynchronousFallingEdgeNotChecked()
        {
            using (DigitalInput d = NewInput())
            {
                d.RequestInterrupts();
                d.SetUpSourceEdge(false, false);
                SimData.DIO[0].Value = true;

                Thread t = new Thread(() =>
                {
                    Thread.Sleep(100);
                    SimData.DIO[0].Value = false;
                });

                t.Start();

                var retVal = d.WaitForInterrupt(0.5);

                Assert.AreEqual(WaitResult.Timeout, retVal);
            }
        }

        [Test]
        public void TestAsync()
        {
            using (DigitalInput d = NewInput())
            {
                int count = 0;
                Action mockDelegate = () =>
                {
                    count++;
                };

                SimData.DIO[0].Value = false;

                d.RequestInterrupts(mockDelegate);
                d.EnableInterrupts();

                SimData.DIO[0].Value = true;

                Thread.Sleep(50);

                Assert.AreEqual(1, count);
            }
        }

        [Test]
        public void TestAsyncReturnValue()
        {
            using (DigitalInput d = NewInput())
            {
                int count = 0;
                object obj = null;
                Action<uint, object> mockDelegate = (m, o) =>
                {
                    count++;
                    obj = o;
                };

                SimData.DIO[0].Value = false;

                d.RequestInterrupts(mockDelegate, this);
                d.EnableInterrupts();

                SimData.DIO[0].Value = true;

                Thread.Sleep(50);

                Assert.AreEqual(1, count);
                Assert.AreSame(this, obj);
            }
        }
    }
}
