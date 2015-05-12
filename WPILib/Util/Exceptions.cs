using System;
using System.Collections.Generic;
using System.Text;

namespace WPILib.Util
{
    public class CheckedAllocationException : Exception
    {
        public CheckedAllocationException(string message)
            : base(message)
        {

        }
    }

    public class AllocationException : SystemException
    {
        public AllocationException(string message)
            : base(message)
        {

        }
    }
}
