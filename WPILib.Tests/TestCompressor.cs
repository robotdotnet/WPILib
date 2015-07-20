using System.Collections.Generic;
using HAL_Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WPILib.Tests
{
    [TestClass]
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

        [TestMethod]
        public void TestCompressorOperation()
        {
            var comp = GetCompressor();
            comp.Start();
            Assert.IsTrue(comp.ClosedLoopControl);

            comp.Stop();
            Assert.IsFalse(comp.ClosedLoopControl);
        }

        [TestMethod]
        public void TestCompressorSwitch()
        {
            GetData(0)["pressure_switch"] = true;
            Assert.IsTrue(GetCompressor().GetPressureSwitchValue());
        }

        [TestMethod]
        public void TestCompressorCurrent()
        {
            GetData(0)["current"] = 42;
            Assert.AreEqual((double)GetCompressor().GetCompressorCurrent(), 42);
        }
    }
}
