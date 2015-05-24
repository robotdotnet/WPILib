using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPILib.Commands;

namespace WPILib.Buttons
{
    public abstract class Button : Trigger
    {


        public void WhenPressed(Command command)
        {
            WhenActive(command);
        }

        public void WhileHeld(Command command)
        {
            WhileActive(command);
        }

        public void WhenReleased(Command command)
        {
            WhenInactive(command);
        }

        public void ToggleWhenPressed(Command command)
        {
            ToggleWhenActive(command);
        }

        public void CanceWhenPressed(Command command)
        {
            ToggleWhenActive(command);
        }
    }
}
