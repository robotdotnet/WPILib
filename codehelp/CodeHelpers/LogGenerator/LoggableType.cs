using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;
using WPILib.CodeHelpers.LogGenerator.Analyzer;

namespace WPILib.CodeHelpers.LogGenerator;

// Contains all information on a loggable type
internal record LoggableType(TypeDeclarationModel TypeDeclaration, string FileName, string TypeName, string? TypeNamespace, EquatableArray<LoggableMember> LoggableMembers)
{
    private void AddClassDeclaration(IndentedStringBuilder builder)
    {
        string iLogged = "";

        if ((TypeDeclaration.Modifiers & TypeModifiers.IsRefLikeType) == 0)
        {
            iLogged = $" : global::{Strings.ILoggedName}";
        }


        builder.AppendFullLine($"{TypeDeclaration.GetClassDeclaration(false, true)} {TypeName}{iLogged}");
    }

    private void WriteMethodDeclaration(IndentedStringBuilder builder)
    {
        builder.AppendFullLine(Strings.FullMethodDeclaration);
    }

    public void WriteMethod(IndentedStringBuilder builder)
    {
        if (TypeNamespace is not null)
        {
            builder.AppendFullLine($"namespace {TypeNamespace}");
            builder.EnterManualScope();
        }

        {
            AddClassDeclaration(builder);
            using var classScope = builder.EnterScope();
            WriteMethodDeclaration(builder);
            using var methodScope = builder.EnterScope();
            foreach (var call in LoggableMembers)
            {
                call.WriteLogCall(builder);
            }
        }

        if (TypeNamespace is not null)
        {
            builder.ExitManualScope();
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

        var nameString = classSymbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
        token.ThrowIfCancellationRequested();

        var nspace = classSymbol.ContainingNamespace is { IsGlobalNamespace: false } ns ? ns.ToDisplayString() : null;
        token.ThrowIfCancellationRequested();

        var fmt = new SymbolDisplayFormat(genericsOptions: SymbolDisplayGenericsOptions.None);
        var loggableType = new LoggableType(typeDeclType, $"{nspace}{classSymbol.ToDisplayString(fmt)}{classSymbol.MetadataName}", nameString, nspace, loggableMembers.ToImmutable());

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
                    context.ReportDiagnostic(Diagnostic.Create(LoggerDiagnostics.MissingGenerateLog, location, context.Symbol.ToDisplayString()));
                }
            }
        }
    }

    public static void ExecuteSourceGeneration(this LoggableType? maybeType, SourceProductionContext context)
    {

        if (maybeType is { } loggableType)
        {
            IndentedStringBuilder builder = new IndentedStringBuilder();

            loggableType.WriteMethod(builder);

            context.AddSource($"Stereologue.{loggableType.FileName}.g.cs", SourceText.From(builder.ToString(), Encoding.UTF8));
        }
    }
}
