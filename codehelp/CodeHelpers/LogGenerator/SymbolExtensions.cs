using Microsoft.CodeAnalysis;

namespace WPILib.CodeHelpers.LogGenerator;

public static class SymbolExtensions
{
    public static bool HasLogAttribute(this ISymbol symbol)
    {
        return symbol.GetAttributes().Where(x => x.AttributeClass?.ToDisplayString() == Strings.LogAttributeName).Any();
    }

    public static bool HasGenerateLogAttribute(this ISymbol symbol)
    {
        return symbol.GetAttributes().Where(x => x.AttributeClass?.ToDisplayString() == Strings.GenerateLogAttributeName).Any();
    }
}
