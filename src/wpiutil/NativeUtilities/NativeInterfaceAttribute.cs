using System;

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
