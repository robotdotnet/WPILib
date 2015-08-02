using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPILib.Extras.AttributedCommandModel
{
    public enum ButtonMethod
    {
        WhenPressed,
        WhenReleased,
        WhileHeld,
        ToggleWhenPressed,
        CancelWhenPressed
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class RunCommandOnJoystickAttribute : Attribute
    {
        public int ControllerId { get; set; }
        public int ButtonId { get; set; }
        public ButtonMethod ButtonMethod { get; set; }

        public RunCommandOnJoystickAttribute(int controllerId, int buttonId, ButtonMethod buttonMethod)
        {
            ControllerId = controllerId;
            ButtonId = buttonId;
            ButtonMethod = buttonMethod;
        }
    }
}
