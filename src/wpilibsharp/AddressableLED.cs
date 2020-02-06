using System;
using Hal;

namespace WPILib
{
    public class AddressableLED : IDisposable
    {


        private readonly int m_pwmHandle;
        private readonly int m_handle;

        public AddressableLED(int port)
        {
            m_pwmHandle = Hal.PWM.InitializePort(Hal.HalBase.GetPort(port));
            m_handle = Hal.AddressableLED.Initialize(m_pwmHandle);
            UsageReporting.Report(ResourceType.AddressableLEDs, port + 1);
        }

        public void Dispose()
        {
            if (m_handle != 0)
            {
                Hal.AddressableLED.Free(m_handle);
            }
            if (m_pwmHandle != 0)
            {
                Hal.PWM.FreePort(m_pwmHandle);
            }
        }

        public void SetLength(int length)
        {
            Hal.AddressableLED.SetLength(m_handle, length);
        }

        public void SetData(ReadOnlySpan<Hal.AddressableLEDData> data)
        {
            Hal.AddressableLED.WriteData(m_handle, data);
        }

        public unsafe void SetData(Hal.AddressableLEDData* data, int length)
        {
            Hal.AddressableLED.WriteData(m_handle, data, length);
        }

        public void SetBitTiming(int lowTime0NanoSeconds, int highTime0NanoSeconds, int lowTime1NanoSeconds, int highTime1NanoSeconds)
        {
            Hal.AddressableLED.SetBitTiming(m_handle, lowTime0NanoSeconds, highTime0NanoSeconds, lowTime1NanoSeconds, highTime1NanoSeconds);
        }

        public void SetSyncTime(int syncTimeMicroSeconds)
        {
            Hal.AddressableLED.SetSyncTime(m_handle, syncTimeMicroSeconds);
        }

        public void Start()
        {
            Hal.AddressableLED.StartOutput(m_handle);
        }

        public void Stop()
        {
            Hal.AddressableLED.StopOutput(m_handle);
        }
    }
}
