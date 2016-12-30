using System.Runtime.InteropServices;
using HAL.NativeLoader;

namespace HAL.Base
{
    public partial class HALCAN
    {
        public static void Ping() { }

        static HALCAN()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALCAN>(LibraryLoaderHolder.NativeLoader);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void FRC_NetworkCommunication_CANSessionMux_sendMessageDelegate(uint messageID, ref ulong data,
            byte dataSize, int periodMs, ref int status);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void FRC_NetworkCommunication_CANSessionMux_sendMessage2Delegate(uint messageID, byte[] data,
            byte dataSize, int periodMs, ref int status);

        [NativeDelegate("FRC_NetworkCommunication_CANSessionMux_sendMessage")]
        public static FRC_NetworkCommunication_CANSessionMux_sendMessage2Delegate
            FRC_NetworkCommunication_CANSessionMux_sendMessage2;

        [NativeDelegate]
        public static FRC_NetworkCommunication_CANSessionMux_sendMessageDelegate
            FRC_NetworkCommunication_CANSessionMux_sendMessage;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void FRC_NetworkCommunication_CANSessionMux_receiveMessageDelegate(ref uint messageID,
            uint messageIDMask, ref ulong data, ref byte dataSize, ref uint timeStamp, ref int status);

        [NativeDelegate]
        public static FRC_NetworkCommunication_CANSessionMux_receiveMessageDelegate
            FRC_NetworkCommunication_CANSessionMux_receiveMessage;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void FRC_NetworkCommunication_CANSessionMux_receiveMessage2Delegate(ref uint messageID,
            uint messageIDMask, [Out] byte[] data, ref byte dataSize, ref uint timeStamp, ref int status);

        [NativeDelegate("FRC_NetworkCommunication_CANSessionMux_receiveMessage")]
        public static FRC_NetworkCommunication_CANSessionMux_receiveMessage2Delegate
            FRC_NetworkCommunication_CANSessionMux_receiveMessage2;

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
            [Out]CANStreamMessage[] messages, uint messagesToRead, ref uint messagesRead, ref int status);

        [NativeDelegate]
        public static FRC_NetworkCommunication_CANSessionMux_readStreamSessionDelegate
            FRC_NetworkCommunication_CANSessionMux_readStreamSession;
    }
}