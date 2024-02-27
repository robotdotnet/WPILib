using Microsoft.CodeAnalysis;

namespace WPILib.CodeHelpers.LogGenerator;

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

    public static bool IsProtobufSerializable(this ITypeSymbol symbol)
    {
        return symbol is
        {
            Name: Strings.IProtobufSerializableString,
            ContainingNamespace:
            {
                Name: "Protobuf",
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

    public static bool IsLogAttributeClass(this ITypeSymbol symbol)
    {
        return symbol is
        {
            Name: Strings.LogAttributeTypeName,
            ContainingNamespace: { Name: Strings.LogNamespace, ContainingNamespace.IsGlobalNamespace: true }
        };
    }

    public static bool IsGenerateLogAttributeClass(this ITypeSymbol symbol)
    {
        return symbol is
        {
            Name: Strings.GenerateLogAttributeTypeName,
            ContainingNamespace: { Name: Strings.LogNamespace, ContainingNamespace.IsGlobalNamespace: true }
        };
    }

    public static bool IsLogInterfaceClass(this ITypeSymbol symbol)
    {
        return symbol is
        {
            Name: Strings.LoggedInterfaceTypeName,
            ContainingNamespace: { Name: Strings.LogNamespace, ContainingNamespace.IsGlobalNamespace: true }
        };
    }

    public static bool HasLogAttribute(this ISymbol symbol)
    {
        return symbol.GetAttributes()
                     .Where(x => x.AttributeClass?.IsLogAttributeClass() ?? false)
                     .Any();
    }

    public static bool HasGenerateLogAttribute(this ISymbol symbol)
    {
        return symbol.GetAttributes()
                     .Where(x => x.AttributeClass?.IsGenerateLogAttributeClass() ?? false)
                     .Any();
    }

    public static bool HasILoggedInterface(this ITypeSymbol symbol)
    {
        return symbol.GetAttributes()
                     .Where(x => x.AttributeClass?.IsLogInterfaceClass() ?? false)
                     .Any();
    }

    public static bool IsLoggerType(this ITypeSymbol symbol)
    {
        return symbol is
        {
            Name: Strings.LoggerTypeName,
            ContainingNamespace: { Name: Strings.LogNamespace, ContainingNamespace.IsGlobalNamespace: true }
        };
    }
}
