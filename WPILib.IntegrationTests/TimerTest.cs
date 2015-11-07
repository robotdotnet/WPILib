using System;
using NUnit.Framework;
using WPILib.IntegrationTests.Test;

namespace WPILib.IntegrationTests
{
    [TestFixture]
    public class TimerTest : AbstractComsSetup
    {
        private static readonly long TIMER_TOLERANCE = (long)(2.5 * 1000);
        private static readonly long TIMER_RUNTIME = 5 * 1000000;

        [Test]
        public void DelayTest()
        {
            Console.WriteLine("Starting Delay");
            long startTime = Utility.GetFPGATime();

            Timer.Delay(TIMER_RUNTIME / 1000000.0);

            long endTime = Utility.GetFPGATime();
            long difference = endTime - startTime;
            Console.WriteLine("Ending Delay");
            long offset = difference - TIMER_RUNTIME;
            Console.WriteLine($"Offset: {offset}");
            Assert.AreEqual(TIMER_RUNTIME, difference, TIMER_TOLERANCE, $"Timer.Delay ran {offset} microseconds too long");

        }
    }
}
