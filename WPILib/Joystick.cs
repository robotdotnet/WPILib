

using System;
using HAL;

namespace WPILib
{
    /// <summary>
    /// Handles inputs from joysticks connected to the driver station.
    /// </summary>
    public class Joystick : GenericHID, IEquatable<Joystick>
    {
        /// <summary>
        /// Axes enum for Joysticks
        /// </summary>
        public enum AxisType
        {
            X = 0,
            Y,
            Z,
            Twist,
            Throttle,
            NumAxis,
        }

        /// <summary>
        /// Button enum for Joysticks
        /// </summary>
        public enum ButtonType
        {
            Trigger = 0,
            Top,
            NumButton,
        }

        /// <summary>
        /// Rumble Type enum for Joysticks
        /// </summary>
        public enum RumbleType
        {
            LeftRumble = 0,
            RightRumble = 1,
        }


        private const byte s_defaultXAxis = 0;
        private const byte s_defaultYAxis = 1;
        private const byte s_defaultZAxis = 2;
        private const byte s_defaultTwistAxis = 2;
        private const byte s_defaultThrottleAxis = 3;
        private const byte s_defaultTriggerButton = 1;
        private const byte s_defaultTopButton = 2;

        public int Port { get; }

        private DriverStation m_ds;
        private byte[] m_axes;
        private byte[] m_buttons;
        private int m_outputs;
        private ushort m_leftRumble;
        private ushort m_rightRumble;

        public Joystick(int port)
            : this(port, (int)AxisType.NumAxis, (int)ButtonType.NumButton)
        {
            m_axes[(int)AxisType.X] = s_defaultXAxis;

            m_axes[(int)AxisType.X] = s_defaultXAxis;
            m_axes[(int)AxisType.Y] = s_defaultYAxis;
            m_axes[(int)AxisType.Z] = s_defaultZAxis;
            m_axes[(int)AxisType.Twist] = s_defaultTwistAxis;
            m_axes[(int)AxisType.Throttle] = s_defaultThrottleAxis;

            m_buttons[(int)ButtonType.Trigger] = s_defaultTriggerButton;
            m_buttons[(int)ButtonType.Top] = s_defaultTopButton;

            HAL.HAL.Report(ResourceType.kResourceType_Joystick, (byte)port);
        }

        protected Joystick(int port, int numAxisTypes, int numButtonTypes)
        {
            m_ds = DriverStation.Instance;
            m_axes = new byte[numAxisTypes];
            m_buttons = new byte[numButtonTypes];
            Port = port;
        }



        public override double GetX(Hand hand)
        {
            return GetRawAxis(m_axes[(int)AxisType.X]);
        }

        public override double GetY(Hand hand)
        {
            return GetRawAxis(m_axes[(int)AxisType.Y]);
        }

        public override double GetZ(Hand hand)
        {
            return GetRawAxis(m_axes[(int)AxisType.Z]);
        }

        public override double GetTwist() => GetRawAxis(m_axes[(int) AxisType.Twist]);

        public override double GetThrottle() => GetRawAxis(m_axes[(int) AxisType.Throttle]);

        public override double GetRawAxis(int axis)
        {
            return m_ds.GetStickAxis(Port, axis);
        }

        public double GetAxis(AxisType axis)
        {
            switch (axis)
            {
                case AxisType.X:
                    return GetX();
                case AxisType.Y:
                    return GetY();
                case AxisType.Z:
                    return GetZ();
                case AxisType.Twist:
                    return GetTwist();
                case AxisType.Throttle:
                    return GetThrottle();
                default:
                    return 0.0;
            }
        }

        public int AxisCount => m_ds.GetStickAxisCount(Port);

        public override bool GetTrigger(Hand hand)
        {
            return GetRawButton(m_buttons[(int)ButtonType.Trigger]);
        }

        public override bool GetTop(Hand hand)
        {
            return GetRawButton(m_buttons[(int)ButtonType.Top]);
        }

        public override bool GetBumper(Hand hand)
        {
            return false;
        }

        public override bool GetRawButton(int button)
        {
            return m_ds.GetStickButton(Port, (byte)button);
        }

        public int ButtonCount => m_ds.GetStickButtonCount(Port);

        public override int GetPOV(int pov)
        {
            return m_ds.GetStickPOV(Port, pov);
        }

        public int POVCount => m_ds.GetStickPOVCount(Port);

        public bool GetButton(ButtonType button)
        {
            switch (button)
            {
                case ButtonType.Trigger:
                    return GetTrigger();
                case ButtonType.Top:
                    return GetTop();
                default:
                    return false;
            }
        }

        public double GetMagnitude() => Math.Sqrt(Math.Pow(GetX(), 2) + Math.Pow(GetY(), 2));

        public double GetDirectionRadians() => Math.Atan2(GetX(), -GetY());

        public double GetDirectionDegrees() => RadianToDegree(GetDirectionRadians());

        private static double RadianToDegree(double angle) => angle * (180.0 / Math.PI);

        public int GetAxisChannel(AxisType axis)
        {
            return m_axes[(int)axis];
        }

        public void SetAxisChannel(AxisType axis, int channel)
        {
            m_axes[(int)axis] = (byte)channel;
        }

        public bool GetIsXbox()
        {
            return m_ds.GetJoystickIsXbox(Port);
        }

        public int GetJoystickType()
        {
            return m_ds.GetJoystickType(Port);
        }

        public string GetName()
        {
            return m_ds.GetJoystickName(Port);
        }

        public void SetRumble(RumbleType type, float value)
        {
            if (value < 0)
                value = 0;
            else if (value > 1)
                value = 1;
            if (type == RumbleType.LeftRumble)
                m_leftRumble = (ushort)(value * 65535);
            else
                m_rightRumble = (ushort)(value * 65535);
            HAL.HAL.HALSetJoystickOutputs((byte)Port, (uint)m_outputs, m_leftRumble, m_rightRumble);
        }

        public void SetOutput(int outputNumber, bool value)
        {
            m_outputs = (m_outputs & ~(1 << (outputNumber - 1))) | ((value ? 1 : 0) << (outputNumber - 1));
            HAL.HAL.HALSetJoystickOutputs((byte)Port, (uint)m_outputs, m_leftRumble, m_rightRumble);
        }

        public void SetOutputs(int value)
        {
            m_outputs = value;
            HAL.HAL.HALSetJoystickOutputs((byte)Port, (uint)m_outputs, m_leftRumble, m_rightRumble);
        }

        public bool Equals(Joystick other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Port == other.Port;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals(obj as Joystick);
        }

        public override int GetHashCode()
        {
            return Port;
        }
    }
}
