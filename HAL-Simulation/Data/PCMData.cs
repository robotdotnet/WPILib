using System.Collections.Generic;

namespace HAL_Simulator.Data
{
    public class PCMData : DataBase
    {
        public IReadOnlyList<SolenoidData> Solenoids { get; }
        public CompressorData Compressor { get; } = new CompressorData();


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

    public class SolenoidData : NotifyDataBase
    {
        private bool m_initialized = false;
        private bool m_solenoidValue = false;

        internal SolenoidData() { }


        public override void ResetData()
        {
            m_initialized = false;
            m_solenoidValue = false;
            base.ResetData();
        }

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

    public class CompressorData : DataBase
    {
        public bool HasSource { get; set; } = false;
        public bool Initialized { get; internal set; } = false;
        public bool On { get; set; } = false;
        public bool CloseLoopEnabled { get; internal set; } = true;
        public bool PressureSwitch { get; set; } = false;
        public float Current { get; set; } = 0.0f;

        internal CompressorData() { }

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
