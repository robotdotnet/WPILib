using Hal;
using REV.SparkMax;
using System;
using System.Linq;
using UnitsNet;
using WPILib;

namespace roboRIODev
{
    public class Robot : TimedRobot
    {
        private TimeSpan lastTime = Timer.FPGATimestamp;
        double[] buffer = new double[50];
        int idx = 0;

        private int can = CANAPI.Initialize(CANManufacturer.kTeamUse, 1, CANDeviceType.kMiscellaneous);

        private PWMSparkMax sparkMax = new PWMSparkMax(0);



        public unsafe override void RobotPeriodic()
        {
            sparkMax.SetVoltage(ElectricPotential.FromVolts(5));
            int idxLocal = idx;
            try
            {
                CANAPI.WritePacket(can, new Span<byte>(&idxLocal, 4), 42);
            }
            catch (UncleanStatusException ex)
            {
                ;
            }

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
