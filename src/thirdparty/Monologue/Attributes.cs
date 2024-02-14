using System;

namespace Monologue;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
public sealed class LogAttribute : Attribute
{
    public string Key { get; set; } = "";
    public LogLevel LogLevel { get; set; } = LogLevel.Default;
    public LogType LogType { get; set; } = LogType.Nt;
    public bool Once { get; set; } = false;
}
