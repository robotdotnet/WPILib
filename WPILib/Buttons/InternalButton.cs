using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPILib.Buttons
{
    public class InternalButton : Button
    {
        private bool m_pressed;
        private bool m_inverted;

        public InternalButton() : this(false)
        {
            
        }

        public InternalButton(bool inverted)
        {
            this.m_pressed = this.m_inverted = inverted;
        }

        public void SetInverted(bool inverted)
        {
            this.m_inverted = inverted;
        }

        public void SetPressed(bool pressed)
        {
            this.m_pressed = pressed;
        }

        public override bool Get()
        {
            return m_pressed ^ m_inverted;
        }
    }
}
