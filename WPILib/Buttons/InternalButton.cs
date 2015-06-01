

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
