using System;
using System.Globalization;
using WPILib.SmartDashboardNS;

namespace WPILib
{
    public abstract class PWMSpeedController : PWM, ISpeedController, ISendable
    {
        protected PWMSpeedController(int channel) : base(channel)
        {

        }

        public override string Description => $"PWM {Channel.ToString(CultureInfo.InvariantCulture)}";

        public bool Inverted { get; set; }

        public void Set(double speed)
        {
            Speed = Inverted ? -speed : speed;
            Feed();
        }

        public double Get()
        {
            return Speed;
        }

        public void Disable()
        {
            SetDisabled();
        }

        public override void InitSendable(ISendableBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.SmartDashboardType = "Speed Controller";
            builder.IsActuator = true;
            builder.SafeState = SetDisabled;
            builder.AddDoubleProperty("Value", Get, Set);
        }
    }
}
