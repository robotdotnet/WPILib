﻿using Hal;
using NetworkTables;
using NetworkTables.Natives;
using System;
using System.Linq;
using WPILib;

namespace desktopDev
{
    public class Robot : TimedRobot
    {
        private TimeSpan lastTime = Timer.FPGATimestamp;
        readonly double[] buffer = new double[50];
        int idx = 0;

        private readonly int pwm = Hal.PWMLowLevel.InitializePort(HALLowLevel.GetPort(42));

        private readonly int can = CANAPILowLevel.Initialize(CANManufacturer.kTeamUse, 1, CANDeviceType.kMiscellaneous);



        public override unsafe void RobotPeriodic()
        {
            int idxLocal = idx;
            CANAPILowLevel.WritePacket(can, new Span<byte>(&idxLocal, 4), 42);

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

        public class HolderMethod
        {
            public void doThing()
            {

            }
        }

        public class Container
        {
            public Action holder;

            public Container(HolderMethod holder)
            {
                this.holder = holder.doThing;
            }
        }

        static void Main(string[] args)
        {
            NtCore.Initialize();
            NetworkTableInstance instance = NetworkTableInstance.Default;
            GC.KeepAlive(instance);
            //var i = SendableRegistry.Instance;

            //var map = new ConditionalWeakTable<HolderMethod, Container>();

            //var holder = new HolderMethod();
            //map.Add(holder, new Container(holder));
            ////holder = null;

            ////while (map.Any())
            ////{
            ////    //Console.WriteLine(map.Count());
            ////    GC.Collect();
            ////}
            //RobotBase.StartRobot<Robot>();
        }
    }
}
