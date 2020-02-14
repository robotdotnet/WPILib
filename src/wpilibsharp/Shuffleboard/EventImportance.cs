using System;

namespace WPILib.Shuffleboard
{
    public enum EventImportance
    {
        Trivial,
        Low,
        Normal,
        High,
        Critical
    }

    public static class EventImportanceExtensions
    {
        public static string GetName(this EventImportance importance)
        {
            return importance switch
            {
                EventImportance.Trivial => "TRIVIAL",
                EventImportance.Low => "LOW",
                EventImportance.Normal => "NORMAL",
                EventImportance.High => "HIGH",
                EventImportance.Critical => "CRITICAL",
                _ => "NORMAL"
            };
        }
    }
}
