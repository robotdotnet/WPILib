using System;

namespace WPIUtil
{
#pragma warning disable CA1032 // Implement standard exception constructors
    public class StatusCheckMethodNotDefinedException : Exception
#pragma warning restore CA1032 // Implement standard exception constructors
    {
        public Type InterfaceType { get; }
        public string MethodName { get; }

        public StatusCheckMethodNotDefinedException(Type interfaceType, string methodName)
#pragma warning disable CA1062 // Validate arguments of public methods
            : base($"Status Check Method was not defined for {methodName} in {interfaceType.Name}")
#pragma warning restore CA1062 // Validate arguments of public methods
        {
            InterfaceType = interfaceType;
            MethodName = methodName;
        }
    }
}
