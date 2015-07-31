using System.Collections.Generic;
using HAL_Base;
using NUnit.Framework;

namespace WPILib.Tests
{
    [TestFixture]
    public class TestCompressor
    {
        private Dictionary<dynamic, dynamic> GetData(int module)
        {
            return HAL.halData["pcm"][module]["compressor"];
        }

        public Compressor GetCompressor()
        {
            return new Compressor();
        }

        [Test]
        public void TestCompressorOperation()
        {
            var comp = GetCompressor();
            comp.Start();
            Assert.IsTrue(comp.ClosedLoopControl);

            comp.Stop();
            Assert.IsFalse(comp.ClosedLoopControl);
        }

        [Test]
        public void TestCompressorSwitch()
        {
            GetData(0)["pressure_switch"] = true;
            Assert.IsTrue(GetCompressor().GetPressureSwitchValue());
        }

        [Test]
        public void TestCompressorCurrent()
        {
            GetData(0)["current"] = 42;
            Assert.AreEqual((double)GetCompressor().GetCompressorCurrent(), 42);
        }
    }
}
