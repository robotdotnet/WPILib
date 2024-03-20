using WPIHal.Natives;
using WPILib.Event;

namespace WPILib;

public class GenericHID
{
    public enum RumbleType
    {
        LeftRumble,
        RightRumble,
        BothRumble
    }

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
        HID1stPersion = 24
    }

    private long m_outputs;
    private int m_leftRumble;
    private int m_rightRumble;

    public GenericHID(int port)
    {
        Port = port;
    }

    public bool GetRawButton(int button)
    {
        return DriverStation.GetStickButton(Port, button);
    }

    public BooleanEvent button(int button, EventLoop loop)
    {
        return new BooleanEvent(loop, () => GetRawButton(button));
    }

    public double GetRawAxis(int axis)
    {
        return DriverStation.GetStickAxis(Port, axis);
    }

    public int GetPOV(int pov)
    {
        return DriverStation.GetStickPOV(Port, pov);
    }

    public int GetPOV() => GetPOV(0);

    public BooleanEvent Pov(int angle, EventLoop loop) => Pov(0, angle, loop);

    public BooleanEvent Pov(int pov, int angle, EventLoop loop)
    {
        return new BooleanEvent(loop, () => GetPOV(pov) == angle);
    }

    public BooleanEvent PovUp(EventLoop loop)
    {
        return Pov(0, loop);
    }

    public int AxisCount => DriverStation.GetStickAxisCount(Port);

    public int POVCount => DriverStation.GetStickPOVCount(Port);

    public int ButtonCount => DriverStation.GetStickButtonCount(Port);

    public bool IsConnected => DriverStation.IsJoystickConnected(Port);

    public HIDType Type => (HIDType)DriverStation.GetJoystickType(Port);

    public string Name => DriverStation.GetJoystickName(Port);

    public int GetAxisType(int axis) => DriverStation.GetJoystickAxisType(Port, axis);

    public int Port { get; }

    public void SetOutput(int outputNumber, bool value)
    {
        m_outputs = (m_outputs & ~(1 << (outputNumber - 1))) | ((value ? 1u : 0u) << (outputNumber - 1));
        _ = HalDriverStation.SetJoystickOutputs(Port, m_outputs, m_leftRumble, m_rightRumble);
    }

    public void SetOutputs(uint value)
    {
        m_outputs = value;
        _ = HalDriverStation.SetJoystickOutputs(Port, m_outputs, m_leftRumble, m_rightRumble);
    }

    public void SetRumble(RumbleType type, double value)
    {
        value = double.Clamp(value, 0, 1);
        ushort rumbleValue = (ushort)(value * uint.MaxValue);

        switch (type)
        {
            case RumbleType.LeftRumble:
                m_leftRumble = rumbleValue;
                break;
            case RumbleType.RightRumble:
                m_rightRumble = rumbleValue;
                break;
            default:
                m_leftRumble = rumbleValue;
                m_rightRumble = rumbleValue;
                break;
        }
        _ = HalDriverStation.SetJoystickOutputs(Port, m_outputs, m_leftRumble, m_rightRumble);
    }
}
