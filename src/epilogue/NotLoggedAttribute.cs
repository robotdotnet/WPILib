namespace WPILib.Logging;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method)]
public sealed class NotLoggedAttribute : Attribute
{
}
