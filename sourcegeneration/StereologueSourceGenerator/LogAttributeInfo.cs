using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Stereologue.SourceGenerator;

// Contains all information about a [Log] attribute
internal record LogAttributeInfo(string? Path, string LogLevel, string LogType, bool UseProtobuf);

internal static class LogAttributeInfoExtensions
{
    public static LogAttributeInfo? ToAttributeInfo(this AttributeData attributeData, INamedTypeSymbol? attributeClass, CancellationToken token)
    {
        if (attributeClass is null)
        {
            return null;
        }
        if (attributeClass.ToDisplayString() == "Stereologue.LogAttribute")
        {
            token.ThrowIfCancellationRequested();

            string? path = null;
            bool useProtobuf = false;
            string logTypeEnum = "Stereologue.LogType.Nt | Stereologue.LogType.File";
            string logLevel = "Stereologue.LogLevel.Default";

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
                    logLevel = named.Value.ToCSharpString();
                    token.ThrowIfCancellationRequested();
                }
                else if (named.Key == "LogType")
                {
                    logTypeEnum = named.Value.ToCSharpString();
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
