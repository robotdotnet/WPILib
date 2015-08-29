using WPILib.Commands;

namespace WPILib.Extras.AttributedCommandModel
{
    public abstract class AttributedSubsystem : Subsystem, IAttributedSubsystem
    {
        protected AttributedSubsystem()
            :base()
        {
        }

        protected AttributedSubsystem(string name)
            :base(name)
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
