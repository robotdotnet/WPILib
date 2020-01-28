using System;
using System.Collections.Generic;
using System.Text;

namespace Hal
{
    public class UncleanStatusException : Exception
    {
        public int StatusCode { get; set; }
        public UncleanStatusException(int statusCode) : base($"Code: {statusCode} Message: {HalBase.GetErrorMessage(statusCode)}")
        {
            StatusCode = statusCode;
        }
    }
}
