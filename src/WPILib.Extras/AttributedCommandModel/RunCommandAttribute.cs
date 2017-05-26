using System;

namespace WPILib.Extras.AttributedCommandModel
{
    /// <summary>
    /// Apply subclasses of this attribute to commands to tie them to triggering conditions.
    /// </summary>
    /// <remarks>
    /// To set up required subsystems, commands can have a constructor that takes subclasses of <see cref="WPILib.Commands.Subsystem"/> that it requires.
    /// Note: this must not have any non-<see cref="WPILib.Commands.Subsystem"/>-subclass parameters. <br />
    /// If there does not exist a constructor that takes subsystems, the registration will call the default constructor, which must exist in this situtation.
    /// </remarks>
    public abstract class RunCommandAttribute : Attribute
    {
    }
}
