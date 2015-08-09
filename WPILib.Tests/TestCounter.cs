using System.Collections.Generic;
using System.Threading;
using HAL_Base;
using NUnit.Framework;
using WPILib.Exceptions;
using WPILib.Interfaces;

namespace WPILib.Tests
{
    [TestFixture]
    public class TestCounter : TestBase
    {

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

        [Test]
        public void TestCounterAllocateAll()
        {
            List<Counter> counters = new List<Counter>();
            Assert.DoesNotThrow(() =>
            {
                for (int i = 0; i < TestBase.NumCounters; i++)
                {
                    counters.Add(new Counter(i));
                }
            });
            foreach (var counter in counters)
            {
                counter?.Dispose();
            }
        }

        [Test]
        public void TestCounterOverAllocate()
        {
            List<Counter> counters = new List<Counter>();
            for (int i = 0; i < TestBase.NumCounters; i++)
            {
                counters.Add(new Counter(i));
            }

            Counter counter = null;
            Assert.Throws<UncleanStatusException>(() =>
            {
                counter = new Counter(TestBase.NumCounters);
            });
            counter?.Dispose();

            foreach (var c in counters)
            {
                c?.Dispose();
            }

        }

        [Test]
        public void TestCounterInit1()
        {
            using (Counter ctr = new Counter())
            {
                TestInit(0, 0, 0, false, false, false, false);
            }
        }

        [Test]
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

        [Test]
        public void TestCounterInit3()
        {
            using (Counter ctr = new Counter(6))
            {
                TestInit(0, 6, 0, true, false, false, false);
            }
        }

        [Test]
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

        [Test]
        public void TestCounterInit5()
        {
            //Assert.Pass();
            //return;
            using (AnalogTrigger at = new AnalogTrigger(2))
            {
                using (Counter ctr = new Counter(at))
                {
                    TestInit(0, 1, 0, true, false, false, false, true);
                }
            }
        }

        [Test]
        public void TestCounterSetUpChannel()
        {
            using (Counter ctr = new Counter())
            {
                ctr.SetUpSource(2);
                TestInit(0, 2, 0, true, false, false, false);
            }
        }

        [Test]
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

        [Test]
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

        [TestCase(false, false)]
        [TestCase(false, true)]
        [TestCase(true, false)]
        [TestCase(true, true)]
        public void TestCountUpSourceEdge(bool rising, bool falling)
        {
            using (Counter c = new Counter())
            {
                c.SetUpSource(2);
                c.SetUpSourceEdge(rising, falling);
                Assert.AreEqual(rising, HalData()["counter"][0]["up_rising_edge"]);
                Assert.AreEqual(falling, HalData()["counter"][0]["up_falling_edge"]);
            }
        }

        [TestCase(false, false)]
        [TestCase(false, true)]
        [TestCase(true, false)]
        [TestCase(true, true)]
        public void TestCountDownSourceEdge(bool rising, bool falling)
        {
            using (Counter c = new Counter())
            {
                c.SetDownSource(2);
                c.SetDownSourceEdge(rising, falling);
                Assert.AreEqual(rising, HalData()["counter"][0]["down_rising_edge"]);
                Assert.AreEqual(falling, HalData()["counter"][0]["down_falling_edge"]);
            }
        }

        [Test]
        public void TestCounterSetUpDownMode()
        {
            using (Counter c = new Counter())
            {
                c.SetUpDownCounterMode();
                Assert.AreEqual(0, HalData()["counter"][0]["mode"]);
            }
        }

