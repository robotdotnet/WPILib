using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace WPIUtil.ILGeneration
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SkipOnRoboRIOAttribute : Attribute
    {
    }
}
