using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAL_Simulator;
using HAL_Simulator.Data;
using NUnit.Framework;
using WPILib.Exceptions;

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
                var s = new CANTalon(CANTalon.TalonIds);
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

        [Test]
        public void TestMultipleAllocation()
        {
            using (CANTalon t = NewTalon())
            {
                Assert.Throws<AllocationException>(() =>
                {
                    CANTalon s = NewTalon();
                });
            }
        }

        [Test]
        public void TestReverseSensor()
        {
            using (CANTalon t = NewTalon())
            {
                t.ReverseSensor(true);

                Assert.That(GetTalonData().RevFeedbackSensor);

                t.ReverseSensor(false);

                Assert.That(!GetTalonData().RevFeedbackSensor);
            }
        }

        [Test]
        public void TestReverseOutput()
        {
            using (CANTalon t = NewTalon())
            {
                t.ReverseOutput(true);
                Assert.That(GetTalonData().RevMotDuringCloseLoopEn);
                t.ReverseOutput(false);
                Assert.That(!GetTalonData().RevMotDuringCloseLoopEn);
            }
        }

        [Test]
        public void TestGetEncoderPosition()
        {
            using (CANTalon t = NewTalon())
            {
                GetTalonData().EncPosition = 600;
                Assert.That(t.GetEncoderPosition(), Is.EqualTo(600).Within(0.01));
            }
        }

        [Test]
        public void TestGetEncoderVelocity()
        {
            using (CANTalon t = NewTalon())
            {
                GetTalonData().EncVel = 600;
                Assert.That(t.GetEncoderVelocity(), Is.EqualTo(600).Within(0.01));
            }
        }

        [Test]
        public void TestSetEncoderPosition()
        {
            using (CANTalon t = NewTalon())
            {
                t.SetEncoderPostition(600);
                Assert.That(GetTalonData().EncPosition, Is.EqualTo(600).Within(0.01));
            }
        }

        [Test]
        public void TestGetNumberQuadIdxRises()
        {
            using (CANTalon t = NewTalon())
            {
                GetTalonData().EncIndexRiseEvents = 500;
                Assert.That(t.GetNumberOfQuadIdxRises(), Is.EqualTo(500).Within(0.01));
            }
        }
    }
}
