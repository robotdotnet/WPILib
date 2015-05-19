using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace HAL_Base
{
    public partial class HALSemaphore
    {
        public const uint SEMAPHORE_Q_FIFO = 0x01;
        public const uint SEMAPHORE_Q_PRIORITY = 0x01;
        public const uint SEMAPHORE_DELETE_SAFE = 0x04;
        public const uint SEMAPHORE_INVERSION_SAFE = 0x08;

        public const int SEMAPHORE_NO_WAIT = 0;
        public const int SEMAPHORE_WAIT_FOREVER = -1;

        public const uint SEMAPHORE_EMPTY = 0;
        public const uint SEMAPHORE_FULL = 1;
    }
}
