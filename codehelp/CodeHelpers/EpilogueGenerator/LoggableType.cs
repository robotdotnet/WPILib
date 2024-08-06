using System.Collections.Immutable;
using System.Diagnostics;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;
using WPILib.CodeHelpers.LogGenerator.Analyzer;
using static WPILib.CodeHelpers.IndentedStringBuilder;

namespace WPILib.CodeHelpers.EpilogueGenerator;

// Contains all information on a loggable type
public record LoggableType(TypeDeclarationModel TypeDeclaration, EquatableArray<LoggableMember> LoggableMembers)
{
    private static void WriteTypeName(TypeDeclarationModel type, IndentedStringBuilder builder, bool leaf)
    {
        if (type.Parent is not null)
        {
            WriteTypeName(type.Parent, builder, false);
        }
        builder.Append($"{type.TypeName}{(leaf ? "" : "_")}");
    }

    public void WriteLoggerFQN(IndentedStringBuilder builder)
    {
        builder.Append("global::");
        TypeDeclaration.WriteFileName(builder, true);
        builder.Append("Logger");
    }

    public void WriteFQN(IndentedStringBuilder builder)
    {
        builder.Append("global::");
        TypeDeclaration.WriteFileName(builder, true);
        TypeDeclaration.GetGenericParameters(builder);
    }

    private int AddClassDeclaration(IndentedStringBuilder builder)
    {
        // Find our namespace and write it
        TypeDeclarationModel root = TypeDeclaration;
        TypeDeclarationModel? parent = TypeDeclaration.Parent;
        while (parent is not null)
        {
            root = parent;
            parent = parent.Parent;
        }

        int scopeCount = root.Namespace!.WriteNamespaceDeclaration(builder);

        // Write out type name
        builder.StartLine();
        builder.Append("public sealed class ");
        WriteTypeName(TypeDeclaration, builder, true);
        builder.Append("Logger");
        builder.EndLine();

        // Add inheritance
        if (builder.Language == LanguageKind.CSharp)
        {
            builder.StartLine();
            builder.Append($"    : {Strings.ClassSpecificLoggerFullyQualifiedTypeName}<");
            WriteFQN(builder);
            builder.Append(">");
            builder.EndLine();
        }
        else if (builder.Language == LanguageKind.VisualBasic)
        {
            throw new NotImplementedException();
            // builder.StartLine();
            // builder.Append("    Implements ");
            // bool first = false;
            // foreach (var item in inheritanceSpan)
            // {
            //     if (!first)
            //     {
            //         first = true;
            //     }
            //     else
            //     {
            //         builder.Append(", ");
            //     }
            //     builder.Append(item);
            // }
            // builder.EndLine();
        }

        builder.EnterScope(ScopeType.Class);

        return scopeCount + 1;
    }

    private void WriteMethodDeclaration(IndentedStringBuilder builder)
    {
        if (builder.Language == LanguageKind.CSharp)
        {
            builder.StartLine();
            builder.Append(Strings.UpdateFunctionStart);
            WriteFQN(builder);
            builder.Append(" value)");
            builder.EndLine();
        }
        else if (builder.Language == LanguageKind.VisualBasic)
        {
            throw new NotImplementedException();
        }
    }

    public void WriteMethod(IndentedStringBuilder builder, ImmutableArray<CustomLoggerType?> customLoggers)
    {
        var classScopes = AddClassDeclaration(builder);
        WriteMethodDeclaration(builder);
        builder.EnterScope(ScopeType.NonReturningMethod);
        foreach (var call in LoggableMembers)
        {
            call.WriteLogCall(builder, customLoggers);
        }
        builder.ExitScope(); // Method scope
        for (int i = 0; i < classScopes; i++)
        {
            builder.ExitScope(); // Class scopes
        }
    }
}

internal static class LoggableTypeExtensions
{
    public static LoggableType GetLoggableType(this ImmutableArray<AttributeData> attributes, INamedTypeSymbol classSymbol, CancellationToken token, List<(FailureMode, ISymbol)>? failures, Dictionary<LoggableMember, ISymbol>? symbolMap)
    {
        // Only use first attribute, we don't allow multiple, and we're guaranteed to have 1.
        Debug.Assert(attributes.Length == 1);

        token.ThrowIfCancellationRequested();

        LogAttributeInfo info = attributes[0].ToAttributeInfo(token);

        token.ThrowIfCancellationRequested();

        var typeDeclType = classSymbol.GetTypeDeclarationModel();
        token.ThrowIfCancellationRequested();

        var classMembers = classSymbol.GetMembers();
        token.ThrowIfCancellationRequested();

        var loggableMembers = ImmutableArray.CreateBuilder<LoggableMember>(classMembers.Length);

        // Find all loggable memeber
        foreach (var member in classMembers)
        {
            token.ThrowIfCancellationRequested();

            var loggableMemberFailure = member.ToLoggableMember(token, info.LogStrategy, out var loggableMember);
            token.ThrowIfCancellationRequested();
            if (loggableMemberFailure == FailureMode.None)
            {
                // This is either a valid log, or is not logged
                if (loggableMember is not null)
                {
                    loggableMembers.Add(loggableMember);
                    symbolMap?.Add(loggableMember, member);
                }
                continue;
            }
            // Getting here means we errored
            failures?.Add((loggableMemberFailure, member));
        }

        var loggableType = new LoggableType(typeDeclType, loggableMembers.ToImmutable());

        return loggableType;
    }

    public static void ExecuteAnalysis(this LoggableType? maybeType, SymbolAnalysisContext context, Dictionary<LoggableMember, ISymbol> symbolMap)
    {
        if (maybeType is { } loggableType)
        {
            foreach (var call in loggableType.LoggableMembers)
            {
                var failureMode = call.WriteLogCall(null, []);
                if (failureMode == FailureMode.None)
                {
                    continue;
                }
                // We're errored
                throw new NotImplementedException();
                //context.ReportDiagnostic(failureMode, symbolMap[call]);
            }

            if (!loggableType.LoggableMembers.IsEmpty && !context.Symbol.HasLoggedAttribute())
            {
                foreach (var location in context.Symbol.Locations)
                {
                    context.ReportDiagnostic(Diagnostic.Create(LoggerDiagnostics.MissingGenerateLog, location, context.Symbol.Name));
                }
            }
        }
    }

    public static void ExecuteSourceGeneration(this LoggableType? maybeType, SourceProductionContext context, ImmutableArray<CustomLoggerType?> customLoggers, LanguageKind language)
    {
        if (maybeType is { } loggableType)
        {
            IndentedStringBuilder builder = new IndentedStringBuilder(language);

            loggableType.TypeDeclaration.WriteFileName(builder, true);
            builder.Append(".g");
            string fileName = builder.ToString();
            builder.Clear();

            loggableType.WriteMethod(builder, customLoggers);

            context.AddSource(fileName, SourceText.From(builder.ToString(), Encoding.UTF8));
        }
    }
}
