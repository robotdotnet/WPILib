using System;
using System.Collections.Generic;
using System.Text;

namespace WPIUtil.ILGeneration
{
    [AttributeUsage(AttributeTargets.Field)]
    public class NativeFunctionPointerAttribute : Attribute
    {
        public string FunctionName { get; }

        public NativeFunctionPointerAttribute(string functionName)
        {
            FunctionName = functionName;
        }
    }
}
