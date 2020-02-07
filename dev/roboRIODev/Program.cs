using System;
using System.Linq;
using UnitsNet;
using WPILib;
using WPILib.Counters;
using WPILib.SmartDashboard;

namespace roboRIODev
{
    public class Robot : TimedRobot
    {
        //private TimeSpan lastTime = Timer.FPGATimestamp;
        //double[] buffer = new double[50];
        //int idx = 0;

        //private int can = CANAPI.Initialize(CANManufacturer.kTeamUse, 1, CANDeviceType.kMiscellaneous);

        private PWMSparkMax sparkMax = new PWMSparkMax(0);
        private DigitalInput di = new DigitalInput(0);
        private Tachometer absoluteTach;
        private DigitalInput quadDI = new DigitalInput(1);
        private DigitalInput quadDIB = new DigitalInput(2);
        private Tachometer quadratureTach;
        private UpDownCounter quadratureCounter;

        private ExternalDirectionCounter externalDirectionCounter;
        private ExternalDirectionCounter externalDirectionCounter2x;

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

        }



        public override unsafe void RobotPeriodic()
        {
            SmartDashboard.PutNumber("External Dir Tach", externalDirectionCounter.Count);

            SmartDashboard.PutNumber("External Dir Tach 2x", externalDirectionCounter2x.Count);


            SmartDashboard.PutNumber("Absolute Tach", absoluteTach.RotationalSpeed.RevolutionsPerSecond);

            SmartDashboard.PutNumber("Quadrature Tach", quadratureTach.RotationalSpeed.RevolutionsPerSecond);

            SmartDashboard.PutNumber("Quadrature Counter", quadratureCounter.Count);

            SmartDashboard.PutNumber("Joystick", DriverStation.Instance.GetStickAxis(0, 1));
            sparkMax.Set(DriverStation.Instance.GetStickAxis(0, 1));

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
