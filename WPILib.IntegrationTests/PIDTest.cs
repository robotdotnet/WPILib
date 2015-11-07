using System;
using NetworkTables;
using NUnit.Framework;
using WPILib.IntegrationTests.Fixtures;
using WPILib.IntegrationTests.Test;
using WPILib.Interfaces;

namespace WPILib.IntegrationTests
{
    public class TalonMotorFixture : MotorEncoderFixture
    {
        public override int GetPdpChannel()
        {
            return TestBench.TalonPdpChannel;
        }

        protected override ISpeedController GiveSpeedController()
        {
            return new Talon(TestBench.TalonChannel);
        }

        protected override DigitalInput GiveDigitalInputA()
        {
            return new DigitalInput(0);
        }

        protected override DigitalInput GiveDigitalInputB()
        {
            return new DigitalInput(1);
        }
    }

    public class VictorMotorFixture : MotorEncoderFixture
    {
        public override int GetPdpChannel()
        {
            return TestBench.VictorPdpChannel;
        }

        protected override ISpeedController GiveSpeedController()
        {
            return new Victor(TestBench.VictorChannel);
        }

        protected override DigitalInput GiveDigitalInputA()
        {
            return new DigitalInput(2);
        }

        protected override DigitalInput GiveDigitalInputB()
        {
            return new DigitalInput(3);
        }
    }

    public class JaguarMotorFixture : MotorEncoderFixture
    {
        public override int GetPdpChannel()
        {
            return TestBench.JaguarPdpChannel;
        }

        protected override ISpeedController GiveSpeedController()
        {
            return new Jaguar(TestBench.JaguarChannel);
        }

        protected override DigitalInput GiveDigitalInputA()
        {
            return new DigitalInput(4);
        }

        protected override DigitalInput GiveDigitalInputB()
        {
            return new DigitalInput(5);
        }
    }

    [TestFixture(0.001d, 0.0005d, 0.0d, typeof(TalonMotorFixture))]
    [TestFixture(0.001d, 0.0005d, 0.0d, typeof(JaguarMotorFixture))]
    [TestFixture(0.001d, 0.0005d, 0.0d, typeof(VictorMotorFixture))]
    public class PidTest : AbstractComsSetup
    {
        private NetworkTable m_table;
        private const double AbsoluteTolerance = 50;
        private const double OutputRange = 0.3;

        private PIDController m_controller;
        private static MotorEncoderFixture s_me;

        private readonly double m_kP, m_kI, m_kD;

        public PidTest(double p, double i, double d, Type type)
        {
            if (RobotBase.IsSimulation)
            {
                return;
            }

            MotorEncoderFixture mef = (MotorEncoderFixture)Activator.CreateInstance(type);

            if (s_me != null && !s_me.Equals(mef))
            {
                s_me.Teardown();
            }
            s_me = mef;
            m_kP = p;
            m_kI = i;
            m_kD = d;
        }




        [TestFixtureSetUp]
        public static void SetUpBeforeClass()
        {
            //TODO: Get mock encoders working in sim
            if (RobotBase.IsSimulation)
            {
                Assert.Ignore();
            }
        }

        [TestFixtureTearDown]
        public static void TearDownAfterClass()
        {
            if (RobotBase.IsSimulation)
            {
                return;
            }
            s_me?.Teardown();
            s_me = null;
        }

        [SetUp]
        public void Setup()
        {
            s_me.Setup();
            m_table = NetworkTable.GetTable("TEST_PID");
            m_controller = new PIDController(m_kP, m_kI, m_kD, s_me.GetEncoder(), s_me.GetMotor());
            m_controller.InitTable(m_table);
        }

        [TearDown]
        public void TearDown()
        {
            m_controller.Disable();
            m_controller.Dispose();
            m_controller = null;
            s_me.Reset();
        }

        private void SetupAbsoluteTolerance()
        {
            m_controller.SetAbsoluteTolerance(AbsoluteTolerance);
        }

        private void SetupOutputRange()
        {
            m_controller.SetOutputRange(-OutputRange, OutputRange);
        }

        [Test]
        public void TestInitialSettings()
        {
            SetupAbsoluteTolerance();
            SetupOutputRange();
            double setpoint = 2500.0;
            m_controller.Setpoint = setpoint;
            Assert.IsFalse(m_controller.Enabled, "PID did not begin disabled");
            Assert.AreEqual(setpoint, m_controller.GetError(), 0, "PID.GetError() did not start at " + setpoint);
            Assert.AreEqual(m_kP, m_table.GetNumber("p"), 0);
            Assert.AreEqual(m_kI, m_table.GetNumber("i"), 0);
            Assert.AreEqual(m_kD, m_table.GetNumber("d"), 0);
            Assert.AreEqual(setpoint, m_table.GetNumber("setpoint"), 0);
            Assert.IsFalse(m_table.GetBoolean("enabled"));
        }

        [Test]
        public void TestRestartAfterEnable()
        {
            SetupAbsoluteTolerance();
            SetupOutputRange();
            double setpoint = 2500.0;
            m_controller.Setpoint = setpoint;
            m_controller.Enable();
            Timer.Delay(0.5);
            Assert.IsTrue(m_table.GetBoolean("enabled"));
            Assert.IsTrue(m_controller.Enabled);
            Assert.AreNotEqual(0.0, s_me.GetMotor().Get());
            m_controller.Reset();
            Assert.IsFalse(m_table.GetBoolean("enabled"));
            Assert.IsFalse(m_controller.Enabled);
            Assert.AreEqual(0, s_me.GetMotor().Get(), 0);
        }

        [Test]
        public void TestSetSetpoint()
        {
            SetupAbsoluteTolerance();
            SetupOutputRange();
            double setpoint = 2500.0;
            m_controller.Disable();
            m_controller.Setpoint = setpoint;
            m_controller.Enable();
            Assert.AreEqual(setpoint, m_controller.Setpoint, "Did not correctly set setpoint");
        }

        [Test, Timeout(10000)]
        public void TestRotateToTarget()
        {
            SetupAbsoluteTolerance();
            SetupOutputRange();
            double setpoint = 2500.0;
            Assert.AreEqual(0, m_controller.Get(), 0, PidData() + " did not start at 0");
            m_controller.Setpoint = setpoint;
            Assert.AreEqual(setpoint, m_controller.GetError(), 0, PidData() + " did not have an error of " + setpoint);
            m_controller.Enable();
            Timer.Delay(0.5);
            m_controller.Disable();
            Assert.IsTrue(m_controller.OnTarget(), PidData() + " Was not on Target. Controller Error: " + m_controller.GetError());
        }

        private string PidData()
        {
            return s_me.GetType() + " PID {P:" + m_controller.P + " I:" + m_controller.I + " D:"
                + m_controller.D + "} ";
        }

        [Test]
        public void TestOnTargetNoToleranceSet()
        {
            SetupOutputRange();
            Assert.Throws<InvalidOperationException>(() =>
            {
                m_controller.OnTarget();
            });
        }
    }
}
