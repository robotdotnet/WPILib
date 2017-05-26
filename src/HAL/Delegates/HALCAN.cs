using System.Runtime.InteropServices;
using FRC.NativeLibraryUtilities;

namespace HAL.Base
{
    public partial class HALCAN
    {
        public static void Ping() { }

        static HALCAN()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALCAN>(LibraryLoaderHolder.NativeLoader);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void FRC_NetworkCommunication_CANSessionMux_sendMessageDelegate(uint messageID, byte[] data,
            byte dataSize, int periodMs, ref int status);

        [NativeDelegate]
        public static FRC_NetworkCommunication_CANSessionMux_sendMessageDelegate
            FRC_NetworkCommunication_CANSessionMux_sendMessage;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void FRC_NetworkCommunication_CANSessionMux_receiveMessageDelegate(ref uint messageID,
            uint messageIDMask, byte[] data, ref byte dataSize, ref uint timeStamp, ref int status);

        [NativeDelegate]
        public static FRC_NetworkCommunication_CANSessionMux_receiveMessageDelegate
            FRC_NetworkCommunication_CANSessionMux_receiveMessage;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void FRC_NetworkCommunication_CANSessionMux_openStreamSessionDelegate(ref uint sessionHandle,
            uint messageID, uint messageIDMast, uint maxMessages, ref int status);

        [NativeDelegate]
        public static FRC_NetworkCommunication_CANSessionMux_openStreamSessionDelegate
            FRC_NetworkCommunication_CANSessionMux_openStreamSession;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void FRC_NetworkCommunication_CANSessionMux_closeStreamSessionDelegate(uint sessionHandle);

        [NativeDelegate]
        public static FRC_NetworkCommunication_CANSessionMux_closeStreamSessionDelegate
            FRC_NetworkCommunication_CANSessionMux_closeStreamSession;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void FRC_NetworkCommunication_CANSessionMux_readStreamSessionDelegate(uint sessionHandle,
            CANStreamMessage messages, uint messagesToRead, uint[] messagesRead, ref int status);

        [NativeDelegate]
        public static FRC_NetworkCommunication_CANSessionMux_readStreamSessionDelegate
            FRC_NetworkCommunication_CANSessionMux_readStreamSession;
    }
}