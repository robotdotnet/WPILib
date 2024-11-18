using CommunityToolkit.Diagnostics;
using CommunityToolkit.HighPerformance;
using WPIHal.Natives;
using WPIHal.Natives.Simulation;

namespace WPILib.Simulation;

public class AddressableLEDSim
{
    private readonly int m_index;

    public AddressableLEDSim()
    {
        m_index = 0;
    }

    public AddressableLEDSim(AddressableLED _)
    {
        m_index = 0;
    }

    private AddressableLEDSim(int index)
    {
        m_index = index;
    }

    public static AddressableLEDSim CreateForChannel(int pwmChannel)
    {
        int index = HalAddressableLEDData.FindForChannel(pwmChannel);
        if (index < 0)
        {
            ThrowHelper.ThrowArgumentOutOfRangeException(nameof(pwmChannel), $"No addressable LED found for PWM channel {pwmChannel}");
        }
        return new AddressableLEDSim(index);
    }

    public static AddressableLEDSim CreateForIndex(int index)
    {
        return new AddressableLEDSim(index);
    }

    public unsafe CallbackStore RegisterInitializedCallback(NotifyCallback callback, bool initialNotify)
    {
        return new CallbackStore(callback, m_index, initialNotify, &HalAddressableLEDData.RegisterInitializedCallback, &HalAddressableLEDData.CancelInitializedCallback);
    }

    public bool GetInitialized()
    {
        return HalAddressableLEDData.GetInitialized(m_index);
    }

    public void SetInitialized(bool initialized)
    {
        HalAddressableLEDData.SetInitialized(m_index, initialized);
    }

    public unsafe CallbackStore RegisterOutputPortCallback(NotifyCallback callback, bool initialNotify)
    {
        return new CallbackStore(callback, m_index, initialNotify, &HalAddressableLEDData.RegisterOutputPortCallback, &HalAddressableLEDData.CancelOutputPortCallback);
    }

    public int GetOutputPort()
    {
        return HalAddressableLEDData.GetOutputPort(m_index);
    }

    public void SetOutputPort(int outputPort)
    {
        HalAddressableLEDData.SetOutputPort(m_index, outputPort);
    }

    public unsafe CallbackStore RegisterLengthCallback(NotifyCallback callback, bool initialNotify)
    {
        return new CallbackStore(callback, m_index, initialNotify, &HalAddressableLEDData.RegisterLengthCallback, &HalAddressableLEDData.CancelLengthCallback);
    }

    public int GetLength()
    {
        return HalAddressableLEDData.GetLength(m_index);
    }

    public void SetLength(int length)
    {
        HalAddressableLEDData.SetLength(m_index, length);
    }

    public unsafe CallbackStore RegisterRunningCallback(NotifyCallback callback, bool initialNotify)
    {
        return new CallbackStore(callback, m_index, initialNotify, &HalAddressableLEDData.RegisterRunningCallback, &HalAddressableLEDData.CancelRunningCallback);
    }

    public bool GetRunning()
    {
        return HalAddressableLEDData.GetRunning(m_index);
    }

    public void SetRunning(bool running)
    {
        HalAddressableLEDData.SetRunning(m_index, running);
    }

    public unsafe CallbackStore RegisterDataCallback(ConstBufferCallback callback)
    {
        return new CallbackStore(callback, m_index, &HalAddressableLEDData.RegisterDataCallback, &HalAddressableLEDData.CancelDataCallback);
    }

    public Memory<HalAddressableLED.LedData> GetData()
    {
        HalAddressableLED.LedData[] data = new HalAddressableLED.LedData[5460];
        var ret = HalAddressableLEDData.GetData(m_index, data.AsSpan());
        return data.AsMemory()[..ret.Length];
    }

    public void SetData(ReadOnlySpan<HalAddressableLED.LedData> data)
    {
        HalAddressableLEDData.SetData(m_index, data);
    }

    public void ResetData()
    {
        HalAddressableLEDData.ResetData(m_index);
    }
}
