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

        [TestCase(CANTalon.FeedbackDevice.AnalogEncoder, Result = CANTalon.FeedbackDeviceStatus.FeedbackStatusUnknown)]
        [TestCase(CANTalon.FeedbackDevice.AnalogPotentiometer, Result = CANTalon.FeedbackDeviceStatus.FeedbackStatusUnknown)]
        [TestCase(CANTalon.FeedbackDevice.QuadEncoder, Result = CANTalon.FeedbackDeviceStatus.FeedbackStatusUnknown)]
        [TestCase(CANTalon.FeedbackDevice.EncoderRising, Result = CANTalon.FeedbackDeviceStatus.FeedbackStatusUnknown)]
        [TestCase(CANTalon.FeedbackDevice.EncoderFalling, Result = CANTalon.FeedbackDeviceStatus.FeedbackStatusUnknown)]
        public CANTalon.FeedbackDeviceStatus TestIsSensorPresent(CANTalon.FeedbackDevice device)
        {
            using (CANTalon t = NewTalon())
            {
                return t.IsSensorPresent(device);
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

        [Test]
        public void TestGetPinStateQuadA()
        {
            using (CANTalon t = NewTalon())
            {
                GetTalonData().QuadApin = 1;
                Assert.That(t.GetPinStateQuadA(), Is.EqualTo(1).Within(0.01));
            }
        }

        [Test]
        public void TestGetPinStateQuadB()
        {
            using (CANTalon t = NewTalon())
            {
                GetTalonData().QuadBpin = 1;
                Assert.That(t.GetPinStateQuadB(), Is.EqualTo(1).Within(0.01));
            }
        }

        [Test]
        public void TestGetPinStateQuadIdx()
        {
            using (CANTalon t = NewTalon())
            {
                GetTalonData().QuadIdxpin = 1;
                Assert.That(t.GetPinStateQuadIdx(), Is.EqualTo(1).Within(0.01));
            }
        }

        [Test]
        public void TestSetAnalogPosition()
        {
            using (CANTalon t = NewTalon())
            {
                t.SetAnalogPosition(500);
                Assert.That(GetTalonData().AinPosition, Is.EqualTo(500).Within(0.01));
            }
        }

        [Test]
        public void TestGetAnalogInPosition()
        {
            using (CANTalon t = NewTalon())
            {
                GetTalonData().AnalogInWithOv = 500;
                Assert.That(t.GetAnalogInPosition(), Is.EqualTo(500).Within(0.01));
            }
        }

        [Test]
        public void TestGetAnalogInRaw()
        {
            using (CANTalon t = NewTalon())
            {
                GetTalonData().AnalogInWithOv = 500;
                Assert.That(t.GetAnalogInRaw(), Is.EqualTo(500 & 0x3FF).Within(0.01));
            }
        }

        [Test]
        public void TestGetAnalogInVelocity()
        {
            using (CANTalon t = NewTalon())
            {
                GetTalonData().AnalogInVel = 500;
                Assert.That(t.GetAnalogInVelocity(), Is.EqualTo(500).Within(0.01));
            }
        }

        [Test]
        public void TestGetClosedLoopError()
        {
            using (CANTalon t = NewTalon())
            {
                GetTalonData().CloseLoopErr = 500;
                Assert.That(t.GetClosedLoopError(), Is.EqualTo(500).Within(0.01));
            }
        }

        [TestCase(0)]
        [TestCase(1)]
        public void TestSetAllowableClosedLoopError(int profile)
        {
            using (CANTalon t = NewTalon())
            {
                t.Profile = profile;
                t.SetAllowableClosedLoopErr(5);
                if (profile == 0)
                {

                    Assert.That(GetTalonData().ProfileParamSlot0_AllowableClosedLoopErr, Is.EqualTo(5).Within(0.01));
                }
                else if (profile == 1)
                {
                    Assert.That(GetTalonData().ProfileParamSlot1_AllowableClosedLoopErr, Is.EqualTo(5).Within(0.01));
                }
            }
        }

        [Test]
        public void TestGetForwardLimitSwitch()
        {
            using (CANTalon t = NewTalon())
            {
                GetTalonData().LimitSwitchClosedFor = false;
                Assert.That(!t.IsForwardLimitSwitchClosed());

                GetTalonData().LimitSwitchClosedFor = true;
                Assert.That(t.IsForwardLimitSwitchClosed());
            }
        }

        [Test]
        public void TestGetReverseLimitSwitch()
        {
            using (CANTalon t = NewTalon())
            {
                GetTalonData().LimitSwitchClosedRev = false;
                Assert.That(!t.IsReverseLimitSwitchClosed());

                GetTalonData().LimitSwitchClosedRev = true;
                Assert.That(t.IsReverseLimitSwitchClosed());
            }
        }

        [Test]
        public void TestBrakeCoastMode()
        {
            using (CANTalon t = NewTalon())
            {
                GetTalonData().BrakeIsEnabled = false;
                Assert.That(!t.IsBrakeEnabledDuringNeutral());

                GetTalonData().BrakeIsEnabled = true;
                Assert.That(t.IsBrakeEnabledDuringNeutral());
            }
        }

        [Test]
        public void TestSetEncoderCodesPerRev()
        {
            using (CANTalon t = NewTalon())
            {
                t.EncoderCodesPerRev = 500;
                Assert.That(GetTalonData().NumberEncoderCPR, Is.EqualTo(500).Within(0.01));
            }
        }

        [Test]
        public void TestSetPotentiometerTurns()
        {
            using (CANTalon t = NewTalon())
            {
                t.PotentiometerTurns = 10;
                Assert.That(GetTalonData().NumberPotTurns, Is.EqualTo(10).Within(0.01));
            }
        }

        [Test]
        public void TestGetTemperature()
        {
            using (CANTalon t = NewTalon())
            {
                GetTalonData().Temp = 75.0;
                Assert.That(t.GetTemperature(), Is.EqualTo(75.0).Within(0.01));
            }
        }

        [Test]
        public void TestGetOutputCurrent()
        {
            using (CANTalon t = NewTalon())
            {
                GetTalonData().Current = 75.0;
                Assert.That(t.GetOutputCurrent(), Is.EqualTo(75.0).Within(0.01));
            }
        }

        [Test]
        public void TestGetOutputVoltage()
        {
            using (CANTalon t = NewTalon())
            {
                GetTalonData().AppliedThrottle = 800;
                GetTalonData().BatteryV = 12.0;
                Assert.That(t.GetOutputVoltage(), Is.EqualTo(12.0 * (800 / 1023.0)).Within(0.01));
            }
        }

        [Test]
        public void TestGetBusVoltage()
        {
            using (CANTalon t = NewTalon())
            {
                GetTalonData().BatteryV = 12.0;
                Assert.That(t.GetBusVoltage(), Is.EqualTo(12.0).Within(0.01));
            }
        }

        //TODO:Test Get Position
        //TODO:Test Set Position



    }
}

