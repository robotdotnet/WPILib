using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace WPILib.CodeHelpers;

public static class SymbolExtensions
{
    public static bool RequiresUnsafe(this ITypeSymbol symbol)
    {
        return symbol.TypeKind == TypeKind.Pointer || symbol.TypeKind == TypeKind.FunctionPointer;
    }

    public static string GetFullyQualifiedTypeName(this ITypeSymbol symbol)
    {
        // TODO stop using fully qualified
        return symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
    }

    public static string? GetNamespace(this ITypeSymbol symbol)
    {
        // TODO Stop using ToDisplayString
        return symbol.ContainingNamespace is { IsGlobalNamespace: false } ns ? ns.ToDisplayString() : null;
    }

    public static string GetFullyQualifiedTypeNameWithoutGenerics(this ITypeSymbol symbol)
    {
        // TODO stop using fully qualified
        var fmt = new SymbolDisplayFormat(
            typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
            globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Included);
        return symbol.ToDisplayString(fmt);
    }
}
