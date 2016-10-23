using NUnit.Framework;
using WPILib.Commands;

namespace WPILib.IntegrationTests.Commands
{
    [TestFixture, Ignore("Need to figure out why this is failing")]
    public class CommandSequentialGroupTest : AbstractCommandTest
    {
        [Test, Timeout(20000)]
        public void TestThreeCommandOnSubSystem()
        {
            ASubsystem subsystem = new ASubsystem();

            MockCommand command1 = new MockCommand();
            command1.AddRequires(subsystem);

            MockCommand command2 = new MockCommand();
            command2.AddRequires(subsystem);

            MockCommand command3 = new MockCommand();
            command3.AddRequires(subsystem);

            CommandGroup commandGroup = new CommandGroup();
            commandGroup.AddSequential(command1, 1.0);
            commandGroup.AddSequential(command2, 2.0);
            commandGroup.AddSequential(command3);

            AssertCommandState(command1, 0, 0, 0, 0, 0);
            AssertCommandState(command2, 0, 0, 0, 0, 0);
            AssertCommandState(command3, 0, 0, 0, 0, 0);
            commandGroup.Start();
            AssertCommandState(command1, 0, 0, 0, 0, 0);
            AssertCommandState(command2, 0, 0, 0, 0, 0);
            AssertCommandState(command3, 0, 0, 0, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command1, 0, 0, 0, 0, 0);
            AssertCommandState(command2, 0, 0, 0, 0, 0);
            AssertCommandState(command3, 0, 0, 0, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command1, 1, 1, 1, 0, 0);
            AssertCommandState(command2, 0, 0, 0, 0, 0);
            AssertCommandState(command3, 0, 0, 0, 0, 0);
            Sleep(1000);// Command 1 timeout
            Scheduler.Instance.Run();
            AssertCommandState(command1, 1, 1, 1, 0, 1);
            AssertCommandState(command2, 1, 1, 1, 0, 0);
            AssertCommandState(command3, 0, 0, 0, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command1, 1, 1, 1, 0, 1);
            AssertCommandState(command2, 1, 2, 2, 0, 0);
            AssertCommandState(command3, 0, 0, 0, 0, 0);
            Sleep(2000);// Command 2 timeout
            Scheduler.Instance.Run();
            AssertCommandState(command1, 1, 1, 1, 0, 1);
            AssertCommandState(command2, 1, 2, 2, 0, 1);
            AssertCommandState(command3, 1, 1, 1, 0, 0);

            Scheduler.Instance.Run();
            AssertCommandState(command1, 1, 1, 1, 0, 1);
            AssertCommandState(command2, 1, 2, 2, 0, 1);
            AssertCommandState(command3, 1, 2, 2, 0, 0);
            command3.SetHasFinished(true);
            AssertCommandState(command1, 1, 1, 1, 0, 1);
            AssertCommandState(command2, 1, 2, 2, 0, 1);
            AssertCommandState(command3, 1, 2, 2, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command1, 1, 1, 1, 0, 1);
            AssertCommandState(command2, 1, 2, 2, 0, 1);
            AssertCommandState(command3, 1, 3, 3, 1, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command1, 1, 1, 1, 0, 1);
            AssertCommandState(command2, 1, 2, 2, 0, 1);
            AssertCommandState(command3, 1, 3, 3, 1, 0);
        }
    }
}
