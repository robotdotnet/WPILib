using System;
using System.Collections.Generic;

namespace WPILib2.Commands
{
    public static class CommandExtensions
    {
        public static ParallelRaceGroup WithTimeout(this Command command, TimeSpan time)
        {
            return new ParallelRaceGroup(command, new WaitCommand(time));
        }
    }

    public interface Command
    {
        public virtual void Initialize()
        {

        }

        public virtual void Execute()
        {

        }

        public virtual void End(bool interrupted)
        {

        }

        public virtual void Schedule(bool interruptible)
        {

        }

        public virtual void Schedule()
        {
            Schedule(true);
        }

        public virtual void Cancel()
        {

        }

        public virtual bool IsFinished => false;

        public virtual bool IsScheduled => false;

        HashSet<Subsystem> Requirements { get; }

        public virtual bool HasRequirement(Subsystem requirement)
        {
            return Requirements.Contains(requirement);
        }

        public virtual bool RunsWhenDisabled => false;

        public virtual string Name => GetType().Name;
    }
}
