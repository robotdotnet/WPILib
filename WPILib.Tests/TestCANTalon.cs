using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAL_Simulator;
using HAL_Simulator.Data;
using NUnit.Framework;

namespace WPILib.Tests
{
    [TestFixture(0)]
    [TestFixture(5)]
    [TestFixture(58)]
    public class TestCANTalon : TestBase
    {
        private readonly int m_id;

        public TestCANTalon(int id)
        {
            m_id = id;
        }

        public CANTalon NewTalon()
        {
            return new CANTalon(m_id);
        }

        private CanTalonData GetTalonData()
        {
            return SimData.GetCanTalon(m_id);
        }

        [Test]
        public void TestTalonIdUnderLimit()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var s = new CANTalon(-1);
            });
        }

        [Test]
        public void TestTalonIdOverLimit()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var s = new CANTalon(CANTalon.MaxTalonId + 1);
            });
        }

        [Test]
        public void TestTalonCreate()
        {
            using (CANTalon t = NewTalon())
            {
                CanTalonData data = GetTalonData();
                Assert.That(data, Is.Not.Null);
            }
        }


    }
}
