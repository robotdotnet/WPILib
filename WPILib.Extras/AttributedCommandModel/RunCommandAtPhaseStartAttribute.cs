using System;

namespace WPILib.Extras.AttributedCommandModel
{
    public enum MatchPhase
    {
        Autonomous,
        Teleoperated,
        Disabled,
        Test
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class RunCommandAtPhaseStartAttribute : RunCommandAttribute
    {
        public MatchPhase Phase { get; }

        RunCommandAtPhaseStartAttribute(MatchPhase phase)
        {
            Phase = phase;
        }
    }
}
