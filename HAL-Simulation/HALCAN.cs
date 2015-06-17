using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using HAL_Base;

namespace HAL_Simulator
{
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
