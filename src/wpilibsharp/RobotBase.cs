using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace WPILib
{
    public abstract class RobotBase : IDisposable
    {
        private static void RunRobot<Robot>(object m, ref Robot? robot) where Robot : RobotBase, new()
        {
            Robot theRobot = new Robot();
            lock (m)
            {
                robot = theRobot;
            }
            theRobot.StartCompetition();
        }

        private static int RunHALInitialization()
        {
            if (!Hal.HalBase.Initialize())
            {
                Console.WriteLine("FATAL ERROR: HAL could not be initialized");
                return -1;
            }

            Console.WriteLine("\n********** Robot program starting **********");

            return 0;
        }

        public static int StartRobot<Robot>() where Robot : RobotBase, new()
        {
            int halInit = RunHALInitialization();
            if (halInit != 0)
            {
                return halInit;
            }

            object m = new object();
            Robot? robot = null;

            if (Hal.Main.HasMain())
            {
                Thread thr = new Thread(() =>
                {
                    try
                    {
                        RunRobot(m, ref robot);
                    }
                    catch
                    {
                        Hal.Main.ExitMain();
                        lock (m)
                        {
                            robot = null;
                            Monitor.PulseAll(m);
                        }
                        throw;
                    }

                    Hal.Main.ExitMain();

                    lock (m)
                    {
                        robot = null;
                        Monitor.PulseAll(m);
                    }
                });
                thr.Name = "Main Robot Thread";
                thr.Start();

                Hal.Main.RunMain();

                robot?.EndCompetition();

                lock (m)
                {
                    if (Monitor.Wait(m, TimeSpan.FromSeconds(1)))
                    {
                        thr.Join();
                    }
                    else
                    {
                        thr.IsBackground = true;
                    }
                }
            }
            else
            {
                RunRobot(m, ref robot);
            }

            robot?.Dispose();

            return 0;
        }

        protected DriverStation m_ds;

        protected static int m_threadId;

        public bool IsEnabled => false;
        public bool IsDisabled => false;
        public bool IsAutonomous => false;
        public bool IsOperatorControl => false;
        public bool IsTest => false;

        public bool IsNewDataAvailable => false;

        static int GetThreadId()
        {
            return 0;
        }


        public static string WPILibVersion => "1234";

        public abstract void StartCompetition();

        public abstract void EndCompetition();

        public virtual void Dispose()
        {
            
        }

        public bool IsReal => Hal.HalBase.GetRuntimeType() == Hal.RuntimeType.Athena;
        public bool IsSimulation => !IsReal;

        public RobotBase()
        {
            m_threadId = Thread.CurrentThread.ManagedThreadId;

            if (IsReal)
            {
                File.WriteAllText("/tmp/frc_versions/FRC_Lib_Version.ini", $"C# {WPILibVersion}");
            }

            m_ds = DriverStation.Instance;

        }

    }
}
