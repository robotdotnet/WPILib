using HAL.Base;

namespace WPILib
{
    /// <summary>
    /// Which hand the Human Interface Device is associated with.
    /// </summary>
    public enum JoystickHand
    {
        ///Use the Left Hand
        Left,
        ///Use the Right Hand
        Right,
    }
    
    /// <summary>
    /// The rumble type to select
    /// </summary>
    public enum RumbleType
    {
        /// <summary>
        /// Rumble the left motor
        /// </summary>
        LeftRumble,
        /// <summary>
        /// Rumble the right motor
        /// </summary>
        RightRumble
    }

    /// <summary>
    /// The type of the Human Interface Device
    /// </summary>
    public enum HIDType
    {
        Unknown = -1,
        XInputUnknown = 0,
        XInputGamepad = 1,
        XInputWheel = 2,
        XInputArcadeStick = 3,
        XInputFlightStick = 4,
        XInputDancePad = 5,
        XInputGuitar = 6,
        XInputGuitar2 = 7,
        XInputDrumKit = 8,
        XInputGuitar3 = 11,
        XInputArcadePad = 19,
        HIDJoystick = 20,
        HIDGamepad = 21,
        HIDDriving = 22,
        HIDFlight = 23,
        HID1stPerson = 24
    }

    /// <summary>
    /// GenericHID Interface
    /// </summary>
    public abstract class GenericHID
    {
        private DriverStation m_ds = DriverStation.Instance;

        /// <summary>
        /// Creates a new <see cref="GenericHID"/>
        /// </summary>
        /// <param name="port">USB Port on the DS</param>
        protected GenericHID(int port)
        {
            Port = port;
        }

        /// <summary>
        /// Get the x position of the HID
        /// </summary>
        /// <returns>The x position of the HID</returns>
        public double GetX() => GetX(JoystickHand.Right);

        /// <summary>
        /// Get the x position of the HID
        /// </summary>
        /// <param name="hand">Which hand, left or right</param>
        /// <returns>The x position</returns>
        public abstract double GetX(JoystickHand hand);

        /// <summary>
        /// Get the y position of the HID
        /// </summary>
        /// <returns>The y position of the HID</returns>
        public double GetY() => GetY(JoystickHand.Right);

        /// <summary>
        /// Get the y position of the HID
        /// </summary>
        /// <param name="hand">Which hand, left or right</param>
        /// <returns>The y position</returns>
        public abstract double GetY(JoystickHand hand);

        /// <summary>
        /// Get the raw axis
        /// </summary>
        /// <param name="axis">Index of the axis</param>
        /// <returns>The raw value of the selected axis</returns>
        public virtual double GetRawAxis(int axis) => m_ds.GetStickAxis(Port, axis);

        /// <summary>
        /// Is the given button pressed
        /// </summary>
        /// <param name="button">Which button number</param>
        /// <returns>True if the button is pressed</returns>
        public bool GetRawButton(int button) => m_ds.GetStickButton(Port, button);

        /// <summary>
        /// Is POV 0 pressed
        /// </summary>
        /// <returns>the POV value</returns>
        public int GetPOV() => GetPOV(0);

        /// <summary>
        /// Is the given POV pressed
        /// </summary>
        /// <param name="pov">Which POV number</param>
        /// <returns>the POV value</returns>
        public int GetPOV(int pov) => m_ds.GetStickPOV(Port, pov);

        public int GetPOVCount() => m_ds.GetStickPOVCount(Port);

        public HIDType Type => (HIDType)m_ds.GetJoystickType(Port);

        public int Port { get; }

        public string Name => m_ds.GetJoystickName(Port);

        private int m_outputs = 0;
        private ushort m_leftRumble = 0;
        private ushort m_rightRumble = 0;

        public void SetOutput(int outputNumber, bool value)
        {
            m_outputs =
            (m_outputs & ~(1 << (outputNumber - 1))) | (value ? 1 : 0 << (outputNumber - 1));

            HALDriverStation.HAL_SetJoystickOutputs(Port, m_outputs, m_leftRumble, m_rightRumble);
        }

        public void SetOutputs(int value)
        {
            m_outputs = value;
            HALDriverStation.HAL_SetJoystickOutputs(Port, m_outputs, m_leftRumble, m_rightRumble);
        }

        public void SetRumble(RumbleType type, double value)
        {
            if (value < 0)
                value = 0;
            else if (value > 1)
                value = 1;

            if (type == RumbleType.LeftRumble)
            {
                m_leftRumble = (ushort)(value * 65535);
            }
            else
            {
                m_rightRumble = (ushort)(value * 65535);
            }
            HALDriverStation.HAL_SetJoystickOutputs(Port, m_outputs, m_leftRumble, m_rightRumble);
        }
    }
}
