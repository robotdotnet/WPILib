using System;
using System.Collections.Generic;
using HAL_Simulator;
using NUnit.Framework;
using WPILib.Exceptions;
using HAL = HAL_Base.HAL;

namespace WPILib.Tests
{
    [TestFixture(0)]
    [TestFixture(7)]
    [TestFixture(59)]
    public class TestDoubleSolenoid : TestBase
    {
        private int m_module;

        public TestDoubleSolenoid(int module)
        {
            m_module = module;
        }

        public DoubleSolenoid NewDoubleSolenoid()
        {
            return new DoubleSolenoid(m_module, 0,1);
        }

        private static Dictionary<dynamic, dynamic> HalData()
        {
            return HAL.halData;
        }

        [Test]
        public void TestDoubleSolenoidModuleUnderLimit()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var s = new DoubleSolenoid(-1, 0, 1);
            });
        }

        [Test]
        public void TestDoubleSolenoidModuleOverLimit()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var s = new DoubleSolenoid(SolenoidModules, 0, 1);
            });
        }

        [Test]
        public void TestDoubleSolenoidCreate()
        {
            using (DoubleSolenoid s = NewDoubleSolenoid())
            {
                Assert.IsTrue(HalData()["pcm"][m_module]["solenoid"][0]["initialized"]);
                Assert.IsTrue(HalData()["pcm"][m_module]["solenoid"][1]["initialized"]);
            }
        }

        [Test]
        public void TestMultipleAllocation()
        {
            using (DoubleSolenoid ds = NewDoubleSolenoid())
            {
                Assert.Throws(typeof(AllocationException), () =>
                {
                    var p = NewDoubleSolenoid();
                });
            }
        }

        [Test]
        public void TestDoubleSolenoidCreateAll()
        {
            List<DoubleSolenoid> solenoids = new List<DoubleSolenoid>();
            for (int i = 0; i < TestBase.SolenoidChannels; i++)
            {
                solenoids.Add(new DoubleSolenoid(m_module, i, i+1));
                i++;
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
                var p = new DoubleSolenoid(m_module, -2, -1);
            });
        }

        [Test]
        public void TestCreateUpperLimit()
        {
            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                var p = new DoubleSolenoid(m_module, TestBase.SolenoidChannels, TestBase.SolenoidChannels + 1);
            });
        }

        [Test]
        public void TestSolenoidSetForward()
        {
            using (DoubleSolenoid ds = NewDoubleSolenoid())
            {
                ds.Set(DoubleSolenoid.Value.Forward);
                Assert.IsTrue(HalData()["pcm"][m_module]["solenoid"][0]["value"]);
                Assert.IsFalse(HalData()["pcm"][m_module]["solenoid"][1]["value"]);
            }
        }

        [Test]
        public void TestSolenoidSetReverse()
        {
            using (DoubleSolenoid ds = NewDoubleSolenoid())
            {
                ds.Set(DoubleSolenoid.Value.Reverse);
                Assert.IsTrue(HalData()["pcm"][m_module]["solenoid"][1]["value"]);
                Assert.IsFalse(HalData()["pcm"][m_module]["solenoid"][0]["value"]);
            }
        }

        [Test]
        public void TestSolenoidSetOff()
        {
            using (DoubleSolenoid ds = NewDoubleSolenoid())
            {
                ds.Set(DoubleSolenoid.Value.Off);
                Assert.IsFalse(HalData()["pcm"][m_module]["solenoid"][1]["value"]);
                Assert.IsFalse(HalData()["pcm"][m_module]["solenoid"][0]["value"]);
            }
        }

        [Test]
        public void TestSolenoidGetForward()
        {
            using (DoubleSolenoid ds = NewDoubleSolenoid())
            {
                HalData()["pcm"][m_module]["solenoid"][0]["value"] = true;
                HalData()["pcm"][m_module]["solenoid"][1]["value"] = false;

                Assert.AreEqual(DoubleSolenoid.Value.Forward, ds.Get());
            }
        }

        [Test]
        public void TestSolenoidGetReverse()
        {
            using (DoubleSolenoid ds = NewDoubleSolenoid())
            {
                HalData()["pcm"][m_module]["solenoid"][0]["value"] = false;
                HalData()["pcm"][m_module]["solenoid"][1]["value"] = true;

                Assert.AreEqual(DoubleSolenoid.Value.Reverse, ds.Get());
            }
        }

        [Test]
        public void TestSolenoidGetOff()
        {
            using (DoubleSolenoid ds = NewDoubleSolenoid())
            {
                HalData()["pcm"][m_module]["solenoid"][0]["value"] = false;
                HalData()["pcm"][m_module]["solenoid"][1]["value"] = false;

                Assert.AreEqual(DoubleSolenoid.Value.Off, ds.Get());
            }
        }

        [Test]
        public void TestBlackList()
        {
            using (DoubleSolenoid ds = NewDoubleSolenoid())
            {
                Assert.IsFalse(ds.FwdSolenoidBlackListed);
                Assert.IsFalse(ds.RevSolenoidBlackListed);
            }
        }


    }
}
