// ReSharper disable CheckNamespace

namespace HAL_Base
{
    public partial class HALCAN
    {
        static HALCAN()
        {
            HAL.Initialize();
        }

        public delegate void FRC_NetworkCommunication_CANSessionMux_sendMessageDelegate(uint messageID, byte[] data,
            byte dataSize, int periodMs, ref int status);

        public static FRC_NetworkCommunication_CANSessionMux_sendMessageDelegate
            FRC_NetworkCommunication_CANSessionMux_sendMessage;

        public delegate void FRC_NetworkCommunication_CANSessionMux_receiveMessageDelegate(ref uint messageID,
            uint messageIDMask, byte[] data, ref byte dataSize, ref uint timeStamp, ref int status);

        public static FRC_NetworkCommunication_CANSessionMux_receiveMessageDelegate
            FRC_NetworkCommunication_CANSessionMux_receiveMessage;

        public delegate void FRC_NetworkCommunication_CANSessionMux_openStreamSessionDelegate(ref uint sessionHandle,
            uint messageID, uint messageIDMast, uint maxMessages, ref int status);

        public static FRC_NetworkCommunication_CANSessionMux_openStreamSessionDelegate
            FRC_NetworkCommunication_CANSessionMux_openStreamSession;

        public delegate void FRC_NetworkCommunication_CANSessionMux_closeStreamSessionDelegate(uint sessionHandle);

        public static FRC_NetworkCommunication_CANSessionMux_closeStreamSessionDelegate
            FRC_NetworkCommunication_CANSessionMux_closeStreamSession;

        public delegate void FRC_NetworkCommunication_CANSessionMux_readStreamSessionDelegate(uint sessionHandle,
            CANStreamMessage messages, uint messagesToRead, uint[] messagesRead, ref int status);

        public static FRC_NetworkCommunication_CANSessionMux_readStreamSessionDelegate
            FRC_NetworkCommunication_CANSessionMux_readStreamSession;
    }
}
