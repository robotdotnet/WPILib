using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPILib.Buttons
{
    public abstract class Trigger : Sendable
    {
        public abstract bool Get();

        private bool Grab()
        {
            return Get(); // Add table when we have it.
        }
    }
}
