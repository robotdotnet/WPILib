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
    public class RunCommandAtPhaseStartAttribute : Attribute
    {
        public MatchPhase Phase { get; }

        RunCommandAtPhaseStartAttribute(MatchPhase phase)
        {
            Phase = phase;
        }
    }
}
