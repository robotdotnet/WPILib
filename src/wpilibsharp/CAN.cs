using System;
using Hal;

namespace WPILib
{
    public sealed class CAN : IDisposable
    {
        public const CANManufacturer TeamManufacturer = CANManufacturer.kTeamUse;
        public const CANDeviceType TeamDeviceType = CANDeviceType.kMiscellaneous;

        private readonly int m_handle;

        public CAN(int deviceId, CANManufacturer manufacturer = TeamManufacturer, CANDeviceType deviceType = TeamDeviceType)
        {
            m_handle = Hal.CANAPILowLevel.Initialize(manufacturer, deviceId, deviceType);
            UsageReporting.Report(ResourceType.CAN, deviceId + 1);
        }



        public void Dispose()
        {
            Hal.CANAPILowLevel.Clean(m_handle);
        }

        public void WritePacket(ReadOnlySpan<byte> data, int apiId)
        {
            Hal.CANAPILowLevel.WritePacket(m_handle, data, apiId);
        }
    }
}
