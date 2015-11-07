using WPILib.Commands;

namespace WPILib.IntegrationTests.Commands
{
    public class MockCommand : Command
    {
        private int m_initializeCount;
        private int m_executeCount;
        private int m_isFinishedCount;
        private bool m_hasFinished;
        private int m_endCount;
        private int m_interruptedCount;

        public MockCommand(string name) : base(name)
        {

        }

        public MockCommand()
        {

        }

        protected override void Initialize()
        {
            ++m_initializeCount;
        }

        protected override void Execute()
        {
            ++m_executeCount;
        }

        protected override bool IsFinished()
        {
            ++m_isFinishedCount;
            return IsHasFinished();
        }

        protected override void End()
        {
            ++m_endCount;
        }

        protected override void Interrupted()
        {
            ++m_interruptedCount;
        }


        public int GetInitializeCount()
        {
            return m_initializeCount;
        }

        public bool HasInitialized()
        {
            return GetInitializeCount() > 0;
        }

        public int GetExecuteCount()
        {
            return m_executeCount;
        }

        public int GetIsFinishedCount()
        {
            return m_isFinishedCount;
        }

        public bool IsHasFinished()
        {
            return m_hasFinished;
        }

        public void SetHasFinished(bool hasFinished)
        {
            m_hasFinished = hasFinished;
        }

        public int GetEndCount()
        {
            return m_endCount;
        }

        public bool HasEnd()
        {
            return GetEndCount() > 0;
        }

        public int GetInterruptedCount()
        {
            return m_interruptedCount;
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
