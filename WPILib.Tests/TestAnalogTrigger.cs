using System;
using System.Collections.Generic;
using System.Linq;
using HAL_Base;
using HAL_Simulator;
using HAL_Simulator.Data;
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
            int index = 0;
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
