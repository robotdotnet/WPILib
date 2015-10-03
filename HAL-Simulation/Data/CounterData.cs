using System;
using HAL_Base;

namespace HAL_Simulator.Data
{
    public class CounterData : EncoderData
    {
        private Mode mode = 0;
        private int averageSize = 0;

        private uint upSourceChannel = 0;
        private bool upSourceTrigger = false;
        private uint downSourceChannel = 0;
        private bool downSourceTrigger = false;

        private bool updateWheEmpty = false;

        private bool upRisingEdge = false;
        private bool upFallingEdge = false;
        private bool downRisingEdge = false;
        private bool downFallingEdge = false;

        private double pulseLengthThreshold = 0;

        public override void ResetData()
        {
            mode = 0;
            averageSize = 0;

            upSourceChannel = 0;
            upSourceTrigger = false;
            downSourceChannel = 0;
            downSourceTrigger = false;

            updateWheEmpty = false;

            upRisingEdge = false;
            upFallingEdge = false;
            downRisingEdge = false;
            downFallingEdge = false;

            pulseLengthThreshold = 0;

            UpCallback = null;
            DownCallback = null;

            base.ResetData();
        }
        public double PulseLengthThreshold
        {
            get { return pulseLengthThreshold; }
            internal set
            {
                if (value.Equals(pulseLengthThreshold)) return;
                pulseLengthThreshold = value;
                OnPropertyChanged(value);
            }
        }


        public Action<string, dynamic> UpCallback { get; internal set; }
        public Action<string, dynamic> DownCallback { get; internal set; }

        public uint UpSourceChannel
        {
            get { return upSourceChannel; }
            internal set
            {
                if (upSourceChannel == value) return;
                upSourceChannel = value;
                OnPropertyChanged(value);
            }
        }
        public uint DownSourceChannel
        {
            get { return downSourceChannel; }
            internal set
            {
                if (downSourceChannel == value) return;
                downSourceChannel = value;
                OnPropertyChanged(value);
            }
        }

        public int AverageSize
        {
            get { return averageSize; }
            internal set
            {
                if (averageSize == value) return;
                averageSize = value;
                OnPropertyChanged(value);
            }
        }

        public Mode Mode
        {
            get { return mode; }
            internal set
            {
                if (mode == value) return;
                mode = value;
                OnPropertyChanged(value);
            }
        }

        public bool UpSourceTrigger
        {
            get { return upSourceTrigger; }
            internal set
            {
                if (upSourceTrigger == value) return;
                upSourceTrigger = value;
                OnPropertyChanged(value);
            }
        }

        public bool DownSourceTrigger
        {
            get { return downSourceTrigger; }
            internal set
            {
                if (downSourceTrigger == value) return;
                downSourceTrigger = value;
                OnPropertyChanged(value);
            }
        }

        public bool UpdateWhenEmpty
        {
            get { return updateWheEmpty; }
            internal set
            {
                if (updateWheEmpty == value) return;
                updateWheEmpty = value;
                OnPropertyChanged(value);
            }
        }

        public bool UpRisingEdge
        {
            get { return upRisingEdge; }
            internal set
            {
                if (upRisingEdge == value) return;
                upRisingEdge = value;
                OnPropertyChanged(value);
            }
        }

        public bool DownRisingEdge
        {
            get { return downRisingEdge; }
            internal set
            {
                if (downRisingEdge == value) return;
                downRisingEdge = value;
                OnPropertyChanged(value);
            }
        }

        public bool UpFallingEdge
        {
            get { return upFallingEdge; }
            internal set
            {
                if (upFallingEdge == value) return;
                upFallingEdge = value;
                OnPropertyChanged(value);
            }
        }

        public bool DownFallingEdge
        {
            get { return downFallingEdge; }
            internal set
            {
                if (downFallingEdge == value) return;
                downFallingEdge = value;
                OnPropertyChanged(value);
            }
        }
    }
}
