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
    public class CommandScheduleTest : AbstractCommandTest
    {
        [Test]
        public void RunTestAndTerminate()
        {
            MockCommand command = new MockCommand();
            command.Start();
            AssertCommandState(command, 0, 0, 0, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command, 0, 0, 0, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command, 1, 1, 1, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command, 1, 2, 2, 0, 0);
            command.SetHasFinished(true);
            AssertCommandState(command, 1, 2, 2, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command, 1, 3, 3, 1, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command, 1, 3, 3, 1, 0);
        }

        [Test]
        public void TestRunAndCancel()
        {
            MockCommand command = new MockCommand();
            command.Start();
            AssertCommandState(command, 0, 0, 0, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command, 0, 0, 0, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command, 1, 1, 1, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command, 1, 2, 2, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command, 1, 3, 3, 0, 0);
            command.Cancel();
            AssertCommandState(command, 1, 3, 3, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command, 1, 3, 3, 0, 1);
            Scheduler.Instance.Run();
            AssertCommandState(command, 1, 3, 3, 0, 1);
        }
    }
}
