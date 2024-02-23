namespace Stereologue;

public enum LogLevel
{
    NotFileOnly,
    Default,
    OverrideFileOnly
}

public static class LogLevelExtensions
{
    public const LogLevel DefaultLogLevel = LogLevel.Default;

    public static bool ShouldLog(this LogLevel logLevel, bool fileOnly, bool nt)
    {
        switch (logLevel)
        {
            case LogLevel.OverrideFileOnly:
                return true;
            case LogLevel.Default:
                if (!fileOnly && nt)
                {
                    return true;
                }
                else if (fileOnly && !nt)
                {
                    return true;
                }
                return false;
            case LogLevel.NotFileOnly:
                return !fileOnly;
            default:
                return false;
        }
    }
}
