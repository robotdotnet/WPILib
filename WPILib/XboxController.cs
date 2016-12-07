using HAL.Base;

namespace WPILib
{
    public class XboxController : GamepadBase
    {
        public XboxController(int port) : base(port)
        {
            HAL.Base.HAL.Report(ResourceType.kResourceType_Joystick, Port);
        }

        public override double GetX(JoystickHand hand)
        {
            if (hand == JoystickHand.Left)
            {
                return GetRawAxis(0);
            }
            else
            {
                return GetRawAxis(4);
            }
        }

        public override double GetY(JoystickHand hand)
        {
            if (hand == JoystickHand.Left)
            {
                return GetRawAxis(1);
            }
            else
            {
                return GetRawAxis(5);
            }
        }

        public override bool GetBumper(JoystickHand hand)
        {
            if (hand == JoystickHand.Left)
            {
                return GetRawButton(5);
            }
            else
            {
                return GetRawButton(6);
            }
        }

        public override bool GetStickButton(JoystickHand hand)
        {
            if (hand == JoystickHand.Left)
            {
                return GetRawButton(9);
            }
            else
            {
                return GetRawButton(10);
            }
        }

        public virtual double GetTriggerAxis(JoystickHand hand)
        {
            if (hand == JoystickHand.Left)
            {
                return GetRawAxis(2);
            }
            else
            {
                return GetRawAxis(3);
            }
        }

        public bool GetAButton()
        {
            return GetRawButton(1);
        }

        public bool GetBButton()
        {
            return GetRawButton(2);
        }

        public bool GetYButton()
        {
            return GetRawButton(4);
        }

        public bool GetXButton()
        {
            return GetRawButton(3);
        }

        public bool GetBackButton()
        {
            return GetRawButton(7);
        }

        public bool GetStartButton()
        {
            return GetRawButton(8);
        }
    }
}
