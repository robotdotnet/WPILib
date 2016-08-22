using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPILib.Exceptions
{
    public class HalHandleException : SystemException
    {
        public HalHandleException(string msg) : base(msg)
        {
            
        }
    }
}
