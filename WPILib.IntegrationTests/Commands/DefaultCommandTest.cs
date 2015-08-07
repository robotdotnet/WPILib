using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WPILib.Commands;

namespace WPILib.IntegrationTests.Commands
{
    [TestFixture]
    public class DefaultCommandTest : AbstractCommandTest
    {
        [Test]
        public void TestDefaultCommandWhereTheInterruptingCommandEndsItself()
        {
            ASubsystem subsystem = new ASubsystem();

            MockCommand defaultCommand = new MockCommand();
            defaultCommand.AddRequires(subsystem);

            MockCommand anotherCommand = new MockCommand();
            anotherCommand.AddRequires(subsystem);

            AssertCommandState(defaultCommand, 0, 0, 0, 0, 0);
            subsystem.Init(defaultCommand);

            AssertCommandState(defaultCommand, 0, 0, 0, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(defaultCommand, 0, 0, 0, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(defaultCommand, 1, 1, 1, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(defaultCommand, 1, 2, 2, 0, 0);

            anotherCommand.Start();
            AssertCommandState(defaultCommand, 1, 2, 2, 0, 0);
            AssertCommandState(anotherCommand, 0, 0, 0, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(defaultCommand, 1, 3, 3, 0, 1);
            AssertCommandState(anotherCommand, 0, 0, 0, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(defaultCommand, 1, 3, 3, 0, 1);
            AssertCommandState(anotherCommand, 1, 1, 1, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(defaultCommand, 1, 3, 3, 0, 1);
            AssertCommandState(anotherCommand, 1, 2, 2, 0, 0);
            anotherCommand.SetHasFinished(true);
            AssertCommandState(defaultCommand, 1, 3, 3, 0, 1);
            AssertCommandState(anotherCommand, 1, 2, 2, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(defaultCommand, 1, 3, 3, 0, 1);
            AssertCommandState(anotherCommand, 1, 3, 3, 1, 0);
            Scheduler.Instance.Run();
            AssertCommandState(defaultCommand, 2, 4, 4, 0, 1);
            AssertCommandState(anotherCommand, 1, 3, 3, 1, 0);
            Scheduler.Instance.Run();
            AssertCommandState(defaultCommand, 2, 5, 5, 0, 1);
            AssertCommandState(anotherCommand, 1, 3, 3, 1, 0);
        }

        [Test]
        public void TestDefaultCommandInterruptingCommandCanceled()
        {
            ASubsystem subsystem = new ASubsystem();

            MockCommand defaultCommand = new MockCommand();
            defaultCommand.AddRequires(subsystem);

            MockCommand anotherCommand = new MockCommand();
            anotherCommand.AddRequires(subsystem);

            AssertCommandState(defaultCommand, 0, 0, 0, 0, 0);
            subsystem.Init(defaultCommand);
            subsystem.PublicInitDefaultCommand();
            AssertCommandState(defaultCommand, 0, 0, 0, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(defaultCommand, 0, 0, 0, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(defaultCommand, 1, 1, 1, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(defaultCommand, 1, 2, 2, 0, 0);

            anotherCommand.Start();
            AssertCommandState(defaultCommand, 1, 2, 2, 0, 0);
            AssertCommandState(anotherCommand, 0, 0, 0, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(defaultCommand, 1, 3, 3, 0, 1);
            AssertCommandState(anotherCommand, 0, 0, 0, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(defaultCommand, 1, 3, 3, 0, 1);
            AssertCommandState(anotherCommand, 1, 1, 1, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(defaultCommand, 1, 3, 3, 0, 1);
            AssertCommandState(anotherCommand, 1, 2, 2, 0, 0);
            anotherCommand.Cancel();
            AssertCommandState(defaultCommand, 1, 3, 3, 0, 1);
            AssertCommandState(anotherCommand, 1, 2, 2, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(defaultCommand, 1, 3, 3, 0, 1);
            AssertCommandState(anotherCommand, 1, 2, 2, 0, 1);
            Scheduler.Instance.Run();
            AssertCommandState(defaultCommand, 2, 4, 4, 0, 1);
            AssertCommandState(anotherCommand, 1, 2, 2, 0, 1);
            Scheduler.Instance.Run();
            AssertCommandState(defaultCommand, 2, 5, 5, 0, 1);
            AssertCommandState(anotherCommand, 1, 2, 2, 0, 1);
        }
    }
}
