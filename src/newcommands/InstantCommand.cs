namespace WPILib;

public class InstantCommand(Action toRun, params ISubsystem[] requirements) : FunctionalCommand(toRun, static () => { }, static interrupted => { }, static () => true, requirements)
{
    public InstantCommand() : this(static () => { }) { }
}
