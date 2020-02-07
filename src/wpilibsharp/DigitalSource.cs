namespace WPILib
{
    public abstract class DigitalSource : InterruptibleSensorBase
    {
        public abstract bool IsAnalogTrigger { get; }
        public abstract int Channel { get; }
    }
}
