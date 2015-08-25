using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIVision
{
    public class VisionException : Exception
    {
        public VisionException(string err) : base(err)
        {
        }
    }
}
