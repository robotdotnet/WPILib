using System;
using System.Collections.Generic;
using HAL_Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPILib.Exceptions;

namespace WPILib.Tests
{
    [TestClass]
    public class TestSolenoid
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

        public Solenoid NewSolenoid()
        {
            return new Solenoid(0);
        }

        private static Dictionary<dynamic, dynamic> HalData()
        {
            return HAL.halData;
        }

        [TestMethod]
        public void TestSolenoidCreate()
        {
            using (Solenoid s = NewSolenoid())
            {
                Assert.IsTrue(HalData()["pcm"][0]["solenoid"][0]["initialized"]);
            }
        }

        [TestMethod, ExpectedException(typeof(AllocationException), AllowDerivedTypes = true)]
        public void TestMultipleAllocation()
        {
            using (Solenoid ds = NewSolenoid())
            {
                var p = NewSolenoid();
            }
        }

        [TestMethod]
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

        [TestMethod]
        public void TestCreateLowerLimit()
        {
            try
            {
                var p = new Solenoid(-1);
                Assert.Fail();
            }
            catch (IndexOutOfRangeException)
            {
            }

            try
            {
                var p = new Solenoid(TestBase.SolenoidChannels);
                Assert.Fail();
            }
            catch (IndexOutOfRangeException)
            {
            }
        }

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
        public void TestBlackList()
        {
            using (Solenoid s = NewSolenoid())
            {
                Assert.IsFalse(s.IsBlackListed());
            }
        }
    }
}
