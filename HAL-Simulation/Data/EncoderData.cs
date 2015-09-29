using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAL_Base;

namespace HAL_Simulator.Data
{
    public class EncoderData : NotifyDataBase
    {
        private bool m_hasSource = false;
        private bool m_initialized = false;
        private int count = 0;
        private double period = double.MaxValue;
        private bool reset = false;
        private double maxPeriod = 0;
        private bool direction = false;
        private bool reverseDirection = false;
        private uint samplesToAverage = 0;

        public Dictionary<string, dynamic> Config = null;

        public override void ResetData()
        {

        }

        public uint SamplesToAverage
        {
            get { return samplesToAverage; }
            set
            {
                if (value == samplesToAverage) return;
                samplesToAverage = value;
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

    }
}
