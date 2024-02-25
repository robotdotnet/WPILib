namespace Stereologue;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public sealed class LogAttribute : Attribute
{
    public string Key { get; init; } = "";
    public LogLevel LogLevel { get; init; } = LogLevelExtensions.DefaultLogLevel;
    public LogType LogType { get; init; } = LogTypeExtensions.DefaultLogType;
    public bool UseProtobuf { get; init; }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class GenerateLogAttribute : Attribute
{
}
