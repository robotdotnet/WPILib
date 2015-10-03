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
        public override int GetPDPChannel()
        {
            return TestBench.kTalonPDPChannel;
        }

        protected override ISpeedController GiveSpeedController()
        {
            return new Talon(TestBench.kTalonChannel);
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
        public override int GetPDPChannel()
        {
            return TestBench.kVictorPDPChannel;
        }

        protected override ISpeedController GiveSpeedController()
        {
            return new Victor(TestBench.kVictorChannel);
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
        public override int GetPDPChannel()
        {
            return TestBench.kJaguarPDPChannel;
        }

        protected override ISpeedController GiveSpeedController()
        {
            return new Jaguar(TestBench.kJaguarChannel);
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
    public class PIDTest : AbstractComsSetup
    {
        private NetworkTable table;
        private const double AbsoluteTolerance = 50;
        private const double OutputRange = 0.3;

        private PIDController controller = null;
        private static MotorEncoderFixture me = null;

        private readonly double k_p, k_i, k_d;

        public PIDTest(double p, double i, double d, Type type)
        {
            if (RobotBase.IsSimulation)
            {
                return;
            }

            MotorEncoderFixture mef = (MotorEncoderFixture)Activator.CreateInstance(type);

            if (me != null && !me.Equals(mef))
            {
                me.Teardown();
            }
            me = mef;
            this.k_p = p;
            this.k_i = i;
            this.k_d = d;
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
            me?.Teardown();
            me = null;
        }

        [SetUp]
        public void Setup()
        {
            me.Setup();
            table = NetworkTable.GetTable("TEST_PID");
            controller = new PIDController(k_p, k_i, k_d, me.GetEncoder(), me.GetMotor());
            controller.InitTable(table);
        }

        [TearDown]
        public void TearDown()
        {
            controller.Disable();
            controller.Dispose();
            controller = null;
            me.Reset();
        }

        private void SetupAbsoluteTolerance()
        {
            controller.SetAbsoluteTolerance(AbsoluteTolerance);
        }

        private void SetupOutputRange()
        {
            controller.SetOutputRange(-OutputRange, OutputRange);
        }

        [Test]
        public void TestInitialSettings()
        {
            SetupAbsoluteTolerance();
            SetupOutputRange();
            double setpoint = 2500.0;
            controller.Setpoint = setpoint;
            Assert.IsFalse(controller.Enabled, "PID did not begin disabled");
            Assert.AreEqual(setpoint, controller.GetError(), 0, "PID.GetError() did not start at " + setpoint);
            Assert.AreEqual(k_p, table.GetNumber("p"), 0);
            Assert.AreEqual(k_i, table.GetNumber("i"), 0);
            Assert.AreEqual(k_d, table.GetNumber("d"), 0);
            Assert.AreEqual(setpoint, table.GetNumber("setpoint"), 0);
            Assert.IsFalse(table.GetBoolean("enabled"));
        }

        [Test]
        public void TestRestartAfterEnable()
        {
            SetupAbsoluteTolerance();
            SetupOutputRange();
            double setpoint = 2500.0;
            controller.Setpoint = setpoint;
            controller.Enable();
            Timer.Delay(0.5);
            Assert.IsTrue(table.GetBoolean("enabled"));
            Assert.IsTrue(controller.Enabled);
            Assert.AreNotEqual(0.0, me.GetMotor().Get());
            controller.Reset();
            Assert.IsFalse(table.GetBoolean("enabled"));
            Assert.IsFalse(controller.Enabled);
            Assert.AreEqual(0, me.GetMotor().Get(), 0);
        }

        [Test]
        public void TestSetSetpoint()
        {
            SetupAbsoluteTolerance();
            SetupOutputRange();
            double setpoint = 2500.0;
            controller.Disable();
            controller.Setpoint = setpoint;
            controller.Enable();
            Assert.AreEqual(setpoint, controller.Setpoint, "Did not correctly set setpoint");
        }

        [Test, Timeout(10000)]
        public void TestRotateToTarget()
        {
            SetupAbsoluteTolerance();
            SetupOutputRange();
            double setpoint = 2500.0;
            Assert.AreEqual(0, controller.Get(), 0, PIDData() + " did not start at 0");
            controller.Setpoint = setpoint;
            Assert.AreEqual(setpoint, controller.GetError(), 0, PIDData() + " did not have an error of " + setpoint);
            controller.Enable();
            Timer.Delay(0.5);
            controller.Disable();
            Assert.IsTrue(controller.OnTarget(), PIDData() + " Was not on Target. Controller Error: " + controller.GetError());
        }

        private string PIDData()
        {
            return me.GetType() + " PID {P:" + controller.P + " I:" + controller.I + " D:"
                + controller.D + "} ";
        }

        [Test]
        public void TestOnTargetNoToleranceSet()
        {
            SetupOutputRange();
            Assert.Throws<InvalidOperationException>(() =>
            {
                controller.OnTarget();
            });
        }
    }
}
