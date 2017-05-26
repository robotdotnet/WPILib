namespace WPILib.Commands
{
    /// <summary>
    /// This command will only finish if whatever <see cref="CommandGroup"/>
    /// it is in has no active children.
    /// </summary>
    /// <remarks>If it is not part of a <see cref="CommandGroup"/>, then it will finish immediately.
    /// If it is itself an active child, then the <see cref="CommandGroup"/> will never end.
    /// <para/>This class is useful for the situation where you want to allow anything running in parallel to finish, 
    /// before continuing in the main <see cref="CommandGroup"/> sequence.</remarks>
    public class WaitForChildren : Command
    {
        ///<inheritdoc/>
        protected override void Initialize()
        {
            
        }

        ///<inheritdoc/>
        protected override void Execute()
        {
            
        }

        ///<inheritdoc/>
        protected override bool IsFinished() => GetGroup() == null || GetGroup().Children.Count == 0;

        ///<inheritdoc/>
        protected override void End()
        {
        }

        ///<inheritdoc/>
        protected override void Interrupted()
        {
        }
    }
}
