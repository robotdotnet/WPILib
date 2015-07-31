using System;
using System.Collections.Generic;
using System.Threading;
using HAL_Base;
using NUnit.Framework;
using Telerik.JustMock;
using WPILib.Exceptions;
using WPILib.Interfaces;

namespace WPILib.Tests
{
    [TestFixture]
    public class TestInterrupt
    {
        [TestFixtureSetUp]
        public static void Initialize()
        {
            TestBase.StartCode();
        }

        [TestFixtureTearDown]
        public static void Kill()
        {
            DriverStation.Instance.Release();
        }

        private static Dictionary<dynamic, dynamic> HalData()
        {
            return HAL.halData;
        }

        public DigitalInput NewInput()
        {
            return new DigitalInput(0);
        }

        [Test]
        public void TestCreateAllInterrupts()
        {
            List<DigitalInput> inputs = new List<DigitalInput>();
            for (int i = 0; i < TestBase.NumInterrupts; i++)
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
            for (int i = 0; i < TestBase.NumInterrupts; i++)
            {
                inputs.Add(new DigitalInput(i));
            }

            foreach (var input in inputs)
            {
                input.RequestInterrupts();
            }
            DigitalInput in9 = new DigitalInput(TestBase.NumInterrupts);
            try
            { 
                in9.RequestInterrupts();
                Assert.Fail();
            }
            catch (AllocationException)
            {
            }
            finally
            {
                in9.Dispose();
            }

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
                    HAL.halData["dio"][0]["value"] = true;
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
                    HAL.halData["dio"][0]["value"] = true;
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
                HAL.halData["dio"][0]["value"] = true;

                Thread t = new Thread(() =>
                {
                    Thread.Sleep(100);
                    HAL.halData["dio"][0]["value"] = false;
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
                HAL.halData["dio"][0]["value"] = true;

                Thread t = new Thread(() =>
                {
                    Thread.Sleep(100);
                    HAL.halData["dio"][0]["value"] = false;
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
                var delegateMock = Mock.Create<Action>();

                Mock.Arrange(() => delegateMock()).OccursOnce();

                HAL.halData["dio"][0]["value"] = false;

                d.RequestInterrupts(delegateMock);
                d.EnableInterrupts();

                HAL.halData["dio"][0]["value"] = true;

                Thread.Sleep(50);

                Mock.Assert(delegateMock);
            }
        }
    }
}
