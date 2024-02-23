using Microsoft.CodeAnalysis;

namespace WPILib.CodeHelpers.LogGenerator;

public static class SymbolExtensions
{
    public static bool IsReadOnlySpan(this ITypeSymbol symbol)
    {
        if (symbol.Name != "ReadOnlySpan")
        {
            return false;
        }
        return symbol.ContainingNamespace?.Name == "System";
    }

    public static bool IsSpan(this ITypeSymbol symbol)
    {
        if (symbol.Name != "Span")
        {
            return false;
        }
        return symbol.ContainingNamespace?.Name == "System";
    }

    public static bool IsReadOnlyMemory(this ITypeSymbol symbol)
    {
        if (symbol.Name != "ReadOnlyMemory")
        {
            return false;
        }
        return symbol.ContainingNamespace?.Name == "System";
    }

    public static bool IsMemory(this ITypeSymbol symbol)
    {
        if (symbol.Name != "Memory")
        {
            return false;
        }
        return symbol.ContainingNamespace?.Name == "System";
    }

    public static bool IsStructSerializable(this INamedTypeSymbol symbol)
    {
        return symbol.Name == Strings.IStructSerializableName;
    }

    public static bool IsProtobufSerializable(this INamedTypeSymbol symbol)
    {
        return symbol.Name == Strings.IProtobufSerializableString;
    }

    public static bool IsLogAttributeClass(this INamedTypeSymbol symbol)
    {
        if (symbol.Name != Strings.LogAttributeTypeName)
        {
            return false;
        }
        return symbol.ContainingNamespace?.Name == Strings.LogNamespace;
    }

    public static bool HasLogAttribute(this ISymbol symbol)
    {
        return symbol.GetAttributes().Where(x =>
        {
            var attributeClass = x.AttributeClass;
            if (attributeClass is null)
            {
                return false;
            }
            return attributeClass.IsLogAttributeClass();
        }).Any();
    }

    public static bool HasGenerateLogAttribute(this ISymbol symbol)
    {
        return symbol.GetAttributes().Where(x =>
        {
            var attributeClass = x.AttributeClass;
            if (attributeClass is null)
            {
                return false;
            }
            if (attributeClass.Name != Strings.GenerateLogAttributeTypeName)
            {
                return false;
            }
            return attributeClass.ContainingNamespace?.Name == Strings.LogNamespace;
        }).Any();
    }

    public static bool HasILoggedInterface(this ITypeSymbol symbol)
    {
        return symbol.AllInterfaces.Where(x =>
        {
            if (x.Name != Strings.LoggedInterfaceTypeName)
            {
                return false;
            }
            return x.ContainingNamespace?.Name == Strings.LogNamespace;
        }).Any();
    }

    public static bool IsLoggerType(this ITypeSymbol symbol)
    {
        if (symbol.Name != Strings.LoggerTypeName)
        {
            return false;
        }
        return symbol.ContainingNamespace?.Name == Strings.LogNamespace;
    }
}
