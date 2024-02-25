using static WPIUtil.WpiGuard;

namespace WPILib;

public class FunctionalCommand : Command
{
    private readonly Action m_onInit;
    private readonly Action m_onExecute;
    private readonly Action<bool> m_onEnd;
    private readonly Func<bool> m_isFinished;

    public FunctionalCommand(Action onInit, Action onExecute, Action<bool> onEnd, Func<bool> isFinished, params ISubsystem[] requirements)
    {
        m_onInit = RequireNotNull(onInit);
        m_onExecute = RequireNotNull(onExecute);
        m_onEnd = RequireNotNull(onEnd);
        m_isFinished = RequireNotNull(isFinished);

        AddRequirements(requirements);
    }

    public override void Initialize()
    {
        m_onInit();
    }

    public override void Execute()
    {
        m_onExecute();
    }

    public override void End(bool interrupted)
    {
        m_onEnd(interrupted);
    }

    public override bool IsFinished => m_isFinished();
}
