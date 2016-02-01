using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WPILib.Interfaces;

namespace WPILib.Tests
{
    //[TestFixture]
    public class TestPIDTolerance : TestBase
    {
        private PIDController pid;

        private const double setPoint = 50.0;
        private const double tolerance = 10.0;
        private const double range = 200;

        private class FakeInput : IPIDSource
        {
            public double val;

            public FakeInput()
            {
                val = 0;
            }


            public double PidGet()
            {
                return val;
            }

            public PIDSourceType PIDSourceType { get; set; } = PIDSourceType.Displacement;
        }

        private FakeInput input;

        private class FakeOutput : IPIDOutput
        {
            public void PidWrite(double value)
            {
                
            }
        }

        private FakeOutput output;

        //[SetUp]
        public void SetUp()
        {
            input = new FakeInput();
            output = new FakeOutput();
            pid = new PIDController(0.05, 0.0, 0.0, input, output);
            pid.SetInputRange(-range/2, range/2);
        }

        //[TearDown]
        public void TearDown()
        {
            pid.Dispose();
            pid = null;
        }

        //[Test]
        public void TestAbsoluteTolerance()
        {
            pid.SetAbsoluteTolerance(tolerance);
            pid.Setpoint = setPoint;
            pid.Enable();
            Timer.Delay(1);
            Assert.That(pid.OnTarget(), Is.False, $"Error was in tolerance when it should not have been. Error was {pid.GetAvgError()}" );
            input.val = setPoint + tolerance/2;
            Timer.Delay(1);
            Assert.That(pid.OnTarget(), Is.True, $"Error was not in tolerance when it should have been. Error was {pid.GetAvgError()}");
            input.val = setPoint + 10*tolerance;
            Timer.Delay(1);
            Assert.That(pid.OnTarget(), Is.False, $"Error was in tolerance when it should not have been. Error was {pid.GetAvgError()}");
        }

        //[Test]
        public void TestPercentTolerance()
        {
            pid.SetPercentTolerance(tolerance);
            pid.Setpoint = setPoint;
            pid.Enable();
            Timer.Delay(1);
            Assert.That(pid.OnTarget(), Is.False, $"Error was in tolerance when it should not have been. Error was {pid.GetAvgError()}");
            input.val = setPoint + (tolerance)/200*range; //Half of percent tolerance away from setpoint.
            Timer.Delay(1);
            Assert.That(pid.OnTarget(), Is.True, $"Error was not in tolerance when it should have been. Error was {pid.GetAvgError()}");
            input.val = setPoint + (tolerance) / 50 * range;//double percent tolerance away from setpoint
            Timer.Delay(1);
            Assert.That(pid.OnTarget(), Is.False, $"Error was in tolerance when it should not have been. Error was {pid.GetAvgError()}");
        }
    }
}
