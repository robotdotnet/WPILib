using System;
using Hal;

namespace WPILib
{
    public class CAN : IDisposable
    {
        public const CANManufacturer TeamManufacturer = CANManufacturer.kTeamUse;
        public const CANDeviceType TeamDeviceType = CANDeviceType.kMiscellaneous;

        private readonly int m_handle;

        public CAN(int deviceId, CANManufacturer manufacturer = TeamManufacturer, CANDeviceType deviceType = TeamDeviceType)
        {
            m_handle = Hal.CANAPI.Initialize(manufacturer, deviceId, deviceType);
            UsageReporting.Report(ResourceType.CAN, deviceId + 1);
        }



        public void Dispose()
        {
            Hal.CANAPI.Clean(m_handle);
        }

        public void WritePacket(ReadOnlySpan<byte> data, int apiId)
        {
            Hal.CANAPI.WritePacket(m_handle, data, apiId);
        }
    }
}
