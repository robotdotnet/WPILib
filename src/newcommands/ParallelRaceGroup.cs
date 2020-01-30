using System;
using System.Collections.Generic;
using System.Text;

namespace WPILib2.Commands
{
    public class ParallelRaceGroup : CommandGroupBase
    {
        public ParallelRaceGroup(params Command[] commands)
        {
            AddCommands(commands);
        }

        public sealed override void AddCommands(params Command[] commands)
        {

        }

      

        public void Initialize()
        {

        }
    }
}
