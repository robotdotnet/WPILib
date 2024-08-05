namespace Epilogue;

[AttributeUsage(AttributeTargets.Class)]
public sealed class CustomLoggerForAttribute : Attribute
{
    Type[] Types { get; init; } = [];
}
