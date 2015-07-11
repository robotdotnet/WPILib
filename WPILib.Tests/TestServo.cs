using System;
using System.Collections.Generic;
using HAL_Simulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HAL = HAL_Base.HAL;

namespace WPILib.Tests
{
    [TestClass]
    public class TestServo
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

        public Servo NewServo()
        {
            return new Servo(2);
        }

        private static Dictionary<dynamic, dynamic> HalData()
        {
            return HAL_Base.HAL.halData;
        }


        [TestMethod]
        public void TestServoInitialized()
        {
            using (Servo s = NewServo())
            {
                Assert.AreEqual(HalData()["pwm"][2]["type"], "servo");
            }
        }
        [TestMethod]
        public void TestServoStarts0()
        {
            using (Servo s = NewServo())
            {
                Assert.AreEqual(s.Get(), 0);
            }
        }
        [TestMethod]
        public void TestServoSet()
        {
            using (Servo s = NewServo())
            {
                s.Set(1.0);
                Assert.AreEqual(s.Get(), 1.0);
            }
        }
        [TestMethod]
        public void TestServoSetAngle()
        {
            using (Servo s = NewServo())
            {
                s.SetAngle(76.5);
                Assert.AreEqual(s.GetAngle(), 76.5);
            }
        }
    }
}
