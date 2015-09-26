using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL_Base;

namespace HAL_RoboRIO
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALCAN
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_sendMessage = (HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_sendMessageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "FRC_NetworkCommunication_CANSessionMux_sendMessage"), typeof(HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_sendMessageDelegate));

            HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_receiveMessage = (HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_receiveMessageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "FRC_NetworkCommunication_CANSessionMux_receiveMessage"), typeof(HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_receiveMessageDelegate));

            HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_openStreamSession = (HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_openStreamSessionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "FRC_NetworkCommunication_CANSessionMux_openStreamSession"), typeof(HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_openStreamSessionDelegate));

            HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_closeStreamSession = (HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_closeStreamSessionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "FRC_NetworkCommunication_CANSessionMux_closeStreamSession"), typeof(HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_closeStreamSessionDelegate));

            HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_readStreamSession = (HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_readStreamSessionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "FRC_NetworkCommunication_CANSessionMux_readStreamSession"), typeof(HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_readStreamSessionDelegate));

            HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_getCANStatus = (HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_getCANStatusDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "FRC_NetworkCommunication_CANSessionMux_getCANStatus"), typeof(HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_getCANStatusDelegate));

        }

        [DllImport(HAL.LibhalathenaSharedSo,
            EntryPoint = "FRC_NetworkCommunication_CANSessionMux_sendMessage")]
        public static extern void FRC_NetworkCommunication_CANSessionMux_sendMessage(uint messageID, byte[] data,
            byte dataSize, int periodMs, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo,
            EntryPoint = "FRC_NetworkCommunication_CANSessionMux_receiveMessage")]
        public static extern void FRC_NetworkCommunication_CANSessionMux_receiveMessage(ref uint messageID,
            uint messageIDMask, byte[] data, ref byte dataSize, ref uint timeStamp, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo,
            EntryPoint = "FRC_NetworkCommunication_CANSessionMux_openStreamSession")]
        public static extern void FRC_NetworkCommunication_CANSessionMux_openStreamSession(ref uint sessionHandle,
            uint messageID, uint messageIDMast, uint maxMessages, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo,
            EntryPoint = "FRC_NetworkCommunication_CANSessionMux_closeStreamSession")]
        public static extern void FRC_NetworkCommunication_CANSessionMux_closeStreamSession(uint sessionHandle);

        [DllImport(HAL.LibhalathenaSharedSo,
            EntryPoint = "FRC_NetworkCommunication_CANSessionMux_readStreamSession")]
        public static extern void FRC_NetworkCommunication_CANSessionMux_readStreamSession(uint sessionHandle,
            CANStreamMessage messages, uint messagesToRead, uint[] messagesRead, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo,
            EntryPoint = "FRC_NetworkCommunication_CANSessionMux_getCANStatus")]
        public static extern void FRC_NetworkCommunication_CANSessionMux_getCANStatus(ref float perfectButUtilization,
            ref uint busOffCount, ref uint txFullCount, ref uint recieveErrorCount, ref uint transmitErrorCount,
            ref int status);


    }
}
