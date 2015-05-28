using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPILib.Commands
{
    public class StartCommand : Command
    {
        private Command m_commandToFork;

        public StartCommand(Command commandToStart) : base("Start(" + commandToStart + ")")
        {
            m_commandToFork = commandToStart;
        }

        protected override void Initialize()
        {
            m_commandToFork.Start();
        }

        protected override void Execute()
        {
        }

        protected override bool IsFinished()
        {
            return true;
        }

        protected override void End()
        {
        }

        protected override void Interrupted()
        {
        }
    }
}
