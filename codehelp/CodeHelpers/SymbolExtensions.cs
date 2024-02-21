using Microsoft.CodeAnalysis;

namespace WPILib.CodeHelpers;

public static class SymbolExtensions
{
    public static bool RequiresUnsafe(this ITypeSymbol symbol)
    {
        return symbol.TypeKind == TypeKind.Pointer || symbol.TypeKind == TypeKind.FunctionPointer;
    }

    public static TypeDeclarationModel GetTypeDeclarationModel(this INamedTypeSymbol symbol)
    {
        TypeModifiers modifiers = TypeModifiers.None;
        if (symbol.IsReadOnly)
        {
            modifiers |= TypeModifiers.IsReadOnly;
        }
        if (symbol.IsRefLikeType)
        {
            modifiers |= TypeModifiers.IsRefLikeType;
        }
        if (symbol.IsRecord)
        {
            modifiers |= TypeModifiers.IsRecord;
        }

        var className = symbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
        string? nspace = null;

        TypeDeclarationModel? parent = null;
        if (symbol.ContainingType is not null) {
            parent = symbol.ContainingType.GetTypeDeclarationModel();
        } else {
            nspace = symbol.ContainingNamespace is { IsGlobalNamespace: false } ns ? ns.ToDisplayString() : null;
        }

        return new(symbol.TypeKind, modifiers, className, nspace, parent);
    }
}
