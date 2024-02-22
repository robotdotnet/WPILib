using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace WPILib.CodeHelpers;

public static class SymbolExtensions
{
    public static bool RequiresUnsafe(this ITypeSymbol symbol)
    {
        return symbol.TypeKind == TypeKind.Pointer || symbol.TypeKind == TypeKind.FunctionPointer;
    }

    public static string GetFullTypeName(this ITypeSymbol symbol)
    {
        // TODO stop using fully qualified
        return symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
    }

    public static string? GetNamespace(this ITypeSymbol symbol)
    {
        // TODO Stop using ToDisplayString
        return symbol.ContainingNamespace is { IsGlobalNamespace: false } ns ? ns.ToDisplayString() : null;
    }
}
