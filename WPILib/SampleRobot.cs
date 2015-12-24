using System;
using System.Threading;
using HAL.Base;
using static HAL.Base.HAL;

namespace WPILib
{
    /// <summary>
    /// A simple robot base class that knows the standard competition states. 
    /// (disabled, autonomous, or operator controlled)
    /// </summary>
    /// <remarks>You can build a simple robot program off of this by overrriding the
    /// <see cref="RobotInit()"/>, <see cref="Disabled()"/>, <see cref="Autonomous()"/>,
    /// and <see cref="OperatorControl()"/> methods. The <see cref="StartCompetition()"/> method will call these methods
    /// (sometimes repeatedly) depending on the state of the competition.
    /// <para/>
    /// Alternatively, you can override the <see cref="RobotMain()"/> method and 
    /// manage all aspects of the robot yourself.
    /// </remarks>
    public class SampleRobot : RobotBase
    {
        private bool m_robotMainOverriden;

        /// <summary>
        /// Creates a new SampleRobot
        /// </summary>
        public SampleRobot()
            : base()
        {
            m_robotMainOverriden = true;
        }

        /// <summary>
        /// Robot-wide initialization code should go here.
        /// </summary>
        /// <remarks>
        /// Users should override this method for default Robot-wide initialiation which will be called
        /// when the robot is first powered on. It will be called exactly one time.
        /// <para></para>
        /// Warning: The Driver Station "Robot Code" light and FMS "Robot Ready" indicators will be off until
        /// <see cref="RobotInit"/> exits. Code in <see cref="RobotInit"/> that waits for enable will cause
        /// the robot to never indicate that the code is ready, causing the robot to be bypassed in a match.
        /// </remarks>
        protected virtual void RobotInit()
        {
            Console.WriteLine("Default Init Running");
        }

        /// <summary>
        /// Disabled should go here.
        /// </summary>
        /// <remarks>Users should override this method to run code that should run while
        /// the field is disabled.
        /// <para/>Called once each time the robot enters the disabled state.</remarks>
        protected virtual void Disabled()
        {
            Console.WriteLine("Default Disabled Running");
        }

        /// <summary>
        /// Autonomous should go here.
        /// </summary>
        /// <remarks>Users should override this method to run code that should run while
        /// the field is in the autonomous period.
        /// <para/>Called once each time the robot enters the autonomous state.</remarks>
        public virtual void Autonomous()
        {
            Console.WriteLine("Default Auto Running");
        }

        /// <summary>
        /// Operator control (tele-operated) code should go here.
        /// </summary>
        /// <remarks>Users should add Operator Control code to this method that should run while 
        /// the field is in the Operator Control (tele-operated) period.
        /// <para/>Called once each time the robot enters the operator-controlled state.</remarks>
        public virtual void OperatorControl()
        {
            Console.WriteLine("Default Operator Control Running");
        }

        /// <summary>
        /// Test code should go here.
        /// </summary>
        /// <remarks>Users should add test code to this method
        /// that should run while the robot is in test mode</remarks>
        public virtual void Test()
        {
            Console.WriteLine("Default Test Running");
        }

        /// <summary>
        /// Robot main program for free-form programs.
        /// </summary>
        /// <remarks>This should be overridden by user subclasses if the intent is to
        /// not use the <see cref="Autonomous()"/> and <see cref="OperatorControl()"/>
        /// methods. In that case, the program is responsible for sensing when to run the 
        /// autonomous and operator control functions in their program.
        /// <para/>This method will be called immediately after the constructor is called. 
        /// If it has not been overridden by a user subclass (i.e. the default version runs),
        /// then the <see cref="RobotInit()"/>, <see cref="Disabled()"/> and <see cref="OperatorControl()"/>
        /// methods will be called.</remarks>
        public void RobotMain()
        {
            m_robotMainOverriden = false;
        }

        /// <summary>
        /// Start a competition.
        /// </summary>
        /// <remarks>This code tracks the order of the field starting to
        /// ensure that everything happens in the right order. Repeatedly run the
        /// correct method, either <see cref="Autonomous"/> or <see cref="OperatorControl"/>
        /// when the robot is enabled. After running the correct method, wait for
        /// some state to change, either the other mode starts or the robot is disabled.
        /// Then go back and wait for the robot to be enabled again.</remarks>
        public override void StartCompetition()
        {
            HAL.Base.HAL.Report(ResourceType.kResourceType_Framework, Instances.kFramework_Sample);

            RobotInit();

            // Tell the DS that the robot is ready to be enabled.
            HALNetworkCommunicationObserveUserProgramStarting();

            RobotMain();

            if (!m_robotMainOverriden)
            {
                LiveWindow.LiveWindow.SetEnabled(false);
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
                        LiveWindow.LiveWindow.SetEnabled(true);
                        m_ds.InTest(true);
                        Test();
                        m_ds.InTest(false);
                        while (IsTest && IsEnabled)
                            Thread.Sleep(1);
                        LiveWindow.LiveWindow.SetEnabled(false);
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
