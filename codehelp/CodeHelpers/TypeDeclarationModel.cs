using System.Collections.Immutable;
using System.Text;
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

public record TypeDeclarationModel(TypeKind Kind, TypeModifiers Modifiers, string TypeName, EquatableArray<TypeParameterModel> TypeParameters, string? Namespace, TypeDeclarationModel? Parent)
{
    public void WriteFileName(StringBuilder builder)
    {
        if (Parent is not null)
        {
            Parent.WriteFileName(builder);
        }
        else if (Namespace is not null)
        {
            builder.Append($"{Namespace}.");
        }
        builder.Append($"{TypeName}.");
    }

    private string GetClassDeclaration(bool addUnsafe)
    {
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

        return $"{unsafeString}partial {recordString}{kindString}";
    }

    private void GetGenericParameters(IndentedStringBuilder builder)
    {
        bool first = true;
        foreach (var typeParamter in TypeParameters.AsSpan())
        {
            if (first)
            {
                first = false;
                builder.Append("<");
            }
            else
            {
                builder.Append(", ");
            }
            typeParamter.WriteTypeParameter(builder);
        }
        if (!first)
        {
            builder.Append(">");
        }
    }

    private int WriteClassDeclarationCount(IndentedStringBuilder builder, bool addUnsafe, string inheritanceToAdd)
    {
        // Write all the way up
        int indentCount = 0;
        if (Parent is not null)
        {
            indentCount = Parent.WriteClassDeclarationCount(builder, false, "");
        }
        else if (Namespace is not null)
        {
            indentCount += 1;
            builder.AppendFullLine($"namespace {Namespace}");
            builder.EnterManualScope();
        }
        builder.StartLine();
        builder.Append($"{GetClassDeclaration(addUnsafe)} {TypeName}");
        GetGenericParameters(builder);
        builder.EndLine();
        return indentCount;
    }

    public IndentedStringBuilder.IndentedScope WriteClassDeclaration(IndentedStringBuilder builder, bool addUnsafe, string inheritanceToAdd)
    {
        int count = WriteClassDeclarationCount(builder, addUnsafe, inheritanceToAdd);
        return builder.EnterScope(count);
    }
}

public static class TypeDeclarationExtensions
{
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

        EquatableArray<TypeParameterModel> typeParameters;
        var symbolTypeParameters = symbol.TypeParameters;
        if (!symbolTypeParameters.IsEmpty)
        {
            var builder = ImmutableArray.CreateBuilder<TypeParameterModel>(symbolTypeParameters.Length);
            foreach (var typeParameter in symbolTypeParameters)
            {
                // No need for constraints on types
                builder.Add(typeParameter.GetTypeParameterModel(false));
            }
            typeParameters = builder.ToImmutable();
        }
        else
        {
            typeParameters = [];
        }

        string? nspace = null;

        TypeDeclarationModel? parent = null;
        if (symbol.ContainingType is not null)
        {
            parent = symbol.ContainingType.GetTypeDeclarationModel();
        }
        else
        {
            nspace = symbol.GetNamespace();
        }

        return new(symbol.TypeKind, modifiers, symbol.Name, typeParameters, nspace, parent);
    }
}
