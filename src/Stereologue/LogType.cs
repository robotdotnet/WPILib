namespace Stereologue;

[Flags]
public enum LogType
{
    None = 0,
    File = 1,
    Nt = 2,
    Once = 4,
}

public static class LogTypeExtensions
{
    public const LogType DefaultLogType = LogType.Nt | LogType.File;
}
