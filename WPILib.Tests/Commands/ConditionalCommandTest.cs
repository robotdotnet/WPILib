using System;
using NUnit.Framework;
using WPILib.Commands;

namespace WPILib.Tests.Commands
{
    [TestFixture]
    public class ConditionalCommandTest : AbstractCommandTest
    {
        [Test]
        public void TestTrueCommand()
        {
            DataStore store = new DataStore();
            Command cmd = new ValueCondition(true,
                                             new ValueCommand(5, store),
                                             new ValueCommand(7, store));
            cmd.Start();
            RunScheduler();
            Assert.AreEqual(5, store.data);
        }

        [Test]
        public void TestFalseCommand()
        {
            DataStore store = new DataStore();
            Command cmd = new ValueCondition(false,
                                             new ValueCommand(5, store),
                                             new ValueCommand(7, store));
            cmd.Start();
            RunScheduler();
            Assert.AreEqual(7, store.data);
        }

        [Test]
        public void TestTrueDefaultCommand()
        {
            DataStore store = new DataStore();
            Command cmd = new ValueCondition(true, new ValueCommand(5, store));
            cmd.Start();
            RunScheduler();
            Assert.AreEqual(5, store.data);
        }

        [Test]
        public void TestFalseDefaultCommand()
        {
            DataStore store = new DataStore();
            Command cmd = new ValueCondition(false, new ValueCommand(5, store));
            cmd.Start();
            RunScheduler();
            Assert.AreEqual(0, store.data);
        }

        private void RunScheduler()
        {
            Scheduler.Instance.Run(); // Start condition command
            Scheduler.Instance.Run(); // Run condition, start value
            Scheduler.Instance.Run(); // Run value command
        }
    }

    public class DataStore {
        public int data = 0;
    }

    public class ValueCommand : InstantCommand
    {
        public readonly int data;
        public readonly DataStore store;
        public ValueCommand(int _data, DataStore _store)
        {
            data = _data;
            store = _store;
        }

        protected override void Initialize()
        {
            store.data = data;
        }
    }

    public class ValueCondition : ConditionalCommand
    {
        public readonly bool cond;
        public ValueCondition(bool _cond,
                              Command onTrue,
                              Command onFalse)
            : base(onTrue, onFalse)
        {
            cond = _cond;
        }
        public ValueCondition(bool _cond,
                              Command onTrue)
            : base(onTrue)
        {
            cond = _cond;
        }
        override protected bool Condition() => cond;
    }

}
