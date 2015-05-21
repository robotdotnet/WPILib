﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
