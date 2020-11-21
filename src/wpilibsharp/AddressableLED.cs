using System;
using Hal;

namespace WPILib
{
    public sealed class AddressableLED : IDisposable
    {


        private readonly int m_pwmHandle;
        private readonly int m_handle;

        public AddressableLED(int port)
        {
            m_pwmHandle = Hal.PWMLowLevel.InitializePort(Hal.HALLowLevel.GetPort(port));
            m_handle = Hal.AddressableLEDLowLevel.Initialize(m_pwmHandle);
            UsageReporting.Report(ResourceType.AddressableLEDs, port + 1);
        }

        public void Dispose()
        {
            if (m_handle != 0)
            {
                Hal.AddressableLEDLowLevel.Free(m_handle);
            }
            if (m_pwmHandle != 0)
            {
                Hal.PWMLowLevel.FreePort(m_pwmHandle);
            }
        }

        public void SetLength(int length)
        {
            Hal.AddressableLEDLowLevel.SetLength(m_handle, length);
        }

        public void SetData(ReadOnlySpan<AddressableLEDData> data)
        {
            Hal.AddressableLEDLowLevel.WriteData(m_handle, data);
        }

        public unsafe void SetData(AddressableLEDData* data, int length)
        {
            Hal.AddressableLEDLowLevel.WriteData(m_handle, data, length);
        }

        public void SetBitTiming(int lowTime0NanoSeconds, int highTime0NanoSeconds, int lowTime1NanoSeconds, int highTime1NanoSeconds)
        {
            Hal.AddressableLEDLowLevel.SetBitTiming(m_handle, lowTime0NanoSeconds, highTime0NanoSeconds, lowTime1NanoSeconds, highTime1NanoSeconds);
        }

        public void SetSyncTime(int syncTimeMicroSeconds)
        {
            Hal.AddressableLEDLowLevel.SetSyncTime(m_handle, syncTimeMicroSeconds);
        }

        public void Start()
        {
            Hal.AddressableLEDLowLevel.StartOutput(m_handle);
        }

        public void Stop()
        {
            Hal.AddressableLEDLowLevel.StopOutput(m_handle);
        }
    }
}
