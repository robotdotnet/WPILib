using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAL_Base;

namespace HAL_Simulator.Data
{
    public class DSControlData : DataBase
    {
        public override void ResetData()
        {
            m_hasSource = false;
            m_enabled = false;
            m_autonomous = false;
            m_test = false;
            m_eStop = false;
            m_fmsAttached = false;
            m_dsAttached = true;
        }

        internal DSControlData() { }

        private bool m_hasSource = false;
        private bool m_enabled = false;
        private bool m_autonomous = false;
        private bool m_test = false;
        private bool m_eStop = false;
        private bool m_fmsAttached = false;
        private bool m_dsAttached = true;

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

        public bool Enabled
        {
            get { return m_enabled; }
            internal set
            {
                if (m_enabled == value) return;
                m_enabled = value;
                OnPropertyChanged(value);
            }
        }

        public bool Autonomous
        {
            get { return m_autonomous; }
            internal set
            {
                if (m_autonomous == value) return;
                m_autonomous = value;
                OnPropertyChanged(value);
            }
        }

        public bool Test
        {
            get { return m_test; }
            internal set
            {
                if (m_test == value) return;
                m_test = value;
                OnPropertyChanged(value);
            }
        }

        public bool EStop
        {
            get { return m_eStop; }
            internal set
            {
                if (m_eStop == value) return;
                m_eStop = value;
                OnPropertyChanged(value);
            }
        }

        public bool FmsAttached
        {
            get { return m_fmsAttached; }
            internal set
            {
                if (m_fmsAttached == value) return;
                m_fmsAttached = value;
                OnPropertyChanged(value);
            }
        }

        public bool DsAttached
        {
            get { return m_dsAttached; }
            internal set
            {
                if (m_dsAttached == value) return;
                m_dsAttached = value;
                OnPropertyChanged(value);
            }
        }
    }

    public class JoystickData : NotifyDataBase
    {
        public override void ResetData()
        {
            m_hasSource = false;
            m_leftRumble = 0;
            m_rightRumble = 0;
            m_isXbox = 0;
            m_type = 0;
            m_name = "Joystick";
            for (int i = 0; i < m_buttons.Length; i++)
            {
                m_buttons[i] = false;
            }

            for (int i = 0; i < m_axes.Length; i++)
            {
                m_axes[i] = 0.0;
            }

            for (int i = 0; i < m_povs.Length; i++)
            {
                m_povs[i] = -1;
            }


            base.ResetData();
        }

        internal JoystickData() { }

        private bool m_hasSource = false;
        private bool[] m_buttons = new bool[13];
        private double[] m_axes = new double[6];
        private int[] m_povs = new[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
        private ushort m_leftRumble = 0;
        private ushort m_rightRumble = 0;
        private int m_isXbox = 0;
        private byte m_type = 0;
        private string m_name = "Joystick";

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

        public bool[] Buttons
        {
            get { return m_buttons; }
            internal set
            {
                if (m_buttons == value) return;
                m_buttons = value;
                OnPropertyChanged(value);
            }
        }
        public double[] Axes
        {
            get { return m_axes; }
            internal set
            {
                if (m_axes == value) return;
                m_axes = value;
                OnPropertyChanged(value);
            }
        }
        public ushort LeftRumble
        {
            get { return m_leftRumble; }
            internal set
            {
                if (m_leftRumble == value) return;
                m_leftRumble = value;
                OnPropertyChanged(value);
            }
        }

        public ushort RightRumble
        {
            get { return m_rightRumble; }
            internal set
            {
                if (m_rightRumble == value) return;
                m_rightRumble = value;
                OnPropertyChanged(value);
            }
        }
        public int IsXbox
        {
            get { return m_isXbox; }
            internal set
            {
                if (m_isXbox == value) return;
                m_isXbox = value;
                OnPropertyChanged(value);
            }
        }

        public byte Type
        {
            get { return m_type; }
            internal set
            {
                if (m_type == value) return;
                m_type = value;
                OnPropertyChanged(value);
            }
        }

        public int[] Povs
        {
            get { return m_povs; }
            internal set
            {
                if (m_povs == value) return;
                m_povs = value;
                OnPropertyChanged(value);
            }
        }

        public string Name
        {
            get { return m_name; }
            internal set
            {
                if (m_name == value) return;
                m_name = value;
                OnPropertyChanged(value);
            }
        }
    }

    public class DriverStationData : DataBase
    {
        public override void ResetData()
        {
            foreach (var joystickData in Joysticks)
            {
                joystickData.ResetData();
            }
            ControlData.ResetData();
            m_allianceStation = 0;
        }

        internal DriverStationData()
        {
            List<JoystickData> data = new List<JoystickData>();

            for (int i = 0; i < 6; i++)
            {
                data.Add(new JoystickData());
            }

            Joysticks = data.AsReadOnly();
        }
 

        public IReadOnlyList<JoystickData> Joysticks { get; } 
        public DSControlData ControlData { get; } = new DSControlData();
        private HALAllianceStationID m_allianceStation = 0;

        public HALAllianceStationID AllianceStation
        {
            get { return m_allianceStation; }
            set
            {
                if (m_allianceStation == value) return;
                m_allianceStation = value;
                OnPropertyChanged(value);
            }
        }
    }
}
