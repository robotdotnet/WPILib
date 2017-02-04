using HAL.Base;

namespace WPILib
{
    public class XboxController : GamepadBase
    {
        public XboxController(int port) : base(port)
        {
            HAL.Base.HAL.Report(ResourceType.kResourceType_Joystick, Port);
        }

        public override double GetX(JoystickHand hand) => GetRawAxis(hand == JoystickHand.Left ? 0 : 4);
        public override double GetY(JoystickHand hand) => GetRawAxis(hand == JoystickHand.Left ? 1 : 5);
        public override bool GetBumper(JoystickHand hand) => GetRawButton(hand == JoystickHand.Left ? 5 : 6);
        public override bool GetStickButton(JoystickHand hand) => GetRawButton(hand == JoystickHand.Left ? 9 : 10);
        public virtual double GetTriggerAxis(JoystickHand hand) => GetRawAxis(hand == JoystickHand.Left ? 2 : 3);

        public bool GetAButton() => GetRawButton(1);
        public bool GetBButton() => GetRawButton(2);
        public bool GetYButton() => GetRawButton(4);
        public bool GetXButton() => GetRawButton(3);
        public bool GetBackButton() => GetRawButton(7);
        public bool GetStartButton() => GetRawButton(8);
    }
}
