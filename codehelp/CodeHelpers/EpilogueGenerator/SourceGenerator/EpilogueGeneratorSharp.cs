using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace WPILib.CodeHelpers.EpilogueGenerator.SourceGenerator;

[Generator(LanguageNames.CSharp)]
public class EpilogueGeneratorSharp : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var attributedTypes = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                Strings.LoggedAttributeNameWithoutGlobal,
                predicate: static (s, _) => s is TypeDeclarationSyntax,
                transform: static (ctx, token) =>
                {
                    if (ctx.TargetSymbol is not INamedTypeSymbol classSymbol)
                    {
                        return null;
                    }
                    return classSymbol.GetLoggableType(token, null, null);
                })
            .Where(static m => m is not null);

        var customLoggers = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                Strings.CustomLoggerForAttributeNameWithoutGlobal,
                predicate: static (s, _) => s is TypeDeclarationSyntax,
                transform: static (ctx, token) =>
                {
                    if (ctx.TargetSymbol is not INamedTypeSymbol classSymbol)
                    {
                        return null;
                    }
                    return ctx.Attributes.GetCustomLoggerType(classSymbol, token);
                })
            .Where(static m => m is not null);

        var value = attributedTypes.Combine(customLoggers.Collect());

        context.RegisterSourceOutput(value,
            static (spc, source) => source.Left.ExecuteSourceGeneration(spc, source.Right, LanguageKind.CSharp));
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
