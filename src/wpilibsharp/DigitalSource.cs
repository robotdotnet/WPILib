using Hal;

namespace WPILib
{
    public interface DigitalSource
    {
        bool IsAnalogTrigger { get; }
        int Channel { get; }

        AnalogTriggerType AnalogTriggerTypeForRouting { get; }
        int PortHandleForRouting { get; }
    }
}
