using System;

namespace WPIUtil.ILGeneration
{
    [AttributeUsage(AttributeTargets.Method)]
    public class StatusCheckRangeAttribute : Attribute
    {
        public int RangeParameterNumber { get; }
        public Type RangeCheckType { get; }
        public string RangeCheckFunctionName { get; }

        public StatusCheckRangeAttribute(int rangeParameterNumber, Type rangeCheckType, string rangeCheckFunctionName)
        {
            RangeParameterNumber = rangeParameterNumber;
            RangeCheckType = rangeCheckType;
            RangeCheckFunctionName = rangeCheckFunctionName;
        }
    }
}
