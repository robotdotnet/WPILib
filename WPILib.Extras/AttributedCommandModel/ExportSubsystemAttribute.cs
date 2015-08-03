using System;

namespace WPILib.Extras.AttributedCommandModel
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class ExportSubsystemAttribute : Attribute
    {
        public Type DefaultCommandType { get; set; }
        
    }
}
