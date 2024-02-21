using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace WPILib.CodeHelpers.StatusCheckGenerator.SourceGenerator;

[Generator]
public class StatusCheckGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var attributedTypes = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                Strings.StatusCheckAttribute,
                predicate: static (s, _) => s is MethodDeclarationSyntax,
                transform: static (ctx, token) =>
                {
                    if (ctx.TargetSymbol is not IMethodSymbol classSymbol)
                    {
                        return null;
                    }
                    return classSymbol.GetMethodModel();
                })
            .Where(static m => m is not null);

        var combined = attributedTypes.Collect();

        context.RegisterSourceOutput(combined,
            static (spc, source) => Execute(source, spc));
    }

    private static void Execute(ImmutableArray<MethodModel?> model, SourceProductionContext context)
    {
        IndentedStringBuilder builder = new IndentedStringBuilder();
        foreach (var method in model)
        {
            method?.WriteMethod(builder);
        }

        context.AddSource("StatusChecks.g.cs", SourceText.From(builder.ToString(), Encoding.UTF8));
    }
}
