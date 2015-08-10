using System;
using System.Collections.Generic;
using HAL_Base;
using NUnit.Framework;
using WPILib.Exceptions;

namespace WPILib.Tests
{
    [TestFixture(0)]
    [TestFixture(5)]
    [TestFixture(58)]
    public class TestSolenoid : TestBase
    {
        private readonly int m_module;

        public TestSolenoid(int module)
        {
            m_module = module;
        }
        public Solenoid NewSolenoid()
        {
            return new Solenoid(m_module, 0);
        }

        private static Dictionary<dynamic, dynamic> HalData()
        {
            return HAL.halData;
        }

        [Test]
        public void TestSolenoidModuleUnderLimit()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var s = new Solenoid(-1, 0);
            });
        }

        [Test]
        public void TestSolenoidModuleOverLimit()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var s = new Solenoid(SolenoidModules, 0);
            });
        }

        [Test]
        public void TestSolenoidCreate()
        {
            using (Solenoid s = NewSolenoid())
            {
                Assert.IsTrue(HalData()["pcm"][m_module]["solenoid"][0]["initialized"]);
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
                solenoids.Add(new Solenoid(m_module, i));
            }

            foreach (var ds in solenoids)
            {
                ds.Dispose();
            }
        }

        [Test]
        public void TestCreateLowerLimit()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var p = new Solenoid(m_module, -1);
            });
        }

        [Test]
        public void TestCreateUpperLimit()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var p = new Solenoid(m_module, TestBase.SolenoidChannels);
            });
        }

        [Test]
        public void TestSolenoidSet()
        {
            using (Solenoid s = NewSolenoid())
            {
                s.Set(true);
                Assert.IsTrue(HalData()["pcm"][m_module]["solenoid"][0]["value"]);

                s.Set(false);
                Assert.IsFalse(HalData()["pcm"][m_module]["solenoid"][0]["value"]);
            }
        }

        [Test]
        public void TestSolenoidGet()
        {
            using (Solenoid s = NewSolenoid())
            {
                HalData()["pcm"][m_module]["solenoid"][0]["value"] = true;
                Assert.IsTrue(s.Get());

                HalData()["pcm"][m_module]["solenoid"][0]["value"] = false;
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
