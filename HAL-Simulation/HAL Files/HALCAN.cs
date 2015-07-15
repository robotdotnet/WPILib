using System;
using HAL_Base;

// ReSharper disable RedundantAssignment
// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace
#pragma warning disable 1591

namespace HAL_Simulator
{
    ///<inheritdoc cref="HAL"/>
    public class HALCAN
    {
        public static void FRC_NetworkCommunication_CANSessionMux_sendMessage(uint messageID, byte[] data,
            byte dataSize, int periodMs, ref int status)
        {
            throw new NotImplementedException();
        }

        public static void FRC_NetworkCommunication_CANSessionMux_receiveMessage(ref uint messageID,
            uint messageIDMask, byte[] data, ref byte dataSize, ref uint timeStamp, ref int status)
        {
            throw new NotImplementedException();
        }

        public static void FRC_NetworkCommunication_CANSessionMux_openStreamSession(ref uint sessionHandle,
            uint messageID, uint messageIDMast, uint maxMessages, ref int status)
        {
            throw new NotImplementedException();
        }

        public static void FRC_NetworkCommunication_CANSessionMux_closeStreamSession(uint sessionHandle) 
        {
            throw new NotImplementedException();
        }

        public static void FRC_NetworkCommunication_CANSessionMux_readStreamSession(uint sessionHandle,
            CANStreamMessage messages, uint messagesToRead, uint[] messagesRead, ref int status)
        {
            throw new NotImplementedException();
        }

        public static void FRC_NetworkCommunication_CANSessionMux_getCANStatus(ref float perfectButUtilization,
            ref uint busOffCount, ref uint txFullCount, ref uint recieveErrorCount, ref uint transmitErrorCount,
            ref int status)
        { throw new NotImplementedException(); }
    }
}
