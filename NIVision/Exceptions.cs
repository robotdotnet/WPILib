using System;

namespace NIVision
{
    public class VisionException : Exception
    {
        public VisionException(string err) : base(err)
        {
        }
    }
}
