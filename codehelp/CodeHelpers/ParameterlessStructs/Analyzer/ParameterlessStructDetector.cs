using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace WPILib.CodeHelpers.ParameterlessStructs.Analyzer;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class ParameterlessStructDetector : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create([
        ParameterlessStructDiagnostics.WPIMathHasFieldInitializers,
        ParameterlessStructDiagnostics.WPIMathMustHaveParameterlessConstructor
    ]);

    public override void Initialize(AnalysisContext context)
    {
        context.EnableConcurrentExecution();
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);

        context.RegisterSymbolAction(AnalyzeWPIMathStruct, SymbolKind.NamedType);

        context.RegisterOperationAction(AnalyzeFieldOperation, OperationKind.FieldInitializer);
        context.RegisterOperationAction(AnalyzePropertyOperation, OperationKind.PropertyInitializer);
    }

    private static bool IsWPIMathNamespace(ISymbol symbol)
    {
        var rootNamespace = symbol.ContainingNamespace;
        if (rootNamespace == null)
        {
            return false;
        }
        while (rootNamespace != null)
        {
            if (rootNamespace.IsGlobalNamespace)
            {
                // We went too far, but this is definitly not it
                return false;
            }
            var currentName = rootNamespace.Name;
            rootNamespace = rootNamespace.ContainingNamespace;
            if (rootNamespace.IsGlobalNamespace)
            {
                // Found the actual root namespace
                if (currentName == "WPIMath")
                {
                    break;
                }
                // Not what we are looking for
                return false;
            }
        }
        return true;
    }

    private static void AnalyzeWPIMathStruct(SymbolAnalysisContext context)
    {
        // Find root namespace
        if (context.Symbol is not INamedTypeSymbol symbol)
        {
            return;
        }
        if (symbol.TypeKind != TypeKind.Struct)
        {
            return;
        }
        if (!IsWPIMathNamespace(symbol))
        {
            return;
        }

        // If we're here, we're a WPIMath struct
        var parameterless = symbol.InstanceConstructors.Where(x => x.Parameters.IsEmpty).First();

        if (parameterless.IsImplicitlyDeclared)
        {
            ReportDiagnostic(context, symbol);
        }
    }

    private static void AnalyzeFieldOperation(OperationAnalysisContext context)
    {
        var fieldInitOperation = (IFieldInitializerOperation)context.Operation;
        IFieldSymbol? field = fieldInitOperation.InitializedFields.FirstOrDefault();
        if (field is null || field.IsConst || field.IsStatic || fieldInitOperation.Value is null)
        {
            return;
        }

        if (field.ContainingType?.TypeKind != TypeKind.Struct)
        {
            return;
        }

        // If we're here, we have an initializer, check to see if we're in WPIMath
        if (IsWPIMathNamespace(field.ContainingType))
        {
            ReportDiagnostic(context, fieldInitOperation);
        }
    }

    private static void AnalyzePropertyOperation(OperationAnalysisContext context)
    {
        var propertyInitOperation = (IPropertyInitializerOperation)context.Operation;
        IPropertySymbol? property = propertyInitOperation.InitializedProperties.FirstOrDefault();
        if (property is null || property.IsStatic || propertyInitOperation.Value is null)
        {
            return;
        }

        if (property.ContainingType?.TypeKind != TypeKind.Struct)
        {
            return;
        }

        // If we're here, we have an initializer, check to see if we're in WPIMath
        if (IsWPIMathNamespace(property.ContainingType))
        {
            ReportDiagnostic(context, propertyInitOperation);
        }
    }

    private static void ReportDiagnostic(OperationAnalysisContext context, IOperation symbol)
    {
        context.ReportDiagnostic(Diagnostic.Create(ParameterlessStructDiagnostics.WPIMathHasFieldInitializers, symbol.Syntax.GetLocation()));
    }

    private static void ReportDiagnostic(SymbolAnalysisContext context, ISymbol symbol)
    {
        foreach (var location in symbol.Locations)
        {
            context.ReportDiagnostic(Diagnostic.Create(ParameterlessStructDiagnostics.WPIMathMustHaveParameterlessConstructor, location));
        }
    }
}
