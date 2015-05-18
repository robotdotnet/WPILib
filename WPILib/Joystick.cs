

using System;
using WPILib.Interfaces;
using HAL_Base;

namespace WPILib
{
    public enum AxisType
    {
        X = 0,
        Y,
        Z,
        Twist,
        Throttle,
        NumAxis,
    }

    public enum ButtonType
    {
        Trigger = 0,
        Top,
        NumButton,
    }

    public enum RumbleType
    {
        LeftRumble = 0,
        RightRumble = 1,
    }

    public class Joystick : GenericHID
    {
        private static byte s_defaultXAxis = 0;
        private static byte s_defaultYAxis = 1;
        private static byte s_defaultZAxis = 2;
        private static byte s_defaultTwistAxis = 2;
        private static byte s_defaultThrottleAxis = 3;
        private static byte s_defaultTriggerButton = 1;
        private static byte s_defaultTopButton = 2;

        private DriverStation _ds;
        private int _port;
        private byte[] _axes;
        private byte[] _buttons;
        private int _outputs;
        private ushort _leftRumble;
        private ushort _rightRumble;

        public Joystick(int port)
            : this(port, (int)AxisType.NumAxis, (int)ButtonType.NumButton)
        {
            _axes[(int)AxisType.X] = s_defaultXAxis;

            _axes[(int)AxisType.X] = s_defaultXAxis;
            _axes[(int)AxisType.Y] = s_defaultYAxis;
            _axes[(int)AxisType.Z] = s_defaultZAxis;
            _axes[(int)AxisType.Twist] = s_defaultTwistAxis;
            _axes[(int)AxisType.Throttle] = s_defaultThrottleAxis;

            _buttons[(int)ButtonType.Trigger] = s_defaultTriggerButton;
            _buttons[(int)ButtonType.Top] = s_defaultTopButton;

            HAL.Report(ResourceType.kResourceType_Joystick, (byte)port);
        }

        protected Joystick(int port, int numAxisTypes, int numButtonTypes)
        {
            _ds = DriverStation.GetInstance();
            _axes = new byte[numAxisTypes];
            _buttons = new byte[numButtonTypes];
            _port = port;
        }



        public override double GetX(Hand hand)
        {
            return GetRawAxis(_axes[(int)AxisType.X]);
        }

        public override double GetY(Hand hand)
        {
            return GetRawAxis(_axes[(int)AxisType.Y]);
        }

        public override double GetZ(Hand hand)
        {
            return GetRawAxis(_axes[(int)AxisType.Z]);
        }

        public override double GetTwist()
        {
            return GetRawAxis(_axes[(int)AxisType.Twist]);
        }

        public override double GetThrottle()
        {
            return GetRawAxis(_axes[(int)AxisType.Throttle]);
        }

        public override double GetRawAxis(int axis)
        {
            return _ds.GetStickAxis(_port, axis);
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

        public int GetAxisCount()
        {
            return _ds.GetStickAxisCount(_port);
        }

        public override bool GetTrigger(Hand hand)
        {
            return GetRawButton(_buttons[(int)ButtonType.Trigger]);
        }

        public override bool GetTop(Hand hand)
        {
            return GetRawButton(_buttons[(int)ButtonType.Top]);
        }

        public override bool GetBumper(Hand hand)
        {
            return false;
        }

        public override bool GetRawButton(int button)
        {
            return _ds.GetStickButton(_port, (byte)button);
        }
        public int GetButtonCount()
        {
            return _ds.GetStickButtonCount(_port);
        }

        public override int GetPOV(int pov)
        {
            return _ds.GetStickPOV(_port, pov);
        }

        public int GetPOVCount()
        {
            return _ds.GetStickPOVCount(_port);
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

        public double GetMagnitude()
        {
            return Math.Sqrt(Math.Pow(GetX(), 2) + Math.Pow(GetY(), 2));
        }

        public double GetDirectionRadians()
        {
            return Math.Atan2(GetX(), -GetY());
        }

        public double GetDirectionDegrees()
        {
            return RadianToDegree(GetDirectionRadians());
        }

        private double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }

        public int GetAxisChannel(AxisType axis)
        {
            return _axes[(int)axis];
        }

        public void SetAxisChannel(AxisType axis, int channel)
        {
            _axes[(int)axis] = (byte)channel;
        }

        public void SetRumble(RumbleType type, float value)
        {
            if (value < 0)
                value = 0;
            else if (value > 1)
                value = 1;
            if (type == RumbleType.LeftRumble)
                _leftRumble = (ushort)(value * 65535);
            else
                _rightRumble = (ushort)(value * 65535);
            HAL.SetJoystickOutputs((byte)_port, (uint)_outputs, _leftRumble, _rightRumble);
        }

        public void SetOutput(int outputNumber, bool value)
        {
            _outputs = (_outputs & ~(1 << (outputNumber - 1))) | ((value ? 1 : 0) << (outputNumber - 1));
            HAL.SetJoystickOutputs((byte)_port, (uint)_outputs, _leftRumble, _rightRumble);
        }

        public void SetOutputs(int value)
        {
            _outputs = value;
            HAL.SetJoystickOutputs((byte)_port, (uint)_outputs, _leftRumble, _rightRumble);
        }
    }
}
