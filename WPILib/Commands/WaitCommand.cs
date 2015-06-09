namespace WPILib.Commands
{
    public class WaitCommand : Command
    {
        protected override void Initialize()
        {
        }

        protected override void Execute()
        {
        }

        protected override bool IsFinished()
        {
            return IsTimedOut();
        }

        protected override void End()
        {
        }

        protected override void Interrupted()
        {
        }
    }
}
