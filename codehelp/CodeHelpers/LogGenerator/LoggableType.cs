using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;
using WPILib.CodeHelpers.LogGenerator.Analyzer;

namespace WPILib.CodeHelpers.LogGenerator;

// Contains all information on a loggable type
public record LoggableType(TypeDeclarationModel TypeDeclaration, EquatableArray<LoggableMember> LoggableMembers)
{
    private int AddClassDeclaration(IndentedStringBuilder builder)
    {
        string? iLogged = null;

        if ((TypeDeclaration.Modifiers & TypeModifiers.IsRefLikeType) == 0)
        {
            if (builder.Language == LanguageKind.CSharp)
            {
                iLogged = Strings.LoggerInterfaceFullyQualified;
            }
            else if (builder.Language == LanguageKind.VisualBasic)
            {
                iLogged = Strings.LoggerInterfaceFullyQualifiedVb;
            }
        }

        return TypeDeclaration.WriteClassDeclaration(builder, false, iLogged is null ? [] : [iLogged]);
    }

    private void WriteMethodDeclaration(IndentedStringBuilder builder)
    {
        if (builder.Language == LanguageKind.CSharp)
        {
            builder.AppendFullLine(Strings.FullMethodDeclaration);
        }
        else if (builder.Language == LanguageKind.VisualBasic)
        {
            builder.AppendFullLine(Strings.FullMethodDeclarationVb);
        }
    }

    public void WriteMethod(IndentedStringBuilder builder)
    {
        var classScopes = AddClassDeclaration(builder);
        WriteMethodDeclaration(builder);
        builder.EnterScope(ScopeType.NonReturningMethod);
        foreach (var call in LoggableMembers)
        {
            call.WriteLogCall(builder);
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

    public static LoggableType GetLoggableType(this INamedTypeSymbol classSymbol, CancellationToken token, List<(FailureMode, ISymbol)>? failures, Dictionary<LoggableMember, ISymbol>? symbolMap)
    {
        token.ThrowIfCancellationRequested();

        var typeDeclType = classSymbol.GetTypeDeclarationModel();
        token.ThrowIfCancellationRequested();

        var classMembers = classSymbol.GetMembers();
        token.ThrowIfCancellationRequested();

        var loggableMembers = ImmutableArray.CreateBuilder<LoggableMember>(classMembers.Length);

        foreach (var member in classMembers)
        {
            token.ThrowIfCancellationRequested();

            var loggableMemberFailure = member.ToLoggableMember(token, out var loggableMember);
            token.ThrowIfCancellationRequested();
            if (loggableMemberFailure == FailureMode.None)
            {
                // This is either a valid log, or has no attribute
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
                var failureMode = call.WriteLogCall(null);
                if (failureMode == FailureMode.None)
                {
                    continue;
                }
                // We're errored
                context.ReportDiagnostic(failureMode, symbolMap[call]);
            }

            if (!loggableType.LoggableMembers.IsEmpty && !context.Symbol.HasGenerateLogAttribute())
            {
                foreach (var location in context.Symbol.Locations)
                {
                    context.ReportDiagnostic(Diagnostic.Create(LoggerDiagnostics.MissingGenerateLog, location, context.Symbol.Name));
                }
            }
        }
    }

    public static void ExecuteSourceGeneration(this LoggableType? maybeType, SourceProductionContext context, LanguageKind language)
    {

        if (maybeType is { } loggableType)
        {
            IndentedStringBuilder builder = new IndentedStringBuilder(language);

            loggableType.TypeDeclaration.WriteFileName(builder);
            builder.Append("g");
            string fileName = builder.ToString();
            builder.Clear();

            loggableType.WriteMethod(builder);

            context.AddSource(fileName, SourceText.From(builder.ToString(), Encoding.UTF8));
        }
    }
}
