namespace Epilogue;

public enum LogStrategy
{
    OptIn,
    OptOut,
}

public static class LogStrategyExtensions
{
    public const LogStrategy DefaultLogStrategy = LogStrategy.OptOut;
}
