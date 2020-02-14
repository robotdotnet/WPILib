using System;

namespace WPIUtil
{
#pragma warning disable CA1032 // Implement standard exception constructors
    public class StatusCheckMethodNotFoundException : Exception
#pragma warning restore CA1032 // Implement standard exception constructors
    {
        public Type SearchType { get; }
        public string MethodName { get; }

        public StatusCheckMethodNotFoundException(string message, Type searchType, string methodName)
#pragma warning disable CA1062 // Validate arguments of public methods
            : base($"Status Check Method Not Found: {message}. Searched in type {searchType.FullName} for method {methodName}")
#pragma warning restore CA1062 // Validate arguments of public methods
        {
            SearchType = searchType;
            MethodName = methodName;
        }
    }
}
