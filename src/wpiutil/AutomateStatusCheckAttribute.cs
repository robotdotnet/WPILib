using System;

namespace WPIUtil;

[AttributeUsage(AttributeTargets.Method)]
public sealed class AutomateStatusCheckAttribute : Attribute
{
    public string StatusCheckMethod { get; init; } = "";
}
