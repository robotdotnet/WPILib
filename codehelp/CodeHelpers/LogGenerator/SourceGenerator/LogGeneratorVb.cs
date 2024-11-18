using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace WPILib.CodeHelpers.LogGenerator.SourceGenerator;

[Generator(LanguageNames.VisualBasic)]
public class LogGeneratorVb : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var attributedTypes = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                Strings.GenerateLogAttributeNameWithoutGlobal,
                predicate: static (s, _) => s is TypeStatementSyntax,
                transform: static (ctx, token) =>
                {
                    if (ctx.TargetSymbol is not INamedTypeSymbol classSymbol)
                    {
                        return null;
                    }

                    return classSymbol.GetLoggableType(token, null, null);
                })
            .Where(static m => m is not null);

        context.RegisterSourceOutput(attributedTypes,
            static (spc, source) => source.ExecuteSourceGeneration(spc, LanguageKind.VisualBasic));
    }
}

// Notes on what analyzer needs to block
// * Type marked [GenerateLog] is not partial
// * Type marked [GenerateLog] is interface (Might be fixed)
// * Array Like types of Nullable<T> for [Log] marked members
// * Pointer types for [Log] marked members
// * Only Allow Span, ROS and [] for array types
// * If array, only allow Long for integers
// * Types must be either primitives, ILogged, Array of ILogged, IStructSerializable, Array of IStructSerializable, or IProtobufSerializable

// What analyzer needs to warn
// * Class contains [Log] annotations, but is not marked [GenerateLog]
