namespace WPILib;

public interface ISubsystem
{
    public void Periodic() { }
    public void SimulationPeriodic() { }
    public string Name => this.GetType().Name;

    public void SetDefaultCommand(Command defaultCommand)
    {

    }

    public Command? DefaultCommand
    {
        get
        {
            return null;
        }
        set
        {

        }
    }

    public Command? CurrentCommand => null;

    public void Register()
    {

    }
}
