using System;

namespace WPILib.Extras
{
    /// <summary>
    /// Handle input from Xbox 360 or Xbox One controllers connected to the Driver Station.
    /// </summary><remarks>
    /// This class handles xbox input that comes from the Driver Station. Each time a value is requested
    /// the most recent value is returned. There is a single class instance for each joystick and the mapping
    /// of ports to hardware buttons depends on the code in the driver station.
    /// </remarks>
    public class XboxController : Joystick
    {
        /// <summary>
        /// Get A Button
        /// </summary>
        /// <returns>The A Button</returns>
        public bool GetA() => GetRawButton(1);

        /// <summary>
        /// Get B Button
        /// </summary>
        public bool GetB() => GetRawButton(2);

        /// <summary>
        /// Get X Button
        /// </summary>
        public new bool GetX() => GetRawButton(3);

        /// <summary>
        /// Get Y Button
        /// </summary>
        public new bool GetY() => GetRawButton(4);

        /// <summary>
        /// Get the X axis from one of the joysticks depending on the hand designated.
        /// </summary>
        /// <param name="hand">The side of the controller to use.</param>
        /// <returns>The value of the X axis from this hand.</returns>
        public override double GetX(Hand hand)
        {
            switch (hand)
            {
                case Hand.Left:
                    return GetLeftXAxis();
                case Hand.Right:
                    return GetRightXAxis();
                default:
                    throw new ArgumentException(nameof(hand));
            }
        }

        /// <summary>
        /// Get the Y axis from one of the joysticks depending on the hand designated.
        /// </summary>
        /// <param name="hand">The side of the controller to use.</param>
        /// <returns>The value of the Y axis from this hand.</returns>
        public override double GetY(Hand hand)
        {
            switch (hand)
            {
                case Hand.Left:
                    return GetLeftYAxis();
                case Hand.Right:
                    return GetRightYAxis();
                default:
                    throw new ArgumentException(nameof(hand));
            }
        }

        /// <summary>
        /// Get Left Stick Button 
        /// </summary>
        public bool GetLeftStickButton() => GetRawButton(9);

        /// <summary>
        /// Get Right Stick Button
        /// </summary>
        public bool GetRightStickButton() => GetRawButton(10);

        /// <summary>
        /// Get Left Bumper
        /// </summary>
        public bool GetLeftBumper() => GetRawButton(5);

        /// <summary>
        /// Get Right Bumper
        /// </summary>
        public bool GetRightBumper() => GetRawButton(6);

        /// <summary>
        /// Get Back Button
        /// </summary>
        public bool GetBack() => GetRawButton(7);

        /// <summary>
        /// Get Start Button
        /// </summary>
        public bool GetStart() => GetRawButton(8);

        /// <summary>
        /// Get Left X Axis
        /// </summary>
        public double GetLeftXAxis() => GetRawAxis(0);

        /// <summary>
        /// Get Left Y Axis
        /// </summary>
        public double GetLeftYAxis() => GetRawAxis(1);

        /// <summary>
        /// Get Right X Axis
        /// </summary>
        public double GetRightXAxis() => GetRawAxis(4);

        /// <summary>
        /// Get Right Y Axis
        /// </summary>
        public double GetRightYAxis() => GetRawAxis(5);

        /// <summary>
        /// Get Left Trigger
        /// </summary>
        public double GetLeftTrigger() => GetRawAxis(2);

        /// <summary>
        /// Get Right Trigger
        /// </summary>
        public double GetRightTrigger() => GetRawAxis(3);

        /// <summary>
        /// Set Left Rumble
        /// </summary>
        public double LeftRumble
        {
            set { SetRumble(RumbleType.LeftRumble, (float)value); }
        }

        /// <summary>
        /// Set Right Rumble
        /// </summary>
        public double RightRumble
        {
            set { SetRumble(RumbleType.RightRumble, (float)value); }
        }

        /// <summary>
        /// Initializes an instance of the <see cref="XboxController"/> class
        /// </summary>
        /// <param name="port">The port the controller is in.</param>
        public XboxController(int port)
            : base(port)
        {

        }

    }
}
