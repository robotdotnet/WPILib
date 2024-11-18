using CommunityToolkit.Diagnostics;
using NetworkTables;
using UnitsNet;
using WPIHal.Natives;

namespace WPILib;

public abstract class IterativeRobotBase : RobotBase
{
    private enum Mode
    {
        None,
        Disabled,
        Autonomous,
        Teleop,
        Test
    }

    private DSControlWord m_word = new();
    private Mode m_lastMode = Mode.None;
    private readonly Watchdog m_watchdog;
    private bool m_calledDsConnected;

    protected IterativeRobotBase(Duration period)
    {
        Period = period;
        m_watchdog = new(period, PrintLoopOverrunMessage);
    }

    public virtual void RobotInit() { }

    public virtual void DriverStationConnected() { }

    public virtual void SimulationInit() { }

    public virtual void DisabledInit() { }

    public virtual void AutonomousInit() { }

    public virtual void TeleopInit() { }

    public virtual void TestInit() { }

    private bool m_rpFirstRun = true;

    /** Periodic code for all robot modes should go here. */
    public virtual void RobotPeriodic()
    {
        if (m_rpFirstRun)
        {
            Console.WriteLine("Default robotPeriodic() method... Override me!");
            m_rpFirstRun = false;
        }
    }

    private bool m_spFirstRun = true;

    public virtual void SimulationPeriodic()
    {
        if (m_spFirstRun)
        {
            Console.WriteLine("Default simulationPeriodic() method... Override me!");
            m_spFirstRun = false;
        }
    }

    private bool m_dpFirstRun = true;

    /** Periodic code for disabled mode should go here. */
    public virtual void DisabledPeriodic()
    {
        if (m_dpFirstRun)
        {
            Console.WriteLine("Default disabledPeriodic() method... Override me!");
            m_dpFirstRun = false;
        }
    }

    private bool m_apFirstRun = true;

    /** Periodic code for autonomous mode should go here. */
    public virtual void AutonomousPeriodic()
    {
        if (m_apFirstRun)
        {
            Console.WriteLine("Default autonomousPeriodic() method... Override me!");
            m_apFirstRun = false;
        }
    }

    private bool m_tpFirstRun = true;

    /** Periodic code for teleop mode should go here. */
    public virtual void TeleopPeriodic()
    {
        if (m_tpFirstRun)
        {
            Console.WriteLine("Default teleopPeriodic() method... Override me!");
            m_tpFirstRun = false;
        }
    }

    private bool m_tmpFirstRun = true;

    /** Periodic code for test mode should go here. */
    public virtual void TestPeriodic()
    {
        if (m_tmpFirstRun)
        {
            Console.WriteLine("Default testPeriodic() method... Override me!");
            m_tmpFirstRun = false;
        }
    }

    public virtual void DisabledExit() { }

    public virtual void AutonomousExit() { }

    public virtual void TeleopExit() { }

    public virtual void TestExit() { }

    public bool NetworkTablesFlushEnabled { get; set; }

    private bool m_reportedLw;
    private bool m_lwEnabledInTest;

    public bool EnableLiveWindowInTest
    {
        get => m_lwEnabledInTest;
        set
        {
            if (IsTestEnabled)
            {
                ThrowHelper.ThrowInvalidOperationException("Can't configure test mode while in test mode!");
            }
            if (!m_reportedLw && value)
            {
                // TODO HAL Report
                m_reportedLw = true;
            }
            m_lwEnabledInTest = value;
        }
    }

    public Duration Period { get; }

    protected void LoopFunc()
    {
        DriverStation.RefreshData();
        m_watchdog.Reset();

        m_word.Refresh();

        // Get current mode
        Mode mode = Mode.None;
        if (!m_word.IsDisabled)
        {
            mode = Mode.Disabled;
        }
        else if (m_word.IsAutonomous)
        {
            mode = Mode.Autonomous;
        }
        else if (m_word.IsTeleop)
        {
            mode = Mode.Teleop;
        }
        else if (m_word.IsTest)
        {
            mode = Mode.Test;
        }

        if (!m_calledDsConnected && m_word.IsDSAttached)
        {
            m_calledDsConnected = true;
            DriverStationConnected();
        }

        // If mode changed, call mode exit and entry functions
        if (m_lastMode != mode)
        {
            // Call last mode's exit function
            if (m_lastMode == Mode.Disabled)
            {
                DisabledExit();
            }
            else if (m_lastMode == Mode.Autonomous)
            {
                AutonomousExit();
            }
            else if (m_lastMode == Mode.Teleop)
            {
                TeleopExit();
            }
            else if (m_lastMode == Mode.Test)
            {
                if (m_lwEnabledInTest)
                {
                    //LiveWindow.setEnabled(false);
                    //Shuffleboard.disableActuatorWidgets();
                }
                TestExit();
            }

            // Call current mode's entry function
            if (mode == Mode.Disabled)
            {
                DisabledInit();
                m_watchdog.AddEpoch("disabledInit()");
            }
            else if (mode == Mode.Autonomous)
            {
                AutonomousInit();
                m_watchdog.AddEpoch("autonomousInit()");
            }
            else if (mode == Mode.Teleop)
            {
                TeleopInit();
                m_watchdog.AddEpoch("teleopInit()");
            }
            else if (mode == Mode.Test)
            {
                if (m_lwEnabledInTest)
                {
                    // LiveWindow.setEnabled(true);
                    // Shuffleboard.enableActuatorWidgets();
                }
                TestInit();
                m_watchdog.AddEpoch("testInit()");
            }

            m_lastMode = mode;
        }

        // Call the appropriate function depending upon the current robot mode
        if (mode == Mode.Disabled)
        {
            HalDriverStation.ObserveUserProgramDisabled();
            DisabledPeriodic();
            m_watchdog.AddEpoch("disabledPeriodic()");
        }
        else if (mode == Mode.Autonomous)
        {
            HalDriverStation.ObserveUserProgramAutonomous();
            AutonomousPeriodic();
            m_watchdog.AddEpoch("autonomousPeriodic()");
        }
        else if (mode == Mode.Teleop)
        {
            HalDriverStation.ObserveUserProgramTeleop();
            TeleopPeriodic();
            m_watchdog.AddEpoch("teleopPeriodic()");
        }
        else
        {
            HalDriverStation.ObserveUserProgramTest();
            TestPeriodic();
            m_watchdog.AddEpoch("testPeriodic()");
        }

        RobotPeriodic();
        m_watchdog.AddEpoch("robotPeriodic()");

        //SmartDashboard.updateValues();
        m_watchdog.AddEpoch("SmartDashboard.updateValues()");
        //LiveWindow.updateValues();
        m_watchdog.AddEpoch("LiveWindow.updateValues()");
        //Shuffleboard.update();
        m_watchdog.AddEpoch("Shuffleboard.update()");

        if (IsSimulation)
        {
            HalBase.SimPeriodicBefore();
            SimulationPeriodic();
            HalBase.SimPeriodicAfter();
            m_watchdog.AddEpoch("simulationPeriodic()");
        }

        m_watchdog.Disable();

        // Flush NetworkTables
        if (NetworkTablesFlushEnabled)
        {
            NetworkTableInstance.Default.FlushLocal();
        }

        // Warn on loop time overruns
        if (m_watchdog.IsExpired)
        {
            m_watchdog.PrintEpochs();
        }
    }

    private void PrintLoopOverrunMessage()
    {
        DriverStation.ReportWarning($"Loop time of {Period}s overrun\n", false);
    }
}
