using Microsoft.CodeAnalysis;

namespace WPILib.CodeHelpers;

// Global namespace is if name is null, next up is global if parent is null
public record NamespaceModel(string? Name, NamespaceModel? Parent)
{
    private void WriteNamespaceDeclarationInternal(IndentedStringBuilder builder, bool isLeaf)
    {
        if (Name is null)
        {
            // This is the global namespace
            return;
        }
        // Parent is only null if name is null
        Parent!.WriteNamespaceDeclarationInternal(builder, false);
        builder.Append($"{Name}{(isLeaf ? "" : ".")}");
    }

    public int WriteNamespaceDeclaration(IndentedStringBuilder builder)
    {
        if (Name is null)
        {
            // Global namespace, no scope
            return 0;
        }
        builder.StartLine();
        if (builder.Language == LanguageKind.CSharp)
        {
            builder.Append("namespace ");
        }
        else if (builder.Language == LanguageKind.VisualBasic)
        {
            builder.Append("Namespace Global.");
        }
        WriteNamespaceDeclarationInternal(builder, true);
        builder.EndLine();
        builder.EnterScope(ScopeType.Namespace);
        return 1;
    }

    public void WriteFileName(IndentedStringBuilder builder)
    {
        if (Name is null)
        {
            return;
        }
        // Parent is only null if name is null
        Parent!.WriteFileName(builder);
        builder.Append($"{Name}.");
    }
}

public static class NamespaceModelExtensions
{
    public static NamespaceModel GetNamespaceModel(this INamespaceSymbol symbol)
    {
        if (symbol.IsGlobalNamespace)
        {
            return new(null, null);
        }

        NamespaceModel parent = symbol.ContainingNamespace.GetNamespaceModel();
        return new(symbol.Name, parent);
    }
}
