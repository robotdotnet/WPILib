namespace HAL.Simulator.Data
{
    public class SPIAccelerometerData : DataBase
    {
        private bool m_hasSource = false;
        private bool m_active = false;
        private byte m_range = 0;
        private double m_x = 0;
        private double m_y = 0;
        private double m_z = 1;

        internal SPIAccelerometerData() { }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has source.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has source; otherwise, <c>false</c>.
        /// </value>
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

        /// <summary>
        /// Gets a value indicating whether this <see cref="AccelerometerData"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
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

        /// <summary>
        /// Gets the range.
        /// </summary>
        /// <value>
        /// The range.
        /// </value>
        public byte Range
        {
            get { return m_range; }
            internal set
            {
                if (value == m_range) return;
                m_range = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets or sets the X Axis on the accelerometer
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
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

        /// <summary>
        /// Gets or sets the y axis on the accelerometer.
        /// </summary>
        /// <value>
        /// The y.
        /// </value>
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

        /// <summary>
        /// Gets or sets the z axis on the accelerometer.
        /// </summary>
        /// <value>
        /// The z.
        /// </value>
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

        /// <inheritdoc/>
        public override void ResetData()
        {
            m_hasSource = false;
            m_active = false;
            m_range = 0;
            m_x = 0;
            m_y = 0;
            m_z = 1;
        }
    }
}
