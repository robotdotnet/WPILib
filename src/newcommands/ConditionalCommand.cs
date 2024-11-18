using static WPIUtil.WpiGuard;

namespace WPILib;

public class ConditionalCommand : Command
{
    private readonly Command m_onTrue;
    private readonly Command m_onFalse;
#pragma warning disable IDE0052 // Remove unread private members
    private readonly Func<bool> m_condition;
#pragma warning restore IDE0052 // Remove unread private members

    public ConditionalCommand(Command onTrue, Command onFalse, Func<bool> condition)
    {
        m_onTrue = RequireNotNull(onTrue);
        m_onFalse = RequireNotNull(onFalse);
        m_condition = RequireNotNull(condition);

        // CommandScheduler

        AddRequirements(m_onTrue.Requirements);
        AddRequirements(m_onFalse.Requirements);
    }
}
