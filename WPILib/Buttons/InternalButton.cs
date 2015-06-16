

namespace WPILib.Buttons
{
    /// <summary>
    /// This class is intended to be used within a program. The programmer can manually set its value.
    /// Also include a setting for whether or not it should invert its value.
    /// </summary>
    public class InternalButton : Button
    {
        private bool m_pressed;
        private bool m_inverted;

        /// <summary>
        /// Creates an InternalButton that is not inverted
        /// </summary>
        public InternalButton()
            : this(false)
        {

        }

        /// <summary>
        /// Creates an InternalButton which is inverted depending on the input.
        /// </summary>
        /// <param name="inverted">If false, then this button is pressed when set to true, otherwise it is pressed when set to false.</param>
        public InternalButton(bool inverted)
        {
            m_pressed = m_inverted = inverted;
        }

        /// <summary>
        /// Sets whether the button is inverted.
        /// </summary>
        /// <param name="inverted">True if inverted.</param>
        public void SetInverted(bool inverted)
        {
            m_inverted = inverted;
        }

        /// <summary>
        /// Sets whether the button is pressed
        /// </summary>
        /// <param name="pressed">True if pressed</param>
        public void SetPressed(bool pressed)
        {
            m_pressed = pressed;
        }

        /// <summary>
        /// Returns whether or not the trigger is active
        /// </summary><remarks>
        /// This method will be called repeatedly a command is linked to the Trigger.
        /// </remarks>
        /// <returns>Whether or not the trigger condition is active.</returns>
        public override bool Get()
        {
            return m_pressed ^ m_inverted;
        }
    }
}
