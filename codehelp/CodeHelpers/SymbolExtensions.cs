using Microsoft.CodeAnalysis;

namespace WPILib.CodeHelpers;

public static class SymbolExtensions
{
    public static bool RequiresUnsafe(this ITypeSymbol symbol)
    {
        return symbol.TypeKind == TypeKind.Pointer || symbol.TypeKind == TypeKind.FunctionPointer;
    }

    public static string? GetNamespace(this ITypeSymbol symbol)
    {
        // TODO Stop using ToDisplayString
        return symbol.ContainingNamespace is { IsGlobalNamespace: false } ns ? ns.ToDisplayString() : null;
    }
}
