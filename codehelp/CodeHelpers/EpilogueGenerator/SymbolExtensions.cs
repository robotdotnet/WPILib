using Microsoft.CodeAnalysis;

namespace WPILib.CodeHelpers.EpilogueGenerator;

public static class SymbolExtensions
{
    public static bool IsReadOnlySpan(this ITypeSymbol symbol)
    {
        return symbol is
        {
            Name: "ReadOnlySpan",
            ContainingNamespace: { Name: "System", ContainingNamespace.IsGlobalNamespace: true }
        };
    }

    public static bool IsSpan(this ITypeSymbol symbol)
    {
        return symbol is
        {
            Name: "Span",
            ContainingNamespace: { Name: "System", ContainingNamespace.IsGlobalNamespace: true }
        };
    }

    public static bool IsReadOnlyMemory(this ITypeSymbol symbol)
    {
        return symbol is
        {
            Name: "ReadOnlyMemory",
            ContainingNamespace: { Name: "System", ContainingNamespace.IsGlobalNamespace: true }
        };
    }

    public static bool IsMemory(this ITypeSymbol symbol)
    {
        return symbol is
        {
            Name: "Memory",
            ContainingNamespace: { Name: "System", ContainingNamespace.IsGlobalNamespace: true }
        };
    }

    public static bool IsStructSerializable(this ITypeSymbol symbol)
    {
        return symbol is
        {
            Name: Strings.IStructSerializableName,
            ContainingNamespace:
            {
                Name: "Struct",
                ContainingNamespace:
                {
                    Name: "Serialization",
                    ContainingNamespace:
                    {
                        Name: "WPIUtil",
                        ContainingNamespace.IsGlobalNamespace: true
                    }
                }
            }
        };
    }

    public static bool IsLoggedAttributeClass(this ITypeSymbol symbol)
    {
        return symbol is
        {
            Name: Strings.LoggedAttributeTypeName,
            ContainingNamespace:
            {
                Name: Strings.LogInnerNamespace,
                ContainingNamespace:
                {
                    Name: Strings.LogOuterNamespace,
                    ContainingNamespace.IsGlobalNamespace: true
                }
            }
        };
    }

    public static bool IsNotLoggedAttributeClass(this ITypeSymbol symbol)
    {
        return symbol is
        {
            Name: Strings.NotLoggedAttributeTypeName,
            ContainingNamespace:
            {
                Name: Strings.LogInnerNamespace,
                ContainingNamespace:
                {
                    Name: Strings.LogOuterNamespace,
                    ContainingNamespace.IsGlobalNamespace: true
                }
            }
        };
    }

    public static bool IsCustomLoggerForAttributeClass(this ITypeSymbol symbol)
    {
        return symbol is
        {
            Name: Strings.CustomLoggerForAttributeName,
            ContainingNamespace:
            {
                Name: Strings.LogInnerNamespace,
                ContainingNamespace:
                {
                    Name: Strings.LogOuterNamespace,
                    ContainingNamespace.IsGlobalNamespace: true
                }
            }
        };
    }


    public static bool HasLoggedAttribute(this ISymbol symbol)
    {
        return symbol.GetAttributes()
                     .Where(x => x.AttributeClass?.IsLoggedAttributeClass() ?? false)
                     .Any();
    }

    public static bool HasNotLoggedAttribute(this ISymbol symbol)
    {
        return symbol.GetAttributes()
                     .Where(x => x.AttributeClass?.IsNotLoggedAttributeClass() ?? false)
                     .Any();
    }
}
