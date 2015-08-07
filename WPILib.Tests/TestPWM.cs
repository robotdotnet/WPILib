using System;
using System.Collections.Generic;
using HAL_Base;
using NUnit.Framework;
using WPILib.Exceptions;

namespace WPILib.Tests
{
    [TestFixture]
    public class TestPWM : TestBase
    {

        public PWM NewPWM()
        {
            return new PWM(2);
        }

        private static Dictionary<dynamic, dynamic> PWMData()
        {
            return HAL.halData["pwm"][2];
        }

        private static PWM boundPWM(PWM pwm)
        {
            pwm.SetBounds(1500, 1050, 1000, 950, 500);
            return pwm;
        }

        [Test]
        public void TestPWMCreate()
        {
            using (PWM pwm = new PWM(5))
            {
                Assert.AreEqual(pwm.Channel, 5);
                Assert.IsFalse(pwm.DeadbandElimination);
                //TODO: Test Reporting
            }
        }

        [Test]
        public void TestAllocateError()
        {
            using (PWM pwm = new PWM(5))
            {
                Assert.Throws<AllocationException>(() =>
                {
                    var p2 = new PWM(5);
                });
            }
        }

        [Test]
        public void TestCreateLimitsLower()
        {
            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                var pwm = new PWM(-1);
            });
        }

        [Test]
        public void TestCreateLimitsUpper()
        {
            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                var pwm = new PWM(TestBase.PwmChannels);
            });
        }

        [Test]
        public void TestPWMCreateAll()
        {
            List<PWM> pwms = new List<PWM>();
            for (int i = 0; i < TestBase.PwmChannels; i++)
            {
                pwms.Add(new PWM(i));
            }

            foreach (var p in pwms)
            {
                p.Dispose();
            }
        }

        [Test]
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

        [Test]
        public void TestEnableDeabandElimination()
        {
            using (PWM pwm = NewPWM())
            {
                pwm.DeadbandElimination = true;
                Assert.IsTrue(pwm.DeadbandElimination);
            }
        }

        [Test]
        public void TestDisableDeabandElimination()
        {
            using (PWM pwm = NewPWM())
            {
                pwm.DeadbandElimination = false;
                Assert.IsFalse(pwm.DeadbandElimination);
            }
        }

        [Test]
        public void TestSetBounds()
        {
            HAL.halData["pwm_loop_timing"] = SensorBase.SystemClockTicksPerMicrosecond;

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

        [Test]
        [TestCase(0.5, 1000)]
        [TestCase(0.25, 750)]
        public void TestSetPosition(double position, int rawValue)
        {
            using (PWM pwm = NewPWM())
            {
                boundPWM(pwm);
                pwm.SetPosition(position);
                Assert.AreEqual(PWMData()["raw_value"], rawValue);
            }
        }

        [Test]
        [TestCase(1.5, 1500)]
        [TestCase(-5, 500)]
        public void TestSetPositionLimit(double position, int raw)
        {
            using (PWM pwm = NewPWM())
            {
                boundPWM(pwm);
                pwm.SetPosition(position);
                Assert.AreEqual(PWMData()["raw_value"], raw);
            }
        }

        [Test]
        [TestCase(0.5, 1000)]
        [TestCase(0.25, 750)]
        public void TestGetPosition(double position, int raw)
        {
            using (PWM pwm = NewPWM())
            {
                boundPWM(pwm);
                PWMData()["raw_value"] = raw;
                Assert.AreEqual(pwm.GetPosition(), position);
            }
        }

        [Test]
        [TestCase(1.0, 1600)]
        [TestCase(0.0, 400)]
        public void TestGetPositionLimits(double position, int raw)
        {
            using (PWM pwm = NewPWM())
            {
                boundPWM(pwm);
                PWMData()["raw_value"] = 1600;
                Assert.AreEqual(pwm.GetPosition(), 1.0);
            }
        }

        [Test]
        [TestCase(false, 0.0, 1000)]
        [TestCase(false, 0.5, 1251)]
        [TestCase(false, -0.5, 750)]
        [TestCase(true, 0.0, 1000)]
        [TestCase(true, 0.5, 1275)]
        [TestCase(true, -0.5, 725)]
        public void TestSetSpeed(bool db, double speed, int expected)
        {
            using (PWM pwm = NewPWM())
            {
                boundPWM(pwm);
                pwm.DeadbandElimination = db;
                pwm.SetSpeed(speed);
                Assert.AreEqual(expected, PWMData()["raw_value"]);
            }
        }

        [Test]
        [TestCase(1.5, 1500)]
        [TestCase(-1.5, 500)]
        public void TestSetSpeedLimits(double speed, int expected)
        {
            using (PWM pwm = NewPWM())
            {
                boundPWM(pwm);
                pwm.SetSpeed(speed);
                Assert.AreEqual(expected, PWMData()["raw_value"]);
            }
        }

        [Test]
        [TestCase(false, 1251, 0.5)]
        [TestCase(false, 750, -0.5)]
        [TestCase(false, 1000, 0.0)]
        [TestCase(true, 1275, 0.5)]
        [TestCase(true, 725, -0.5)]
        [TestCase(true, 1050, 0.0)]
        [TestCase(true, 950, 0.0)]
        public void TestGetSpeed(bool db, int speed, double expected)
        {
            using (PWM pwm = NewPWM())
            {
                boundPWM(pwm);
                pwm.DeadbandElimination = db;
                PWMData()["raw_value"] = speed;
                Assert.AreEqual(expected, Math.Round(pwm.GetSpeed(), 2));
            }
        }

        [Test]
        [TestCase(1600, 1.0)]
        [TestCase(400, -1.0)]
        public void TestGetSpeedLimits(int speed, double expected)
        {
            using (PWM pwm = NewPWM())
            {
                boundPWM(pwm);
                PWMData()["raw_value"] = speed;
                Assert.AreEqual(expected, pwm.GetSpeed());
            }
        }

        [Test]
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
        [Test]
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

        [Test]
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
        [Test]
        public void TestGetRawFreed()
        {
            PWM pwm = NewPWM();
            pwm.Dispose();
            pwm.GetRaw();
        }
        */

        [Test]
        [TestCase(PeriodMultiplier.K1X, 0)]
        [TestCase(PeriodMultiplier.K2X, 1)]
        [TestCase(PeriodMultiplier.K4X, 3)]
        public void TestSetPeriodMultiplier(PeriodMultiplier setting, int expected)
        {
            using (PWM pwm = NewPWM())
            {
                pwm.PeriodMultiplier = setting;
                Assert.AreEqual(expected, PWMData()["period_scale"]);
            }
        }

        [Test]
        public void TestPWMZeroLatch()
        {
            using (PWMOverride pwm = new PWMOverride(2))
            {
                pwm.PublicSetZeroLatch();
                Assert.IsTrue(PWMData()["zero_latch"]);
            }
            Assert.IsFalse(PWMData()["zero_latch"]);
        }

        
    }

    internal class PWMOverride : PWM
    {
        public PWMOverride(int channel) : base(channel)
        {
        }

        internal void PublicSetZeroLatch()
        {
            SetZeroLatch();
        }
    }
}
