namespace WPILib
{
    public abstract class DigitalSource
    {
        public abstract bool IsAnalogTrigger { get; }
        public abstract int Channel { get; }
    }
}
