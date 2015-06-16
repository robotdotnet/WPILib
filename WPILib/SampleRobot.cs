

using System;
using System.Threading;
using HAL_Base;
using WPILib.LiveWindows;

namespace WPILib
{
    public class SampleRobot : RobotBase
    {
        private bool m_robotMainOverriden;

        public SampleRobot()
            : base()
        {
            m_robotMainOverriden = true;
        }

        protected virtual void RobotInit()
        {
            Console.WriteLine("Default Init Running");
        }

        protected virtual void Disabled()
        {
            Console.WriteLine("Default Disabled Running");
        }

        public virtual void Autonomous()
        {
            Console.WriteLine("Default Auto Running");
        }

        public virtual void OperatorControl()
        {
            Console.WriteLine("Default Operator Control Running");
        }

        public virtual void Test()
        {
            Console.WriteLine("Default Test Running");
        }

        public void RobotMain()
        {
            m_robotMainOverriden = false;
        }

        public override void StartCompetition()
        {
            HAL.Report(ResourceType.kResourceType_Framework, Instances.kFramework_Simple);
            RobotMain();

            if (!m_robotMainOverriden)
            {
                LiveWindow.SetEnabled(false);
                RobotInit();
                while (true)
                {
                    if (IsDisabled)
                    {
                        m_ds.InDisabled(true);
                        Disabled();
                        m_ds.InDisabled(false);
                        while (IsDisabled)
                        {
                            Thread.Sleep(1);
                        }
                    }
                    else if (IsAutonomous)
                    {
                        m_ds.InAutonomous(true);
                        Autonomous();
                        m_ds.InAutonomous(false);
                        while (IsAutonomous && !IsDisabled)
                        {
                            Thread.Sleep(1);
                        }
                    }
                    else if (IsTest)
                    {
                        LiveWindow.SetEnabled(true);
                        m_ds.InTest(true);
                        Test();
                        m_ds.InTest(false);
                        while (IsTest && IsEnabled)
                            Thread.Sleep(1);
                        LiveWindow.SetEnabled(false);
                    }
                    else
                    {
                        m_ds.InOperatorControl(true);
                        OperatorControl();
                        m_ds.InOperatorControl(false);
                        while (IsOperatorControl && !IsDisabled)
                        {
                            Thread.Sleep(1);
                        }
                    }
                } /* while loop */
            }
        }
    }
}
