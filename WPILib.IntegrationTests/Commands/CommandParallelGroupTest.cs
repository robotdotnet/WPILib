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
    public class CommandParallelGroupTest : AbstractCommandTest
    {
        [Test]
        public void TestParallelCommandGroupWithTwoCommands()
        {
            MockCommand command1 = new MockCommand();
            MockCommand command2 = new MockCommand();

            CommandGroup commandGroup = new CommandGroup();
            commandGroup.AddParallel(command1);
            commandGroup.AddParallel(command2);

            AssertCommandState(command1, 0, 0, 0, 0, 0);
            AssertCommandState(command2, 0, 0, 0, 0, 0);
            commandGroup.Start();
            AssertCommandState(command1, 0, 0, 0, 0, 0);
            AssertCommandState(command2, 0, 0, 0, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command1, 0, 0, 0, 0, 0);
            AssertCommandState(command2, 0, 0, 0, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command1, 1, 1, 1, 0, 0);
            AssertCommandState(command2, 1, 1, 1, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command1, 1, 2, 2, 0, 0);
            AssertCommandState(command2, 1, 2, 2, 0, 0);
            command1.SetHasFinished(true);
            Scheduler.Instance.Run();
            AssertCommandState(command1, 1, 3, 3, 1, 0);
            AssertCommandState(command2, 1, 3, 3, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command1, 1, 3, 3, 1, 0);
            AssertCommandState(command2, 1, 4, 4, 0, 0);
            command2.SetHasFinished(true);
            Scheduler.Instance.Run();
            AssertCommandState(command1, 1, 3, 3, 1, 0);
            AssertCommandState(command2, 1, 5, 5, 1, 0);
        }
    }
}
