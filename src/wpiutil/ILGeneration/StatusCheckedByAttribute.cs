using System;

namespace WPIUtil.ILGeneration
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method)]
    public class StatusCheckedByAttribute : Attribute
    {
        public Type StatusCheckType { get; }
        public string StatusCheckFunctionName { get; }
        public StatusCheckedByAttribute(Type statusCheckType, string? statusCheckFunctionName = null)
        {
            StatusCheckType = statusCheckType;
            StatusCheckFunctionName = statusCheckFunctionName ?? "";
        }
    }
}
