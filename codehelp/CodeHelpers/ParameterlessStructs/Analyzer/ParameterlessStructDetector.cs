using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace WPILib.CodeHelpers.ParameterlessStructs.Analyzer;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class ParameterlessStructDetector : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create([
        ParameterlessStructDiagnostics.ParameterlessConstructorMissing
    ]);

    public override void Initialize(AnalysisContext context)
    {
        context.EnableConcurrentExecution();
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);

        context.RegisterOperationAction(AnalyzeFieldOperation, OperationKind.FieldInitializer);
        context.RegisterOperationAction(AnalyzePropertyOperation, OperationKind.PropertyInitializer);
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

        // If we're here, we have an initializer, check to see if we have a default parameterless constructor
        var parameterless = field.ContainingType.InstanceConstructors.Where(x => x.Parameters.IsEmpty).First();

        if (parameterless.IsImplicitlyDeclared)
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

        // If we're here, we have an initializer, check to see if we have a default parameterless constructor
        var parameterless = property.ContainingType.InstanceConstructors.Where(x => x.Parameters.IsEmpty).First();

        if (parameterless.IsImplicitlyDeclared)
        {
            ReportDiagnostic(context, propertyInitOperation);
        }
    }

    private static void ReportDiagnostic(OperationAnalysisContext context, IOperation symbol)
    {
        context.ReportDiagnostic(Diagnostic.Create(ParameterlessStructDiagnostics.ParameterlessConstructorMissing, symbol.Syntax.GetLocation()));
    }
}
