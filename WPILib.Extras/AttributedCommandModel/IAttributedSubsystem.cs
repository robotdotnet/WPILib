using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPILib.Extras.AttributedCommandModel
{
    interface IAttributedSubsystem
    {
        void InitDefaultCommand(Commands.Command cmd);
    }
}
