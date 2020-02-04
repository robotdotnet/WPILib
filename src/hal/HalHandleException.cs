using System;
using System.Collections.Generic;
using System.Text;

namespace Hal
{
    public class HalHandleException : UncleanStatusException
    {
        public HalHandleException(int statusCode) : base(statusCode)
        {

        }
    }
}
