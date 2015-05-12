using System;
using System.Collections.Generic;
using System.Text;

namespace WPILib.Interfaces
{
    public enum Hand
    {
        kLeft,
        kRight,
    }
    public abstract class GenericHID
    {
        public double GetX()
        {
            return GetX(Hand.kRight);
        }

        public abstract double GetX(Hand hand);

        public double GetY()
        {
            return GetY(Hand.kRight);
        }
        public abstract double GetY(Hand hand);

        public double GetZ()
        {
            return GetZ(Hand.kRight);
        }

        public abstract double GetZ(Hand hand);

        public abstract double GetTwist();
        public abstract double GetThrottle();
        public abstract double GetRawAxis(int which);

        public bool GetTrigger()
        {
            return GetTrigger(Hand.kRight);
        }

        public abstract bool GetTrigger(Hand hand);

        /**
     * Is the top button pressed
     * @return true if the top button is pressed
     */
        public bool GetTop()
        {
            return GetTop(Hand.kRight);
        }

        /**
         * Is the top button pressed
         * @param hand which hand
         * @return true if hte top button for the given hand is pressed
         */
        public abstract bool GetTop(Hand hand);

        /**
         * Is the bumper pressed
         * @return true if the bumper is pressed
         */
        public bool GetBumper()
        {
            return GetBumper(Hand.kRight);
        }

        /**
         * Is the bumper pressed
         * @param hand which hand
         * @return true if hte bumper is pressed
         */
        public abstract bool GetBumper(Hand hand);

        /**
         * Is the given button pressed
         * @param button which button number
         * @return true if the button is pressed
         */
        public abstract bool GetRawButton(int button);

        public abstract int GetPOV(int pov);

        public int GetPOV()
        {
            return GetPOV(0);
        }
    }
}
