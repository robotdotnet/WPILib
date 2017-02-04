using System;
using HAL.Base;
using static HAL.Base.HALDriverStation;

namespace WPILib
{
    /// <summary>
    /// Handles inputs from joysticks connected to the driver station.
    /// </summary>
    /// <remarks>
    /// This class handles standard input that comes from the Driver Station. Each time a
    /// value is requested the most recent value is returned. There is a single class instrance
    /// for each joystick and the mapping of ports to hardware buttons depends on the code
    /// in the driver stations.
    /// </remarks>
    public class Joystick : JoystickBase, IEquatable<Joystick>
    {
        /// <summary>
        /// Axes enum for Joysticks
        /// </summary>
        public enum AxisType
        {
            /// <summary>
            /// X Axis
            /// </summary>
            X = 0,
            /// <summary>
            /// Y Axis
            /// </summary>
            Y,
            /// <summary>
            /// Z Axis
            /// </summary>
            Z,
            /// <summary>
            /// Twist
            /// </summary>
            Twist,
            /// <summary>
            /// Throttle
            /// </summary>
            Throttle,
            /// <summary>
            /// Number of axis
            /// </summary>
            NumAxis,
        }

        /// <summary>
        /// Button enum for Joysticks
        /// </summary>
        public enum ButtonType
        {
            /// <summary>
            /// Trigger button
            /// </summary>
            Trigger = 0,
            /// <summary>
            /// Top button
            /// </summary>
            Top,
            /// <summary>
            /// Number of buttons
            /// </summary>
            NumButton,
        }

        public const byte DefaultXAxis = 0;
        public const byte DefaultYAxis = 1;
        public const byte DefaultZAxis = 2;
        public const byte DefaultTwistAxis = 2;
        public const byte DefaultThrottleAxis = 3;
        public const byte DefaultTriggerButton = 1;
        public const byte DefaultTopButton = 2;

        private readonly DriverStation m_ds;
        private readonly int[] m_axes;
        private readonly int[] m_buttons;

        /// <summary>
        /// Constructs an instance of a joystick with the specified index.
        /// </summary>
        /// <param name="port">The port on the Driver Station the joystick is connected to [0..5]</param>
        public Joystick(int port)
            : this(port, (int)AxisType.NumAxis, (int)ButtonType.NumButton)
        {
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
        /// <remarks>
        /// This constructor allows the subclass to configure the number of constants
        /// for axes and buttons.
        /// </remarks>
        /// <param name="port">The port on the Driver Station the joystick is connected to [0..5]</param>
        /// <param name="numAxisTypes">The number of axis types in the enum</param>
        /// <param name="numButtonTypes">The number of button types in the enum</param>
        public Joystick(int port, int numAxisTypes, int numButtonTypes) : base(port)
        {
            m_ds = DriverStation.Instance;
            m_axes = new int[numAxisTypes];
            m_buttons = new int[numButtonTypes];
        }

        public int GetAxisChannel(AxisType axis)
        {
            return m_axes[(int)axis];
        }

        public void SetAxisChannel(AxisType axis, int channel)
        {
            m_axes[(int)axis] = channel;
        }

        public override double GetX(JoystickHand hand)
        {
            return GetRawAxis(m_axes[(int)AxisType.X]);
        }

        public override double GetY(JoystickHand hand)
        {
            return GetRawAxis(m_axes[(int)AxisType.Y]);
        }

        public override double GetZ(JoystickHand hand)
        {
            return GetRawAxis(m_axes[(int)AxisType.Z]);
        }

        public override double GetTwist()
        {
            return GetRawAxis(m_axes[(int)AxisType.Twist]);
        }

        public override double GetThrottle()
        {
            return GetRawAxis(m_axes[(int)AxisType.Throttle]);
        }

        public virtual double GetAxis(AxisType axis)
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

        public override bool GetTrigger(JoystickHand hand)
        {
            return GetRawButton(m_buttons[(int)ButtonType.Trigger]);
        }

        public override bool GetTop(JoystickHand hand)
        {
            return GetRawButton(m_buttons[(int)ButtonType.Top]);
        }

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

        public virtual double GetMagnitude()
        {
            double x = GetX();
            double y = GetY();
            return Math.Sqrt(x * x + y * y);
        }

        public virtual double GetDirectionRadians() => Math.Atan2(GetX(), GetY());

        public virtual double GetDirectionDegrees()
        {
            return (180.0 / Math.Acos(-1)) * GetDirectionRadians();
        }

        public int GetAxisType(int axis) => m_ds.GetJoystickAxisType(Port, axis);
        public int GetAxisCount() => m_ds.GetStickAxisCount(Port);
        public int GetButtonCount() => m_ds.GetStickButtonCount(Port);



        ///<inheritdoc/>
        public bool Equals(Joystick other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Port == other.Port;
        }
        ///<inheritdoc/>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as Joystick);
        }
        ///<inheritdoc/>
        public override int GetHashCode()
        {
            return Port;
        }
    }
}
