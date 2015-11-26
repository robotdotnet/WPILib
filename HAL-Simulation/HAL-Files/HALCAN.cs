using System;
using HAL_Base;

// ReSharper disable RedundantAssignment
// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace
#pragma warning disable 1591

namespace HAL_Simulator
{
    ///<inheritdoc cref="HAL"/>
    internal class HALCAN
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_sendMessage = FRC_NetworkCommunication_CANSessionMux_sendMessage;
            HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_receiveMessage = FRC_NetworkCommunication_CANSessionMux_receiveMessage;
            HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_openStreamSession = FRC_NetworkCommunication_CANSessionMux_openStreamSession;
            HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_closeStreamSession = FRC_NetworkCommunication_CANSessionMux_closeStreamSession;
            HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_readStreamSession = FRC_NetworkCommunication_CANSessionMux_readStreamSession;
        }

        [CalledSimFunction]
        public static void FRC_NetworkCommunication_CANSessionMux_sendMessage(uint messageID, byte[] data,
            byte dataSize, int periodMs, ref int status)
        {
            throw new NotImplementedException();
        }

        [CalledSimFunction]
        public static void FRC_NetworkCommunication_CANSessionMux_receiveMessage(ref uint messageID,
            uint messageIDMask, byte[] data, ref byte dataSize, ref uint timeStamp, ref int status)
        {
            throw new NotImplementedException();
        }

        [CalledSimFunction]
        public static void FRC_NetworkCommunication_CANSessionMux_openStreamSession(ref uint sessionHandle,
            uint messageID, uint messageIDMast, uint maxMessages, ref int status)
        {
            throw new NotImplementedException();
        }

        [CalledSimFunction]
        public static void FRC_NetworkCommunication_CANSessionMux_closeStreamSession(uint sessionHandle)
        {
            throw new NotImplementedException();
        }

        [CalledSimFunction]
        public static void FRC_NetworkCommunication_CANSessionMux_readStreamSession(uint sessionHandle,
            CANStreamMessage messages, uint messagesToRead, uint[] messagesRead, ref int status)
        {
            throw new NotImplementedException();
        }
    }
}
