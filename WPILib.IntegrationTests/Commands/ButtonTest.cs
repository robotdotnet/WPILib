using NUnit.Framework;
using WPILib.Buttons;
using WPILib.Commands;

namespace WPILib.IntegrationTests.Commands
{
    [TestFixture]
    public class ButtonTest : AbstractCommandTest
    {
        private InternalButton button1;
        private InternalButton button2;

        [TestFixtureSetUp]
        public static void SetUpBeforeClass()
        {

        }

        [TestFixtureTearDown]
        public static void TearDownAfterClass()
        {

        }

        [SetUp]
        public void SetUp()
        {
            button1 = new InternalButton();
            button2 = new InternalButton();
            base.CommandSetup();
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void Test()
        {
            MockCommand command1 = new MockCommand();
            MockCommand command2 = new MockCommand();
            MockCommand command3 = new MockCommand();
            MockCommand command4 = new MockCommand();

            button1.WhenPressed(command1);
            button1.WhenReleased(command2);
            button1.WhileHeld(command3);
            button2.WhileHeld(command4);

            AssertCommandState(command1, 0, 0, 0, 0, 0);
            AssertCommandState(command2, 0, 0, 0, 0, 0);
            AssertCommandState(command3, 0, 0, 0, 0, 0);
            AssertCommandState(command4, 0, 0, 0, 0, 0);
            button1.SetPressed(true);
            AssertCommandState(command1, 0, 0, 0, 0, 0);
            AssertCommandState(command2, 0, 0, 0, 0, 0);
            AssertCommandState(command3, 0, 0, 0, 0, 0);
            AssertCommandState(command4, 0, 0, 0, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command1, 0, 0, 0, 0, 0);
            AssertCommandState(command2, 0, 0, 0, 0, 0);
            AssertCommandState(command3, 0, 0, 0, 0, 0);
            AssertCommandState(command4, 0, 0, 0, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command1, 1, 1, 1, 0, 0);
            AssertCommandState(command2, 0, 0, 0, 0, 0);
            AssertCommandState(command3, 1, 1, 1, 0, 0);
            AssertCommandState(command4, 0, 0, 0, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command1, 1, 2, 2, 0, 0);
            AssertCommandState(command2, 0, 0, 0, 0, 0);
            AssertCommandState(command3, 1, 2, 2, 0, 0);
            AssertCommandState(command4, 0, 0, 0, 0, 0);
            button2.SetPressed(true);
            Scheduler.Instance.Run();
            AssertCommandState(command1, 1, 3, 3, 0, 0);
            AssertCommandState(command2, 0, 0, 0, 0, 0);
            AssertCommandState(command3, 1, 3, 3, 0, 0);
            AssertCommandState(command4, 0, 0, 0, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command1, 1, 4, 4, 0, 0);
            AssertCommandState(command2, 0, 0, 0, 0, 0);
            AssertCommandState(command3, 1, 4, 4, 0, 0);
            AssertCommandState(command4, 1, 1, 1, 0, 0);
            button1.SetPressed(false);
            Scheduler.Instance.Run();
            AssertCommandState(command1, 1, 5, 5, 0, 0);
            AssertCommandState(command2, 0, 0, 0, 0, 0);
            AssertCommandState(command3, 1, 4, 4, 0, 1);
            AssertCommandState(command4, 1, 2, 2, 0, 0);
            Scheduler.Instance.Run();
            AssertCommandState(command1, 1, 6, 6, 0, 0);
            AssertCommandState(command2, 1, 1, 1, 0, 0);
            AssertCommandState(command3, 1, 4, 4, 0, 1);
            AssertCommandState(command4, 1, 3, 3, 0, 0);
            button2.SetPressed(false);
            Scheduler.Instance.Run();
            AssertCommandState(command1, 1, 7, 7, 0, 0);
            AssertCommandState(command2, 1, 2, 2, 0, 0);
            AssertCommandState(command3, 1, 4, 4, 0, 1);
            AssertCommandState(command4, 1, 3, 3, 0, 1);
            command1.Cancel();
            Scheduler.Instance.Run();
            AssertCommandState(command1, 1, 7, 7, 0, 1);
            AssertCommandState(command2, 1, 3, 3, 0, 0);
            AssertCommandState(command3, 1, 4, 4, 0, 1);
            AssertCommandState(command4, 1, 3, 3, 0, 1);
            command2.SetHasFinished(true);
            Scheduler.Instance.Run();
            AssertCommandState(command1, 1, 7, 7, 0, 1);
            AssertCommandState(command2, 1, 4, 4, 1, 0);
            AssertCommandState(command3, 1, 4, 4, 0, 1);
            AssertCommandState(command4, 1, 3, 3, 0, 1);
            Scheduler.Instance.Run();
            AssertCommandState(command1, 1, 7, 7, 0, 1);
            AssertCommandState(command2, 1, 4, 4, 1, 0);
            AssertCommandState(command3, 1, 4, 4, 0, 1);
            AssertCommandState(command4, 1, 3, 3, 0, 1);
        }
    }
}
