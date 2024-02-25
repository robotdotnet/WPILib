namespace WPILib;

public class RunCommand(Action toRun, params ISubsystem[] requirements) : FunctionalCommand(static () => { }, toRun, static _ => { }, static () => false, requirements)
{
}
