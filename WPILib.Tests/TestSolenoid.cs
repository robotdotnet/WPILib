using System;
using System.Collections.Generic;
using HAL_Base;
using NUnit.Framework;
using WPILib.Exceptions;

namespace WPILib.Tests
{
    [TestFixture]
    public class TestSolenoid : TestBase
    {

        public Solenoid NewSolenoid()
        {
            return new Solenoid(0);
        }

        private static Dictionary<dynamic, dynamic> HalData()
        {
            return HAL.halData;
        }

        [Test]
        public void TestSolenoidCreate()
        {
            using (Solenoid s = NewSolenoid())
            {
                Assert.IsTrue(HalData()["pcm"][0]["solenoid"][0]["initialized"]);
            }
        }

        [Test]
        public void TestMultipleAllocation()
        {
            using (Solenoid ds = NewSolenoid())
            {
                Assert.Throws(typeof (AllocationException), () =>
                {
                    var p = NewSolenoid();
                });
            }
        }

        [Test]
        public void TestSolenoidCreateAll()
        {
            List<Solenoid> solenoids = new List<Solenoid>();
            for (int i = 0; i < TestBase.SolenoidChannels; i++)
            {
                solenoids.Add(new Solenoid(i));
            }

            foreach (var ds in solenoids)
            {
                ds.Dispose();
            }
        }

        [Test]
        public void TestCreateLowerLimit()
        {
            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                var p = new Solenoid(-1);
            });
        }

        [Test]
        public void TestCreateUpperLimit()
        {
            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                var p = new Solenoid(TestBase.SolenoidChannels);
            });
        }

        [Test]
        public void TestSolenoidSet()
        {
            using (Solenoid s = NewSolenoid())
            {
                s.Set(true);
                Assert.IsTrue(HalData()["pcm"][0]["solenoid"][0]["value"]);

                s.Set(false);
                Assert.IsFalse(HalData()["pcm"][0]["solenoid"][0]["value"]);
            }
        }

        [Test]
        public void TestSolenoidGet()
        {
            using (Solenoid s = NewSolenoid())
            {
                HalData()["pcm"][0]["solenoid"][0]["value"] = true;
                Assert.IsTrue(s.Get());

                HalData()["pcm"][0]["solenoid"][0]["value"] = false;
                Assert.IsFalse(s.Get());
            }
        }

        [Test]
        public void TestBlackList()
        {
            using (Solenoid s = NewSolenoid())
            {
                Assert.IsFalse(s.IsBlackListed());
            }
        }
    }
}
