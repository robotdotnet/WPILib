using Hal.Natives;
using System;
using System.Collections.Generic;
using System.Text;
using WPIUtil.NativeUtilities;

namespace Hal
{
    [NativeInterface(typeof(ICANAPI))]
    public static class CANAPI
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        private static ICANAPI api;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // Field is never assigned to
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        public static int Initialize(CANManufacturer manufacturer, int deviceId, CANDeviceType deviceType)
        {
            return api.HAL_InitializeCAN(manufacturer, deviceId, deviceType);
        }

        public static void Clean(int handle)
        {
            api.HAL_CleanCAN(handle);
        }

        public unsafe static void WritePacket(int handle, ReadOnlySpan<byte> data, int apiId)
        {
            if (data.Length > 8)
            {
                throw new InvalidOperationException("Data size too big");
            }
            byte* toWrite = stackalloc byte[8];
            data.CopyTo(new Span<byte>(toWrite, 8));
            api.HAL_WriteCANPacket(handle, toWrite, data.Length, apiId);
        }

        public unsafe static void WritePacketRepeating(int handle, ReadOnlySpan<byte> data, int apiId, TimeSpan repeatTime)
        {
            if (data.Length > 8)
            {
                throw new InvalidOperationException("Data size too big");
            }
            byte* toWrite = stackalloc byte[8];
            data.CopyTo(new Span<byte>(toWrite, 8));
            api.HAL_WriteCANPacketRepeating(handle, toWrite, data.Length, apiId, (int)repeatTime.TotalMilliseconds);
        }


        //[StatusCheckLastParameter]
        //void HAL_WriteCANRTRFrame(int handle, int length, int apiId);

        //[StatusCheckLastParameter]
        //void HAL_StopCANPacketRepeating(int handle, int apiId);

        //[StatusCheckLastParameter]
        //void HAL_ReadCANPacketNew(int handle, int apiId, byte* data, int* length, ulong* receivedTimestamp);

        //[StatusCheckLastParameter]
        //void HAL_ReadCANPacketLatest(int handle, int apiId, byte* data, int* length, ulong* receivedTimestamp);

        //[StatusCheckLastParameter]
        //void HAL_ReadCANPackettimeout(int handle, int apiId, byte* data, int* length, ulong* receivedTimestamp, int timeoutMs);
    }
}
