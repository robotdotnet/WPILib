using System;
using WPILib.Commands;

namespace WPILib.Extras
{
    /// <summary>
    /// A <see cref="ActionCommand"/> will run an <see cref="Action"/> when it is
    /// initialized and will finish immediately.
    /// </summary>
    public abstract class SubsystemCommand : Command
    {
        protected readonly Subsystem m_subsystem;

        /// <summary>
        /// Creates a new <see cref="SubsystemCommand"/> which will depend on the given <see cref="Subsystem"/>.
        /// </summary>
        /// <param name="action">The <see cref="Action"/> to run.</param>
        public SubsystemCommand(Subsystem subsystem)
        {
            Requires(subsystem);
            m_subsystem = subsystem;
        }

        /// <summary>
        /// Creates a new <see cref="SubsystemCommand"/> with a specific name, 
        /// which will depend on the given <see cref="Subsystem"/>.
        /// </summary>
        /// <param name="subsystem">The <see cref="Subsystem"/> to require.</param>
        /// <param name="name">The name for the command.</param>
        public SubsystemCommand(Subsystem subsystem, string name)
            : base(name)
        {
            Requires(subsystem);
            m_subsystem = subsystem;
        }
    }
}
