using System.Collections.Generic;

namespace HAL.Simulator.Data
{
    /// <summary>
    /// PCM Sim Data
    /// </summary>
    /// <seealso cref="DataBase" />
    public class PCMData : DataBase
    {
        /// <summary>
        /// Gets the solenoids.
        /// </summary>
        /// <value>
        /// The solenoids.
        /// </value>
        public IReadOnlyList<SolenoidData> Solenoids { get; }
        /// <summary>
        /// Gets the compressor.
        /// </summary>
        /// <value>
        /// The compressor.
        /// </value>
        public CompressorData Compressor { get; } = new CompressorData();

        /// <inheritdoc/>
        public override void ResetData()
        {
            foreach (SolenoidData t in Solenoids)
            {
                t.ResetData();
            }
            Compressor.ResetData();
        }

        internal PCMData()
        {
            List<SolenoidData> data = new List<SolenoidData>();

            for (int i = 0; i < 8; i++)
            {
                data.Add(new SolenoidData());
            }
            Solenoids = data.AsReadOnly();

            Compressor.ResetData();
        }
    }

    /// <summary>
    /// Solenoid Sim Data
    /// </summary>
    /// <seealso cref="NotifyDataBase" />
    public class SolenoidData : NotifyDataBase
    {
        private bool m_initialized = false;
        private bool m_solenoidValue = false;

        internal SolenoidData() { }

        /// <inheritdoc/>
        public override void ResetData()
        {
            m_initialized = false;
            m_solenoidValue = false;
            base.ResetData();
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="SolenoidData"/> is initialized.
        /// </summary>
        /// <value>
        ///   <c>true</c> if initialized; otherwise, <c>false</c>.
        /// </value>
        public bool Initialized
        {
            get { return m_initialized; }
            internal set
            {
                if (value == m_initialized) return;
                m_initialized = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="SolenoidData"/> is value.
        /// </summary>
        /// <value>
        ///   <c>true</c> if value; otherwise, <c>false</c>.
        /// </value>
        public bool Value
        {
            get { return m_solenoidValue; }
            internal set
            {
                if (value == m_solenoidValue) return;
                m_solenoidValue = value;
                OnPropertyChanged(value);
            }
        }
    }

    /// <summary>
    /// Compressor Sim Data
    /// </summary>
    /// <seealso cref="DataBase" />
    public class CompressorData : DataBase
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance has source.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has source; otherwise, <c>false</c>.
        /// </value>
        public bool HasSource { get; set; } = false;
        /// <summary>
        /// Gets a value indicating whether this <see cref="CompressorData"/> is initialized.
        /// </summary>
        /// <value>
        ///   <c>true</c> if initialized; otherwise, <c>false</c>.
        /// </value>
        public bool Initialized { get; internal set; } = false;
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CompressorData"/> is on.
        /// </summary>
        /// <value>
        ///   <c>true</c> if on; otherwise, <c>false</c>.
        /// </value>
        public bool On { get; set; } = false;
        /// <summary>
        /// Gets a value indicating whether [close loop enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [close loop enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool CloseLoopEnabled { get; internal set; } = true;
        /// <summary>
        /// Gets or sets a value indicating whether [pressure switch].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [pressure switch]; otherwise, <c>false</c>.
        /// </value>
        public bool PressureSwitch { get; set; } = false;
        /// <summary>
        /// Gets or sets the current.
        /// </summary>
        /// <value>
        /// The current.
        /// </value>
        public float Current { get; set; } = 0.0f;

        internal CompressorData() { }

        /// <inheritdoc/>
        public override void ResetData()
        {
            HasSource = false;
            Initialized = false;
            On = false;
            CloseLoopEnabled = true;
            PressureSwitch = false;
            Current = 0.0f;
        }


    }
}
