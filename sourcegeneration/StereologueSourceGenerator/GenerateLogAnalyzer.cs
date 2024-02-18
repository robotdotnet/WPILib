using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Stereologue.SourceGenerator;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class GenerateLogAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create([
        LoggerDiagnostics.GeneratedTypeNotPartial
    ]);

    public override void Initialize(AnalysisContext context)
    {
        context.EnableConcurrentExecution();
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);

        // context.RegisterSyntaxNodeAction(AnalyzeSyntax,
        //     SyntaxKind.ClassDeclaration,
        //     SyntaxKind.StructDeclaration,
        //     SyntaxKind.RecordDeclaration,
        //     SyntaxKind.RecordStructDeclaration,
        //     SyntaxKind.InterfaceDeclaration);

        context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.NamedType);
    }

    // private static void AnalyzeSyntax(SyntaxNodeAnalysisContext context)
    // {
    //     if (context.SemanticModel.GetDeclaredSymbol(context.Node) is not INamedTypeSymbol namedTypeSymbol)
    //     {
    //         return;
    //     }

    //     if (!namedTypeSymbol.GetAttributes().Where(x => x.AttributeClass?.ToDisplayString() == Strings.GenerateLogAttributeName).Any())
    //     {
    //         return;
    //     }

    //     if (context.Node is TypeDeclarationSyntax typeSyntax)
    //     {
    //         // Ensure type is partial.
    //         if (!typeSyntax.IsInPartialContext(out var nonPartialIdentifier))
    //         {
    //             context.ReportDiagnostic(
    //                 Diagnostic.Create(LoggerDiagnostics.GeneratedTypeNotPartial, typeSyntax.GetLocation(), namedTypeSymbol.Name)
    //             );
    //         }
    //     }
    // }

    private static void AnalyzeSymbol(SymbolAnalysisContext context)
    {
        var token = context.CancellationToken;
        token.ThrowIfCancellationRequested();

        if (context.Symbol is not INamedTypeSymbol namedTypeSymbol)
        {
            return;
        }

        var model = namedTypeSymbol.GetLoggableType(token);
        token.ThrowIfCancellationRequested();

        foreach (var member in model.LoggableMembers)
        {
            if (member.MemberDeclaration.LoggedType == DeclarationType.Protobuf)
            {
                if (member.MemberDeclaration.LoggedKind != DeclarationKind.None && member.MemberDeclaration.LoggedKind != DeclarationKind.NullableValueType && member.MemberDeclaration.LoggedKind != DeclarationKind.NullableReferenceType)
                {
                    foreach (var location in namedTypeSymbol.Locations)
                    {
                        context.ReportDiagnostic(
                            Diagnostic.Create(LoggerDiagnostics.GeneratedTypeNotPartial, location, namedTypeSymbol.Name));
                    }

                }
            }
        }


        // // Find our attributes

        // if (!namedTypeSymbol.GetAttributes().Where(x => x.AttributeClass?.ToDisplayString() == Strings.GenerateLogAttributeName).Any())
        // {
        //     return;
        // }

        // token.ThrowIfCancellationRequested();

        // var syntaxNodes = namedTypeSymbol.DeclaringSyntaxReferences.Select(x => x.GetSyntax());

        // foreach (var node in syntaxNodes)
        // {

        //     if (node is TypeDeclarationSyntax typeSyntax)
        //     {
        //         // Ensure type is partial.
        //         if (!typeSyntax.IsInPartialContext(out var nonPartialIdentifier))
        //         {
        //             context.ReportDiagnostic(
        //                 Diagnostic.Create(LoggerDiagnostics.GeneratedTypeNotPartial, typeSyntax.GetLocation(), namedTypeSymbol.Name)
        //             );
        //         }
        //     }

        // }

    }
}
