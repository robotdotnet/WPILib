using NUnit.Framework;
using WPILib.Internal;

namespace WPILib.Tests
{
    [TestFixture]
    public class TestWPITimer : TestBase
    {
        [Test]
        public void TestTimerCreate()
        {
            Assert.DoesNotThrow(() =>
            {
                Timer timer = new Timer();
            });
        }

        [Test]
        public void TestTimerGet()
        {
            Timer timer = new Timer();
            timer.Reset();
            timer.Start();
            Timer.Delay(.3);
            timer.Stop();
            Assert.That(timer.HasPeriodPassed(0.25));
            Assert.That(!timer.HasPeriodPassed(0.55));
            Assert.That(timer.Get, Is.EqualTo(0.3).Within(0.05));

            Timer.Delay(0.3);
            Assert.That(timer.HasPeriodPassed(0.25));
            Assert.That(!timer.HasPeriodPassed(0.55));
            Assert.That(timer.Get, Is.EqualTo(0.3).Within(0.05));
        }
    }
}