        [Test]
        public void TestCounterSetExtDirMode()
        {
            using (Counter c = new Counter())
            {
                c.SetExternalDirectionMode();
                Assert.AreEqual(3, HalData()["counter"][0]["mode"]);
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void TestCounterSetSemiPeriodMode(bool high)
        {
            using (Counter c = new Counter())
            {
                c.SetSemiPeriodMode(high);
                Assert.AreEqual(1, HalData()["counter"][0]["mode"]);
                Assert.AreEqual(high, HalData()["counter"][0]["up_rising_edge"]);
                Assert.IsFalse(HalData()["counter"][0]["update_when_empty"]);
            }
        }

        [TestCase(1.0)]
        [TestCase(4.5)]
        [TestCase(1.5)]
        public void TestCounterSetPlMode(double thresh)
        {
            using (Counter c = new Counter())
            {
                c.SetPulseLengthMode(thresh);
                Assert.AreEqual(2, HalData()["counter"][0]["mode"]);
                Assert.AreEqual(thresh, HalData()["counter"][0]["pulse_length_threshold"]);
            }
        }

        [Test]
        public void TestCounterGet()
        {
            using (Counter c = new Counter())
            {
                HalData()["counter"][0]["count"] = 5;
                Assert.AreEqual(5, c.Get());

                HalData()["counter"][0]["count"] = 258;
                Assert.AreEqual(258, c.Get());
            }
        }

        [Test]
        public void TestCounterGetDistance()
        {
            using (Counter c = new Counter())
            {
                c.DistancePerPulse = 2;

                HalData()["counter"][0]["count"] = 5;
                Assert.AreEqual(5 * 2, c.GetDistance());

                c.DistancePerPulse = 5;
                HalData()["counter"][0]["count"] = 258;
                Assert.AreEqual(258 * 5, c.GetDistance());
            }
        }

        [Test]
        public void TestCounterReset()
        {
            using (Counter c = new Counter())
            {
                HalData()["counter"][0]["count"] = 5;
                Assert.AreEqual(5, c.Get());
                Assert.IsFalse(HalData()["counter"][0]["reset"]);

                c.Reset();

                Assert.AreEqual(0, HalData()["counter"][0]["count"]);
                Assert.AreEqual(0, c.Get());
                Assert.IsTrue(HalData()["counter"][0]["reset"]);
            }
        }

        [TestCase(1.0)]
        [TestCase(4.5)]
        [TestCase(1.5)]
        public void TestCounterSetMaxPeriod(double period)
        {
            using (Counter c = new Counter())
            {
                Assert.AreEqual(0.5, HalData()["counter"][0]["max_period"], 0.0001);
                c.MaxPeriod = period;
                Assert.AreEqual(period, HalData()["counter"][0]["max_period"], 0.0001);
            }
        }

        [TestCase(true)]
        [TestCase(false)]
        public void TestCounterSetUpdateEmpty(bool enabled)
        {
            using (Counter c = new Counter())
            {
                Assert.IsFalse(HalData()["counter"][0]["update_when_empty"]);
                c.UpdateWhenEmpty = enabled;
                Assert.AreEqual(enabled, HalData()["counter"][0]["update_when_empty"]);
            }
        }

        [Test]
        public void TestCounterGetStopped()
        {
            using (Counter c = new Counter())
            {
                HalData()["counter"][0]["period"] = 6;
                HalData()["counter"][0]["max_period"] = 7;
                Assert.IsFalse(c.GetStopped());
                HalData()["counter"][0]["period"] = 7;
                HalData()["counter"][0]["max_period"] = 3;
                Assert.IsTrue(c.GetStopped());
            }
        }

        [TestCase(true)]
        [TestCase(false)]
        public void TestCounterGetDirection(bool dir)
        {
            using (Counter c = new Counter())
            {
                HalData()["counter"][0]["direction"] = dir;
                Assert.AreEqual(dir, c.GetDirection());
            }
        }

        [TestCase(true)]
        [TestCase(false)]
        public void TestCounterSetReverseDirection(bool dir)
        {
            using (Counter c = new Counter())
            {
                c.SetReverseDirection(dir);
                Assert.AreEqual(dir, HalData()["counter"][0]["reverse_direction"]);
            }
        }

        [TestCase(1.0)]
        [TestCase(5.76)]
        [TestCase(2.222)]
        public void TestCounterGetPeriod(double period)
        {
            using (Counter c = new Counter())
            {
                HalData()["counter"][0]["period"] = period;
                Assert.AreEqual(period, c.GetPeriod(), 0.00001);
            }
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(10)]
        public void TestCounterSetGetSamples(int samples)
        {
            using (Counter c = new Counter())
            {
                c.SamplesToAverage = samples;
                Assert.AreEqual(samples, HalData()["counter"][0]["samples_to_average"]);
                Assert.AreEqual(samples, c.SamplesToAverage);
            }
        }
    }
}
