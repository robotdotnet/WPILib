using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace WPILib.CodeHelpers.ActionFuncDetector.Analyzer;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class ActionFuncDetector : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create([
        ActionFuncDiagnostics.UseOfActionFunc
    ]);

    public override void Initialize(AnalysisContext context)
    {
        context.EnableConcurrentExecution();
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.RegisterSymbolAction(AnalyzeMethod, SymbolKind.Method);
        context.RegisterSymbolAction(AnalyzeProperty, SymbolKind.Property);
        context.RegisterSymbolAction(AnalyzeField, SymbolKind.Field);
    }

    private static void AnalyzeMethod(SymbolAnalysisContext context)
    {
        if (context.Symbol is not IMethodSymbol methodSymbol)
        {
            return;
        }

        if (methodSymbol.DeclaredAccessibility != Accessibility.Public)
        {
            return;
        }

        if (TypeCheck(methodSymbol.ReturnType))
        {
            ReportDiagnostic(context, methodSymbol.ReturnType);
        }

        foreach (var parameter in methodSymbol.Parameters)
        {
            if (TypeCheck(parameter.Type))
            {
                ReportDiagnostic(context, parameter);
            }
        }
    }

    private static void ReportDiagnostic(SymbolAnalysisContext context, ISymbol symbol)
    {
        foreach (var location in symbol.Locations)
        {
            context.ReportDiagnostic(Diagnostic.Create(ActionFuncDiagnostics.UseOfActionFunc, location));
        }
    }

    private static void AnalyzeProperty(SymbolAnalysisContext context)
    {
        if (context.Symbol is not IPropertySymbol propertySymbol)
        {
            return;
        }

        if (propertySymbol.DeclaredAccessibility != Accessibility.Public)
        {
            return;
        }

        if (TypeCheck(propertySymbol.Type))
        {
            ReportDiagnostic(context, propertySymbol.Type);
        }
    }

    private static bool TypeCheck(ITypeSymbol typeSymbol)
    {
        var containingNamespace = typeSymbol.ContainingNamespace;
        if (containingNamespace is null)
        {
            return false;
        }
        if (containingNamespace.Name != "System")
        {
            return false;
        }
        var name = typeSymbol.Name;
        return name == "Action" || name == "Func";
    }

    private static void AnalyzeField(SymbolAnalysisContext context)
    {
        if (context.Symbol is not IFieldSymbol fieldSymbol)
        {
            return;
        }

        if (fieldSymbol.DeclaredAccessibility != Accessibility.Public)
        {
            return;
        }

        if (TypeCheck(fieldSymbol.Type))
        {
            ReportDiagnostic(context, fieldSymbol.Type);
        }
    }
}
