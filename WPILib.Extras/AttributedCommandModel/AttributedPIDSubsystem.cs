using WPILib.Commands;

namespace WPILib.Extras.AttributedCommandModel
{
    public abstract class AttributedPIDSubsystem : PIDSubsystem, IAttributedSubsystem
    {
        protected AttributedPIDSubsystem(string name, double p, double i, double d)
            : base(name, p, i, d)
        {
        }

        protected AttributedPIDSubsystem(string name, double p, double i, double d, double f)
            : base(name, p, i, d, f)
        {
        }

        protected AttributedPIDSubsystem(string name, double p, double i, double d, double f, double period)
            : base(name, p, i, d, f, period)
        {
        }

        protected AttributedPIDSubsystem(double p, double i, double d)
            :base(p, i, d)
        {
        }

        protected AttributedPIDSubsystem(double p, double i, double d, double period, double f)
            : base(p, i, d, period, f)
        {
        }

        protected AttributedPIDSubsystem(double p, double i, double d, double period)
            : base(p, i, d, period)
        {
        }


        protected override void InitDefaultCommand()
        {
            //Purposefully do nothing.  The AttributedCommandModelRobot class will set the default command via InitDefaultCommand(Command).
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CC0068:Unused Method", Justification = "Used in initialization code in the AttributedRobot class.")]
        void IAttributedSubsystem.InitDefaultCommand(Command cmd)
        {
            SetDefaultCommand(cmd);
        }
    }
}
