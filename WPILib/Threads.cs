using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAL.Base;
using static WPILib.Utility;

namespace WPILib
{
    public class Threads
    {
        public static int GetCurrentThreadPriority()
        {
            bool rt = false;
            int status = 0;
            var ret = HALThreads.HAL_GetCurrentThreadPriority(ref rt, ref status);
            CheckStatus(status);
            return ret;
        }

        public static bool GetCurrentThreadIsRealTime()
        {
            bool rt = false;
            int status = 0;
            HALThreads.HAL_GetCurrentThreadPriority(ref rt, ref status);
            CheckStatus(status);
            return rt;
        }

        public static bool SetCurrentThreadPriority(bool realTime, int priority)
        {
            int status = 0;
            var ret = HALThreads.HAL_SetCurrentThreadPriority(realTime, priority, ref status);
            CheckStatus(status);
            return ret;
        }
    }
}
