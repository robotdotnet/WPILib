using System;

namespace WPILib.Extras.AttributedCommandModel
{
    /// <summary>
    /// The different phases of a match.
    /// </summary>
    public enum MatchPhase
    {
        /// <summary>
        /// Starts in Autonomous.
        /// </summary>
        Autonomous,
        /// <summary>
        /// Starts in Teleoperated.
        /// </summary>
        Teleoperated,
        /// <summary>
        /// Starts in Disabled.
        /// </summary>
        Disabled,
        /// <summary>
        /// Starts in Test.
        /// </summary>
        Test
    }

    /// <summary>
    /// Apply this attribute to a command to have it started when the respective match phase starts.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class RunCommandAtPhaseStartAttribute : RunCommandAttribute
    {
        /// <summary>
        /// Gets the <see cref="MatchPhase"/> to start the command.
        /// </summary>
        public MatchPhase Phase { get; }

        /// <summary>
        /// Apply this attribute to a command to have it started when the respective match phase starts.
        /// </summary>
        /// <param name="phase">The match phase to start the command.</param>
        public RunCommandAtPhaseStartAttribute(MatchPhase phase)
        {
            Phase = phase;
        }
    }
}
