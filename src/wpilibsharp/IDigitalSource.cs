using Hal;

namespace WPILib
{
    public interface IDigitalSource
    {
        bool IsAnalogTrigger { get; }
        int Channel { get; }

        AnalogTriggerType AnalogTriggerTypeForRouting { get; }
        int PortHandleForRouting { get; }
    }
}
