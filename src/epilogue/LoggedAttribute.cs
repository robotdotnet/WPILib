namespace WPILib.Logging;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Struct)]
public sealed class LoggedAttribute : Attribute
{
    public string Name { get; init; } = "";
    public LogStrategy Strategy { get; init; } = LogStrategy.OptOut;
    public LogImportance Importance { get; init; } = LogImportance.Debug;
}
