﻿using System;
using WPILib;
using WPILib.Counters;
using WPILib.SmartDashboardNS;

namespace roboRIODev
{
    public class Robot : TimedRobot
    {
        //private TimeSpan lastTime = Timer.FPGATimestamp;
        //double[] buffer = new double[50];
        //int idx = 0;

        //private int can = CANAPI.Initialize(CANManufacturer.kTeamUse, 1, CANDeviceType.kMiscellaneous);

        private readonly PWMSparkMax sparkMax = new PWMSparkMax(0);
        private readonly DigitalInput di = new DigitalInput(0);
        private Tachometer absoluteTach;
        private readonly DigitalInput quadDI = new DigitalInput(1);
        private readonly DigitalInput quadDIB = new DigitalInput(2);
        private Tachometer quadratureTach;
        private UpDownCounter quadratureCounter;

        private ExternalDirectionCounter externalDirectionCounter;
        private ExternalDirectionCounter externalDirectionCounter2x;

        AsynchronousInterrupt interrupt;

        int count = 0;

        public override void RobotInit()
        {
            absoluteTach = new Tachometer(di);
            quadratureTach = new Tachometer(quadDI);
            quadratureTach.EdgesPerRevolution = 2048;
            quadratureCounter = new UpDownCounter();
            quadratureCounter.UpSource = quadDI;
            quadratureCounter.UpEdgeConfiguration = EdgeConfiguration.kRisingEdge;

            externalDirectionCounter = new ExternalDirectionCounter(quadDI, quadDIB);

            externalDirectionCounter2x = new ExternalDirectionCounter(quadDI, quadDIB);
            externalDirectionCounter2x.EdgeConfiguration = EdgeConfiguration.kBothEdges;
            externalDirectionCounter2x.ReverseDirection = true;

            DigitalGlitchFilter filter = new DigitalGlitchFilter();
            filter.SetPeriod(TimeSpan.FromSeconds(1));
            //filter.Add(di);

            interrupt = new AsynchronousInterrupt(di, (r, f) =>
            {
                Console.WriteLine("Interrupt Occured " + count);
                count++;
                //Thread.Sleep(20);
            });

            interrupt.SetInterruptEdges(true, false);

            interrupt.Enable();
        }



        public override unsafe void RobotPeriodic()
        {
            SmartDashboard.PutNumber("External Dir Tach", externalDirectionCounter.Count);

            SmartDashboard.PutNumber("External Dir Tach 2x", externalDirectionCounter2x.Count);


            SmartDashboard.PutNumber("Absolute Tach", absoluteTach.RotationalSpeed?.RevolutionsPerSecond ?? double.MaxValue);

            SmartDashboard.PutNumber("Quadrature Tach", quadratureTach.RotationalSpeed?.RevolutionsPerSecond ?? 0);

            SmartDashboard.PutNumber("Quadrature Counter", quadratureCounter.Count);

            SmartDashboard.PutNumber("Joystick", DriverStation.Instance.GetStickAxis(0, 1));
            sparkMax.Set(DriverStation.Instance.GetStickAxis(0, 1));

            if (DriverStation.Instance.GetStickButton(0, 1))
            {
                Console.WriteLine("Disable");
                interrupt.Disable();
            }

            if (DriverStation.Instance.GetStickButton(0, 2))
            {
                Console.WriteLine("Enable");
                interrupt.Enable();
            }
            //sparkMax.SetVoltage(ElectricPotential.FromVolts(5));
            //int idxLocal = idx;
            //try
            //{
            //    CANAPI.WritePacket(can, new Span<byte>(&idxLocal, 4), 42);
            //}
            //catch (UncleanStatusException ex)
            //{
            //    ;
            //}

            //var current = Timer.FPGATimestamp;
            //var delta = current - lastTime;
            //lastTime = current;
            //buffer[idx] = delta.TotalMilliseconds;
            //idx++;
            //if (idx == 50)
            //{
            //    Console.WriteLine(buffer.Average());
            //    idx = 0;
            //}
            //base.RobotPeriodic();
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
