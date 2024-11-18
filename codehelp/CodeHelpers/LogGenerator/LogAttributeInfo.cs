using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Stereologue;

namespace WPILib.CodeHelpers.LogGenerator;

// Contains all information about a [Log] attribute
public record LogAttributeInfo(string? Path, LogLevel LogLevel, LogType LogType, bool UseProtobuf)
{
    public string GetLogLevelString(LanguageKind language)
    {
        if (language == LanguageKind.CSharp)
        {
            return AllLevelValues[(int)LogLevel];
        }
        else if (language == LanguageKind.VisualBasic)
        {
            return AllLevelValuesVb[(int)LogLevel];
        }
        return "";
    }

    public string GetLogTypeString(LanguageKind language)
    {
        if (language == LanguageKind.CSharp)
        {
            return AllTypeValues[(int)LogType];
        }
        else if (language == LanguageKind.VisualBasic)
        {
            return AllTypeValuesVb[(int)LogType];
        }
        return "";
    }

    private static ImmutableList<string> GetAllLevelValues()
    {
        LogLevel[] allLogLevels = (LogLevel[])Enum.GetValues(typeof(LogLevel));
        var builder = ImmutableList.CreateBuilder<string>();
        string fullName = typeof(LogLevel).FullName;
        string rootName = $"global::{fullName}.";
        foreach (var i in allLogLevels)
        {
            builder.Add($"{rootName}{i}");
        }
        return builder.ToImmutable();
    }

    private static ImmutableList<string> GetAllTypeValues()
    {
        LogType[] allLogTypes = (LogType[])Enum.GetValues(typeof(LogType));
        var builder = ImmutableList.CreateBuilder<string>();
        LogType baseLog = LogType.None;
        foreach (var i in allLogTypes)
        {
            baseLog |= i;
        }
        int permutations = (int)baseLog;
        permutations += 1;
        string fullName = typeof(LogType).FullName;
        string rootName = $"global::{fullName}.";
        builder.Add($"{rootName}{nameof(LogType.None)}");
        StringBuilder stringBuilder = new();
        for (int i = 1; i < permutations; i++)
        {
            LogType type = (LogType)i;

            if ((type & LogType.File) != 0)
            {
                if (stringBuilder.Length != 0)
                {
                    stringBuilder.Append(" | ");
                }
                stringBuilder.Append($"{rootName}{nameof(LogType.File)}");
            }
            if ((type & LogType.Nt) != 0)
            {
                if (stringBuilder.Length != 0)
                {
                    stringBuilder.Append(" | ");
                }
                stringBuilder.Append($"{rootName}{nameof(LogType.Nt)}");
            }
            if ((type & LogType.Once) != 0)
            {
                if (stringBuilder.Length != 0)
                {
                    stringBuilder.Append(" | ");
                }
                stringBuilder.Append($"{rootName}{nameof(LogType.Once)}");
            }
            builder.Add(stringBuilder.ToString());
            stringBuilder.Clear();
        }

        return builder.ToImmutable();
    }

    private static ImmutableList<string> GetAllLevelValuesVb()
    {
        LogLevel[] allLogLevels = (LogLevel[])Enum.GetValues(typeof(LogLevel));
        var builder = ImmutableList.CreateBuilder<string>();
        string fullName = typeof(LogLevel).FullName;
        string rootName = $"Global.{fullName}.";
        foreach (var i in allLogLevels)
        {
            builder.Add($"{rootName}{i}");
        }
        return builder.ToImmutable();
    }

    private static ImmutableList<string> GetAllTypeValuesVb()
    {
        LogType[] allLogTypes = (LogType[])Enum.GetValues(typeof(LogType));
        var builder = ImmutableList.CreateBuilder<string>();
        LogType baseLog = LogType.None;
        foreach (var i in allLogTypes)
        {
            baseLog |= i;
        }
        int permutations = (int)baseLog;
        permutations += 1;
        string fullName = typeof(LogType).FullName;
        string rootName = $"Global.{fullName}.";
        builder.Add($"{rootName}{nameof(LogType.None)}");
        StringBuilder stringBuilder = new();
        for (int i = 1; i < permutations; i++)
        {
            LogType type = (LogType)i;

            if ((type & LogType.File) != 0)
            {
                if (stringBuilder.Length != 0)
                {
                    stringBuilder.Append(" Or ");
                }
                stringBuilder.Append($"{rootName}{nameof(LogType.File)}");
            }
            if ((type & LogType.Nt) != 0)
            {
                if (stringBuilder.Length != 0)
                {
                    stringBuilder.Append(" Or ");
                }
                stringBuilder.Append($"{rootName}{nameof(LogType.Nt)}");
            }
            if ((type & LogType.Once) != 0)
            {
                if (stringBuilder.Length != 0)
                {
                    stringBuilder.Append(" Or ");
                }
                stringBuilder.Append($"{rootName}{nameof(LogType.Once)}");
            }
            builder.Add(stringBuilder.ToString());
            stringBuilder.Clear();
        }

        return builder.ToImmutable();
    }

    public static ImmutableList<string> AllTypeValues { get; } = GetAllTypeValues();

    public static ImmutableList<string> AllLevelValues { get; } = GetAllLevelValues();

    public static ImmutableList<string> AllTypeValuesVb { get; } = GetAllTypeValuesVb();

    public static ImmutableList<string> AllLevelValuesVb { get; } = GetAllLevelValuesVb();
}

internal static class LogAttributeInfoExtensions
{
    public static LogAttributeInfo? ToAttributeInfo(this AttributeData attributeData, INamedTypeSymbol? attributeClass, CancellationToken token)
    {
        if (attributeClass is null)
        {
            return null;
        }
        if (attributeClass.IsLogAttributeClass())
        {
            token.ThrowIfCancellationRequested();

            string? path = null;
            bool useProtobuf = false;
            LogType logTypeEnum = LogTypeExtensions.DefaultLogType;
            LogLevel logLevel = LogLevelExtensions.DefaultLogLevel;

            // Get the log attribute
            foreach (var named in attributeData.NamedArguments)
            {
                if (named.Key == "Key")
                {
                    if (!named.Value.IsNull)
                    {
                        path = SymbolDisplay.FormatPrimitive(named.Value.Value!, false, false);
                        token.ThrowIfCancellationRequested();
                    }
                }
                else if (named.Key == "LogLevel")
                {
                    // A boxed primitive can be unboxed to an enum with the same underlying type.
                    logLevel = (LogLevel)named.Value.Value!;
                    token.ThrowIfCancellationRequested();
                }
                else if (named.Key == "LogType")
                {
                    // A boxed primitive can be unboxed to an enum with the same underlying type.
                    logTypeEnum = (LogType)named.Value.Value!;
                    token.ThrowIfCancellationRequested();
                }
                else if (named.Key == "UseProtobuf")
                {
                    useProtobuf = (bool)named.Value.Value!;
                }
            }

            return new LogAttributeInfo(path, logLevel, logTypeEnum, useProtobuf);
        }
        return null;
    }


}
