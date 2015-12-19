using WPILib.Commands;

namespace WPILib.Buttons
{
    /// <summary>
    /// An internal class of <see cref="Trigger"/>. The user should ignore this, it is
    /// only public to interface between packages
    /// </summary>
    public abstract class ButtonScheduler
    {
        /// <summary>
        /// True if the button was pressed last.
        /// </summary>
        protected bool m_pressedLast;
        /// <summary>
        /// The button trigger.
        /// </summary>
        protected readonly Trigger m_button;
        /// <summary>
        /// The button command.
        /// </summary>
        protected readonly Command m_command;

        /// <summary>
        /// Creates a new <see cref="ButtonScheduler"/>.
        /// </summary>
        /// <param name="last">True if the button was last pressed.</param>
        /// <param name="button">The button trigger.</param>
        /// <param name="orders">The button command.</param>
        protected ButtonScheduler(bool last, Trigger button, Command orders)
        {
            m_pressedLast = last;
            m_button = button;
            m_command = orders;
        }

        /// <summary>
        /// Executes the trigger.
        /// </summary>
        public abstract void Execute();

        /// <summary>
        /// Starts the button scheduler.
        /// </summary>
        public void Start()
        {
            Scheduler.Instance.AddButton(this);
        }
    }

    /// <summary>
    /// A derivation of <see cref="ButtonScheduler"/>. The user should ignore this.
    /// It shouldn't be viewable from user code anyway.
    /// </summary>
    internal class PressedButtonScheduler : ButtonScheduler
    {
        public PressedButtonScheduler(bool last, Trigger button, Command orders)
            : base(last, button, orders)
        {

        }

        public override void Execute()
        {
            if (m_button.Grab())
            {
                if (!m_pressedLast)
                {
                    m_pressedLast = true;
                    m_command.Start();
                }
            }
            else
            {
                m_pressedLast = false;
            }
        }
    }

    /// <summary>
    /// A derivation of <see cref="ButtonScheduler"/>. The user should ignore this.
    /// It shouldn't be viewable from user code anyway.
    /// </summary>
    internal class ReleasedButtonScheduler : ButtonScheduler
    {
        public ReleasedButtonScheduler(bool last, Trigger button, Command orders)
            : base(last, button, orders)
        {

        }

        public override void Execute()
        {
            if (m_button.Grab())
            {
                m_pressedLast = true;
            }
            else
            {
                if (m_pressedLast)
                {
                    m_pressedLast = false;
                    m_command.Start();
                }
            }
        }
    }

    /// <summary>
    /// A derivation of <see cref="ButtonScheduler"/>. The user should ignore this.
    /// It shouldn't be viewable from user code anyway.
    /// </summary>
    internal class HeldButtonScheduler : ButtonScheduler
    {
        public HeldButtonScheduler(bool last, Trigger button, Command orders)
            : base(last, button, orders)
        {

        }

        public override void Execute()
        {
            if (m_button.Grab())
            {
                m_pressedLast = true;
                m_command.Start();
            }
            else
            {
                if (m_pressedLast)
                {
                    m_pressedLast = false;
                    m_command.Cancel();
                }
            }
        }
    }

    /// <summary>
    /// A derivation of <see cref="ButtonScheduler"/>. The user should ignore this.
    /// It shouldn't be viewable from user code anyway.
    /// </summary>
    internal class CancelButtonScheduler : ButtonScheduler
    {
        public CancelButtonScheduler(bool last, Trigger button, Command orders)
            : base(last, button, orders)
        {

        }

        public override void Execute()
        {
            if (m_button.Grab())
            {
                if (!m_pressedLast)
                {
                    m_pressedLast = true;
                    m_command.Cancel();
                }
                else
                {
                    m_pressedLast = false;
                }
            }
        }
    }

    /// <summary>
    /// A derivation of <see cref="ButtonScheduler"/>. The user should ignore this.
    /// It shouldn't be viewable from user code anyway.
    /// </summary>
    internal class ToggleButtonScheduler : ButtonScheduler
    {
        public ToggleButtonScheduler(bool last, Trigger button, Command orders)
            : base(last, button, orders)
        {

        }

        public override void Execute()
        {
            if (m_button.Grab())
            {
                if (!m_pressedLast)
                {
                    m_pressedLast = true;
                    if (m_command.IsRunning())
                    {
                        m_command.Cancel();
                    }
                    else
                    {
                        m_command.Start();
                    }
                }
            }
            else
            {
                m_pressedLast = false;
            }
        }
    }

}
