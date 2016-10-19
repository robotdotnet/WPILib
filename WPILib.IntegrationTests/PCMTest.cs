using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAL.Simulator;
using HAL.Simulator.Data;
using NUnit.Framework;
using WPILib.IntegrationTests.Test;

namespace WPILib.IntegrationTests
{
    [TestFixture]
    public class PCMTest : AbstractComsSetup
    {
        protected const double CompressorDelayTime = 2.0;
        protected const double SolenoidDelayTime = 1.0;

        protected const double CompressorOnVoltage = 5.00;
        protected const double CompressorOffVoltage = 1.68;

        private static Compressor compressor;
        private static DigitalOutput fakePressureSwitch;
        private static AnalogInput fakeCompressor;

        private static DigitalInput fakeSolenoid1, fakeSolenoid2;

        private static NotifyCallback pressureSwitchCallback = null;

        private static int callbackId;

        [TestFixtureSetUp]
        public static void SetUpBeforeClass()
        {
            compressor = new Compressor();

            fakePressureSwitch = new DigitalOutput(11);

            fakeCompressor = new AnalogInput(1);

            fakeSolenoid1 = new DigitalInput(12);
            fakeSolenoid2 = new DigitalInput(13);

            if (RobotBase.IsSimulation)
            {
                /*
                pressureSwitchCallback = (s, o) =>
                {
                    var comp = SimData.GetPCM(0).Compressor;
                    comp.PressureSwitch = o;
                    comp.On = o;
                    double voltage = o ? CompressorOffVoltage : CompressorOnVoltage;
                    SimData.AnalogIn[1].Voltage = voltage;
                };
                */
                pressureSwitchCallback = (string name, HAL_Value value) =>
                {
                    SimData.PCM[0].SetPressureSwitch(value.GetBoolean());
                    SimData.PCM[0].SetCompressorOn(value.GetBoolean());
                    double voltage = value.GetBoolean() ? CompressorOffVoltage : CompressorOnVoltage;
                    SimData.AnalogIn[1].SetVoltage(voltage);
                };
                callbackId = SimData.DIO[11].RegisterValueCallback(pressureSwitchCallback, false);
            }
        }

        [TestFixtureTearDown]
        public static void TearDownAfterClass()
        {
            compressor.Dispose();

            fakePressureSwitch.Dispose();
            fakeCompressor.Dispose();

            fakeSolenoid1.Dispose();
            fakeSolenoid2.Dispose();

            if (RobotBase.IsSimulation)
            {
                SimData.DIO[11].CancelValueCallback(callbackId);
            }
        }

        [SetUp]
        public void Reset()
        {
            compressor.Stop();
            fakePressureSwitch.Set(false);
        }

        [Test]
        public void TestPressureSwitch()
        {
            double range = 0.1;
            Reset();
            compressor.ClosedLoopControl = true;

            fakePressureSwitch.Set(true);
            Timer.Delay(CompressorDelayTime);

            Assert.That(fakeCompressor.GetVoltage(), Is.EqualTo(CompressorOffVoltage).Within(range));

            fakePressureSwitch.Set(false);

            Assert.That(fakeCompressor.GetVoltage(), Is.EqualTo(CompressorOnVoltage).Within(range));
        }

