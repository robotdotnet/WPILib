using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPILib.Commands;

namespace WPILib.Buttons
{

    public abstract class ButtonScheduler
    {
        protected bool m_pressedLast;
        protected Trigger m_button;
        protected Command m_command;

        protected ButtonScheduler(bool last, Trigger button, Command orders)
        {
            m_pressedLast = last;
            m_button = button;
            m_command = orders;
        }

        public abstract void Execute();

        public void Start()
        {
            Scheduler.GetInstance().AddButton(this);
        }
    }

    public class PressedButtonScheduler : ButtonScheduler
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

    public class ReleasedButtonScheduler : ButtonScheduler
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

    public class HeldButtonScheduler : ButtonScheduler
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
            //throw new NotImplementedException();
        }
    }

    public class CancelButtonScheduler : ButtonScheduler
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
            //throw new NotImplementedException();
        }
    }

    public class ToggleButtonScheduler : ButtonScheduler
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
