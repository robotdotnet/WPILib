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
        protected bool PressedLast;
        /// <summary>
        /// The button trigger.
        /// </summary>
        protected readonly Trigger Button;
        /// <summary>
        /// The button command.
        /// </summary>
        protected readonly Command Command;

        /// <summary>
        /// Creates a new <see cref="ButtonScheduler"/>.
        /// </summary>
        /// <param name="last">True if the button was last pressed.</param>
        /// <param name="button">The button trigger.</param>
        /// <param name="orders">The button command.</param>
        protected ButtonScheduler(bool last, Trigger button, Command orders)
        {
            PressedLast = last;
            Button = button;
            Command = orders;
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
            if (Button.Grab())
            {
                if (!PressedLast)
                {
                    PressedLast = true;
                    Command.Start();
                }
            }
            else
            {
                PressedLast = false;
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
            if (Button.Grab())
            {
                PressedLast = true;
            }
            else
            {
                if (PressedLast)
                {
                    PressedLast = false;
                    Command.Start();
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
            if (Button.Grab())
            {
                PressedLast = true;
                Command.Start();
            }
            else
            {
                if (PressedLast)
                {
                    PressedLast = false;
                    Command.Cancel();
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
            if (Button.Grab())
            {
                if (!PressedLast)
                {
                    PressedLast = true;
                    Command.Cancel();
                }
                else
                {
                    PressedLast = false;
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
            if (Button.Grab())
            {
                if (!PressedLast)
                {
                    PressedLast = true;
                    if (Command.IsRunning())
                    {
                        Command.Cancel();
                    }
                    else
                    {
                        Command.Start();
                    }
                }
            }
            else
            {
                PressedLast = false;
            }
        }
    }

}
