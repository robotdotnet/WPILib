namespace CodeHelpers.Test.LogGenerator;

public static class LogResources
{
    private static string GetLogLevel()
    {
        using StreamReader reader = new StreamReader(typeof(LogResources).Assembly.GetManifestResourceStream("LogLevel.cs")!);
        return reader.ReadToEnd();
    }

    private static string GetLogType()
    {
        using StreamReader reader = new StreamReader(typeof(LogResources).Assembly.GetManifestResourceStream("LogType.cs")!);
        return reader.ReadToEnd();
    }

    private static string GetAttributes()
    {
        using StreamReader reader = new StreamReader(typeof(LogResources).Assembly.GetManifestResourceStream("Attributes.cs")!);
        return reader.ReadToEnd();
    }

    private static string GetExternalInit()
    {
        return  @"namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit {}
}
";
    }

    public static string LogType { get; } = GetLogType();
    public static string LogLevel { get; } = GetLogLevel();
    public static string Attributes { get; } = GetAttributes();
    public static string ExternalInit {get;} = GetExternalInit();
}
