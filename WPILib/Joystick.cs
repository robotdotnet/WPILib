

using System;
using System.Collections.Generic;
using System.Text;
using WPILib.Interfaces;
using HAL_FRC;

namespace WPILib
{
    public enum AxisType
    {
        kX = 0,
        kY,
        kZ,
        kTwist,
        kThrottle,
        kNumAxis,
    }

    public enum ButtonType
    {
        kTrigger = 0,
        kTop,
        kNumButton,
    }

    public enum RumbleType
    {
        kLeftRumble = 0,
        kRightRumble = 1,
    }

    public class Joystick : GenericHID
    {
        private static byte s_kDefaultXAxis = 0;
        private static byte s_kDefaultYAxis = 1;
        private static byte s_kDefaultZAxis = 2;
        private static byte s_kDefaultTwistAxis = 2;
        private static byte s_kDefaultThrottleAxis = 3;
        private static byte s_kDefaultTriggerButton = 1;
        private static byte s_kDefaultTopButton = 2;

        private DriverStation _ds;
        private int _port;
        private byte[] _axes;
        private byte[] _buttons;
        private int _outputs;
        private ushort _leftRumble;
        private ushort _rightRumble;

        public Joystick(int port)
            : this(port, (int)AxisType.kNumAxis, (int)ButtonType.kNumButton)
        {
            _axes[(int)AxisType.kX] = s_kDefaultXAxis;

            _axes[(int)AxisType.kX] = s_kDefaultXAxis;
            _axes[(int)AxisType.kY] = s_kDefaultYAxis;
            _axes[(int)AxisType.kZ] = s_kDefaultZAxis;
            _axes[(int)AxisType.kTwist] = s_kDefaultTwistAxis;
            _axes[(int)AxisType.kThrottle] = s_kDefaultThrottleAxis;

            _buttons[(int)ButtonType.kTrigger] = s_kDefaultTriggerButton;
            _buttons[(int)ButtonType.kTop] = s_kDefaultTopButton;

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
            return GetRawAxis(_axes[(int)AxisType.kX]);
        }

        public override double GetY(Hand hand)
        {
            return GetRawAxis(_axes[(int)AxisType.kY]);
        }

        public override double GetZ(Hand hand)
        {
            return GetRawAxis(_axes[(int)AxisType.kZ]);
        }

        public override double GetTwist()
        {
            return GetRawAxis(_axes[(int)AxisType.kTwist]);
        }

        public override double GetThrottle()
        {
            return GetRawAxis(_axes[(int)AxisType.kThrottle]);
        }

        public override double GetRawAxis(int axis)
        {
            return _ds.GetStickAxis(_port, axis);
        }

        public double GetAxis(AxisType axis)
        {
            switch (axis)
            {
                case AxisType.kX:
                    return GetX();
                case AxisType.kY:
                    return GetY();
                case AxisType.kZ:
                    return GetZ();
                case AxisType.kTwist:
                    return GetTwist();
                case AxisType.kThrottle:
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
            return GetRawButton(_buttons[(int)ButtonType.kTrigger]);
        }

        public override bool GetTop(Hand hand)
        {
            return GetRawButton(_buttons[(int)ButtonType.kTop]);
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
                case ButtonType.kTrigger:
                    return GetTrigger();
                case ButtonType.kTop:
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
            if (type == RumbleType.kLeftRumble)
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
