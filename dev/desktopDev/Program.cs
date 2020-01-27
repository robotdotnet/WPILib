using Hal;
using REV.SparkMax.Natives;
using System;
using System.IO;
using System.Linq;
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
            var types = typeof(CANAPI).Assembly.GetTypes().Where(x => x.Namespace == "Hal.Natives").Where(x => x.IsInterface);
            ;
        }
    }
}
