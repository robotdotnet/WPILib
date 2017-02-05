using WPILib.Commands;

namespace WPILib.Extras
{
    /// <summary>
    /// Implements the boilerplate needed to use an <see cref="IterativeRobot"/>
    /// with the command-based model, by running the scheduler as needed
    /// </summary>
    public abstract class CommandRobot : IterativeRobot
    {
        // This function is called periodically while disabled
        public override void DisabledPeriodic() => Scheduler.Instance.Run();

        // This function is called periodically during autonomous
        public override void AutonomousPeriodic() => Scheduler.Instance.Run();

        // This function is called when the disabled button is hit.
        public override void DisabledInit() { }

        // This function is called periodically during operator control
        public override void TeleopPeriodic() => Scheduler.Instance.Run();

        // This function is called periodically during test mode
        public override void TestPeriodic() => LiveWindow.LiveWindow.Run();
    }
}
