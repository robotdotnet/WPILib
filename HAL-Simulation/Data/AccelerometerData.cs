using HAL_Base;

namespace HAL_Simulator.Data
{
    public class AccelerometerData : DataBase
    {
        private bool m_hasSource = false;
        private bool m_active = false;
        private AccelerometerRange m_range = AccelerometerRange.Range_2G;
        private double m_x = 0;
        private double m_y = 0;
        private double m_z = 1;

        public bool HasSource
        {
            get { return m_hasSource; }
            set
            {
                if (value == m_hasSource) return;
                m_hasSource = value;
                OnPropertyChanged(value);
            }
        }

        public bool Active
        {
            get { return m_active; }
            internal set
            {
                if (value == m_active) return;
                m_active = value;
                OnPropertyChanged(value);
            }
        }

        public AccelerometerRange Range
        {
            get { return m_range; }
            internal set
            {
                if (value == m_range) return;
                m_range = value;
                OnPropertyChanged(value);
            }
        }

        public double X
        {
            get { return m_x; }
            set
            {
                if (value.Equals(m_x)) return;
                m_x = value;
                OnPropertyChanged(value);
            }
        }

        public double Y
        {
            get { return m_y; }
            set
            {
                if (value.Equals(m_y)) return;
                m_y = value;
                OnPropertyChanged(value);
            }
        }

        public double Z
        {
            get { return m_z; }
            set
            {
                if (value.Equals(m_z)) return;
                m_z = value;
                OnPropertyChanged(value);
            }
        }

        public override void ResetData()
        {
            m_hasSource = false;
            m_active = false;
            m_range = AccelerometerRange.Range_2G;
            m_x = 0.0;
            m_y = 0.0;
            m_z = 1.0;
        }
    }
}
