using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPILib.CAN
{
    public class CANMessageNotFoundException : SystemException
    {
        public CANMessageNotFoundException()
            : base()
        {

        }
    }

    public class CANMessageNotAllowedException : SystemException
    {
        public CANMessageNotAllowedException(string msg)
            : base(msg)
        {
        }
    }

    public class CANInvalidBufferException : SystemException
    {
        public CANInvalidBufferException()
            : base()
        {
        }
    }

    public class CANNotInitializedException  : SystemException
    {
        public CANNotInitializedException()
            : base()
        {
        }
    }
}
