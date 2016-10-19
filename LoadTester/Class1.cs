using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAL.Simulator;
using WPILib;
using WPILib.SmartDashboard;

namespace LoadTester
{
    public class Class1 : IterativeRobot
    {
        private Joystick joystick;
        private Servo servo;

        private AnalogGyro gyro;
        public override void RobotInit()
        {
            gyro = new AnalogGyro(0);
            joystick = new Joystick(0);
            servo = new Servo(0);
        }

        public override void DisabledPeriodic()
        {
            SmartDashboard.PutNumber("Gyro", gyro.GetAngle());
        }

        public override void TeleopPeriodic()
        {
            SmartDashboard.PutNumber("Gyro", gyro.GetAngle());
            if (joystick.GetRawButton(1))
            {
                servo.SetAngle(160);
            }
            else
            {
                servo.SetAngle(20);
            }
        }

        public static void Main(string[] args)
        {
            RobotBase.Main(null, typeof(Class1));
            
        }
    }
}
