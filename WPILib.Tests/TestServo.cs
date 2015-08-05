using System.Collections.Generic;
using HAL_Base;
using NUnit.Framework;

namespace WPILib.Tests
{
    [TestFixture]
    public class TestServo : TestBase
    {


        public Servo NewServo()
        {
            return new Servo(2);
        }

        private static Dictionary<dynamic, dynamic> HalData()
        {
            return HAL.halData;
        }


        [Test]
        public void TestServoInitialized()
        {
            using (Servo s = NewServo())
            {
                Assert.AreEqual(HalData()["pwm"][2]["type"], "servo");
            }
        }
        [Test]
        public void TestServoStarts0()
        {
            using (Servo s = NewServo())
            {
                Assert.AreEqual(s.Get(), 0);
            }
        }
        [Test]
        public void TestServoSet()
        {
            using (Servo s = NewServo())
            {
                s.Set(1.0);
                Assert.AreEqual(s.Get(), 1.0);
            }
        }
        [Test]
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
