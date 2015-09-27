using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HAL_Base;

namespace HAL_Simulator.Data
{
    public class GlobalData : DataBase
    {
        private bool m_programStarted = false;
        private HALAllianceStationID m_allianceStationId = HALAllianceStationID.HALAllianceStationID_red1;
        private long m_programStart = SimHooks.GetTime();
        private double m_matchStart = 0.0;
        private double m_analogSampleRate = HALAnalog.DefaultSampleRate;
        private bool m_button = false;

        public override void ResetData()
        {
            m_programStarted = false;
            m_allianceStationId = HALAllianceStationID.HALAllianceStationID_red1;
            m_programStart = SimHooks.GetTime();
            m_matchStart = 0.0;
            m_analogSampleRate = HALAnalog.DefaultSampleRate;
            m_button = false;
        }

        public bool FPGAButton
        {
            get { return m_button; }
            set
            {
                if (value == m_button) return;
                m_button = value;
                OnPropertyChanged(value);
            }
        }

        public bool ProgramStarted
        {
            get { return m_programStarted; }
            set
            {
                if (value == m_programStarted) return;
                m_programStarted = value;
                OnPropertyChanged(value);
            }
        }

        public HALAllianceStationID AllianceStationId
        {
            get { return m_allianceStationId; }
            set
            {
                if (value == m_allianceStationId) return;
                m_allianceStationId = value;
                OnPropertyChanged(value);
            }
        }

        public long ProgramStartTime
        {
            get { return m_programStart; }
            set
            {
                if (value == m_programStart) return;
                m_programStart = value;
                OnPropertyChanged(value);
            }
        }

        public double MatchStart
        {
            get { return m_matchStart; }
            set
            {
                if (value.Equals(m_matchStart)) return;
                m_matchStart = value;
                OnPropertyChanged(value);
            }
        }

        public double AnalogSampleRate
        {
            get { return m_analogSampleRate; }
            set
            {
                if (value.Equals(m_analogSampleRate)) return;
                m_analogSampleRate = value;
                OnPropertyChanged(value);
            }
        }
    }
}
