using Microsoft.CodeAnalysis;

namespace WPILib.CodeHelpers;

public record TypeModel(string FullyQualifiedName);

public static class TypeModelExtensions
{
    public static TypeModel GetTypeModel(ITypeSymbol type)
    {
        // TODO stop using FQN
        return new TypeModel(type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));
    }
}
