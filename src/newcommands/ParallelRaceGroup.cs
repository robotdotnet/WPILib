namespace WPILib2.Commands
{
    public class ParallelRaceGroup : CommandGroupBase
    {
        public ParallelRaceGroup(params ICommand[] commands)
        {
            AddCommands(commands);
        }

        public sealed override void AddCommands(params ICommand[] commands)
        {

        }



        public override void Initialize()
        {

        }
    }
}
