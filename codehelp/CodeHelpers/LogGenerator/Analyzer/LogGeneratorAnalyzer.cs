using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace WPILib.CodeHelpers.LogGenerator.Analyzer;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class LogGeneratorAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create([
        LoggerDiagnostics.UnknownMemberType,
        LoggerDiagnostics.ProtobufIsArray,
        LoggerDiagnostics.UnknownSpecialTypeArray,
        LoggerDiagnostics.LoggedMethodReturnsVoid,
        LoggerDiagnostics.LoggedMethodTakeParameters,
        LoggerDiagnostics.LoggedHasUnknownType,
        LoggerDiagnostics.UnknownFailureMode,
        LoggerDiagnostics.NullableStructArray,
        LoggerDiagnostics.UnknownSpecialTypeIntArray,
        LoggerDiagnostics.MissingGenerateLog,
    ]);

    public override void Initialize(AnalysisContext context)
    {
        context.EnableConcurrentExecution();
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.NamedType);
        context.RegisterSymbolAction(AnalyzeMembers, SymbolKind.Method);
        context.RegisterSymbolAction(AnalyzeMembers, SymbolKind.Field);
        context.RegisterSymbolAction(AnalyzeMembers, SymbolKind.Property);
    }

    private static void AnalyzeMembers(SymbolAnalysisContext context)
    {
        if (!context.Symbol.HasLogAttribute()) {
            return;
        }

        var containingType = context.Symbol.ContainingType;
        if (containingType is null) {
            return;
        }

        if (!containingType.HasGenerateLogAttribute()) {
            foreach (var location in containingType.Locations) {
                context.ReportDiagnostic(Diagnostic.Create(LoggerDiagnostics.MissingGenerateLog, location, context.Symbol.Name, containingType.ToDisplayString()));
            }
        }
    }

    private static void AnalyzeSymbol(SymbolAnalysisContext context)
    {
        var token = context.CancellationToken;
        token.ThrowIfCancellationRequested();

        if (context.Symbol is not INamedTypeSymbol namedTypeSymbol)
        {
            return;
        }

        List<(FailureMode, ISymbol)> failures = [];
        Dictionary<LoggableMember, ISymbol> symbolMap = [];

        var model = namedTypeSymbol.GetLoggableType(token, failures, symbolMap);
        token.ThrowIfCancellationRequested();

        model.ExecuteAnalysis(context, symbolMap);

        foreach (var failure in failures)
        {
            context.ReportDiagnostic(failure.Item1, failure.Item2);
        }
    }
}
