using System;

namespace WPILib.Commands
{
    public class IllegalUseOfCommandException : SystemException
    {
        public IllegalUseOfCommandException()
        {
            
        }

        public IllegalUseOfCommandException(string message) : base(message)
        {
            
        }
    }
}
