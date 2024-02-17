using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Stereologue.SourceGenerator;

[Generator]
public class LogGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var attributedTypes = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                "Stereologue.GenerateLogAttribute",
                predicate: static (s, _) => s is TypeDeclarationSyntax,
                transform: static (ctx, token) => ctx.GetLoggableType(token))
            .Where(static m => m is not null);

        context.RegisterSourceOutput(attributedTypes,
            static (spc, source) => source.ExecuteSourceGeneration(spc));
    }
}
