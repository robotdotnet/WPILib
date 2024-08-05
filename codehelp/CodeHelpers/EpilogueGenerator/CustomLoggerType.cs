using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace WPILib.CodeHelpers.EpilogueGenerator;

public record CustomLoggerType(TypeDeclarationModel TypeDeclarations, EquatableArray<TypeDeclarationModel> SupportedTypes);

internal static class CustomLoggerTypeExtensions
{
    public static CustomLoggerType GetCustomLoggerType(this ImmutableArray<AttributeData> attributes, INamedTypeSymbol symbol, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        var loggableTypes = ImmutableArray.CreateBuilder<TypeDeclarationModel>(1);

        foreach (var attribute in attributes)
        {
            token.ThrowIfCancellationRequested();
            foreach (var named in attribute.NamedArguments)
            {
                token.ThrowIfCancellationRequested();
                if (named.Key == "Types")
                {
                    if (!named.Value.IsNull && named.Value.Kind is TypedConstantKind.Array)
                    {
                        foreach (var value in named.Value.Values)
                        {
                            token.ThrowIfCancellationRequested();
                            if (!value.IsNull && value.Kind is TypedConstantKind.Type && value.Value is INamedTypeSymbol typeFor)
                            {
                                loggableTypes.Add(typeFor.GetTypeDeclarationModel());
                            }
                        }
                    }
                }
            }
        }

        token.ThrowIfCancellationRequested();

        return new(symbol.GetTypeDeclarationModel(), loggableTypes.ToImmutable());
    }
}
