using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPILib.Extras.AttributedCommandModel
{
    /// <summary>
    /// Apply this attribute to a subsystem parameter in a <see cref="WPILib.Commands.Command"/> constructor to disambiguate between multiple-exported subsystems of the same type for the <see cref="AttributedRobot"/> auto-loading.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true, Inherited = false)]
    public sealed class ImportSubsystemAttribute : Attribute
    {
        public string Name { get; }
        /// <summary>
        /// Apply this attribute to a subsystem parameter in a <see cref="WPILib.Commands.Command"/> constructor to disambiguate between multiple-exported subsystems of the same type for the <see cref="AttributedRobot"/> auto-loading.
        /// </summary>
        /// <param name="name">The name of the subsystem as specified in its <see cref="ExportSubsystemAttribute.Name"/> value.</param>
        public ImportSubsystemAttribute(string name)
        {
            Name = name;
        }
    }
}
