using WPIHal.Handles;
using WPIHal.Natives;

namespace WPILib;

public class AddressableLED : IDisposable
{
    private readonly HalDigitalHandle m_pwmHandle;
    private readonly HalAddressableLEDHandle m_handle;

    public AddressableLED(int port)
    {
        m_pwmHandle = HalPWM.InitializePWMPort(HalBase.GetPort(port), Environment.StackTrace);
        m_handle = HalAddressableLED.InitializeAddressableLED(m_pwmHandle);
        // TODO report
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        if (m_handle.Handle != 0)
        {
            HalAddressableLED.FreeAddressableLED(m_handle);
        }
        if (m_pwmHandle.Handle != 0)
        {
            HalPWM.FreePWMPort(m_pwmHandle);
        }
    }

    public int Length
    {
        set
        {
            HalAddressableLED.SetAddressableLEDLength(m_handle, value);
        }
    }

    public void SetData(ReadOnlySpan<HalAddressableLED.LedData> buffer)
    {
        HalAddressableLED.WriteAddressableLEDData(m_handle, buffer);
    }

    public void SetBitTiming(TimeSpan highTime0, TimeSpan lowTime0, TimeSpan highTime1, TimeSpan lowTime1)
    {
        HalAddressableLED.SetAddressableLEDBitTiming(m_handle, (int)highTime0.TotalNanoseconds, (int)lowTime0.TotalNanoseconds, (int)highTime1.TotalNanoseconds, (int)lowTime1.TotalNanoseconds);
    }

    public void SetSyncTime(TimeSpan syncTime)
    {
        HalAddressableLED.SetAddressableLEDSyncTime(m_handle, (int)syncTime.TotalMicroseconds);
    }

    public void Start()
    {
        HalAddressableLED.StartAddressableLEDOutput(m_handle);
    }

    public void Stop()
    {
        HalAddressableLED.StopAddressableLEDOutput(m_handle);
    }
}
