using System.Collections.Immutable;
using System.Text;
using Epilogue;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.PooledObjects;

namespace WPILib.CodeHelpers.EpilogueGenerator;

// Contains all information about a [Logged] attribute
public record LogAttributeInfo(string? Name, LogStrategy LogStrategy, LogImportance LogImportance)
{
    // public string GetLogStrageyString(LanguageKind language)
    // {
    //     if (language == LanguageKind.CSharp)
    //     {
    //         return AllLevelValues[(int)LogLevel];
    //     }
    //     else if (language == LanguageKind.VisualBasic)
    //     {
    //         return AllLevelValuesVb[(int)LogLevel];
    //     }
    //     return "";
    // }

    // public string GetLogTypeString(LanguageKind language)
    // {
    //     if (language == LanguageKind.CSharp)
    //     {
    //         return AllTypeValues[(int)LogType];
    //     }
    //     else if (language == LanguageKind.VisualBasic)
    //     {
    //         return AllTypeValuesVb[(int)LogType];
    //     }
    //     return "";
    // }

    // private static ImmutableList<string> GetAllLevelValues()
    // {
    //     LogLevel[] allLogLevels = (LogLevel[])Enum.GetValues(typeof(LogLevel));
    //     var builder = ImmutableList.CreateBuilder<string>();
    //     string fullName = typeof(LogLevel).FullName;
    //     string rootName = $"global::{fullName}.";
    //     foreach (var i in allLogLevels)
    //     {
    //         builder.Add($"{rootName}{i}");
    //     }
    //     return builder.ToImmutable();
    // }

    // private static ImmutableList<string> GetAllTypeValues()
    // {
    //     LogType[] allLogTypes = (LogType[])Enum.GetValues(typeof(LogType));
    //     var builder = ImmutableList.CreateBuilder<string>();
    //     LogType baseLog = LogType.None;
    //     foreach (var i in allLogTypes)
    //     {
    //         baseLog |= i;
    //     }
    //     int permutations = (int)baseLog;
    //     permutations += 1;
    //     string fullName = typeof(LogType).FullName;
    //     string rootName = $"global::{fullName}.";
    //     builder.Add($"{rootName}{nameof(LogType.None)}");
    //     StringBuilder stringBuilder = new();
    //     for (int i = 1; i < permutations; i++)
    //     {
    //         LogType type = (LogType)i;

    //         if ((type & LogType.File) != 0)
    //         {
    //             if (stringBuilder.Length != 0)
    //             {
    //                 stringBuilder.Append(" | ");
    //             }
    //             stringBuilder.Append($"{rootName}{nameof(LogType.File)}");
    //         }
    //         if ((type & LogType.Nt) != 0)
    //         {
    //             if (stringBuilder.Length != 0)
    //             {
    //                 stringBuilder.Append(" | ");
    //             }
    //             stringBuilder.Append($"{rootName}{nameof(LogType.Nt)}");
    //         }
    //         if ((type & LogType.Once) != 0)
    //         {
    //             if (stringBuilder.Length != 0)
    //             {
    //                 stringBuilder.Append(" | ");
    //             }
    //             stringBuilder.Append($"{rootName}{nameof(LogType.Once)}");
    //         }
    //         builder.Add(stringBuilder.ToString());
    //         stringBuilder.Clear();
    //     }

    //     return builder.ToImmutable();
    // }

    // private static ImmutableList<string> GetAllLevelValuesVb()
    // {
    //     LogLevel[] allLogLevels = (LogLevel[])Enum.GetValues(typeof(LogLevel));
    //     var builder = ImmutableList.CreateBuilder<string>();
    //     string fullName = typeof(LogLevel).FullName;
    //     string rootName = $"Global.{fullName}.";
    //     foreach (var i in allLogLevels)
    //     {
    //         builder.Add($"{rootName}{i}");
    //     }
    //     return builder.ToImmutable();
    // }

