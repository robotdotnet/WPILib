using System;
using HAL.Simulator;
using HAL.Simulator.Data;
using NUnit.Framework;

namespace WPILib.Tests
{
    [TestFixture(0)]
    [TestFixture(18)]
    [TestFixture(53)]
    public class TestPowerDistributionPanel : TestBase
    {
        private readonly int m_module;

        public TestPowerDistributionPanel(int module)
        {
            m_module = module;
        }

        public HALSimPDPData PDPData()
        {
            return SimData.PDP[m_module];
            //return SimData.GetPDP(m_module);
        }

        public PowerDistributionPanel GetPDP(int module)
        {
            return new PowerDistributionPanel(m_module);
        }

        [TestCase(1)]
        [TestCase(53)]
        public void TestCreatePDPModule(int module)
        {
            SimData.ResetHALData(false);

        }

        [Test]
        public void TestCreatePDPLowerLimit()
        {
            SimData.ResetHALData(false);
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                PowerDistributionPanel pdp = new PowerDistributionPanel(-1);
                pdp.GetCurrent(0);
            });
        }

        [Test]
        public void TestCreatePDPUpperLimit()
        {
            SimData.ResetHALData(false);
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                PowerDistributionPanel pdp = new PowerDistributionPanel(PDPModules);
                pdp.GetCurrent(0);
            });
        }

        [Test]
        public void TestPDPGetVoltage()
        {
            var pdp = GetPDP(m_module);
            PDPData().SetVoltage(3.14);
            Assert.AreEqual(3.14, pdp.GetVoltage());
        }

        [Test]
        public void TestPDPGetTemperature()
        {
            var pdp = GetPDP(m_module);
            PDPData().SetTemperature(90);
            Assert.AreEqual(90, pdp.GetTemperature());
        }

        [Test]
        public void TestPDPGetCurrent([Range(0, 15)]int channel)
        {
            var pdp = GetPDP(m_module);
            PDPData().SetCurrent(channel, channel * 3); 
            Assert.AreEqual(channel * 3, pdp.GetCurrent(channel));
        }

        [TestCase(-1)]
        [TestCase(16)]
        public void TestPDPGetCurrentLimits(int channel)
        {
            var pdp = GetPDP(m_module);
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                pdp.GetCurrent(channel);
            });
        }
        /*
        [Test]
        public void TestPDPGetTotalCurrent()
        {
            var pdp = GetPDP(m_module);
            double sum = 0;
            for (int i = 0; i < 16; i++)
            {
                double set = 3.14 * i;
                PDPData().SetCurrent(i, set);
                sum += set;
            }
            Assert.AreEqual(sum, pdp.GetTotalCurrent());
        }
        /*
        [Test]
        public void TestPDPGetTotalPower()
        {
            var pdp = GetPDP(m_module);
            double sum = 0;
            for (int i = 0; i < 16; i++)
            {
                double set = 3.14 * i;
                PDPData().SetCurrent(i, set);
                sum += set;
            }
            PDPData().SetVoltage(12.5);
            sum *= 12.5;
            Assert.AreEqual(sum, pdp.GetTotalPower());
        }

        [Test]
        public void TestPDPGetTotalEnergy()
        {
            var pdp = GetPDP(m_module);
            PDPData().
            Assert.AreEqual(42, pdp.GetTotalEnergy());
        }

        [Test]
        public void TestPDPResetTotalEnergy()
        {
            var pdp = GetPDP(m_module);
            PDPData().TotalEnergy = 42;
            Assert.AreEqual(42, pdp.GetTotalEnergy());
            pdp.ResetTotalEnergy();
            Assert.AreEqual(0, pdp.GetTotalEnergy());
        }
        */

        [Test]
        public void TestPDPClearStickyFaults()
        {
            var pdp = GetPDP(m_module);
            Assert.DoesNotThrow(() =>
            {
                pdp.ClearStickyFaults();
            });
        }
    }
}
