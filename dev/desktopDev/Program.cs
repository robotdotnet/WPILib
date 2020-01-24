using REV.SparkMax.Natives;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using WPILib;

namespace desktopDev
{
    public class Robot : TimedRobot
    {
        public override void RobotPeriodic()
        {
            base.RobotPeriodic();
        }
    }

    class Program
    {


        static void Main(string[] args)
        {
            int retVal = RobotBase.StartRobot<Robot>();
            Console.WriteLine(retVal);
            return;
        }
    }
}
