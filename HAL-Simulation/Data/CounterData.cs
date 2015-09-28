using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAL_Base;

namespace HAL_Simulator.Data
{
    public class CounterData : NotifyDataBase
    {
        private bool m_hasSource = false;
        private bool m_initialized = false;
        private int count = 0;
        private double period = double.MaxValue;
        private bool reset = false;
        private double maxPeriod = 0;
        private bool direction = false;
        private bool reverseDirection = false;
        private int samplesToAverage = 0;
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
            
        }

        public int SamplesToAverage
        {
            get { return samplesToAverage; }
            set
            {
                if (value == samplesToAverage) return;
                samplesToAverage = value;
                OnPropertyChanged(value);
            }
        }

        public double PulseLengthThreshold
        {
            get { return pulseLengthThreshold; }
            set
            {
                if (value.Equals(pulseLengthThreshold)) return;
                pulseLengthThreshold = value;
                OnPropertyChanged(value);
            }
        }


        public Action<string, dynamic> UpCallback { get; set; }
        public Action<string, dynamic> DownCallback { get; set; }

        public uint UpSourceChannel
        {
            get { return upSourceChannel; }
            set
            {
                if (upSourceChannel == value) return;
                upSourceChannel = value;
                OnPropertyChanged(value);
            }
        }
        public uint DownSourceChannel
        {
            get { return downSourceChannel; }
            set
            {
                if (downSourceChannel == value) return;
                downSourceChannel = value;
                OnPropertyChanged(value);
            }
        }

        public int AverageSize
        {
            get { return averageSize; }
            set
            {
                if (averageSize == value) return;
                averageSize = value;
                OnPropertyChanged(value);
            }
        }

        public Mode Mode
        {
            get { return mode; }
            set
            {
                if (mode == value) return;
                mode = value;
                OnPropertyChanged(value);
            }
        }

        public int Count
        {
            get { return count; }
            set
            {
                if (count == value) return;
                count = value;
                OnPropertyChanged(value);
            }
        }

        public double Period
        {
            get { return period; }
            set
            {
                if (value.Equals(period)) return;
                period = value;
                OnPropertyChanged(value);
            }
        }

        public double MaxPeriod
        {
            get { return maxPeriod; }
            set
            {
                if (value.Equals(maxPeriod)) return;
                maxPeriod = value;
                OnPropertyChanged(value);
            }
        }



        public bool HasSource
        {
            get { return m_hasSource; }
            set
            {
                if (m_hasSource == value) return;
                m_hasSource = value;
                OnPropertyChanged(value);
            }
        }

        public bool Initialized
        {
            get { return m_initialized; }
            set
            {
                if (m_initialized == value) return;
                m_initialized = value;
                OnPropertyChanged(value);
            }
        }

        public bool Reset
        {
            get { return reset; }
            set
            {
                if (reset == value) return;
                reset = value;
                OnPropertyChanged(value);
            }
        }

        public bool Direction
        {
            get { return direction; }
            set
            {
                if (direction == value) return;
                direction = value;
                OnPropertyChanged(value);
            }
        }

        public bool ReverseDirection
        {
            get { return reverseDirection; }
            set
            {
                if (reverseDirection == value) return;
                reverseDirection = value;
                OnPropertyChanged(value);
            }
        }

        public bool UpSourceTrigger
        {
            get { return upSourceTrigger; }
            set
            {
                if (upSourceTrigger == value) return;
                upSourceTrigger = value;
                OnPropertyChanged(value);
            }
        }

        public bool DownSourceTrigger
        {
            get { return downSourceTrigger; }
            set
            {
                if (downSourceTrigger == value) return;
                downSourceTrigger = value;
                OnPropertyChanged(value);
            }
        }

        public bool UpdateWhenEmpty
        {
            get { return updateWheEmpty; }
            set
            {
                if (updateWheEmpty == value) return;
                updateWheEmpty = value;
                OnPropertyChanged(value);
            }
        }

        public bool UpRisingEdge
        {
            get { return upRisingEdge; }
            set
            {
                if (upRisingEdge == value) return;
                upRisingEdge = value;
                OnPropertyChanged(value);
            }
        }

        public bool DownRisingEdge
        {
            get { return downRisingEdge; }
            set
            {
                if (downRisingEdge == value) return;
                downRisingEdge = value;
                OnPropertyChanged(value);
            }
        }

        public bool UpFallingEdge
        {
            get { return upFallingEdge; }
            set
            {
                if (upFallingEdge == value) return;
                upFallingEdge = value;
                OnPropertyChanged(value);
            }
        }

        public bool DownFallingEdge
        {
            get { return downFallingEdge; }
            set
            {
                if (downFallingEdge == value) return;
                downFallingEdge = value;
                OnPropertyChanged(value);
            }
        }
    }
}
