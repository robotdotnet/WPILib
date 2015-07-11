using System;
using System.Collections.Generic;
using System.Threading;
using HAL_Simulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPILib.Exceptions;
using HAL = HAL_Base.HAL;

namespace WPILib.Tests
{
    [TestClass]
    public class TestInterrupt
    {
        [ClassInitialize]
        public static void Initialize(TestContext c)
        {
            TestBase.StartCode();
        }

        [ClassCleanup]
        public static void Kill()
        {
            DriverStation.Instance.Release();
        }

        private static Dictionary<dynamic, dynamic> HalData()
        {
            return HAL_Base.HAL.halData;
        }

        public DigitalInput NewInput()
        {
            return new DigitalInput(0);
        }

        [TestMethod]
        public void TestCreateAllInterrupts()
        {
            List<DigitalInput> inputs = new List<DigitalInput>();
            for (int i = 0; i < 8; i++)
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

        [TestMethod]
        public void TestCreateLimits()
        {
            List<DigitalInput> inputs = new List<DigitalInput>();
            for (int i = 0; i < 8; i++)
            {
                inputs.Add(new DigitalInput(i));
            }

            foreach (var input in inputs)
            {
                input.RequestInterrupts();
            }
            DigitalInput in9 = new DigitalInput(9); ;
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

        [TestMethod]
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

        [TestMethod]
        public void TestSynchronousTimeout()
        {
            using (DigitalInput d = NewInput())
            {
                d.RequestInterrupts();
                var retVal = d.WaitForInterrupt(0.5);

                Assert.AreEqual(WaitResult.Timeout, retVal);
            }
        }

        [TestMethod]
        public void TestSynchronousRisingEdgeChecked()
        {
            using (DigitalInput d = NewInput())
            {
                d.RequestInterrupts();

                Thread t = new Thread(() =>
                {
                    Thread.Sleep(100);
                    NotifyDict<dynamic, dynamic> nd = HAL_Base.HAL.halData["dio"][0];
                    nd["value"] = true;
                });

                t.Start();

                var retVal = d.WaitForInterrupt(0.5);

                Assert.AreEqual(WaitResult.RisingEdge, retVal);
            }
        }

        [TestMethod]
        public void TestSynchronousRisingEdgeNotChecked()
        {
            using (DigitalInput d = NewInput())
            {
                d.RequestInterrupts();
                d.SetUpSourceEdge(false, false);

                Thread t = new Thread(() =>
                {
                    Thread.Sleep(100);
                    NotifyDict<dynamic, dynamic> nd = HAL_Base.HAL.halData["dio"][0];
                    nd["value"] = true;
                });

                t.Start();

                var retVal = d.WaitForInterrupt(0.5);

                Assert.AreEqual(WaitResult.Timeout, retVal);
            }
        }

        [TestMethod]
        public void TestSynchronousFallingEdgeChecked()
        {
            using (DigitalInput d = NewInput())
            {
                d.RequestInterrupts();
                d.SetUpSourceEdge(false, true);
                HAL_Base.HAL.halData["dio"][0]["value"] = true;

                Thread t = new Thread(() =>
                {
                    Thread.Sleep(100);
                    NotifyDict<dynamic, dynamic> nd = HAL_Base.HAL.halData["dio"][0];
                    nd["value"] = false;
                });

                t.Start();

                var retVal = d.WaitForInterrupt(0.5);

                Assert.AreEqual(WaitResult.FallingEdge, retVal);
            }
        }

        [TestMethod]
        public void TestSynchronousFallingEdgeNotChecked()
        {
            using (DigitalInput d = NewInput())
            {
                d.RequestInterrupts();
                d.SetUpSourceEdge(false, false);
                HAL_Base.HAL.halData["dio"][0]["value"] = true;

                Thread t = new Thread(() =>
                {
                    Thread.Sleep(100);
                    NotifyDict<dynamic, dynamic> nd = HAL_Base.HAL.halData["dio"][0];
                    nd["value"] = false;
                });

                t.Start();

                var retVal = d.WaitForInterrupt(0.5);

                Assert.AreEqual(WaitResult.Timeout, retVal);
            }
        }

        [TestMethod]
        public void TestAsync()
        {
            using (DigitalInput d = NewInput())
            {
                bool fired = false;
                HAL_Base.HAL.halData["dio"][0]["value"] = false;
                d.RequestInterrupts(() =>
                {
                    fired = true;
                });
                d.EnableInterrupts();

                NotifyDict<dynamic, dynamic> nd = HAL_Base.HAL.halData["dio"][0];
                nd["value"] = true;
                Thread.Sleep(50);

                Assert.IsTrue(fired);
            }
        }

        public void TestMethod1()
        {
            //HalData()["time"]["program_start"] = SimHooks.GetTime();
            //SimHooks.GetFPGATimestamp();
            Thread.Sleep(3000);
            //Console.WriteLine(SimHooks.GetTime() - HalData()["time"]["program_start"]);
            Console.WriteLine(SimHooks.GetFPGATimestamp());
        }
    }
}
