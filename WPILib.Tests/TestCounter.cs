using System.Collections.Generic;
using HAL_Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPILib.Exceptions;
using WPILib.Interfaces;

namespace WPILib.Tests
{
    [TestClass]
    public class TestCounter
    {
        [ClassInitialize]
        public static void Initialize(TestContext ctx)
        {
            TestBase.StartCode();
        }

        [ClassCleanup]
        public static void Kill()
        {
            DriverStation.Instance.Release();
        }

        private Dictionary<dynamic, dynamic> HalData()
        {
            return HAL.halData;
        }


        public void TestInit(int mode, uint up, uint down, bool upRising, bool upFalling, bool downRising,
            bool downFalling, bool triggerUp = false, bool triggerDown = false)
        {
            Assert.IsTrue(HalData()["counter"][0]["initialized"]);
            Assert.AreEqual(mode, HalData()["counter"][0]["mode"]);
            Assert.AreEqual(up, HalData()["counter"][0]["up_source_channel"]);
            Assert.AreEqual(down, HalData()["counter"][0]["down_source_channel"]);
            Assert.AreEqual(upRising, HalData()["counter"][0]["up_rising_edge"]);
            Assert.AreEqual(upFalling, HalData()["counter"][0]["up_falling_edge"]);
            Assert.AreEqual(downRising, HalData()["counter"][0]["down_rising_edge"]);
            Assert.AreEqual(downFalling, HalData()["counter"][0]["down_falling_edge"]);
            Assert.AreEqual(triggerUp, HalData()["counter"][0]["up_source_trigger"]);
            Assert.AreEqual(triggerDown, HalData()["counter"][0]["down_source_trigger"]);
        }

        [TestMethod]
        public void TestCounterAllocateAll()
        {
            List<Counter> counters = new List<Counter>();
            try
            {
                
                for (int i = 0; i < TestBase.NumCounters; i++)
                {
                    counters.Add(new Counter(i));
                }
            }
            catch (AllocationException)
            {
                Assert.Fail();
            }

            finally
            {
                foreach (var counter in counters)
                {
                    counter?.Dispose();
                }
            }

        }

        [TestMethod]
        public void TestCounterOverAllocate()
        {
            List<Counter> encoders = new List<Counter>();
            for (int i = 0; i < TestBase.NumCounters; i++)
            {
                encoders.Add(new Counter(i));
            }

            Counter enc = null;
            try
            {
                enc = new Counter(TestBase.NumCounters);
                Assert.Fail();
            }
            catch (UncleanStatusException)
            {
            }
            finally
            {
                enc?.Dispose();
            }

            foreach (var encoder in encoders)
            {
                encoder?.Dispose();
            }

        }

        [TestMethod]
        public void TestCounterInit1()
        {
            using (Counter ctr = new Counter())
            {
                TestInit(0, 0, 0, false, false, false, false);
            }
        }

        [TestMethod]
        public void TestCounterInit2()
        {
            using (DigitalInput di = new DigitalInput(5))
            {
                using (Counter ctr = new Counter(di))
                {
                    TestInit(0, 5, 0, true, false, false, false);
                }
            }
        }

        [TestMethod]
        public void TestCounterInit3()
        {
            using (Counter ctr = new Counter(6))
            {
                TestInit(0, 6, 0, true, false, false, false);
            }
        }

        [TestMethod]
        public void TestCounterInit4()
        {
            using (DigitalInput us = new DigitalInput(3))
            {
                using (DigitalInput ds = new DigitalInput(4))
                {
                    using (Counter ctr = new Counter(EncodingType.K1X, us, ds, true))
                    {
                        TestInit(3, 3, 4, true, false, true, true);
                    }
                }
            }
        }

        [TestMethod]
        public void TestCounterInit5()
        {
            using (AnalogTrigger at = new AnalogTrigger(2))
            {
                using (Counter ctr = new Counter(at))
                {
                    TestInit(0, 1, 0, true, false, false, false, true);
                }
            }
        }

        [TestMethod]
        public void TestCounterSetUpChannel()
        {
            using (Counter ctr = new Counter())
            {
                ctr.SetUpSource(2);
                TestInit(0, 2, 0, true, false, false, false);
            }
        }

        [TestMethod]
        public void TestCounterSetUpSource()
        {
            using (DigitalInput src = new DigitalInput(3))
            {
                using (Counter ctr = new Counter())
                {
                    ctr.SetUpSource(src);
                    TestInit(0, 3, 0, true, false, false, false);
                }
            }
        }

        [TestMethod]
        public void TestCounterFree()
        {
            

            Assert.IsFalse(HalData()["counter"][0]["initialized"]);
            Assert.IsFalse(HalData()["dio"][0]["initialized"]);
            Assert.IsFalse(HalData()["dio"][1]["initialized"]);
            Counter ctr = null;
            try
            {
                ctr = new Counter();
                ctr.SetUpSource(0);
                ctr.SetDownSource(1);

                Assert.IsTrue(HalData()["counter"][0]["initialized"]);
                Assert.IsTrue(HalData()["dio"][0]["initialized"]);
                Assert.IsTrue(HalData()["dio"][1]["initialized"]);
            }
            finally
            {
                ctr?.Dispose();
            }
            Assert.IsFalse(HalData()["counter"][0]["initialized"]);
            Assert.IsFalse(HalData()["dio"][0]["initialized"]);
            Assert.IsFalse(HalData()["dio"][1]["initialized"]);
        }

    }
}
