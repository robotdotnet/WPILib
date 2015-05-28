using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPILib.Commands
{
    public class PrintCommand : Command
    {
        private string m_message;

        public PrintCommand(string message) : base("Print(\"" + message + "\"")
        {
            m_message = message;
        }

        protected override void Initialize()
        {
            Console.WriteLine(m_message);
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