    // private static ImmutableList<string> GetAllTypeValuesVb()
    // {
    //     LogType[] allLogTypes = (LogType[])Enum.GetValues(typeof(LogType));
    //     var builder = ImmutableList.CreateBuilder<string>();
    //     LogType baseLog = LogType.None;
    //     foreach (var i in allLogTypes)
    //     {
    //         baseLog |= i;
    //     }
    //     int permutations = (int)baseLog;
    //     permutations += 1;
    //     string fullName = typeof(LogType).FullName;
    //     string rootName = $"Global.{fullName}.";
    //     builder.Add($"{rootName}{nameof(LogType.None)}");
    //     StringBuilder stringBuilder = new();
    //     for (int i = 1; i < permutations; i++)
    //     {
    //         LogType type = (LogType)i;

    //         if ((type & LogType.File) != 0)
    //         {
    //             if (stringBuilder.Length != 0)
    //             {
    //                 stringBuilder.Append(" Or ");
    //             }
    //             stringBuilder.Append($"{rootName}{nameof(LogType.File)}");
    //         }
    //         if ((type & LogType.Nt) != 0)
    //         {
    //             if (stringBuilder.Length != 0)
    //             {
    //                 stringBuilder.Append(" Or ");
    //             }
    //             stringBuilder.Append($"{rootName}{nameof(LogType.Nt)}");
    //         }
    //         if ((type & LogType.Once) != 0)
    //         {
    //             if (stringBuilder.Length != 0)
    //             {
    //                 stringBuilder.Append(" Or ");
    //             }
    //             stringBuilder.Append($"{rootName}{nameof(LogType.Once)}");
    //         }
    //         builder.Add(stringBuilder.ToString());
    //         stringBuilder.Clear();
    //     }

    //     return builder.ToImmutable();
    // }

    // public static ImmutableList<string> AllTypeValues { get; } = GetAllTypeValues();

    // public static ImmutableList<string> AllLevelValues { get; } = GetAllLevelValues();

    // public static ImmutableList<string> AllTypeValuesVb { get; } = GetAllTypeValuesVb();

    // public static ImmutableList<string> AllLevelValuesVb { get; } = GetAllLevelValuesVb();
}

internal static class LogAttributeInfoExtensions
{
    public static LogAttributeInfo? ToAttributeInfo(this AttributeData attributeData, INamedTypeSymbol? attributeClass, CancellationToken token, out bool notLogged)
    {
        if (attributeClass is null)
        {
            notLogged = false;
            return null;
        }

        if (attributeClass.IsNotLoggedAttributeClass())
        {
            notLogged = true;
            return null;
        }
        notLogged = false;
        if (attributeClass.IsLoggedAttributeClass())
        {
            token.ThrowIfCancellationRequested();

            string? path = null;
            LogStrategy logStrategyEnum = LogStrategyExtensions.DefaultLogStrategy;
            LogImportance logImportanceEnum = LogImportanceExtensions.DefaultLogImportance;

            // Get the log attribute
            foreach (var named in attributeData.NamedArguments)
            {
                if (named.Key == "Name")
                {
                    if (!named.Value.IsNull)
                    {
                        path = SymbolDisplay.FormatPrimitive(named.Value.Value!, false, false);
                    }
                    token.ThrowIfCancellationRequested();
                }
                else if (named.Key == "Strategy")
                {
                    // A boxed primitive can be unboxed to an enum with the same underlying type.
                    logStrategyEnum = (LogStrategy)named.Value.Value!;
                    token.ThrowIfCancellationRequested();
                }
                else if (named.Key == "Importance")
                {
                    // A boxed primitive can be unboxed to an enum with the same underlying type.
                    logImportanceEnum = (LogImportance)named.Value.Value!;
                    token.ThrowIfCancellationRequested();
                }
            }

            return new LogAttributeInfo(path, logStrategyEnum, logImportanceEnum);
        }
        return null;
    }


}
