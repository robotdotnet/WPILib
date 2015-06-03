using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPILib.Commands
{
    public class WaitForChildren : Command
    {

        protected override void Initialize()
        {
            
        }

        protected override void Execute()
        {
            
        }

        protected override bool IsFinished()
        {
            return GetGroup() == null || GetGroup().Children.Count == 0;
        }

        protected override void End()
        {
        }

        protected override void Interrupted()
        {
        }
    }
}
