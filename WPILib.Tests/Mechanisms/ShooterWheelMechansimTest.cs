using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAL.Simulator.Inputs;
using HAL.Simulator.Mechanisms;
using HAL.Simulator.Outputs;
using NUnit.Framework;

namespace WPILib.Tests.Mechanisms
{
    [TestFixture]
    public class ShooterWheelMechansimTest : TestBase
    {
        [Test]
        public void TestSpinUpTime()
        {
            using (Talon t = new Talon(0))
            using (Counter c = new Counter(0))
            {
                ISimSpeedController s = new SimPWMController(0);
                IServoFeedback f = new SimCounter(0);
                DCMotor motor = DCMotor.MakeCIM();

                double inertia = 0.005;
                double deaccel = -80.0;

                ShooterWheelMechanism mech = new ShooterWheelMechanism(s, f, motor, false, 0, deaccel, inertia);




            }

               
        }
    }
}
