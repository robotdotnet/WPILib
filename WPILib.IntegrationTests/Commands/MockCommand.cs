using WPILib.Commands;

namespace WPILib.IntegrationTests.Commands
{
    public class MockCommand : Command
    {
        private int initializeCount = 0;
        private int executeCount = 0;
        private int isFinishedCount = 0;
        private bool hasFinished = false;
        private int endCount = 0;
        private int interruptedCount = 0;

        public MockCommand(string name) : base(name)
        {

        }

        public MockCommand()
        {

        }

        protected override void Initialize()
        {
            ++initializeCount;
        }

        protected override void Execute()
        {
            ++executeCount;
        }

        protected override bool IsFinished()
        {
            ++isFinishedCount;
            return IsHasFinished();
        }

        protected override void End()
        {
            ++endCount;
        }

        protected override void Interrupted()
        {
            ++interruptedCount;
        }


        public int GetInitializeCount()
        {
            return initializeCount;
        }

        public bool HasInitialized()
        {
            return GetInitializeCount() > 0;
        }

        public int GetExecuteCount()
        {
            return executeCount;
        }

        public int GetIsFinishedCount()
        {
            return isFinishedCount;
        }

        public bool IsHasFinished()
        {
            return hasFinished;
        }

        public void SetHasFinished(bool hasFinished)
        {
            this.hasFinished = hasFinished;
        }

        public int GetEndCount()
        {
            return endCount;
        }

        public bool HasEnd()
        {
            return GetEndCount() > 0;
        }

        public int GetInterruptedCount()
        {
            return interruptedCount;
        }

        public bool HasInterrupted()
        {
            return GetInterruptedCount() > 0;
        }

        public void AddRequires(Subsystem subsystem)
        {
            Requires(subsystem);
        }

        public void SetInterruptable(bool interrupt)
        {
            Interruptible = interrupt;
        }

        public void MockSetTimeout(double timeout)
        {
            SetTimeout(timeout);
        }
    }
}
