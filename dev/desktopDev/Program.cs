using Hal;
using System;
using System.Linq;
using WPILib;

namespace desktopDev
{
    public class Robot : TimedRobot
    {
        private TimeSpan lastTime = Timer.FPGATimestamp;
        double[] buffer = new double[50];
        int idx = 0;

        private int can = CANAPI.Initialize(CANManufacturer.kTeamUse, 1, CANDeviceType.kMiscellaneous);



        public unsafe override void RobotPeriodic()
        {
            int idxLocal = idx;
            CANAPI.WritePacket(can, new Span<byte>(&idxLocal, 4), 42);

            var current = Timer.FPGATimestamp;
            var delta = current - lastTime;
            lastTime = current;
            buffer[idx] = delta.TotalMilliseconds;
            idx++;
            if (idx == 50)
            {
                Console.WriteLine(buffer.Average());
                idx = 0;
            }
            base.RobotPeriodic();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            RobotBase.StartRobot<Robot>();
        }
    }
}
