using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using WPILib.CodeHelpers.LogGenerator.Analyzer;

namespace WPILib.CodeHelpers.LogGenerator.CodeFixer;

[ExportCodeFixProvider(LanguageNames.CSharp)]
public class LogGeneratorFixer : CodeFixProvider
{
    private const string title = "Make constant";

    public override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create([
        LoggerDiagnostics.Ids.MissingGenerateLog,
    ]);

    public sealed override FixAllProvider GetFixAllProvider()
    {
        return WellKnownFixAllProviders.BatchFixer;
    }

    public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

        var diagnostic = context.Diagnostics.First();
        var diagnosticSpan = diagnostic.Location.SourceSpan;

        // Find the type declaration identified by the diagnostic.
        var declaration = root!.FindToken(diagnosticSpan.Start).Parent!.AncestorsAndSelf().OfType<TypeDeclarationSyntax>().First();

        // Register a code action that will invoke the fix.
        context.RegisterCodeFix(
            CodeAction.Create(
                title: title,
                createChangedDocument: c => AddGenerateLogAttribute(context.Document, declaration!, c),
                equivalenceKey: title),
            diagnostic);
    }

    private async Task<Document> AddGenerateLogAttribute(Document document, TypeDeclarationSyntax typeSyntax, CancellationToken cancellationToken)
    {
        var root = await document.GetSyntaxRootAsync(cancellationToken)!;
        var attributes = typeSyntax.AttributeLists.Add(
            SyntaxFactory.AttributeList(SyntaxFactory.SingletonSeparatedList(
                SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("GenerateLog"))
            )));

        return document.WithSyntaxRoot(
            root!.ReplaceNode(
                typeSyntax,
                typeSyntax.WithAttributeLists(attributes).NormalizeWhitespace()
            ));
    }
}
