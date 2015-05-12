using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using HAL_RoboRIO;

namespace WPILib
{
    public class SampleRobot : RobotBase
    {
        private bool _RobotMainOverriden;

        public SampleRobot()
            : base()
        {
            _RobotMainOverriden = true;
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
            _RobotMainOverriden = false;
        }

        public override void StartCompetition()
        {
            HAL.Report(ResourceType.kResourceType_Framework, Instances.kFramework_Simple);
            RobotMain();

            if (!_RobotMainOverriden)
            {
                RobotInit();
                while (true)
                {
                    if (isDisabled())
                    {
                        m_ds.InDisabled(true);
                        Disabled();
                        m_ds.InDisabled(false);
                        while (isDisabled())
                        {
                            Thread.Sleep(1);
                        }
                    }
                    else if (isAutonomous())
                    {
                        m_ds.InAutonomous(true);
                        Autonomous();
                        m_ds.InAutonomous(false);
                        while (isAutonomous() && !isDisabled())
                        {
                            Thread.Sleep(1);
                        }
                    }
                    else if (isTest())
                    {
                        //LiveWindow.setEnabled(true);
                        m_ds.InTest(true);
                        Test();
                        m_ds.InTest(false);
                        while (isTest() && isEnabled())
                            Thread.Sleep(1);
                        //LiveWindow.setEnabled(false);
                    }
                    else
                    {
                        m_ds.InOperatorControl(true);
                        OperatorControl();
                        m_ds.InOperatorControl(false);
                        while (isOperatorControl() && !isDisabled())
                        {
                            Thread.Sleep(1);
                        }
                    }
                } /* while loop */
            }
        }
    }
}
