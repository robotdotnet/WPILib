using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPILib.Extras
{
    public class ActionCommand : Commands.Command
    {
        private Action action;

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
            action();
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
