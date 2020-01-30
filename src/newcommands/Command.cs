using System;
using System.Collections.Generic;

namespace WPILib2.Commands
{
    public static class CommandExtensions
    {
        public static ParallelRaceGroup WithTimeout(this ICommand command, TimeSpan time)
        {
            return new ParallelRaceGroup(command, new WaitCommand(time));
        }
    }

    public interface ICommand
    {
        void Initialize();

        void Execute();

        void End(bool interrupted);

        void Schedule(bool interruptible = true);

        void Cancel();

        bool IsFinished { get; }

        bool IsScheduled { get; }

        HashSet<ISubsystem> Requirements { get; }

        bool HasRequirement(ISubsystem requirement);

        public bool RunsWhenDisabled { get; }

        string Name { get; }
    }
}
