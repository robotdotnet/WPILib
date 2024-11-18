using static WPIUtil.WpiGuard;

namespace WPILib;

public class DeferredCommand : Command
{
    private readonly Command m_nullCommand = new PrintCommand("[DeferredCommand] Supplied command was null!");

#pragma warning disable IDE0052 // Remove unread private members
    private readonly Func<Command> m_supplier;
    private Command m_command;
#pragma warning restore IDE0052 // Remove unread private members

    public DeferredCommand(Func<Command> supplier, HashSet<ISubsystem> requirements)
    {
        m_supplier = RequireNotNull(supplier);
        m_command = m_nullCommand;
        AddRequirements(requirements);
    }
}
