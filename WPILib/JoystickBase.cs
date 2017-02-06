namespace WPILib
{
    public abstract class JoystickBase : GenericHID
    {
        protected JoystickBase(int port) : base(port) { }

        public double GetZ() => GetZ(JoystickHand.Right);

        public abstract double GetZ(JoystickHand hand);

        public abstract double GetTwist();

        public abstract double GetThrottle();

        public bool GetTrigger() => GetTrigger(JoystickHand.Right);

        public abstract bool GetTrigger(JoystickHand hand);

        public bool GetTop() => GetTop(JoystickHand.Right);

        public abstract bool GetTop(JoystickHand hand);
    }
}
