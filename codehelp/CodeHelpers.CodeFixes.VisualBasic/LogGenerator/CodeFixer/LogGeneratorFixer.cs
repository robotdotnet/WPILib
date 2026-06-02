using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using WPILib.CodeHelpers.Core.LogGenerator.Analyzer;

namespace WPILib.CodeHelpers.CodeFixes.VisualBasic.LogGenerator.CodeFixer;

[ExportCodeFixProvider(LanguageNames.VisualBasic)]
public class LogGeneratorFixer : CodeFixProvider
{
    private const string Title = "Add <GenerateLog>";

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

        var declaration = root!.FindToken(diagnosticSpan.Start).Parent!.AncestorsAndSelf().OfType<TypeStatementSyntax>().First();

        context.RegisterCodeFix(
            CodeAction.Create(
                title: Title,
                createChangedDocument: c => AddGenerateLogAttribute(context.Document, declaration, c),
                equivalenceKey: Title),
            diagnostic);
    }

    private async Task<Document> AddGenerateLogAttribute(Document document, TypeStatementSyntax typeSyntax, CancellationToken cancellationToken)
    {
        var root = await document.GetSyntaxRootAsync(cancellationToken);
        SyntaxToken firstToken = typeSyntax.GetFirstToken();
        SyntaxTriviaList leadingTrivia = firstToken.LeadingTrivia;
        TypeStatementSyntax trimmedLocal = typeSyntax.ReplaceToken(
            firstToken, firstToken.WithLeadingTrivia(SyntaxTriviaList.Empty));

        var attributes = typeSyntax.AttributeLists.Add(
            SyntaxFactory.AttributeList(SyntaxFactory.SingletonSeparatedList(
                SyntaxFactory.Attribute(SyntaxFactory.ParseName("GenerateLog"))
            )).WithLeadingTrivia(leadingTrivia));

        var newLocal = trimmedLocal.WithAttributeLists(attributes);
        var formattedLocal = newLocal.WithAdditionalAnnotations(Formatter.Annotation);

        var newRoot = root!.ReplaceNode(
            typeSyntax,
            formattedLocal
        );

        return document.WithSyntaxRoot(newRoot);
    }
}
