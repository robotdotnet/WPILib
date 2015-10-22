using System;

namespace WPILib.Extras.AttributedCommandModel
{
    /// <summary>
    /// Apply this attribute to subsystems to have <see cref="AttributedRobot"/> auto-load it.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class ExportSubsystemAttribute : Attribute
    {
        /// <summary>
        /// The type of the default command.  The command must have a constructor that takes one <see cref="WPILib.Commands.Subsystem"/> object.
        /// </summary>
        public Type DefaultCommandType { get; set; }

        /// <summary>
        /// The name of the subsystem object.  This is used to disambiguate different subsystem objects of the same type for the <see cref="RunCommandAttribute"/> command autoloading.
        /// </summary>
        public string Name { get; set; }
    }
}
