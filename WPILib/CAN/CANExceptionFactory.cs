using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPILib.Util;

namespace WPILib.CAN
{
    class CANExceptionFactory
    {
        public const int ERR_CANSessionMux_InvalidBuffer = -44086;
        public const int ERR_CANSessionMux_MessageNotFound = -44087;
        public const int ERR_CANSessionMux_NotAllowed = -44088;
        public const int ERR_CANSessionMux_NotInitialized = -44089;

        public const int kRioStatusOffset = -63000;

        public const int kRioStatusSuccess = 0;
        public const int kRIOStatusBufferInvalidSize = kRioStatusOffset - 80;
        public const int kRIOStatusOperationTimedOut = -52007;
        public const int kRIOStatusFeatureNotSupported = kRioStatusOffset - 193;
        public const int kRIOStatusResourceNotInitialized = -52010;

        public static void checkStatus(int status, int messageID)
        {
            switch (status)
            {
                case kRioStatusSuccess:
                    // Everything is ok... don't throw.
                    return;
                case ERR_CANSessionMux_InvalidBuffer:
                case kRIOStatusBufferInvalidSize:
                    throw new CANInvalidBufferException();
                case ERR_CANSessionMux_MessageNotFound:
                case kRIOStatusOperationTimedOut:
                    throw new CANMessageNotFoundException();
                case ERR_CANSessionMux_NotAllowed:
                case kRIOStatusFeatureNotSupported:
                    throw new CANMessageNotAllowedException("MessageID = " + messageID);
                case ERR_CANSessionMux_NotInitialized:
                case kRIOStatusResourceNotInitialized:
                    throw new CANNotInitializedException();
                default:
                    throw new UncleanStatusException("Fatal status code detected:  " + status);

            }
        }
    }
}
