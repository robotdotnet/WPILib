namespace WPILib;

public static class Commands
{
    public static Command None()
    {
        return new InstantCommand();
    }

    public static Command Idle(params ISubsystem[] requirements)
    {
        return Run(static () => { }, requirements);
    }

    public static Command RunOnce(Action action, params ISubsystem[] requirements)
    {
        return new InstantCommand(action, requirements);
    }

    public static Command Run(Action action, params ISubsystem[] requirements)
    {
        return new RunCommand(action, requirements);
    }
}
