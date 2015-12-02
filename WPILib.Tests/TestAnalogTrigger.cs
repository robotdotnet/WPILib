using HAL.Simulator;
using HAL.Simulator.Data;
using NUnit.Framework;

namespace WPILib.Tests
{
    [TestFixture]
    public class TestAnalogTrigger : TestBase
    {

        private AnalogTriggerData GetData(int index)
        {
            return SimData.AnalogTrigger[index];
        }

        [Test]
        public void TestAnalogTriggerInitFree([Range(0, 7)]int pin)
        {
            int index;
            using (AnalogTrigger trigger = new AnalogTrigger(pin))
            {
                index = trigger.Index;
                Assert.IsTrue(GetData(index).Initialized);
                Assert.AreEqual(pin, GetData(index).AnalogPin);
            }
            Assert.IsFalse(GetData(index).Initialized);
        }

        [Test]
        public void TestSetFiltered()
        {
            using (AnalogTrigger at = new AnalogTrigger(2))
            {
                at.Filtered = true;
                Assert.AreEqual(TrigerType.Filtered, GetData(at.Index).TrigType);
            }
        }
    }
}
