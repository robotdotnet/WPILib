using NUnit.Framework;
using WPILib.Commands;

namespace WPILib.Tests.Commands
{
    [TestFixture]
    public class CommandSupersedeTest : AbstractCommandTest
    {
        [Test]
        public void TestOneCommandSupersedingAnotherBecauseOfDependencies()
        {
            ASubsystem subsystem = new ASubsystem();

            MockCommand command1 = new MockCommand("Num1");
            command1.AddRequires(subsystem);

            MockCommand command2 = new MockCommand("Num2");
            command2.AddRequires(subsystem);

            AssertCommandState(command1, 0, 0, 0, 0, 0);
            AssertCommandState(command2, 0, 0, 0, 0, 0);
            command1.Start();
            AssertCommandState(command1, 0, 0, 0, 0, 0);
            AssertCommandState(command2, 0, 0, 0, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command1, 0, 0, 0, 0, 0);
            AssertCommandState(command2, 0, 0, 0, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command1, 1, 1, 1, 0, 0);
            AssertCommandState(command2, 0, 0, 0, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command1, 1, 2, 2, 0, 0);
            AssertCommandState(command2, 0, 0, 0, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command1, 1, 3, 3, 0, 0);
            AssertCommandState(command2, 0, 0, 0, 0, 0);
            command2.Start();
            AssertCommandState(command1, 1, 3, 3, 0, 0);
            AssertCommandState(command2, 0, 0, 0, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command1, 1, 4, 4, 0, 1);
            AssertCommandState(command2, 0, 0, 0, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command1, 1, 4, 4, 0, 1);
            AssertCommandState(command2, 1, 1, 1, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command1, 1, 4, 4, 0, 1);
            AssertCommandState(command2, 1, 2, 2, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command1, 1, 4, 4, 0, 1);
            AssertCommandState(command2, 1, 3, 3, 0, 0);
        }

        [Test]
        public void TestCommandFailingSupersedingBecauseFirstCannotBeInterrupted()
        {
            ASubsystem subsystem = new ASubsystem();

            MockCommand command1 = new MockCommand("Num1");
            command1.AddRequires(subsystem);
            command1.SetInterruptable(false);

            MockCommand command2 = new MockCommand("Num2");
            command2.AddRequires(subsystem);

            AssertCommandState(command1, 0, 0, 0, 0, 0);
            AssertCommandState(command2, 0, 0, 0, 0, 0);
            command1.Start();
            AssertCommandState(command1, 0, 0, 0, 0, 0);
            AssertCommandState(command2, 0, 0, 0, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command1, 0, 0, 0, 0, 0);
            AssertCommandState(command2, 0, 0, 0, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command1, 1, 1, 1, 0, 0);
            AssertCommandState(command2, 0, 0, 0, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command1, 1, 2, 2, 0, 0);
            AssertCommandState(command2, 0, 0, 0, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command1, 1, 3, 3, 0, 0);
            AssertCommandState(command2, 0, 0, 0, 0, 0);
            command2.Start();
            AssertCommandState(command1, 1, 3, 3, 0, 0);
            AssertCommandState(command2, 0, 0, 0, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command1, 1, 4, 4, 0, 0);
            AssertCommandState(command2, 0, 0, 0, 0, 0);
        }
    }
}
