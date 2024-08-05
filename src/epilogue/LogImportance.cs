namespace Epilogue;

public enum LogImportance
{
    Debug,
    Info,
    Critical,
}

public static class LogImportanceExtensions
{
    public const LogImportance DefaultLogImportance = LogImportance.Debug;
}
