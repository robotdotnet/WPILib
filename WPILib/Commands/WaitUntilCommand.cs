using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPILib.Commands
{
    public class WaitUntilCommand : Command
    {
        private double m_time;

        public WaitUntilCommand(double time) : base("WaitUntil(" + time + ")")
        {
            m_time = time;
        }


        protected override void Initialize()
        {
        }

        protected override void Execute()
        {
        }

        protected override bool IsFinished()
        {
            return Timer.GetMatchTime() >= m_time;
        }

        protected override void End()
        {
        }

        protected override void Interrupted()
        {
        }
    }
}
