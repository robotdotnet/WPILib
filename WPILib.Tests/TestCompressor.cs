using System;
using System.Collections.Generic;
using HAL_Base;
using NUnit.Framework;

namespace WPILib.Tests
{
    [TestFixture]
    public class TestCompressor : TestBase
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
        public void TestCompressorAllModules()
        {
            List<Compressor> compressors = new List<Compressor>();
            for (int i = 0; i < TestBase.SolenoidModules; i++)
            {
                compressors.Add(new Compressor(i));
            }

            foreach (var compressor in compressors)
            {
                compressor?.Dispose();
            }
        }

        [Test]
        public void TestCompressorLimits()
        {
            Assert.Throws(typeof (ArgumentOutOfRangeException), () =>
            {
                var cmp = new Compressor(TestBase.SolenoidModules);
            });           
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

        [Test]
        public void TestCompressorEnabled()
        {
            var comp = GetCompressor();

            GetData(0)["on"] = true;
            Assert.IsTrue(comp.Enabled());

            GetData(0)["on"] = false;
            Assert.IsFalse(comp.Enabled());
        }

        public void TestCompressorSmartDashboardName()
        {
            Assert.AreEqual("Compressor", GetCompressor().SmartDashboardType);
        }
    }
}
