using System;
using System.Collections.Generic;
using HAL.Simulator;
using HAL.Simulator.Data;
using NetworkTables.Tables;
using NUnit.Framework;
using WPILib.Interfaces;

// ReSharper disable UnusedVariable

namespace WPILib.Tests
{
    [TestFixture(0)]
    [TestFixture(5)]
    [TestFixture(58)]
    public class TestCompressor : TestBase
    {
        private readonly int m_module;

        public TestCompressor(int module)
        {
            m_module = module;
        }

        private CompressorData GetData()
        {
            return SimData.GetPCM(m_module).Compressor;
        }

        public Compressor GetCompressor()
        {
            return new Compressor(m_module);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            SimData.ResetHALData(false);
        }

        [Test]
        public void TestCompressorAllModules()
        {
            List<Compressor> compressors = new List<Compressor>();
            for (int i = 0; i < SolenoidModules; i++)
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
                var cmp = new Compressor(SolenoidModules);
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
            GetData().PressureSwitch = true;
            Assert.IsTrue(GetCompressor().GetPressureSwitchValue());
        }

        [Test]
        public void TestCompressorCurrent()
        {
            GetData().Current = 42;
            Assert.AreEqual((double)GetCompressor().GetCompressorCurrent(), 42);
        }

        [Test]
        public void TestCompressorEnabled()
        {
            var comp = GetCompressor();

            GetData().On = true;
            Assert.IsTrue(comp.Enabled());

            GetData().On = false;
            Assert.IsFalse(comp.Enabled());
        }

        [Test]
        public void TestCompressorCurrentTooHighSticky()
        {
            var comp = GetCompressor();

            Assert.That(comp.GetCompressorCurrentTooHighStickyFault(), Is.False);
        }
        [Test]
        public void TestCompressorCurrentTooHigh()
        {
            var comp = GetCompressor();

            Assert.That(comp.GetCompressorCurrentTooHighFault(), Is.False);
        }
        [Test]
        public void TestCompressorShorted()
        {
            var comp = GetCompressor();

            Assert.That(comp.GetCompressorShortedFault(), Is.False);
        }
        [Test]
        public void TestCompressorShortedSticky()
        {
            var comp = GetCompressor();

            Assert.That(comp.GetCompressorShortedStickyFault(), Is.False);
        }
        [Test]
        public void TestCompressorNotConntected()
        {
            var comp = GetCompressor();

            Assert.That(comp.GetCompressorNotConnectedFault(), Is.False);
        }
        [Test]
        public void TestCompressorNotConnectedSticky()
        {
            var comp = GetCompressor();

            Assert.That(comp.GetCompressorNotConnectedStickyFault(), Is.False);
        }

        [Test]
        public void TestCompressorClearAllStickyFaults()
        {
            var comp = GetCompressor();

            Assert.DoesNotThrow(() =>
            {
                comp.ClearAllPCMStickyFaults();
            });
        }

        [Test]
        public void TestCompressorSmartDashboardName()
        {
            Assert.AreEqual("Compressor", GetCompressor().SmartDashboardType);
        }


        [Test]
        public void TestUpdateTableNull()
        {
            Compressor compressor = new Compressor();
            Assert.DoesNotThrow(() =>
            {
                compressor.UpdateTable();
            });
        }

        [Test]
        public void TestStartLiveWindowMode()
        {
            Compressor compressor = new Compressor();
            Assert.DoesNotThrow(() =>
            {
                compressor.StartLiveWindowMode();
            });
        }

        [Test]
        public void TestStopLiveWindowMode()
        {
            Compressor compressor = new Compressor();
            Assert.DoesNotThrow(() =>
            {
                compressor.StopLiveWindowMode();
            });
        }

        [Test]
        public void TestStartLiveWindowModeTable()
        {
            Compressor compressor = new Compressor();
            Assert.DoesNotThrow(() =>
            {
                ITable table = new MockNetworkTable();
                compressor.InitTable(table);
            });


        }

        [Test]
        public void TestInitTable()
        {
            Compressor compressor = new Compressor();
            ITable table = new MockNetworkTable();
            Assert.DoesNotThrow(() =>
            {
                compressor.InitTable(table);
            });
            Assert.That(compressor.Table, Is.EqualTo(table));
        }
    }
}
