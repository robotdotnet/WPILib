using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HAL_Base;
using HAL_Simulator.Annotations;

namespace HAL_Simulator.Data
{
    public class ProgramData : INotifyPropertyChanged
    {
        private bool m_programStarted = false;
        private HALAllianceStationID m_allianceStationId = HALAllianceStationID.HALAllianceStationID_red1;
        private long m_programStart = SimHooks.GetTime();
        private double m_matchStart = 0.0;
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool ProgramStarted
        {
            get { return m_programStarted; }
            set
            {
                if (value == m_programStarted) return;
                m_programStarted = value;
                OnPropertyChanged();
            }
        }

        public HALAllianceStationID AllianceStationId
        {
            get { return m_allianceStationId; }
            set
            {
                if (value == m_allianceStationId) return;
                m_allianceStationId = value;
                OnPropertyChanged();
            }
        }

        public long ProgramStart
        {
            get { return m_programStart; }
            set
            {
                if (value == m_programStart) return;
                m_programStart = value;
                OnPropertyChanged();
            }
        }

        public double MatchStart
        {
            get { return m_matchStart; }
            set
            {
                if (value.Equals(m_matchStart)) return;
                m_matchStart = value;
                OnPropertyChanged();
            }
        }
    }
}
