using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPILib.Commands;

namespace WPILib.Buttons
{
    public abstract class Trigger : Sendable
    {
        public abstract bool Get();

        private bool Grab()
        {
            return Get(); // Add table when we have it.
        }

        public void WhenActive(Command command)
        {
            
        }
    }
}
