using System;

namespace WPILib.Extras.AttributedCommandModel
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class RunCommandOnJoystickAttribute : RunCommandAttribute
    {
        public int ControllerId { get; }
        public int ButtonId { get; }
        public ButtonMethod ButtonMethod { get; }

        public RunCommandOnJoystickAttribute(int controllerId, int buttonId, ButtonMethod buttonMethod)
        {
            ControllerId = controllerId;
            ButtonId = buttonId;
            ButtonMethod = buttonMethod;
        }
    }
}
