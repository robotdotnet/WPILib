using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using HAL.Base;
using HAL.Simulator;
using WPILib;
using WPILib.SmartDashboard;
using HAL = HAL.Base.HAL;

namespace LoadTester
{
    public class Class1 : IterativeRobot
    {
        //private Joystick joystick;
        //private Servo servo;

        //private AnalogGyro gyro;

        //private int handle;

        private DigitalInput[] input;
        public override void RobotInit()
        {
            //input = new DigitalInput(0);
            //gyro = new AnalogGyro(0);
            //joystick = new Joystick(0);
            //servo = new Servo(0);
            //int status = 0;
            //handle = HALDIO.HAL_InitializeDIOPort(global::HAL.Base.HAL.HAL_GetPort(0), true, ref status);
            //Console.WriteLine(status);
            input = new DigitalInput[5];

            for (int i = 26; i < 31; i++)
            {
                input[i - 26] = new DigitalInput(i);
            }
        }

        public override void DisabledPeriodic()
        {
             List<bool> res = new List<bool>();
            foreach (DigitalInput digitalInput in input)
            {
                res.Add(digitalInput.Get());
            }
            SmartDashboard.PutBooleanArray("Array", res.ToArray());
        }

        public override void TeleopPeriodic()
        {
            /*
            //SmartDashboard.PutNumber("Gyro", gyro.GetAngle());
            if (joystick.GetRawButton(1))
            {
                //servo.SetAngle(160);
            }
            else
            {
                //servo.SetAngle(20);
            }
            */
        }

        public static void Main(string[] args)
        {

            RobotBase.Main(null, typeof(Class1));
        }
    }
}
