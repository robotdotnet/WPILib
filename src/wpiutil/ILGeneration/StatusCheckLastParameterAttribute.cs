using System;
using System.Collections.Generic;
using System.Text;

namespace WPIUtil.ILGeneration
{
    [AttributeUsage(AttributeTargets.Method)]
    public class StatusCheckLastParameterAttribute : Attribute
    {
    }
}
