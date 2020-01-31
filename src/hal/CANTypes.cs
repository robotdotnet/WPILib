using System;
using System.Collections.Generic;
using System.Text;

namespace Hal
{
    public unsafe struct CANStreamMessage
    {
        public uint MessageId;
        public uint TimeStamp;
        public fixed byte Data[8];
        public byte DataSize;
    }
}
