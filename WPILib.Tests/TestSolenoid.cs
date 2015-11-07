using System;
using System.Collections.Generic;
using HAL_Simulator;
using HAL_Simulator.Data;
using NUnit.Framework;
using WPILib.Exceptions;
// ReSharper disable UnusedVariable

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

        private IReadOnlyList<SolenoidData> GetSolenoids()
        {
            return SimData.GetPCM(m_module).Solenoids;
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
                Assert.IsTrue(GetSolenoids()[0].Initialized);
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
            for (int i = 0; i < SolenoidChannels; i++)
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
                var p = new Solenoid(m_module, SolenoidChannels);
            });
        }

        [Test]
        public void TestSolenoidSet()
        {
            using (Solenoid s = NewSolenoid())
            {
                s.Set(true);
                Assert.IsTrue(GetSolenoids()[0].Value);

                s.Set(false);
                Assert.IsFalse(GetSolenoids()[0].Value);
            }
        }

        [Test]
        public void TestSolenoidGet()
        {
            using (Solenoid s = NewSolenoid())
            {
                GetSolenoids()[0].Value = true;
                Assert.IsTrue(s.Get());

                GetSolenoids()[0].Value = false;
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
