using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HAL_Base
{
    public partial class HALCAN
    {
        internal static void SetupDelegates()
        {
            string className = MethodBase.GetCurrentMethod().DeclaringType.Name;
            var types = HAL.HALAssembly.GetTypes();
            var q = from t in types where t.IsClass && t.Name == className select t;
            Type type = HAL.HALAssembly.GetType(q.ToList()[0].FullName);

            FRC_NetworkCommunication_CANSessionMux_sendMessage =
                (FRC_NetworkCommunication_CANSessionMux_sendMessageDelegate)
                    Delegate.CreateDelegate(typeof (FRC_NetworkCommunication_CANSessionMux_sendMessageDelegate),
                        type.GetMethod("FRC_NetworkCommunication_CANSessionMux_sendMessage"));

            FRC_NetworkCommunication_CANSessionMux_receiveMessage =
                (FRC_NetworkCommunication_CANSessionMux_receiveMessageDelegate)
                    Delegate.CreateDelegate(typeof(FRC_NetworkCommunication_CANSessionMux_receiveMessageDelegate),
                        type.GetMethod("FRC_NetworkCommunication_CANSessionMux_receiveMessage"));

            FRC_NetworkCommunication_CANSessionMux_openStreamSession =
                (FRC_NetworkCommunication_CANSessionMux_openStreamSessionDelegate)
                    Delegate.CreateDelegate(typeof(FRC_NetworkCommunication_CANSessionMux_openStreamSessionDelegate),
                        type.GetMethod("FRC_NetworkCommunication_CANSessionMux_openStreamSession"));

            FRC_NetworkCommunication_CANSessionMux_closeStreamSession =
                (FRC_NetworkCommunication_CANSessionMux_closeStreamSessionDelegate)
                    Delegate.CreateDelegate(typeof(FRC_NetworkCommunication_CANSessionMux_closeStreamSessionDelegate),
                        type.GetMethod("FRC_NetworkCommunication_CANSessionMux_closeStreamSession"));

            FRC_NetworkCommunication_CANSessionMux_readStreamSession =
                (FRC_NetworkCommunication_CANSessionMux_readStreamSessionDelegate)
                    Delegate.CreateDelegate(typeof(FRC_NetworkCommunication_CANSessionMux_readStreamSessionDelegate),
                        type.GetMethod("FRC_NetworkCommunication_CANSessionMux_readStreamSession"));

            FRC_NetworkCommunication_CANSessionMux_getCANStatus =
                (FRC_NetworkCommunication_CANSessionMux_getCANStatusDelegate)
                    Delegate.CreateDelegate(typeof(FRC_NetworkCommunication_CANSessionMux_getCANStatusDelegate),
                        type.GetMethod("FRC_NetworkCommunication_CANSessionMux_getCANStatus"));

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

        public delegate void FRC_NetworkCommunication_CANSessionMux_getCANStatusDelegate(ref float perfectButUtilization,
            ref uint busOffCount, ref uint txFullCount, ref uint recieveErrorCount, ref uint transmitErrorCount,
            ref int status);

        public static FRC_NetworkCommunication_CANSessionMux_getCANStatusDelegate
            FRC_NetworkCommunication_CANSessionMux_getCANStatus;

    }
}
