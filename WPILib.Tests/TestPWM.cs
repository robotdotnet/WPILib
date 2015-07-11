using System;
using System.Collections.Generic;
using HAL_Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPILib.Exceptions;

namespace WPILib.Tests
{
    [TestClass]
    public class TestPWM
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

        public PWM NewPWM()
        {
            return new PWM(2);
        }

        private static Dictionary<dynamic, dynamic> PWMData()
        {
            return HAL_Base.HAL.halData["pwm"][2];
        }

        private static PWM boundPWM(PWM pwm)
        {
            pwm.SetBounds(1500, 1050, 1000, 950, 500);
            return pwm;
        }

        [TestMethod]
        public void TestPWMCreate()
        {
            using (PWM pwm = new PWM(5))
            {
                Assert.AreEqual(pwm.Channel, 5);
                Assert.IsFalse(pwm.DeadbandElimination);
                //TODO: Test Reporting
            }
        }

        [TestMethod]
        public void TestAllocateError()
        {
            using (PWM pwm = new PWM(5))
            {
                try
                {
                    var p2 = new PWM(5);
                    Assert.Fail();
                }
                catch (AllocationException)
                {
                }
            }
        }

        [TestMethod]
        public void TestCreateLimits()
        {
            try
            {
                var pwm = new PWM(-1);
                Assert.Fail();
            }
            catch (IndexOutOfRangeException)
            {
            }

            try
            {
                var pwm = new PWM(SensorBase.PwmChannels);
                Assert.Fail();
            }
            catch (IndexOutOfRangeException)
            {
            }
        }

        [TestMethod]
        public void TestPWMCreateAll()
        {
            List<PWM> pwms = new List<PWM>();
            for (int i = 0; i < SensorBase.PwmChannels; i++)
            {
                pwms.Add(new PWM(i));
            }

            foreach (var p in pwms)
            {
                p.Dispose();
            }
        }

        [TestMethod]
        public void TestPWMDispose()
        {
            PWM pwm = NewPWM();
            Assert.IsTrue(PWMData()["initialized"]);
            pwm.Dispose();
            Assert.IsFalse(PWMData()["initialized"]);
            pwm = null;
            pwm = NewPWM();
            Assert.IsTrue(PWMData()["initialized"]);
            pwm.Dispose();
        }

        [TestMethod]
        public void TestEnableDeabandElimination()
        {
            using (PWM pwm = NewPWM())
            {
                pwm.DeadbandElimination = true;
                Assert.IsTrue(pwm.DeadbandElimination);
            }
        }

        [TestMethod]
        public void TestDisableDeabandElimination()
        {
            using (PWM pwm = NewPWM())
            {
                pwm.DeadbandElimination = false;
                Assert.IsFalse(pwm.DeadbandElimination);
            }
        }

        [TestMethod]
        public void TestSetBounds()
        {
            HAL_Base.HAL.halData["pwm_loop_timing"] = SensorBase.SystemClockTicksPerMicrosecond;

            using (PWM pwm = NewPWM())
            {
                pwm.SetBounds(2.027, 1.525, 1.507, 1.49, 1.026);
                pwm.DeadbandElimination = true;
                Assert.AreEqual(pwm.MaxPositivePwm, 1526);
                Assert.AreEqual(pwm.MinPositivePwm, 1024);
                Assert.AreEqual(pwm.CenterPwm, 1005);
                Assert.AreEqual(pwm.MaxNegativePwm, 989);
                Assert.AreEqual(pwm.MinNegativePwm, 525);
            }
        }

        [TestMethod]
        public void TestSetPosition5()
        {
            using (PWM pwm = NewPWM())
            {
                boundPWM(pwm);
                pwm.SetPosition(0.5);
                Assert.AreEqual(PWMData()["raw_value"], 1000);
            }
        }

        [TestMethod]
        public void TestSetPosition25()
        {
            using (PWM pwm = NewPWM())
            {
                boundPWM(pwm);
                pwm.SetPosition(0.25);
                Assert.AreEqual(PWMData()["raw_value"], 750);
            }
        }

        //Set 1.5, expect 1500;
        [TestMethod]
        public void TestSetPositionLimit15()
        {
            using (PWM pwm = NewPWM())
            {
                boundPWM(pwm);
                pwm.SetPosition(1.5);
                Assert.AreEqual(PWMData()["raw_value"], 1500);
            }
        }

        //Set -.5, expect 500;
        [TestMethod]
        public void TestSetPositionLimitNeg5()
        {
            using (PWM pwm = NewPWM())
            {
                boundPWM(pwm);
                pwm.SetPosition(-0.5);
                Assert.AreEqual(PWMData()["raw_value"], 500);
            }
        }

        [TestMethod]
        public void TestGetPosition1000()
        {
            using (PWM pwm = NewPWM())
            {
                boundPWM(pwm);
                PWMData()["raw_value"] = 1000;
                Assert.AreEqual(pwm.GetPosition(), 0.5);
            }
        }

        [TestMethod]
        public void TestGetPosition750()
        {
            using (PWM pwm = NewPWM())
            {
                boundPWM(pwm);
                PWMData()["raw_value"] = 750;
                Assert.AreEqual(pwm.GetPosition(), 0.25);
            }
        }

        [TestMethod]
        public void TestGetPositionLimits1600()
        {
            using (PWM pwm = NewPWM())
            {
                boundPWM(pwm);
                PWMData()["raw_value"] = 1600;
                Assert.AreEqual(pwm.GetPosition(), 1.0);
            }
        }

        [TestMethod]
        public void TestGetPositionLimits400()
        {
            using (PWM pwm = NewPWM())
            {
                boundPWM(pwm);
                PWMData()["raw_value"] = 400;
                Assert.AreEqual(pwm.GetPosition(), 0.0);
            }
        }

        [TestMethod]
        public void TestSetRaw()
        {
            using (PWM pwm = NewPWM())
            {
                pwm.SetRaw(60);
                Assert.AreEqual(PWMData()["raw_value"], 60);
            }
        }

        //Skiping this test for now, because we don't check to see if we are allocated
        //unlike python
        /*
        [TestMethod]
        public void TestSetRawFreed()
        {
            PWM pwm = NewPWM();
            pwm.Dispose();

            pwm.SetRaw(60);

            /*
            try
            {
                
                Assert.Fail();
            }
            catch()
            *
        }
        */

        [TestMethod]
        public void TestGetRaw()
        {
            using (PWM pwm = NewPWM())
            {
                PWMData()["raw_value"] = 1234;
                Assert.AreEqual(pwm.GetRaw(), 1234);
            }
        }

        //Skiping this test for now, because we don't check to see if we are allocated
        //unlike python
        /*
        [TestMethod]
        public void TestGetRawFreed()
        {
            PWM pwm = NewPWM();
            pwm.Dispose();
            pwm.GetRaw();
        }
        */
    }
}
