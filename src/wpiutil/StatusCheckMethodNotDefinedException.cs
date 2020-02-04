using System;
using System.Collections.Generic;
using System.Text;

namespace WPIUtil
{
    public class StatusCheckMethodNotDefinedException : Exception
    {
        public Type InterfaceType { get; }
        public string MethodName { get; }

        public StatusCheckMethodNotDefinedException(Type interfaceType, string methodName)
            : base($"Status Check Method was not defined for {methodName} in {interfaceType.Name}")
        {
            InterfaceType = interfaceType;
            MethodName = methodName;
        }
    }
}
