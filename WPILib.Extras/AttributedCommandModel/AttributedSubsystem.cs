using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib.Commands;

namespace WPILib.Extras.AttributedCommandModel
{
    public abstract class AttributedSubsystem : Commands.Subsystem, IAttributedSubsystem
    {
        public AttributedSubsystem()
            :base()
        {
        }

        public AttributedSubsystem(string name)
            :base(name)
        {
        }

        protected override void InitDefaultCommand()
        {
            //Purposefully do nothing.  The AttributedCommandModelRobot class will set the default command via InitDefaultCommand(Command).
        }

        void IAttributedSubsystem.InitDefaultCommand(Command cmd)
        {
            SetDefaultCommand(cmd);
        }
    }
}
