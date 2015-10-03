using System;
using WPILib.Commands;

namespace WPILib.Extras
{
    public class ActionCommand : Command
    {
        private readonly Action action;

        public ActionCommand(Action action)
        {
            this.action = action;
        }

        public ActionCommand(Action action, string name)
            :base(name)
        {
            this.action = action;
        }

        protected override void End()
        {
        }

        protected override void Execute()
        {
        }

        protected override void Initialize()
        {
            action?.Invoke();
        }

        protected override void Interrupted()
        {
        }

        protected override bool IsFinished()
        {
            return true;
        }
    }
}
