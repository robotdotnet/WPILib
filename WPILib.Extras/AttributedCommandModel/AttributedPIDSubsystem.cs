using WPILib.Commands;

namespace WPILib.Extras.AttributedCommandModel
{
    public abstract class AttributedPIDSubsystem : PIDSubsystem, IAttributedSubsystem
    {
        public AttributedPIDSubsystem(string name, double p, double i, double d)
            : base(name, p, i, d)
        {
        }

        public AttributedPIDSubsystem(string name, double p, double i, double d, double f)
            : base(name, p, i, d, f)
        {
        }

        public AttributedPIDSubsystem(string name, double p, double i, double d, double f, double period)
            : base(name, p, i, d, f, period)
        {
        }

        public AttributedPIDSubsystem(double p, double i, double d)
            :base(p, i, d)
        {
        }

        public AttributedPIDSubsystem(double p, double i, double d, double period, double f)
            : base(p, i, d, f)
        {
        }

        public AttributedPIDSubsystem(double p, double i, double d, double period)
            : base(p, i, d, period)
        {
        }


        protected override void InitDefaultCommand()
        {
            //Purposefully do nothing.  The AttributedCommandModelRobot class will set the default command via InitDefaultCommand(Command).
        }

        void IAttributedSubsystem.InitDefaultCommand(Command cmd)
        {
            SetDefaultCommand(cmd);
        }
    }
}
