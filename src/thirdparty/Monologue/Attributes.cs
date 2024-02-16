using System;

namespace Monologue;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public sealed class LogAttribute : Attribute
{
    public string Key { get; init; } = "";
    public LogLevel LogLevel { get; init; } = LogLevel.Default;
    public LogType LogType { get; init; } = LogType.Nt | LogType.File;
    public bool UseProtobuf { get; init; } = false;
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
public sealed class GenerateLogAttribute : Attribute
{
}
