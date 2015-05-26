using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPILib;

namespace WPILib.Buttons
{
    class JoystickButton : Button
    {
        private GenericHID m_joystick;
        private int m_buttonNumber;

        public JoystickButton(GenericHID joystick, int buttonNumber)
        {
            m_joystick = joystick;
            m_buttonNumber = buttonNumber;
        }

        public override bool Get()
        {
            return m_joystick.GetRawButton(m_buttonNumber);
        }
    }
}
