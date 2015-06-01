namespace WPILib
{
    /// <summary>
    /// Which hand hte Human Interface Device is associated with.
    /// </summary>
    public enum Hand
    {
        Left,
        Right,
    }

    /// <summary>
    /// GenericHID Interface
    /// </summary>
    public abstract class GenericHID
    {
        /// <summary>
        /// Get the x position of the HID
        /// </summary>
        /// <returns>The x position of the HID</returns>
        public double GetX()
        {
            return GetX(Hand.Right);
        }

        /// <summary>
        /// Get the x position of the HID
        /// </summary>
        /// <param name="hand">Which hand, left or right</param>
        /// <returns>The x position</returns>
        public abstract double GetX(Hand hand);

        /// <summary>
        /// Get the y position of the HID
        /// </summary>
        /// <returns>The y position of the HID</returns>
        public double GetY()
        {
            return GetY(Hand.Right);
        }

        /// <summary>
        /// Get the y position of the HID
        /// </summary>
        /// <param name="hand">Which hand, left or right</param>
        /// <returns>The y position</returns>
        public abstract double GetY(Hand hand);

        /// <summary>
        /// Get the z position of the HID
        /// </summary>
        /// <returns>The z position of the HID</returns>
        public double GetZ()
        {
            return GetZ(Hand.Right);
        }

        /// <summary>
        /// Get the z position of the HID
        /// </summary>
        /// <param name="hand">Which hand, left or right</param>
        /// <returns>The z position</returns>
        public abstract double GetZ(Hand hand);

        /// <summary>
        /// Get the twist value
        /// </summary>
        /// <returns>The twist value</returns>
        public abstract double GetTwist();

        /// <summary>
        /// Get the Throttle Value
        /// </summary>
        /// <returns>The Trottle Value</returns>
        public abstract double GetThrottle();

        /// <summary>
        /// Get the raw axis
        /// </summary>
        /// <param name="which">Index of the axis</param>
        /// <returns>The raw value of the selected axis</returns>
        public abstract double GetRawAxis(int which);

        /// <summary>
        /// Is the trigger pressed
        /// </summary>
        /// <returns>True if pressed</returns>
        public bool GetTrigger()
        {
            return GetTrigger(Hand.Right);
        }

        /// <summary>
        /// Is the trigger pressed
        /// </summary>
        /// <param name="hand">Which hand</param>
        /// <returns>True if the trigger for the given hand is pressed</returns>
        public abstract bool GetTrigger(Hand hand);

        /// <summary>
        /// Is the top button pressed
        /// </summary>
        /// <returns>True if pressed</returns>
        public bool GetTop()
        {
            return GetTop(Hand.Right);
        }

        /// <summary>
        /// Is the top button pressed
        /// </summary>
        /// <param name="hand">Which hand</param>
        /// <returns>True if the top button for the given hand is pressed</returns>
        public abstract bool GetTop(Hand hand);

        /// <summary>
        /// Is the bumper pressed
        /// </summary>
        /// <returns>True if pressed</returns>
        public bool GetBumper()
        {
            return GetBumper(Hand.Right);
        }

        /// <summary>
        /// Is the bumper pressed
        /// </summary>
        /// <param name="hand">Which hand</param>
        /// <returns>True if the bumper for the given hand is pressed</returns>
        public abstract bool GetBumper(Hand hand);

        /// <summary>
        /// Is the given button pressed
        /// </summary>
        /// <param name="button">Which button number</param>
        /// <returns>True if the button is pressed</returns>
        public abstract bool GetRawButton(int button);

        /// <summary>
        /// Is the given POV pressed
        /// </summary>
        /// <param name="pov">Which POV number</param>
        /// <returns>the POV value</returns>
        public abstract int GetPOV(int pov);

        /// <summary>
        /// Is POV 0 pressed
        /// </summary>
        /// <returns>the POV value</returns>
        public int GetPOV()
        {
            return GetPOV(0);
        }
    }
}
