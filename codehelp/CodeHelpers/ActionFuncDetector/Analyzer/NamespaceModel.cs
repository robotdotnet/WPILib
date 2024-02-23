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

    public IndentedStringBuilder.IndentedScope WriteNamespaceDeclaration(IndentedStringBuilder builder)
    {
        var scope = builder.EnterEmptyScope();
        if (Name is null)
        {
            // Global namespace, no scope
            return scope;
        }
        builder.StartLine();
        builder.Append("namespace ");
        WriteNamespaceDeclarationInternal(builder, true);
        builder.EndLine();
        scope.AddLineToScope();
        return scope;
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
