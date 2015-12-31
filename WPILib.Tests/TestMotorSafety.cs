using HAL.Simulator;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WPILib.Tests
{
    [TestFixture]
    public class TestMotorSafety : TestBase
    {
        [Test]
        public void TestMotorSafetyNoFeed()
        {
            using (Talon t = new Talon(0))
            {
                t.SafetyEnabled = true;
                t.Expiration = 0.05;
                t.Set(1.0);
                double valAfterSet = SimData.PWM[0].Value;
                bool aliveAfterSet = t.Alive;
                Thread.Sleep(250);
                double valAfterSleep = SimData.PWM[0].Value;
                bool aliveAfterSleep = t.Alive;

                Assert.That(valAfterSet, Is.EqualTo(1.0).Within(0.0001));
                Assert.That(aliveAfterSet, Is.True);
                Assert.That(valAfterSleep, Is.EqualTo(0.0).Within(0.001));
                Assert.That(aliveAfterSleep, Is.False);

            }
        }

        [Test]
        public void TestMotorSafetyFeed()
        {
            using (Talon t = new Talon(0))
            {
                using (System.Threading.Timer timer = new System.Threading.Timer((o) =>
                {
                    t.Set(1.0);
                }, null, 20, 20))
                {

                    t.SafetyEnabled = true;
                    t.Expiration = 0.05;
                    t.Set(1.0);
                    double valAfterSet = SimData.PWM[0].Value;
                    bool aliveAfterSet = t.Alive;

                    Thread.Sleep(250);
                    double valAfterSleep = SimData.PWM[0].Value;
                    bool aliveAfterSleep = t.Alive;

                    Assert.That(valAfterSet, Is.EqualTo(1.0).Within(0.0001));
                    Assert.That(aliveAfterSet, Is.True);
                    Assert.That(valAfterSleep, Is.EqualTo(1.0).Within(0.001));
                    Assert.That(aliveAfterSleep, Is.True);
                }
            }
        }
    }
}
