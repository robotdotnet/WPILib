using System;
using System.Threading;
using HAL_Simulator;
using WPILib.IntegrationTests.SimulatedHardware;
using WPILib.LiveWindows;

namespace WPILib.IntegrationTests.Test
{
    public abstract class AbstractComsSetup
    {
        private static readonly bool m_initialized = false;

        static AbstractComsSetup()
        {
            if (!m_initialized)
            {
                RobotBase.InitializeHardwareConfiguration();
                HAL_Base.HAL.HALNetworkCommunicationObserveUserProgramStarting();

                LiveWindow.SetEnabled(false);
                Console.WriteLine("Started coms");

                if (RobotBase.IsSimulation)
                {
                    m_initialized = true;

                    DriverStationHelper.StartDSLoop();
                    DriverStationHelper.SetRobotMode(DriverStationHelper.RobotMode.Teleop);
                    DriverStationHelper.SetEnabledState(DriverStationHelper.EnabledState.Enabled);

                    Thread.Sleep(500);

                    SimJumpers.AttachDIOPins(6, 7);
                    SimJumpers.AttachDIOPins(8, 9);
                    SimJumpers.AttachRelay(0, 19, 18);
                    SimJumpers.AttachAIO(2, 0);

                    SimData.Accelerometer.X = 0.0;
                    SimData.Accelerometer.Y = 0.0;
                    SimData.Accelerometer.Z = 1.0;
                    return;
                }

                int i = 0;
                while (!DriverStation.Instance.Enabled)
                {
                    try
                    {
                        Thread.Sleep(1000);
                    }
                    catch (ThreadInterruptedException e)
                    {
                        Console.WriteLine(e);
                    }
                    Console.Write("\rWaiting for enable: " + i++);
                }

                Console.WriteLine();

                m_initialized = true;

                Console.WriteLine("Running!");
            }
        }
    }
}
