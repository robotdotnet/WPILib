using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPILib.Extras.AttributedCommandModel
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class ExportSubsystemAttribute : Attribute
    {
        public Type DefaultCommandType { get; set; }
        
    }
}
