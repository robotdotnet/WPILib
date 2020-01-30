using System;
using System.Collections.Generic;
using System.Text;

namespace WPILib2.Commands
{
    public abstract class CommandGroupBase : CommandBase, Command
    {
        public abstract void AddCommands(params Command[] commands);
    }
}
