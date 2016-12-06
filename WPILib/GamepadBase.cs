namespace WPILib
{
    public abstract class GamepadBase : GenericHID
    {
        public GamepadBase(int port) : base(port)
        {

        }

        public bool GetBumper()
        {
            return GetBumper(JoystickHand.Right);
        }

        public abstract bool GetBumper(JoystickHand hand);

        public abstract bool GetStickButton(JoystickHand hand);
    }
}
