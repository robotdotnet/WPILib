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
        void Initialize();

        void Execute();

        void End(bool interrupted);

        void Schedule(bool interruptible = true);

        void Cancel();

        bool IsFinished { get; }

        bool IsScheduled { get; }

        HashSet<Subsystem> Requirements { get; }

        bool HasRequirement(Subsystem requirement);

        public bool RunsWhenDisabled { get; }

        string Name { get; }
    }
}
