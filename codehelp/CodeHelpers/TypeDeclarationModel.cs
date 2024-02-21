using Microsoft.CodeAnalysis;

namespace WPILib.CodeHelpers;

[Flags]
public enum TypeModifiers
{
    None = 0,
    IsReadOnly = 1,
    IsRefLikeType = 2,
    IsRecord = 4,
}

public record TypeDeclarationModel(TypeKind Kind, TypeModifiers Modifiers, string TypeName, string? Namespace, TypeDeclarationModel? Parent)
{
    private string GetClassDeclaration(bool addUnsafe)
    {
        string readonlyString = (Modifiers & TypeModifiers.IsReadOnly) != 0 ? "readonly " : "";
        string refString = (Modifiers & TypeModifiers.IsRefLikeType) != 0 ? "ref " : "";
        string recordString = (Modifiers & TypeModifiers.IsRecord) != 0 ? "record " : "";
        string unsafeString = addUnsafe ? "unsafe " : "";
#pragma warning disable CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).
        string kindString = Kind switch
        {
            TypeKind.Class => "class",
            TypeKind.Struct => "struct",
            TypeKind.Interface => "interface",
        };
#pragma warning restore CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).

        return $"{unsafeString}{readonlyString}{refString}partial {recordString}{kindString}";
    }

    private int WriteClassDeclarationCount(IndentedStringBuilder builder, bool addUnsafe, string inheritanceToAdd) {
        // Write all the way up
        int indentCount = 0;
        if (Parent is not null) {
            indentCount = Parent.WriteClassDeclarationCount(builder, false, "");
        } else if (Namespace is not null) {
            indentCount += 1;
            builder.AppendFullLine($"namespace {Namespace}");
            builder.EnterManualScope();
        }
        builder.AppendFullLine($"{GetClassDeclaration(addUnsafe)} {TypeName}{inheritanceToAdd}");
        return indentCount;
    }

    public IndentedStringBuilder.IndentedScope WriteClassDeclaration(IndentedStringBuilder builder, bool addUnsafe, string inheritanceToAdd) {
        int count = WriteClassDeclarationCount(builder, addUnsafe, inheritanceToAdd);
        return builder.EnterScope(count);
    } 
}

public static class TypeDeclarationExtensions {
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