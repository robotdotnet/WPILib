using System;
using System.Collections.Generic;
using System.Text;

namespace WPILib2.Commands
{
    public abstract class CommandGroupBase : CommandBase, ICommand
    {
        public abstract void AddCommands(params ICommand[] commands);
    }
}
