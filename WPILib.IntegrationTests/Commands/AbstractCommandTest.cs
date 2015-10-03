using System.Threading;
using NUnit.Framework;
using WPILib.Commands;
using WPILib.IntegrationTests.Test;

namespace WPILib.IntegrationTests.Commands
{
    public abstract class AbstractCommandTest : AbstractComsSetup
    {
        [SetUp]
        public void CommandSetup()
        {
            Scheduler.Instance.RemoveAll();
            Scheduler.Instance.Enable();
        }

        public class ASubsystem : Subsystem
        {
            internal Command command;
            protected override void InitDefaultCommand()
            {
                if (command != null)
                    SetDefaultCommand(command);
            }

            public void PublicInitDefaultCommand()
            {
                InitDefaultCommand();
            }

            public void Init(Command command)
            {
                this.command = command;
            }
        }

        public void AssertCommandState(MockCommand command, int initialize, int execute, int isFinished, int end,
            int interrupted)
        {
            Assert.AreEqual(initialize, command.GetInitializeCount());
            Assert.AreEqual(execute, command.GetExecuteCount());
            Assert.AreEqual(isFinished, command.GetIsFinishedCount());
            Assert.AreEqual(end, command.GetEndCount());
            Assert.AreEqual(interrupted, command.GetInterruptedCount());
        }

        public void Sleep(int time)
        {
            try
            {
                Thread.Sleep(time);
            }
            catch (ThreadInterruptedException)
            {
                Assert.Fail("Sleep Interrupted!?!?!?!?!");
            }
        }
    }
}
