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
        private long m_programStart = SimHooks.GetTime();
        private double m_analogSampleRate = HALAnalog.DefaultSampleRate;

        private ushort m_pwmLoopTiming = 40;

        public override void ResetData()
        {
            m_programStarted = false;
            m_programStart = SimHooks.GetTime();
            m_analogSampleRate = HALAnalog.DefaultSampleRate;
            m_pwmLoopTiming = 40;
            DigitalPWMRate = 0;
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

        public double AnalogSampleRate
        {
            get { return m_analogSampleRate; }
            internal set
            {
                if (value.Equals(m_analogSampleRate)) return;
                m_analogSampleRate = value;
                OnPropertyChanged(value);
            }
        }

        public ushort PWMLoopTiming
        {
            get { return m_pwmLoopTiming; }
            set
            {
                if (value == m_pwmLoopTiming) return;
                m_pwmLoopTiming = value;
                OnPropertyChanged(value);
            }
        }

        public double DigitalPWMRate { get; internal set; } = 0;
    }
}
