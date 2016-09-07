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
    public class Joystick : GenericHID, IEquatable<Joystick>
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

        /// <summary>
        /// Rumble Type enum for Joysticks
        /// </summary>
        public enum RumbleType
        {
            /// <summary>
            /// Left Rumble
            /// </summary>
            LeftRumble = 0,
            /// <summary>
            /// Right Rumble
            /// </summary>
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
        /// <param name="port">The port on the Driver Station the joystick is connected to [0..5]</param>
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
        /// <remarks>
        /// This constructor allows the subclass to configure the number of constants
        /// for axes and buttons.
        /// </remarks>
        /// <param name="port">The port on the Driver Station the joystick is connected to [0..5]</param>
        /// <param name="numAxisTypes">The number of axis types in the enum</param>
        /// <param name="numButtonTypes">The number of button types in the enum</param>
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
        
        /// <summary>
        /// Get the value of the axis
        /// </summary>
        /// <param name="axis">The axis to read, starting at 0.</param>
        /// <returns>The value of the axis [-1.0..1.0]</returns>
        public override double GetRawAxis(int axis)
        {
            return m_ds.GetStickAxis(Port, axis);
        }

        /// <summary>
        /// Return the axis determined by the argument.
        /// </summary>
        /// <param name="axis">The axis to read</param>
        /// <returns>The value of the axis</returns>
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

        /// <summary>
        /// Return the number of axis on the current joystick.
        /// </summary>
        public int AxisCount => m_ds.GetStickAxisCount(Port);

        /// <inheritdoc/>
        public override bool GetTrigger(Hand hand)
        {
            return GetRawButton(m_buttons[(int)ButtonType.Trigger]);
        }
        /// <inheritdoc/>
        public override bool GetTop(Hand hand)
        {
            return GetRawButton(m_buttons[(int)ButtonType.Top]);
        }
        /// <inheritdoc/>
        public override bool GetBumper(Hand hand)
        {
            return false;
        }

        /// <summary>
        /// Get the button value (starting at 1).
        /// </summary>
        /// <param name="button">The button number to be read (starting at 1).</param>
        /// <returns>The state of the button.</returns>
        public override bool GetRawButton(int button)
        {
            return m_ds.GetStickButton(Port, (byte)button);
        }

        /// <summary>
        /// Gets the number of buttons on this joystick.
        /// </summary>
        public int ButtonCount => m_ds.GetStickButtonCount(Port);

        /// <summary>
        /// Get the state of a POV on the joystick.
        /// </summary>
        /// <param name="pov">The index of the POV to read (starting at 0).</param>
        /// <returns>The angle of the POV in degrees, or -1 if not pressed.</returns>
        public override int GetPOV(int pov)
        {
            return m_ds.GetStickPOV(Port, pov);
        }

        /// <summary>
        /// Gets the number of POVs on this joystick.
        /// </summary>
        public int POVCount => m_ds.GetStickPOVCount(Port);

        /// <summary>
        /// Get buttons based on an enumberated type.
        /// </summary>
        /// <param name="button">The type of the button to read</param>
        /// <returns>The state of the button.</returns>
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

        /// <summary>
        /// Gets the magnitude of the direction vector formed by the joystick's
        /// current position relative to it's origin.
        /// </summary>
        /// <returns>The magnitude of the direction vector.</returns>
        public double GetMagnitude() => Math.Sqrt(Math.Pow(GetX(), 2) + Math.Pow(GetY(), 2));

        /// <summary>
        /// Gets the direction of the vector formed by the joystick and its origin in radians.
        /// </summary>
        /// <returns>The direction of the vector in radians</returns>
        public double GetDirectionRadians() => Math.Atan2(GetX(), -GetY());

        /// <summary>
        /// Gets the direction of the vector formed by the joystick and its orign in degrees.
        /// </summary>
        /// <returns>The direction of the vector in degrees.</returns>
        public double GetDirectionDegrees() => RadianToDegree(GetDirectionRadians());

        private static double RadianToDegree(double angle) => angle * (180.0 / Math.PI);

        /// <summary>
        /// Gets the channel currently associated with the specified axis.
        /// </summary>
        /// <param name="axis">The axis to look up the channel for.</param>
        /// <returns>The channel for the axis.</returns>
        public int GetAxisChannel(AxisType axis)
        {
            return m_axes[(int)axis];
        }

        /// <summary>
        /// Set the channel associated with the specified axis.
        /// </summary>
        /// <param name="axis">The axis to set the channel for.</param>
        /// <param name="channel">The channel to set the axis to.</param>
        public void SetAxisChannel(AxisType axis, int channel)
        {
            m_axes[(int)axis] = (byte)channel;
        }

        /// <summary>
        /// Gets if the joystick is an Xbox controller.
        /// </summary>
        /// <returns></returns>
        public bool IsXbox => m_ds.GetJoystickIsXbox(Port);

        /// <summary>
        /// Gets the HID type of the current joystick.
        /// </summary>
        public int JoystickType => m_ds.GetJoystickType(Port);

        /// <summary>
        /// Gets the name of the current joystick.
        /// </summary>
        public string Name => m_ds.GetJoystickName(Port);

        /// <summary>
        /// Sets the rumble output for the joystick. 
        /// </summary>
        /// <remarks>
        /// The DS currently supports 2 rumble values, left rumble and right rumble.
        /// </remarks>
        /// <param name="type">Which rumble value to set.</param>
        /// <param name="value">The normalized value (0 to 1) to set the rumble to.</param>
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
            HAL_SetJoystickOutputs((byte)Port, (uint)m_outputs, m_leftRumble, m_rightRumble);
        }

        /// <summary>
        /// Sets a single HID output value for the joystick.
        /// </summary>
        /// <param name="outputNumber">The index of the output to set [1..32]</param>
        /// <param name="value">The value to set the output to.</param>
        public void SetOutput(int outputNumber, bool value)
        {
            m_outputs = (m_outputs & ~(1 << (outputNumber - 1))) | ((value ? 1 : 0) << (outputNumber - 1));
            HAL_SetJoystickOutputs((byte)Port, (uint)m_outputs, m_leftRumble, m_rightRumble);
        }

        /// <summary>
        /// Sets all HID output values for the joystick.
        /// </summary>
        /// <param name="value">The 32 bit output value (1 bit for each output)</param>
        public void SetOutputs(int value)
        {
            m_outputs = value;
            HAL_SetJoystickOutputs((byte)Port, (uint)m_outputs, m_leftRumble, m_rightRumble);
        }

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
