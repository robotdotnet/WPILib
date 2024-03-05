using WPIHal;
using WPIHal.Natives.Simulation;

namespace WPILib.Simulation;

public class AddressableLEDSim
{
    private readonly int m_index;

    public AddressableLEDSim()
    {
        m_index = 0;
    }

    public unsafe CallbackStore registerInitializedCallback(Action<string, HalValue> callback, bool initialNotify)
    {
        return new CallbackStore(callback, m_index, initialNotify, &HalAddressableLEDData.RegisterAddressableLEDInitializedCallback, &HalAddressableLEDData.CancelAddressableLEDInitializedCallback);
    }
}
