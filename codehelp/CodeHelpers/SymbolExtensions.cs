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

        return new(symbol.TypeKind, modifiers);
    }
}
