using System;

namespace HAL_Simulator
{
    public class TalonSRXEventArgs : EventArgs
    {
        public TalonSRXEventArgs(bool added)
        {
            this.added = added;
        }
        private bool added;
        public bool Added => added;
    }
}
