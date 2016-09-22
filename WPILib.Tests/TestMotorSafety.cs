//using HAL.Simulator;
//using NUnit.Framework;
//using System.Threading;
//using static HAL.Simulator.DriverStationHelper;

//namespace WPILib.Tests
//{
//    [TestFixture]
//    [Ignore("Ignore until I can figure this out. Will probably split into 2")]
//    public class TestMotorSafety : TestBase
//    {
//        [TestFixtureSetUp]
//        public static void TestSetup()
//        {
//            StopDSLoop();
//        }

//        [TestFixtureTearDown]
//        public static void TestTeardown()
//        {
//            StartDSLoop();
//        }

//        [Test]
//        public void TestMotorSafetyNoFeed()
//        {
//            using (Talon t = new Talon(0))
//            {
//                t.SafetyEnabled = true;
//                t.Expiration = 0.05;
//                t.Set(1.0);
//                double valAfterSet = SimData.PWM[0].Value;
//                bool aliveAfterSet = t.Alive;
//                UpdateData();
//                Thread.Sleep(1);
//                UpdateData();
//                Thread.Sleep(1);
//                UpdateData();
//                Thread.Sleep(1);
//                UpdateData();
//                Thread.Sleep(250);
//                UpdateData();
//                Thread.Sleep(1);
//                UpdateData();
//                Thread.Sleep(1);
//                UpdateData();
//                Thread.Sleep(1);
//                UpdateData();
//                Thread.Sleep(1);
//                UpdateData();
//                Thread.Sleep(1);
//                UpdateData();
//                Thread.Sleep(1);
//                double valAfterSleep = SimData.PWM[0].Value;
//                bool aliveAfterSleep = t.Alive;

//                Assert.That(valAfterSet, Is.EqualTo(1.0).Within(0.0001));
//                Assert.That(aliveAfterSet, Is.True);
//                Assert.That(valAfterSleep, Is.EqualTo(0.0).Within(0.001));
//                Assert.That(aliveAfterSleep, Is.False);

//            }
//        }

//        [Test]
//        public void TestMotorSafetyFeed()
//        {
//            using (Talon t = new Talon(0))
//            {

//                t.SafetyEnabled = true;
//                t.Expiration = 0.05;
//                t.Set(1.0);
//                double valAfterSet = SimData.PWM[0].Value;
//                bool aliveAfterSet = t.Alive;
//                UpdateData();
//                t.Set(1.0);
//                UpdateData();
//                t.Set(1.0);
//                UpdateData();
//                t.Set(1.0);
//                UpdateData();
//                Thread.Sleep(100);
//                t.Set(1.0);
//                UpdateData();
//                t.Set(1.0);
//                UpdateData();
//                t.Set(1.0);
//                UpdateData();
//                t.Set(1.0);
//                UpdateData();
//                t.Set(1.0);
//                UpdateData();
//                t.Set(1.0);
//                UpdateData();
//                double valAfterSleep = SimData.PWM[0].Value;
//                bool aliveAfterSleep = t.Alive;

//                Assert.That(valAfterSet, Is.EqualTo(1.0).Within(0.0001));
//                Assert.That(aliveAfterSet, Is.True);
//                Assert.That(valAfterSleep, Is.EqualTo(1.0).Within(0.001));
//                Assert.That(aliveAfterSleep, Is.True);

//            }
//        }
//    }
//}
