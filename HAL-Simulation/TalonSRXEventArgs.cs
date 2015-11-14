using System;

namespace HAL_Simulator
{
    public class TalonSRXEventArgs : EventArgs
    {
        public TalonSRXEventArgs(bool added)
        {
            this.Added = added;
        }

        public bool Added { get; }
    }
}
