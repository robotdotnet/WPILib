using System;
using System.Collections.Generic;
using System.Text;

namespace WPIUtil
{
    public class StatusCheckMethodNotFoundException : Exception
    {
        public Type SearchType { get; }
        public string MethodName { get; }

        public StatusCheckMethodNotFoundException(string message, Type searchType, string methodName)
            : base($"Status Check Method Not Found: {message}. Searched in type {searchType.FullName} for method {methodName}")
        {
            SearchType = searchType;
            MethodName = methodName;
        }
    }
}
