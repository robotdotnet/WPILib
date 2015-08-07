using System;
using NUnit.Framework;
using WPILib.Commands;

namespace WPILib.Tests
{
    [TestFixture]
    public class TestCommand
    {
        [Test]
        public void TestInheritedNameCommand()
        {
            Command cmd = new CommandTest();
            Assert.AreEqual(nameof(CommandTest), cmd.Name);
        }

        [Test]
        public void TestSetNameCommand()
        {
            string name = "This is a test name";
            Command cmd = new CommandTest(name);
            Assert.AreEqual(name, cmd.Name);
        }


    }

    public class CommandTest : Command
    {
        public CommandTest()
        {
            
        }

        public CommandTest(string name) : base(name)
        {
            
        }

        protected override void Initialize()
        {
            throw new NotImplementedException();
        }

        protected override void Execute()
        {
            throw new NotImplementedException();
        }

        protected override bool IsFinished()
        {
            throw new NotImplementedException();
        }

        protected override void End()
        {
            throw new NotImplementedException();
        }

        protected override void Interrupted()
        {
            throw new NotImplementedException();
        }
    }
}
