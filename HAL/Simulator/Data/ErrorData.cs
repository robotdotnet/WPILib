using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL.Simulator.Data
{
    public struct ErrorData
    {
        public bool IsError { get; }
        public int ErrorCode { get; }
        public bool IsLVCode { get; }
        public string Details { get; }
        public string Location { get; }
        public string StackTrace { get; }
        
        public ErrorData(bool isError, int errorCode, bool isLVCode, string details,
            string location, string callStack)
        {
            IsError = isError;
            ErrorCode = errorCode;
            IsLVCode = isLVCode;
            Details = details;
            Location = location;
            StackTrace = callStack;
        } 
    }
}
