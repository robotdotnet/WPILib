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

    public unsafe CallbackStore RegisterInitializedCallback(NotifyCallback callback, bool initialNotify)
    {
    return new CallbackStore(callback, m_index, initialNotify, &HalAddressableLEDData.RegisterInitializedCallback, &HalAddressableLEDData.CancelInitializedCallback);
    }

    public unsafe CallbackStore RegisterOutputPortCallback(NotifyCallback callback, bool initialNotify)
    {
    return new CallbackStore(callback, m_index, initialNotify, &HalAddressableLEDData.RegisterOutputPortCallback, &HalAddressableLEDData.CancelOutputPortCallback);
    }

    public unsafe CallbackStore RegisterLengthCallback(NotifyCallback callback, bool initialNotify)
    {
    return new CallbackStore(callback, m_index, initialNotify, &HalAddressableLEDData.RegisterLengthCallback, &HalAddressableLEDData.CancelLengthCallback);
    }

    public unsafe CallbackStore RegisterRunningCallback(NotifyCallback callback, bool initialNotify)
    {
    return new CallbackStore(callback, m_index, initialNotify, &HalAddressableLEDData.RegisterRunningCallback, &HalAddressableLEDData.CancelRunningCallback);
    }

    public unsafe CallbackStore RegisterDataCallback(ConstBufferCallback callback)
    {
    return new CallbackStore(callback, m_index, initialNotify, &HalAddressableLEDData.RegisterDataCallback, &HalAddressableLEDData.CancelDataCallback);
    }
}
