namespace WPILib
{
    public abstract class GenericHID
    {
        public enum RumbleType
        {
            kLeftRumble,
            kRightRumble
        }

        public enum HIDType
        {
            kUnknown = -1,
            kXInputUnknown = 0,
            kXInputGamepad = 1,
            kXInputWheel = 2,
            kXInputArcadeStick = 3,
            kXInputFlightStick = 4,
            kXInputDancePad = 5,
            kXInputGuitar = 6,
            kXInputGuitar2 = 7,
            kXInputDrumKit = 8,
            kXInputGuitar3 = 11,
            kXInputArcadePad = 19,
            kHIDJoystick = 20,
            kHIDGamepad = 21,
            kHIDDriving = 22,
            kHIDFlight = 23,
            kHID1stPerson = 24
        };

        public enum JoystickHand { kLeftHand = 0, kRightHand = 1 };


        private readonly DriverStation m_ds;
        private int m_outputs;
        private short m_leftRumble;
        private short m_rightRumble;

        public GenericHID(int port)
        {
            m_ds = DriverStation.Instance;
            Port = port;
        }

        public double GetX()
        {
            return GetX(JoystickHand.kRightHand);
        }

        public abstract double GetX(JoystickHand hand);

        public double GetY()
        {
            return GetY(JoystickHand.kRightHand);
        }

        public abstract double GetY(JoystickHand hand);

        public bool GetRawButton(int button)
        {
            return m_ds.GetStickButton(Port, button);
        }

        public bool GetRawButtonPressed(int button)
        {
            return m_ds.GetStickButtonPressed(Port, button);
        }


        public bool GetRawButtonReleased(int button)
        {
            return m_ds.GetStickButtonReleased(Port, button);
        }

        public double GetRawAxis(int axis)
        {
            return m_ds.GetStickAxis(Port, axis);
        }

        public int GetPOV(int pov = 0)
        {
            return m_ds.GetStickPOV(Port, pov);
        }

        public int AxisCount => m_ds.GetStickAxisCount(Port);
        public int POVCount => m_ds.GetStickPOVCount(Port);
        public int ButtonCount => m_ds.GetStickButtonCount(Port);

        public HIDType Type => (HIDType)m_ds.GetJoystickType(Port);

        public string Name => m_ds.GetJoystickName(Port);

        public int Port { get; }

        public void SetOutput(int outputNumber, bool value)
        {

            m_outputs = (m_outputs & ~(1 << (outputNumber - 1))) | ((value ? 1 : 0) << (outputNumber - 1));
            Hal.DriverStationLowLevel.SetJoystickOutputs(Port, m_outputs, m_leftRumble, m_rightRumble);
        }

        public void SetOutputs(int value)
        {
            m_outputs = value;
            Hal.DriverStationLowLevel.SetJoystickOutputs(Port, m_outputs, m_leftRumble, m_rightRumble);
        }

        public void SetRumble(RumbleType type, double value)
        {
            if (value < 0)
            {
                value = 0;
            }
            else if (value > 1)
            {
                value = 1;
            }
            if (type == RumbleType.kLeftRumble)
            {
                m_leftRumble = (short)(value * 65535);
            }
            else
            {
                m_rightRumble = (short)(value * 65535);
            }
            Hal.DriverStationLowLevel.SetJoystickOutputs(Port, m_outputs, m_leftRumble, m_rightRumble);
        }
    }
}
