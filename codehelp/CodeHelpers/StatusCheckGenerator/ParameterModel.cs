using Microsoft.CodeAnalysis;

namespace WPILib.CodeHelpers.StatusCheckGenerator;

public record ParameterModel(string ParameterString, string Name, RefKind RefKind)
{
    public void WriteCallString(IndentedStringBuilder builder)
    {
        if (RefKind == RefKind.RefReadOnlyParameter)
        {
            builder.Append("in ");
        }
        else if (RefKind == RefKind.Ref)
        {
            builder.Append("ref ");
        }
        else if (RefKind == RefKind.Out)
        {
            builder.Append("out ");
        }
        builder.Append(Name);
    }
}

internal static class ParameterModelExtensions
{
    public static ParameterModel GetParameterModel(this IParameterSymbol symbol, ref bool needsUnsafe)
    {
        var fmt = new SymbolDisplayFormat(
            typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
            parameterOptions: SymbolDisplayParameterOptions.IncludeExtensionThis |
                              SymbolDisplayParameterOptions.IncludeModifiers |
                              SymbolDisplayParameterOptions.IncludeType |
                              SymbolDisplayParameterOptions.IncludeName |
                              SymbolDisplayParameterOptions.IncludeDefaultValue,
            genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
            miscellaneousOptions: SymbolDisplayMiscellaneousOptions.UseSpecialTypes,
            globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Included
        );

        if (symbol.Type.RequiresUnsafe())
        {
            needsUnsafe = true;
        }

        return new(symbol.ToDisplayString(fmt), symbol.Name, symbol.RefKind);
    }
}
