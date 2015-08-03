using WPILib.Commands;

namespace WPILib.Extras.AttributedCommandModel
{
    public abstract class AttributedSubsystem : Subsystem, IAttributedSubsystem
    {
        public AttributedSubsystem()
            :base()
        {
        }

        public AttributedSubsystem(string name)
            :base(name)
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
