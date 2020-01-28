using System;
using System.Collections.Generic;
using System.Text;

namespace WPIUtil.NativeUtilities
{
    [AttributeUsage(AttributeTargets.Class)]
    public class NativeInterfaceAttribute : Attribute
    {
        public Type InterfaceType { get; }
        public NativeInterfaceAttribute(Type interfaceType)
        {
            InterfaceType = interfaceType;
        }
    }

}
