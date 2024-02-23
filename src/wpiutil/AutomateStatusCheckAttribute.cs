namespace WPIUtil;

[AttributeUsage(AttributeTargets.Method)]
public sealed class AutomateStatusCheckAttribute : Attribute
{
    public required string StatusCheckMethod { get; init; }
}
