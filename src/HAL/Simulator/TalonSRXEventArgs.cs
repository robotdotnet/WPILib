using System;

namespace HAL.Simulator
{
    public class TalonSRXEventArgs : EventArgs
    {
        public TalonSRXEventArgs(bool added)
        {
            Added = added;
        }

        public bool Added { get; }
    }
}
