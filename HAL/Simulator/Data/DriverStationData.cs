using System.Collections.Generic;
using HAL.Base;

namespace HAL.Simulator.Data
{
    /// <summary>
    /// Driver Station Sim Control Data
    /// </summary>
    /// <seealso cref="DataBase" />
    public class DSControlData : DataBase
    {
        /// <inheritdoc/>
        public override void ResetData()
        {
            m_enabled = false;
            m_autonomous = false;
            m_test = false;
            m_eStop = false;
            m_fmsAttached = false;
            m_dsAttached = true;
        }

        internal DSControlData() { }

        private bool m_enabled = false;
        private bool m_autonomous = false;
        private bool m_test = false;
        private bool m_eStop = false;
        private bool m_fmsAttached = false;
        private bool m_dsAttached = true;

        /// <summary>
        /// Gets a value indicating whether this <see cref="DSControlData"/> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
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

        /// <summary>
        /// Gets a value indicating whether this <see cref="DSControlData"/> is autonomous.
        /// </summary>
        /// <value>
        ///   <c>true</c> if autonomous; otherwise, <c>false</c>.
        /// </value>
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

        /// <summary>
        /// Gets a value indicating whether this <see cref="DSControlData"/> is test.
        /// </summary>
        /// <value>
        ///   <c>true</c> if test; otherwise, <c>false</c>.
        /// </value>
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

        /// <summary>
        /// Gets a value indicating whether [e stop].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [e stop]; otherwise, <c>false</c>.
        /// </value>
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

        /// <summary>
        /// Gets a value indicating whether [FMS attached].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [FMS attached]; otherwise, <c>false</c>.
        /// </value>
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

        /// <summary>
        /// Gets a value indicating whether [ds attached].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [ds attached]; otherwise, <c>false</c>.
        /// </value>
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

    /// <summary>
    /// Joystick Sim Data
    /// </summary>
    /// <seealso cref="NotifyDataBase" />
    public class JoystickData : NotifyDataBase
    {
        /// <inheritdoc/>
        public override void ResetData()
        {
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
            m_numAxes = 6;
            m_numButtons = 32;
            m_numPovs = 12;

            base.ResetData();
        }

        internal JoystickData() { }

        private bool[] m_buttons = new bool[33];
        private double[] m_axes = new double[6];
        private int[] m_povs = new[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
        private int m_numAxes = 6;
        private int m_numButtons = 32;
        private int m_numPovs = 12;
        private ushort m_leftRumble = 0;
        private ushort m_rightRumble = 0;
        private int m_isXbox = 0;
        private byte m_type = 0;
        private string m_name = "Joystick";

        /// <summary>
        /// Gets the buttons.
        /// </summary>
        /// <value>
        /// The buttons.
        /// </value>
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
        /// <summary>
        /// Gets the axes.
        /// </summary>
        /// <value>
        /// The axes.
        /// </value>
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

        /// <summary>
        /// Gets the buttons.
        /// </summary>
        /// <value>
        /// The buttons.
        /// </value>
        public int NumButtons
        {
            get { return m_numButtons; }
            set
            {
                if (m_numButtons == value) return;
                m_numButtons = value;
                OnPropertyChanged(value);
            }
        }
        /// <summary>
        /// Gets the axes.
        /// </summary>
        /// <value>
        /// The axes.
        /// </value>
        public int NumAxes
        {
            get { return m_numAxes; }
            set
            {
                if (m_numAxes == value) return;
                m_numAxes = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets the axes.
        /// </summary>
        /// <value>
        /// The axes.
        /// </value>
        public int NumPovs
        {
            get { return m_numPovs; }
            set
            {
                if (m_numPovs == value) return;
                m_numPovs = value;
                OnPropertyChanged(value);
            }
        }

        /// <summary>
        /// Gets the left rumble.
        /// </summary>
        /// <value>
        /// The left rumble.
        /// </value>
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

        /// <summary>
        /// Gets the right rumble.
        /// </summary>
        /// <value>
        /// The right rumble.
        /// </value>
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
        /// <summary>
        /// Gets the is xbox.
        /// </summary>
        /// <value>
        /// The is xbox.
        /// </value>
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

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
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

        /// <summary>
        /// Gets the povs.
        /// </summary>
        /// <value>
        /// The povs.
        /// </value>
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

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
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

    /// <summary>
    /// Driver Station Sim Data
    /// </summary>
    /// <seealso cref="DataBase" />
    public class DriverStationData : DataBase
    {
        /// <inheritdoc/>
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


        /// <summary>
        /// Gets the joysticks.
        /// </summary>
        /// <value>
        /// The joysticks.
        /// </value>
        public IReadOnlyList<JoystickData> Joysticks { get; }
        /// <summary>
        /// Gets the control data.
        /// </summary>
        /// <value>
        /// The control data.
        /// </value>
        public DSControlData ControlData { get; } = new DSControlData();
        private HALAllianceStationID m_allianceStation = 0;

        /// <summary>
        /// Gets or sets the alliance station.
        /// </summary>
        /// <value>
        /// The alliance station.
        /// </value>
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
