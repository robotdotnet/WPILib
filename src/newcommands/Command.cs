using CommunityToolkit.Diagnostics;
using WPIUtil.Sendable;

namespace WPILib;

public abstract class Command : ISendable
{
    protected Command()
    {
        string name = this.GetType().Name;
        SendableRegistery.Add(this, name);
    }

    public virtual void Initialize() { }

    public virtual void Execute() { }

    public virtual void End(bool interrupted) { }

    public virtual bool IsFinished { get; }

    public HashSet<ISubsystem> Requirements { get; } = [];

    public void AddRequirements(params ISubsystem[] requirements)
    {
        foreach (var requirement in requirements)
        {
            Guard.IsNotNull(requirement);
            Requirements.Add(requirement);
        }
    }

    public void AddRequirements(IEnumerable<ISubsystem> requirements)
    {
        foreach (var requirement in requirements)
        {
            Guard.IsNotNull(requirement);
            Requirements.Add(requirement);
        }
    }

    public string Name
    {
        get
        {
            return SendableRegistery.GetName(this);
        }
        set
        {
            SendableRegistery.SetName(this, value);
        }
    }

    public string Subsystem
    {
        get
        {
            return SendableRegistery.GetSubsystem(this);
        }
        set
        {
            SendableRegistery.SetSubsystem(this, value);
        }
    }

    public void InitSendable(ISendableBuilder builder)
    {
        builder.SetSmartDashboardType("Command");
        builder.AddStringProperty(".name", () => Name, null);
    }
}
