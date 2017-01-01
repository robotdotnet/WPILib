using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPILib.Exceptions
{
    /// <summary>
    /// The exception that occurs when a HAL Handle exception occurs
    /// </summary>
    public class HalHandleException : SystemException
    {
        /// <summary>
        /// Creates a new HalHandleException
        /// </summary>
        /// <param name="msg">The exception message</param>
        public HalHandleException(string msg) : base(msg)
        {
            
        }
    }
}
