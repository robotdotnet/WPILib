using static WPIUtil.WpiGuard;

namespace WPILib;

public class ConditionalCommand : Command
{
    private readonly Command m_onTrue;
    private readonly Command m_onFalse;
    private readonly Func<bool> m_condition;

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