        [Test]
        public void TestSolenoid()
        {
            Reset();

            using (Solenoid solenoid1 = new Solenoid(0))
            using (Solenoid solenoid2 = new Solenoid(1))
            {
                NotifyCallback solenoid1Callback = null;
                NotifyCallback solenoid2Callback = null;
                int callback1Id = -1;
                int callback2Id = -1;

                if (RobotBase.IsSimulation)
                {
                    solenoid1Callback = (string s, HAL_Value val) =>
                    {
                        SimData.DIO[12].SetValue(!val.GetBoolean());
                    };
                    callback1Id = SimData.PCM[0].RegisterSolenoidOutputCallback(0,solenoid1Callback , false);

                    solenoid2Callback = (s, o) =>
                    {
                        SimData.DIO[13].SetValue(!o.GetBoolean());
                    };
                    callback2Id = SimData.PCM[0].RegisterSolenoidOutputCallback(1, solenoid2Callback);
                }

                solenoid1.Set(false);
                solenoid2.Set(false);

                Timer.Delay(SolenoidDelayTime);

                Assert.That(fakeSolenoid1.Get());
                Assert.That(fakeSolenoid2.Get());
                Assert.That(!solenoid1.Get());
                Assert.That(!solenoid2.Get());

                solenoid1.Set(true);
                solenoid2.Set(false);

                Timer.Delay(SolenoidDelayTime);

                Assert.That(!fakeSolenoid1.Get());
                Assert.That(fakeSolenoid2.Get());
                Assert.That(solenoid1.Get());
                Assert.That(!solenoid2.Get());

                solenoid1.Set(false);
                solenoid2.Set(true);

                Timer.Delay(SolenoidDelayTime);

                Assert.That(fakeSolenoid1.Get());
                Assert.That(!fakeSolenoid2.Get());
                Assert.That(!solenoid1.Get());
                Assert.That(solenoid2.Get());

                solenoid1.Set(true);
                solenoid2.Set(true);

                Timer.Delay(SolenoidDelayTime);

                Assert.That(!fakeSolenoid1.Get());
                Assert.That(!fakeSolenoid2.Get());
                Assert.That(solenoid1.Get());
                Assert.That(solenoid2.Get());

                if (RobotBase.IsSimulation)
                {
                    SimData.PCM[0].CancelSolenoidOutputCallback(0, callback1Id);
                    SimData.PCM[0].CancelSolenoidOutputCallback(1, callback2Id);
                }
            }

        }

        [Test]
        public void TestDoubleSolenoid()
        {
            using (DoubleSolenoid solenoid = new DoubleSolenoid(0, 1))
            {
                NotifyCallback solenoid1Callback = null;
                NotifyCallback solenoid2Callback = null;

                int callback1Id = -1;
                int callback2Id = -1;

                if (RobotBase.IsSimulation)
                {
                    solenoid1Callback = (s, o) =>
                    {
                        SimData.DIO[12].SetValue(!o.GetBoolean());
                    };
                    callback1Id = SimData.PCM[0].RegisterSolenoidOutputCallback(0, solenoid1Callback);

                    solenoid2Callback = (s, o) =>
                    {
                        SimData.DIO[13].SetValue(!o.GetBoolean());
                    };
                    callback2Id = SimData.PCM[0].RegisterSolenoidOutputCallback(1, solenoid2Callback);
                }

                solenoid.Set(DoubleSolenoid.Value.Off);
                Timer.Delay(SolenoidDelayTime);
                Assert.That(fakeSolenoid1.Get());
                Assert.That(fakeSolenoid2.Get());
                Assert.That(solenoid.Get(), Is.EqualTo(DoubleSolenoid.Value.Off));

                solenoid.Set(DoubleSolenoid.Value.Forward);
                Timer.Delay(SolenoidDelayTime);
                Assert.That(!fakeSolenoid1.Get());
                Assert.That(fakeSolenoid2.Get());
                Assert.That(solenoid.Get(), Is.EqualTo(DoubleSolenoid.Value.Forward));

                solenoid.Set(DoubleSolenoid.Value.Reverse);
                Timer.Delay(SolenoidDelayTime);
                Assert.That(fakeSolenoid1.Get());
                Assert.That(!fakeSolenoid2.Get());
                Assert.That(solenoid.Get(), Is.EqualTo(DoubleSolenoid.Value.Reverse));

                if (RobotBase.IsSimulation)
                {
                    SimData.PCM[0].CancelSolenoidOutputCallback(0, callback1Id);
                    SimData.PCM[0].CancelSolenoidOutputCallback(1, callback2Id);
                }
            }
        }
    }
}
