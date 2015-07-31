using System;
using System.Collections.Generic;
using HAL_Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPILib.Exceptions;

namespace WPILib.Tests
{
    [TestClass]
    public class TestDoubleSolenoid
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

        public DoubleSolenoid NewDoubleSolenoid()
        {
            return new DoubleSolenoid(0,1);
        }

        private static Dictionary<dynamic, dynamic> HalData()
        {
            return HAL.halData;
        }

        [TestMethod]
        public void TestDoubleSolenoidCreate()
        {
            using (DoubleSolenoid s = NewDoubleSolenoid())
            {
                Assert.IsTrue(HalData()["pcm"][0]["solenoid"][0]["initialized"]);
                Assert.IsTrue(HalData()["pcm"][0]["solenoid"][1]["initialized"]);
            }
        }

        [TestMethod, ExpectedException(typeof(AllocationException), AllowDerivedTypes = true)]
        public void TestMultipleAllocation()
        {
            using (DoubleSolenoid ds = NewDoubleSolenoid())
            {
                var p = NewDoubleSolenoid();
            }
        }

        [TestMethod]
        public void TestDoubleSolenoidCreateAll()
        {
            List<DoubleSolenoid> solenoids = new List<DoubleSolenoid>();
            for (int i = 0; i < TestBase.SolenoidChannels; i++)
            {
                solenoids.Add(new DoubleSolenoid(i, i+1));
                i++;
            }

            foreach (var ds in solenoids)
            {
                ds.Dispose();
            }
        }

        [TestMethod]
        public void TestCreateLowerLimit()
        {
            try
            {
                var p = new DoubleSolenoid(-2, -1);
                Assert.Fail();
            }
            catch (IndexOutOfRangeException)
            {
            }

            try
            {
                var p = new DoubleSolenoid(TestBase.SolenoidChannels, TestBase.SolenoidChannels + 1);
                Assert.Fail();
            }
            catch (IndexOutOfRangeException)
            {
            }
        }

        [TestMethod]
        public void TestSolenoidSetForward()
        {
            using (DoubleSolenoid ds = NewDoubleSolenoid())
            {
                ds.Set(DoubleSolenoid.Value.Forward);
                Assert.IsTrue(HalData()["pcm"][0]["solenoid"][0]["value"]);
                Assert.IsFalse(HalData()["pcm"][0]["solenoid"][1]["value"]);
            }
        }

        [TestMethod]
        public void TestSolenoidSetReverse()
        {
            using (DoubleSolenoid ds = NewDoubleSolenoid())
            {
                ds.Set(DoubleSolenoid.Value.Reverse);
                Assert.IsTrue(HalData()["pcm"][0]["solenoid"][1]["value"]);
                Assert.IsFalse(HalData()["pcm"][0]["solenoid"][0]["value"]);
            }
        }

        [TestMethod]
        public void TestSolenoidSetOff()
        {
            using (DoubleSolenoid ds = NewDoubleSolenoid())
            {
                ds.Set(DoubleSolenoid.Value.Off);
                Assert.IsFalse(HalData()["pcm"][0]["solenoid"][1]["value"]);
                Assert.IsFalse(HalData()["pcm"][0]["solenoid"][0]["value"]);
            }
        }

        [TestMethod]
        public void TestSolenoidGetForward()
        {
            using (DoubleSolenoid ds = NewDoubleSolenoid())
            {
                HalData()["pcm"][0]["solenoid"][0]["value"] = true;
                HalData()["pcm"][0]["solenoid"][1]["value"] = false;

                Assert.AreEqual(DoubleSolenoid.Value.Forward, ds.Get());
            }
        }

        [TestMethod]
        public void TestSolenoidGetReverse()
        {
            using (DoubleSolenoid ds = NewDoubleSolenoid())
            {
                HalData()["pcm"][0]["solenoid"][0]["value"] = false;
                HalData()["pcm"][0]["solenoid"][1]["value"] = true;

                Assert.AreEqual(DoubleSolenoid.Value.Reverse, ds.Get());
            }
        }

        [TestMethod]
        public void TestSolenoidGetOff()
        {
            using (DoubleSolenoid ds = NewDoubleSolenoid())
            {
                HalData()["pcm"][0]["solenoid"][0]["value"] = false;
                HalData()["pcm"][0]["solenoid"][1]["value"] = false;

                Assert.AreEqual(DoubleSolenoid.Value.Off, ds.Get());
            }
        }

        [TestMethod]
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
