using Hal.Natives;
using System;
using WPIUtil.ILGeneration;
using WPIUtil.NativeUtilities;

namespace Hal
{

    public static class CANAPILowLevel
    {
        internal static CANAPILowLevelNative lowLevel = null!;

        internal static void InitializeNatives(IFunctionPointerLoader loader)
        {
            lowLevel = new(loader);
        }

        public static int Initialize(CANManufacturer manufacturer, int deviceId, CANDeviceType deviceType)
        {
            return lowLevel.HAL_InitializeCAN(manufacturer, deviceId, deviceType);
        }

        public static void Clean(int handle)
        {
            lowLevel.HAL_CleanCAN(handle);
        }

        public static unsafe void WritePacket(int handle, ReadOnlySpan<byte> data, int apiId)
        {
            if (data.Length > 8)
            {
                throw new InvalidOperationException("Data size too big");
            }
            byte* toWrite = stackalloc byte[8];
            data.CopyTo(new Span<byte>(toWrite, 8));
            lowLevel.HAL_WriteCANPacket(handle, toWrite, data.Length, apiId);
        }

        public static unsafe void WritePacketRepeating(int handle, ReadOnlySpan<byte> data, int apiId, TimeSpan repeatTime)
        {
            if (data.Length > 8)
            {
                throw new InvalidOperationException("Data size too big");
            }
            byte* toWrite = stackalloc byte[8];
            data.CopyTo(new Span<byte>(toWrite, 8));
            lowLevel.HAL_WriteCANPacketRepeating(handle, toWrite, data.Length, apiId, (int)repeatTime.TotalMilliseconds);
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
