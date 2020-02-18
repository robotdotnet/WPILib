using System;
using System.Collections.Generic;
using System.Text;

namespace WPILib.Oblog.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = true)]
    public class ConfigAttribute : Attribute
    {
        public string Name { get; set; } = "NO_NAME";
        public string TabName { get; set; } = "DEFAULT";

        public string MethodName { get; set; } = "DEFAULT";


    }
}
