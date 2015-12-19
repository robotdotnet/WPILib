

using System;
using HAL;
using HAL.Base;

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


        private const byte DefaultXAxis = 0;
        private const byte DefaultYAxis = 1;
        private const byte DefaultZAxis = 2;
        private const byte DefaultTwistAxis = 2;
        private const byte DefaultThrottleAxis = 3;
        private const byte DefaultTriggerButton = 1;
        private const byte DefaultTopButton = 2;

        /// <summary>
        /// Gets the port the joystick is attached to on the Driver Station.
        /// </summary>
        public int Port { get; }

        private readonly DriverStation m_ds;
        private readonly byte[] m_axes;
        private readonly byte[] m_buttons;
        private int m_outputs;
        private ushort m_leftRumble;
        private ushort m_rightRumble;

        /// <summary>
        /// Constructs an instance of a joystick with the specified index.
        /// </summary>
        /// <param name="port"></param>
        public Joystick(int port)
            : this(port, (int)AxisType.NumAxis, (int)ButtonType.NumButton)
        {
            m_axes[(int)AxisType.X] = DefaultXAxis;

            m_axes[(int)AxisType.X] = DefaultXAxis;
            m_axes[(int)AxisType.Y] = DefaultYAxis;
            m_axes[(int)AxisType.Z] = DefaultZAxis;
            m_axes[(int)AxisType.Twist] = DefaultTwistAxis;
            m_axes[(int)AxisType.Throttle] = DefaultThrottleAxis;

            m_buttons[(int)ButtonType.Trigger] = DefaultTriggerButton;
            m_buttons[(int)ButtonType.Top] = DefaultTopButton;

            HAL.Base.HAL.Report(ResourceType.kResourceType_Joystick, (byte)port);
        }

        /// <summary>
        /// Protected version of the constructor to be called by sub-classes.
        /// </summary>
        /// <param name="port"></param>
        /// <param name="numAxisTypes"></param>
        /// <param name="numButtonTypes"></param>
        protected Joystick(int port, int numAxisTypes, int numButtonTypes)
        {
            m_ds = DriverStation.Instance;
            m_axes = new byte[numAxisTypes];
            m_buttons = new byte[numButtonTypes];
            Port = port;
        }


        /// <inheritdoc/>
        public override double GetX(Hand hand)
        {
            return GetRawAxis(m_axes[(int)AxisType.X]);
        }
        /// <inheritdoc/>
        public override double GetY(Hand hand)
        {
            return GetRawAxis(m_axes[(int)AxisType.Y]);
        }
        /// <inheritdoc/>
        public override double GetZ(Hand hand)
        {
            return GetRawAxis(m_axes[(int)AxisType.Z]);
        }
        /// <inheritdoc/>
        public override double GetTwist() => GetRawAxis(m_axes[(int) AxisType.Twist]);
        /// <inheritdoc/>
        public override double GetThrottle() => GetRawAxis(m_axes[(int) AxisType.Throttle]);
        /// <inheritdoc/>
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
            HAL.Base.HAL.HALSetJoystickOutputs((byte)Port, (uint)m_outputs, m_leftRumble, m_rightRumble);
        }

        public void SetOutput(int outputNumber, bool value)
        {
            m_outputs = (m_outputs & ~(1 << (outputNumber - 1))) | ((value ? 1 : 0) << (outputNumber - 1));
            HAL.Base.HAL.HALSetJoystickOutputs((byte)Port, (uint)m_outputs, m_leftRumble, m_rightRumble);
        }

        public void SetOutputs(int value)
        {
            m_outputs = value;
            HAL.Base.HAL.HALSetJoystickOutputs((byte)Port, (uint)m_outputs, m_leftRumble, m_rightRumble);
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
