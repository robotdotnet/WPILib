using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public MatchPhase Phase { get; set; }

        RunCommandAtPhaseStartAttribute(MatchPhase phase)
        {
            Phase = phase;
        }
    }
}
