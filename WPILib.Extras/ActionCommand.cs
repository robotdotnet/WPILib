using System;
using WPILib.Commands;

namespace WPILib.Extras
{
    /// <summary>
    /// A <see cref="ActionCommand"/> will run an <see cref="Action"/> when it is
    /// initialized and will finish immediately.
    /// </summary>
    public class ActionCommand : Command
    {
        private readonly Action m_action;

        /// <summary>
        /// Creates a new <see cref="ActionCommand"/> which will start the given <see cref="Action"/>
        /// when its <see cref="Initialize"/> is called.
        /// </summary>
        /// <param name="action">The <see cref="Action"/> to run.</param>
        public ActionCommand(Action action)
        {
            m_action = action;
        }

        /// <summary>
        /// Creates a new <see cref="ActionCommand"/> with a specific name, 
        /// which will start the given <see cref="Action"/> when its <see cref="Initialize"/> is called.
        /// </summary>
        /// <param name="action">The <see cref="Action"/> to run.</param>
        /// <param name="name">The name for the command.</param>
        public ActionCommand(Action action, string name)
            :base(name)
        {
            m_action = action;
        }

        ///<inheritdoc/>
        protected override void End()
        {
        }

        ///<inheritdoc/>
        protected override void Execute()
        {
        }

        ///<inheritdoc/>
        protected override void Initialize()
        {
            m_action?.Invoke();
        }

        ///<inheritdoc/>
        protected override void Interrupted()
        {
        }

        ///<inheritdoc/>
        protected override bool IsFinished()
        {
            return true;
        }
    }
}
