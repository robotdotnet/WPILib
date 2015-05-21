using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPILib;
using WPILib.Interfaces;

namespace WPILib_Extras
{
    public class XboxController : Joystick
    {
        public bool A
        {
            get { return GetRawButton(1); }
        }

        public bool B
        {
            get { return GetRawButton(2); }
        }

        public bool X
        {
            get { return GetRawButton(3); }
        }

        public bool Y
        {
            get { return GetRawButton(4); }
        }

        public bool LeftStickButton
        {
            get { return GetRawButton(9); }
        }

        public bool RightStickButton
        {
            get { return GetRawButton(10); }
        }

        public bool LeftBumper
        {
            get { return GetRawButton(5); }
        }

        public bool RightBumper
        {
            get { return GetRawButton(6); }
        }

        public bool Back
        {
            get { return GetRawButton(7); }
        }

        public bool Start
        {
            get { return GetRawButton(8); }
        }

        public double LeftXAxis
        {
            get { return GetRawAxis(0); }
        }

        public double LeftYAxis
        {
            get { return GetRawAxis(1); }
        }

        public double RightXAxis
        {
            get { return GetRawAxis(4); }
        }

        public double RightYAxis
        {
            get { return GetRawAxis(5); }
        }

        public double LeftTrigger
        {
            get { return GetRawAxis(2); }
        }

        public double RightTrigger
        {
            get { return GetRawAxis(3); }
        }

        public float LeftRumble
        {
            set { SetRumble(RumbleType.LeftRumble, value); }
        }

        public float RightRumble
        {
            set { SetRumble(RumbleType.RightRumble, value); }
        }

        public XboxController(int port) : base(port)
        {
            
        }

    }
}
